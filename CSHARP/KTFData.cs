namespace KTF
{
    public class KTFData
{
    public List<string> Status { get; set; } = new List<string>();
    public List<string> WKN { get; set; } = new List<string>();
    public List<string> ISIN { get; set; } = new List<string>();
    public List<string> Betrag_Lauf_Fondskosten { get; set; } = new List<string>();
    public List<string> Datum_Lauf_Fondskosten { get; set; } = new List<string>();
    public List<string> Nullmeldung_Lauf_Fondskosten { get; set; } = new List<string>();
    public List<string> Betrag_Transaktionskosten { get; set; } = new List<string>();
    public List<string> Datum_Transaktionskosten { get; set; } = new List<string>();
    public List<string> Nullmeldung_Transaktionskosten { get; set; } = new List<string>();
    public List<string> Betrag_Anlassbez_Kosten { get; set; } = new List<string>();
    public List<string> Datum_Anlassbez_Kosten { get; set; } = new List<string>();
    public List<string> Nullmeldung_Anlassbez_Kosten { get; set; } = new List<string>();
    public List<string> Betrag_Tat_Ruecknahmekosten { get; set; } = new List<string>();
    public List<string> Datum_Tat_Ruecknahmekosten { get; set; } = new List<string>();
    public List<string> Nullmeldung_Tat_Ruecknahmekosten { get; set; } = new List<string>();
    public List<string> Swing_Pricing { get; set; } = new List<string>();
}
}