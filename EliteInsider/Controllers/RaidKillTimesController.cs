using EliteInsider.Data;
using Microsoft.AspNetCore.Mvc;

namespace EliteInsider.Controllers
{
    [ApiController]
    [Route("elite-api/v1/[controller]")]
    public class RaidKillTimesController : ControllerBase
    {
        private readonly IRaidLogService _logService;

        public RaidKillTimesController(IRaidLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRaidKillTimes()
        {
            var raidKillTimes = await _logService.GetRaidKillTimesAsync();
            raidKillTimes.OrderByDescending(r => r.StartTime);

            if (raidKillTimes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Raid Kill Times in the database.");
            }

            return StatusCode(StatusCodes.Status200OK, raidKillTimes);
        }
    }
}
