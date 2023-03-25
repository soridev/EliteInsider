namespace EliteInsider.Models.Converters
{
    public class GuildWeek
    {
        public string GuildName { get; set; }
        public int YearWeek { get; set; }

        public override string ToString()
        {
            return $"[{this.GuildName}] {this.YearWeek.ToString().Substring(0, 4)} W{this.YearWeek.ToString().Substring(4, 2)}";
        }
    }
}
