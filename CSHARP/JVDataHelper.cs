using System.Globalization;
using EMT;

namespace JV
{
    public static class JVDataHelper
    {

        public static JVData EMTtoJV(EMTData amtData, EMTData emtData)
        {
            JVData jvData = new JVData();
            (jvData.WKN,
            jvData.ISIN,
            jvData.DatumVon,
            jvData.DatumBis,
            jvData.TransaktionskostenBetrag,
            jvData.TransaktionskostenKennzeichen,
            jvData.LaufendeKostenBetrag,
            jvData.LaufendeKostenKennzeichen,
            jvData.AnlassbezogeneKostenBetrag,
            jvData.AnlassbezogeneKostenKennzeichen) = Write(amtData, emtData);
            //jvData.ISIN = WriteISIN(amtData);
            return jvData;
        }

        public static (List<string>, List<string>, List<string>, List<string>, List<string>, List<string>, List<string>, List<string>, List<string>, List<string>)
        Write(EMTData amtData, EMTData emtData)
        {
            var amtRowNum = amtData.Kolone[1].Redovi.Count;
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> wkn = new List<string>();
            List<string> isin = new List<string>();
            List<string> datumVon = new List<string>();
            List<string> datumBis = new List<string>();
            List<string> transaktionskostenBetrag = new List<string>();
            List<string> transaktionskostenKennzeichen = new List<string>();
            List<string> laufendeKostenBetrag = new List<string>();
            List<string> laufendeKostenKennzeichen = new List<string>();
            List<string> anlassbezogeneKostenBetrag = new List<string>();
            List<string> anlassbezogeneKostenKennzeichen = new List<string>();
            for (int i = 6; i < amtRowNum; i++)
            {
                string wknValue = "";
                string isinValue = "";
                foreach (EMT.ColumnInfo col in amtData.Kolone)
                {
                    if (col.Naziv == "OFST020015")
                    {
                        wknValue = col.Redovi[i];
                    }
                    if (col.Naziv == "OFST020000")
                    {
                        isinValue = col.Redovi[i];
                    }
                }
                wkn.Add(wknValue);
                isin.Add(isinValue);
            }
            for (int i = 0; i < emtRowNum; i++)
            {
                string datumVonValue = "";
                string datumBisValue = "";
                string transaktionskostenBetragValue = "";
                string transaktionskostenKennzeichenValue = "";
                string laufendeKostenBetragValue = "";
                string laufendeKostenKennzeichenValue = "";
                string anlassbezogeneKostenBetragValue = "";
                string anlassbezogeneKostenKennzeichenValue = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "08090")
                        datumVonValue = DateTime.ParseExact(col.Redovi[i].TrimEnd('.'), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    if (col.Naziv.Substring(0, 5) == "08100")
                        datumBisValue = DateTime.ParseExact(col.Redovi[i].TrimEnd('.'), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    if (col.Naziv.Substring(0, 5) == "08070")
                    {
                        if (col.Redovi[i] == "")
                            transaktionskostenKennzeichenValue = "H";
                        else
                            transaktionskostenBetragValue = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace(",", ".");
                        if (transaktionskostenBetragValue == "0.00000")
                            transaktionskostenKennzeichenValue = "A";
                    }
                    if (col.Naziv.Substring(0, 5) == "08030")
                    {
                        if (col.Redovi[i] == "")
                            laufendeKostenKennzeichenValue = "H";
                        else
                            laufendeKostenBetragValue = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace(",", ".");
                        if (laufendeKostenBetragValue == "0.00000")
                            laufendeKostenKennzeichenValue = "A";
                    }
                    if (col.Naziv.Substring(0, 5) == "08080")
                    {
                        if (col.Redovi[i] == "")
                            anlassbezogeneKostenKennzeichenValue = "H";
                        else
                            anlassbezogeneKostenBetragValue = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace(",", ".");
                        if (anlassbezogeneKostenBetragValue == "0.00000")
                            anlassbezogeneKostenKennzeichenValue = "A";
                    }
                }
                datumVon.Add(datumVonValue);
                datumBis.Add(datumBisValue);
                transaktionskostenBetrag.Add(transaktionskostenBetragValue);
                transaktionskostenKennzeichen.Add(transaktionskostenKennzeichenValue);
                laufendeKostenBetrag.Add(laufendeKostenBetragValue);
                laufendeKostenKennzeichen.Add(laufendeKostenKennzeichenValue);
                anlassbezogeneKostenBetrag.Add(anlassbezogeneKostenBetragValue);
                anlassbezogeneKostenKennzeichen.Add(anlassbezogeneKostenKennzeichenValue);
            }
            return
            (
            wkn,
            isin,
            datumVon,
            datumBis,
            transaktionskostenBetrag,
            transaktionskostenKennzeichen,
            laufendeKostenBetrag,
            laufendeKostenKennzeichen,
            anlassbezogeneKostenBetrag,
            anlassbezogeneKostenKennzeichen
            );
        }
        public static void SaveJVDataToCSV(JV.JVData data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Zapisujemo zaglavlje kolona
                writer.WriteLine("WKN,ISIN,DatumVon,DatumBis,KorrekturKZ,TransaktionskostenBetrag,TransaktionskostenKennzeichen,LaufendeKostenBetrag,LaufendeKostenKennzeichen,AnlassbezogeneKostenBetrag,AnlassbezogeneKostenKennzeichen");

                // Nalazimo najveći broj redova među svim listama
                int maxRows = new[]
                {
            data.WKN,
            data.ISIN,
            data.DatumVon,
            data.DatumBis,
            data.KorrekturKZ,
            data.TransaktionskostenBetrag,
            data.TransaktionskostenKennzeichen,
            data.LaufendeKostenBetrag,
            data.LaufendeKostenKennzeichen,
            data.AnlassbezogeneKostenBetrag,
            data.AnlassbezogeneKostenKennzeichen,
        }.Max(list => list.Count);

                for (int i = 0; i < maxRows; i++)
                {
                    // Pravljenje reda sa vrednostima iz svih listi
                    var row = new List<string>
            {
                GetListValueAtIndex(data.WKN, i),
                GetListValueAtIndex(data.ISIN, i),
                GetListValueAtIndex(data.DatumVon, i),
                GetListValueAtIndex(data.DatumBis, i),
                GetListValueAtIndex(data.KorrekturKZ, i),
                GetListValueAtIndex(data.TransaktionskostenBetrag, i),
                GetListValueAtIndex(data.TransaktionskostenKennzeichen, i),
                GetListValueAtIndex(data.LaufendeKostenBetrag, i),
                GetListValueAtIndex(data.LaufendeKostenKennzeichen, i),
                GetListValueAtIndex(data.AnlassbezogeneKostenBetrag, i),
                GetListValueAtIndex(data.AnlassbezogeneKostenKennzeichen, i),
            };

                    // Zapisujemo red u CSV
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }
        // Pomoćna funkcija za dobijanje vrednosti iz liste na osnovu indeksa
        private static string GetListValueAtIndex(List<string> list, int index)
        {
            return index < list.Count ? list[index] : string.Empty;
        }

    }
}