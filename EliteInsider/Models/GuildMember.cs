using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteInsider.Models
{
    [Table("guild_members", Schema = "public")]
    [PrimaryKey(nameof(GuildId), nameof(AccountName))]
    public class GuildMember
    {
        [Column("guild_id")]
        public string GuildId { get; set; }
        
        [Column("account_name")]
        public string AccountName { get; set; }
        
        [Column("is_admin")]
        public bool IsAdmin { get; set; }
    }
}
