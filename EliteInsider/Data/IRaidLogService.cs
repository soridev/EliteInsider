using EliteInsider.Models;

namespace EliteInsider.Data
{
    public interface IRaidLogService
    {
        Task<List<RaidKillTime>> GetRaidKillTimesAsync();
        Task<RaidKillTime?> GetRaidKillTimeAsync(string logId);
    }
}
