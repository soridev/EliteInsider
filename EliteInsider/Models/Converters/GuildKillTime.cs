namespace EliteInsider.Models.Converters
{
    public class GuildKillTime
    {
        public string EncounterName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double KillDurationSeconds { get; set; }
        public bool Success { get; set; }
        public bool CM { get; set; }
        public string LinkToUpload { get; set; }
    }
}
