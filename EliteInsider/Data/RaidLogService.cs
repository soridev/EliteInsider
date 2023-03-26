using EliteInsider.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using EliteInsider.Models.Converters;

namespace EliteInsider.Data
{
    public class RaidLogService : IRaidLogService
    {
        private readonly ApplicationDbContext _context;

        public RaidLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RaidKillTime>> GetRaidKillTimesAsync()
        {
            try
            {
                var rkt = await _context.RaidKillTimes.ToListAsync();
                return rkt.OrderByDescending(x => x.StartTime).ToList();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return new List<RaidKillTime>();
            }
        }

        public async Task<RaidKillTime?> GetRaidKillTimeAsync(string logId)
        {
            try
            {
                return await _context.RaidKillTimes.FindAsync(logId);
            }
            catch (Exception ex) {
                Debug.Write(ex.Message);
                return null;
            }
        }

        public async Task<List<GuildWeek>> GetGuildClearWeeksAsync(string guildName)
        {
            try
            {
                var groupedResult = await _context.GuildLogs.Where(r => r.GuildName == guildName)
                    .GroupBy(r => new { r.GuildName, r.YearWeek }).ToListAsync();

                groupedResult = groupedResult.OrderByDescending(r => r.Key.YearWeek).ToList();

                List<GuildWeek> gw = new List<GuildWeek>();
                foreach(var e in groupedResult)
                {
                    gw.Add(new GuildWeek()
                    {
                        GuildName = e.Key.GuildName,
                        YearWeek = e.Key.YearWeek
                    });
                }

                return gw;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw ex;
            }
        }

        public async Task<List<GuildKillTime>> GetFullclearDataAsync(GuildWeek guildweek)
        {
            try
            {
                List<GuildKillTime> guildKillTimes = new List<GuildKillTime>();

                var gkt = await (from gl in _context.GuildLogs 
                            join rkt in _context.RaidKillTimes
                            on gl.LogId equals rkt.LogId where gl.YearWeek == guildweek.YearWeek
                            join re in _context.RaidEncounters
                            on rkt.EncounterName.Replace(" CM", "") equals re.EncounterName
                            select new
                            {
                                EncounterName = rkt.EncounterName,
                                StartTime = rkt.StartTime,
                                EndTime = rkt.EndTime,
                                KillDurationSeconds = rkt.KillDurationSeconds,
                                Success = rkt.Success,
                                CM = rkt.CM,
                                RaidWing = re.RaidWing,
                                BossPosition = re.BossPosition,
                                WingName = re.WingName,
                                LinkToUpload = rkt.LinkToUpload
                            }).ToListAsync();

                foreach(var e in  gkt)
                {
                    guildKillTimes.Add(new GuildKillTime
                    {
                        EncounterName = e.EncounterName,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        KillDurationSeconds = e.KillDurationSeconds,
                        Success = e.Success,
                        CM = e.CM,
                        RaidWing = e.RaidWing,
                        BossPosition = e.BossPosition,
                        WingName = e.WingName,
                        LinkToUpload = e.LinkToUpload
                    });
                }

                return guildKillTimes.OrderBy(r => r.StartTime).ToList();
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
                throw ex;
            }
        }

        public async Task<List<PersonalBest>> GetPersonalBestsAsync()
        {
            try
            {
                var query = await (from rkt in _context.RaidKillTimes
                                   join gl in _context.GuildLogs
                                   on rkt.LogId equals gl.LogId
                                   join re in _context.RaidEncounters
                                   on rkt.EncounterName.Replace(" CM", "") equals re.EncounterName
                                   where rkt.Success
                                   select new PersonalBest
                                   {
                                       EncounterName = rkt.EncounterName,
                                       Duration = TimeSpan.FromSeconds(rkt.KillDurationSeconds),
                                       LinkToUpload = rkt.LinkToUpload,
                                       RaidWing = re.RaidWing,
                                       BossPosition = re.BossPosition
                                   }).ToListAsync();

                var groupedResult = query.GroupBy(bp => bp.EncounterName).Select(g => new
                {
                    g.Key,
                    MinDuration = g.Min(bp => bp.Duration)
                });

                var finalResult = (from rkt in query
                                   join gr in groupedResult
                                   on
                                   new { EncounterName = rkt.EncounterName, Duration = rkt.Duration } equals
                                   new { EncounterName = gr.Key, Duration = gr.MinDuration}
                                   select new PersonalBest
                                   {
                                       EncounterName = rkt.EncounterName,
                                       Duration = rkt.Duration,
                                       LinkToUpload= rkt.LinkToUpload,
                                       RaidWing = rkt.RaidWing,
                                       BossPosition = rkt.BossPosition,
                                       BossImage = $"images/boss-icons/{rkt.EncounterName.Replace(" CM", "").Replace(" ", "-").ToLower()}.png"
                                   }).ToList();

                finalResult = finalResult.GroupBy(x => x.EncounterName).Select(y => y.First()).ToList();
                return finalResult.OrderBy(x => x.RaidWing).ThenBy(n => n.BossPosition).ToList();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw ex;
            }
        }
    }
}
