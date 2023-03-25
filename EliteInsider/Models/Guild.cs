using System.ComponentModel.DataAnnotations.Schema;

namespace EliteInsider.Models
{
    [Table("guilds", Schema = "public")]
    public class Guild
    {
        
        [Column("guild_id")]
        public int GuildId { get; set; }

        [Column("guild_name")]
        public string GuildName { get; set;}
    }
}
