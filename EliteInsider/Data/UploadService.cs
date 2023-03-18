using EliteInsider.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using MudBlazor;
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EliteInsider.Data
{
    public class UploadService : IUploadService
    {
        private readonly string _dpsReportBaseUrl = "https://dps.report";
        private readonly ApplicationDbContext _context;

        public UploadService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task RegisterNewFile(string fileName)
        {
            DpsReportUploadResponse? uploadResponse = await UploadToDpsReportAsync(fileName);

            if (uploadResponse != null)
            {
                DpsReportJsonData? jsonData = await DownloadDpsReportJsonDataAsync(uploadResponse.id);

                if (jsonData != null)
                {
                    await RegisterLogIntoDatabase(jsonData, uploadResponse.permalink);
                }
            }
        }

        public async Task<DpsReportUploadResponse?> UploadToDpsReportAsync(string fileName)
        {
            try
            {
                // check if file exists.
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException($"Unable to locate the given file with name: {fileName}");
                }

                // prepare request body
                using var form = new MultipartFormDataContent();
                using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(fileName));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                form.Add(fileContent, "file", fileName);

                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_dpsReportBaseUrl)
                };

                var response = await httpClient.PostAsync($"/uploadContent?json=1", form);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The upload request returned an error code: {response.StatusCode}");
                }

                DpsReportUploadResponse? uploadResponse = JsonSerializer.Deserialize<DpsReportUploadResponse>(response.Content.ReadAsStream());
                return uploadResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DpsReportJsonData?> DownloadDpsReportJsonDataAsync(string logid)
        {
            try
            {
                var httpClient = new HttpClient()
                {
                    BaseAddress= new Uri(_dpsReportBaseUrl)
                };

                var response = await httpClient.GetAsync($"/getJson?id={logid}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The getJson request returned an error code: {response.StatusCode}");
                }

                // load .net object from json response body. Can be up to 5MB big so might be slow.
                DpsReportJsonData? reportData = JsonSerializer.Deserialize<DpsReportJsonData>(response.Content.ReadAsStream());
                
                return reportData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RegisterLogIntoDatabase(DpsReportJsonData jsonData, string linkToUpload)
        {
            try
            {
                // convert data that is not in .NET datatypes
                using var sha1 = SHA1.Create();
                string logid = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(jsonData.fightName + jsonData.timeStart)));
                DateTime startTime = DateTime.Parse(jsonData.timeStart).ToUniversalTime();
                DateTime endTime = DateTime.Parse(jsonData.timeEnd).ToUniversalTime();
                string[] durationParts = jsonData.duration.Replace("m", "").Replace("s", "").Split(' ');
                int durationSeconds = 0;

                // convert duration string to seconds, if only miliseconds we count as 1 second.
                if(durationParts.Length == 3) {
                    durationSeconds = (Convert.ToInt32(durationParts[0]) * 60) + Convert.ToInt32(durationParts[1]);
                }
                else if (durationParts.Length == 2)
                {
                    durationSeconds = Convert.ToInt32(durationParts[1]);
                }
                else
                {
                    durationSeconds = 1;
                }

                // create database objects.
                RaidKillTime rkt = new RaidKillTime();
                rkt.LogId = logid;
                rkt.EncounterName = jsonData.fightName;
                rkt.QualifyingDate = DateOnly.FromDateTime(startTime);
                rkt.StartTime = startTime;
                rkt.EndTime = endTime;
                rkt.KillDurationSeconds = durationSeconds;
                rkt.Success = jsonData.success;
                rkt.CM = jsonData.isCM;
                rkt.LinkToUpload = linkToUpload;

                await _context.AddAsync(rkt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
