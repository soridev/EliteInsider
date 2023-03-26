namespace EliteInsider.Models.Converters
{
    public class PersonalBest
    {
        public string EncounterName { get; set; }
        public TimeSpan Duration { get; set; }
        public string LinkToUpload { get; set; }
        public int RaidWing { get; set; }
        public int BossPosition { get; set; }
        public string BossImage { get; set; } = string.Empty;

        public string GetDurationToString()
        {
            return $"{Duration.Minutes}m {Duration.Seconds}s {Duration.Milliseconds}ms";
        }
    }
}
