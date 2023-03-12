using EliteInsider.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
                return await _context.RaidKillTimes.ToListAsync();
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
    }
}
