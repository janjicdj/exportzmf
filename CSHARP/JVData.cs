namespace JV
{
    public class JVData
    {
        public List<string> WKN { get; set; } = new List<string>();
        public List<string> ISIN { get; set; } = new List<string>();
        public List<string> DatumVon { get; set; } = new List<string>();
        public List<string> DatumBis { get; set; } = new List<string>();
        public List<string> KorrekturKZ { get; set; } = new List<string>();
        public List<string> TransaktionskostenBetrag { get; set; } = new List<string>();
        public List<string> TransaktionskostenKennzeichen { get; set; } = new List<string>();
        public List<string> LaufendeKostenBetrag { get; set; } = new List<string>();
        public List<string> LaufendeKostenKennzeichen { get; set; } = new List<string>();
        public List<string> AnlassbezogeneKostenBetrag { get; set; } = new List<string>();
        public List<string> AnlassbezogeneKostenKennzeichen { get; set; } = new List<string>();
    }

}
