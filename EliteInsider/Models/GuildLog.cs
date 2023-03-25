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
}
