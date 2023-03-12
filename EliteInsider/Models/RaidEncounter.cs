using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EliteInsider.Models
{
    [Table("raid_encounters", Schema = "public")]
    public class RaidEncounter
    {
        [Column("encounter_name")]
        [Key]
        public string EncounterName { get; set; }
        [Column("arc_folder_name")]
        public string ArcFolderName { get; set; }
        [Column("has_cm")]
        public bool HasCM { get; set; }
        [Column("raid_wing")]
        public int RaidWing { get; set; }
        [Column("boss_position")]
        public int BossPosition { get; set; }
        [Column("relevant_boss")]
        public bool RelevantBoss { get; set; }
        [Column("wing_name")]
        public string WingName { get; set; }
    }
}
