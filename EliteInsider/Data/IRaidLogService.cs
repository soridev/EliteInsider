using EliteInsider.Models;
using EliteInsider.Models.Converters;

namespace EliteInsider.Data
{
    public interface IRaidLogService
    {
        Task<List<RaidKillTime>> GetRaidKillTimesAsync();
        Task<RaidKillTime?> GetRaidKillTimeAsync(string logId);
        Task<List<GuildWeek>> GetGuildClearWeeksAsync(string guildName);
        Task<List<GuildKillTime>> GetFullclearDataAsync(GuildWeek guildweek);
    }
}
