using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteInsider.Models
{
    [Table("guild_logs", Schema = "public")]
    public class GuildLog
    {
        [Key]
        [Column("log_id")]
        public string LogId { get; set; }

        [Column("guild_name")]
        public string GuildName { get; set; }

        [Column("qualifying_date")]
        public DateOnly QualifyingDate { get; set; }

        [Column("yearweek")]
        public int YearWeek { get; set; }
    }

    public class GuildWeek
    {
        public string GuildName { get; set; }
        public int YearWeek { get; set; }

        public override string ToString()
        {
            return $"[{this.GuildName}] {this.YearWeek.ToString().Substring(0,4)} W{this.YearWeek.ToString().Substring(4,2)}";
        }
    }
}
