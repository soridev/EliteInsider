using EliteInsider.Models;

namespace EliteInsider.Data
{
    public interface IUploadService
    {
        Task RegisterNewFile(string fileName);
        Task<DpsReportUploadResponse?> UploadToDpsReportAsync(string fileName);
        Task<DpsReportJsonData?> DownloadDpsReportJsonDataAsync(string logid);
        Task RegisterLogIntoDatabase(DpsReportJsonData jsonData, string linkToUpload);
    }
}
