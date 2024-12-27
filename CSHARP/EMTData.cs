using OfficeOpenXml;  // Potrebno je dodati EPPlus paket

namespace EMT
{
    public class EMTData
    {
        public List<ColumnInfo> Kolone { get; set; } = new List<ColumnInfo>();  // Lista svih kolona u Excel fajlu
    }
    public class ColumnInfo
    {
        public int ID { get; set; }  // ID kolone
        public string Naziv { get; set; } = string.Empty;  // Naziv kolone
        public List<string> Redovi { get; set; } = new List<string>();  // Lista redova ove kolone
    }

}

