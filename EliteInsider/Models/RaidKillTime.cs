using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteInsider.Models
{
    [Table("raid_kill_times", Schema = "public")]
    public class RaidKillTime
    {
        [Column("log_id")]
        [Key]
        public string LogId { get; set; }
        [Column("encounter_name")]
        public string EncounterName { get; set; }
        [Column("qualifying_date")]
        public DateOnly QualifyingDate { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        [Column("kill_duration_seconds")]
        public double KillDurationSeconds { get; set; }
        [Column("success")]
        public bool Success { get; set; }
        [Column("cm")]
        public bool CM { get; set; }
        [Column("input_file")]
        public string InputFile { get; set; }
        [Column("link_to_upload")]
        public string? LinkToUpload { get; set; }
        [Column("uploaded_by")]
        public string UploadedBy { get; set; }
    }
}
