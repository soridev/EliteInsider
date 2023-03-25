namespace EliteInsider.Models
{
    public class DpsReportJsonData
    {
        public string fightName { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public string duration { get; set; }
        public long durationMS { get; set; }
        public bool success { get; set; }
        public bool isCM { get; set; }

        public List<DpsReportPlayer> players { get; set; } = new List<DpsReportPlayer>();   
        public List<DpsReportMechanic> mechanics { get; set; } = new List<DpsReportMechanic>() { };
    }

    public class DpsReportPlayer
    {
        public string? account { get; set; }
        public string? name { get; set; }
        public string? profession { get; set; }
        public List<List<DpsReportDpsTarget>> dpsTargets { get; set; } = new List<List<DpsReportDpsTarget>>();
    }

    public class DpsReportDpsTarget
    {
        public long dps { get; set; }
        public long damage { get; set; }
        public long condiDps { get; set; }
        public long condiDamage { get; set; }
        public long powerDps { get; set; }
        public long powerDamage { get; set; }
        public double breakbarDamage { get; set; }
    }

    public class DpsReportMechanic
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public List<DpsReportMechanicsData> mechanicsData { get; set; } = new List<DpsReportMechanicsData>();
    }

    public class DpsReportMechanicsData
    {
        public long time { get; set; }
        public string? actor { get; set; }
    }
}
