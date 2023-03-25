using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteInsider.Models
{
    [Table("player_info", Schema = "public")]
    public class PlayerInfo
    {
        [Column("id")]
        [Key]
        public long id { get; set; }

        [Column("log_id")]
        public string LogId { get; set; }

        [Column("account_name")]
        public string AccountName { get; set; }

        [Column("character_name")]
        public string CharacterName { get; set; }

        [Column("profession")]
        public string Profession { get; set; }

        [Column("target_dps")]
        public long TargetDps { get; set; }

        [Column("total_cc")]
        public double TotalCC { get; set; }

        [Column("downstates")]
        public int Downstates { get; set; }

        [Column("died")]
        public bool Died { get; set; }
    }
}
