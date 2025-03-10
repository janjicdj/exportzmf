using System.Globalization;
using EMT;

namespace KTF
{
    public static class KTFDataHelper
    {

        public static KTFData EMTtoKTF(EMTData emtData, EMTData amtData)
        {
            KTFData ktfData = new KTFData();

            (ktfData.Betrag_Lauf_Fondskosten,
            ktfData.Status,
            ktfData.Betrag_Transaktionskosten,
            ktfData.Betrag_Anlassbez_Kosten,
            ktfData.Nullmeldung_Lauf_Fondskosten,
            ktfData.Nullmeldung_Transaktionskosten,
            ktfData.Nullmeldung_Anlassbez_Kosten) = WriteBetragNullmeldung(emtData);
            (ktfData.Datum_Lauf_Fondskosten,
            ktfData.Datum_Transaktionskosten,
            ktfData.Datum_Anlassbez_Kosten) = WriteDatum(emtData);
            (ktfData.WKN,
            ktfData.ISIN,
            ktfData.Betrag_Tat_Ruecknahmekosten,
            ktfData.Datum_Tat_Ruecknahmekosten,
            ktfData.Nullmeldung_Tat_Ruecknahmekosten,
            ktfData.Swing_Pricing) = Write(amtData);

            return ktfData;
        }

        private static (List<string>, List<string>, List<string>, List<string>, List<string>, List<string>) Write(EMTData amtData)
        {
            var amtRowNum = amtData.Kolone[1].Redovi.Count;
            List<string> wkn = new List<string>();
            List<string> isin = new List<string>();
            List<string> betrag_Tat_Ruecknahmekosten = new List<string>();
            List<string> datum_Tat_Ruecknahmekosten = new List<string>();
            List<string> nullmeldung_Tat_Ruecknahmekosten = new List<string>();
            List<string> swing_Pricing = new List<string>();

            for (int i = 6; i < amtRowNum; i++)
            {
                string value_wkn = "";
                string value_isin = "";
                string value_betrag_Tat_Ruecknahmekosten = "";
                string value_datum_Tat_Ruecknahmekosten = "";
                string value_swing_Pricing = "";
                foreach (EMT.ColumnInfo col in amtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 10) == "OFST020000")
                        value_wkn = col.Redovi[i];
                    if (col.Naziv.Substring(0, 10) == "OFST020015")
                        value_wkn = col.Redovi[i];
                    if (col.Naziv.Substring(0, 10) == "OFST451405")
                    {
                        if (Double.TryParse(col.Redovi[i], NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                            value_betrag_Tat_Ruecknahmekosten = Math.Round(result * 100, 5).ToString("F5").Replace('.', ',');
                    }
                    if (col.Naziv.Substring(0, 10) == "OFST451406")
                        if (!string.IsNullOrEmpty(col.Redovi[i]))
                            value_datum_Tat_Ruecknahmekosten = DateTime.ParseExact(col.Redovi[i].TrimEnd('.'), "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    if (col.Naziv.Substring(0, 10) == "OFST401003")
                        value_swing_Pricing = col.Redovi[i];
                }
                wkn.Add(value_wkn);
                isin.Add(value_isin);
                betrag_Tat_Ruecknahmekosten.Add(value_betrag_Tat_Ruecknahmekosten);
                datum_Tat_Ruecknahmekosten.Add(value_datum_Tat_Ruecknahmekosten);
                nullmeldung_Tat_Ruecknahmekosten.Add(value_betrag_Tat_Ruecknahmekosten == "" ? "" : "1");
                swing_Pricing.Add(value_swing_Pricing == "no swing NAV" ? "N" : "J");
            }

            return (
                wkn,
                isin,
                betrag_Tat_Ruecknahmekosten,
                datum_Tat_Ruecknahmekosten,
                nullmeldung_Tat_Ruecknahmekosten,
                swing_Pricing
            );
        }


        //public static List<string> WriteStatus(EMTData emtData)
        public static (List<string>, List<string>, List<string>, List<string>, List<string>, List<string>, List<string>)
        WriteBetragNullmeldung(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> betrag_Lauf_Fondskosten = new List<string>();
            List<string> betrag_Transaktionskosten = new List<string>();
            List<string> betrag_Anlassbez_Kosten = new List<string>();
            List<string> nullmeldung_Lauf_Fondskosten = new List<string>();
            List<string> nullmeldung_Transaktionskosten = new List<string>();
            List<string> nullmeldung_Anlassbez_Kosten = new List<string>();
            List<string> status = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value_betrag_Lauf_Fondskosten = "";
                string value_betrag_Transaktionskosten = "";
                string value_betrag_Anlassbez_Kosten = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "07100")
                        value_betrag_Lauf_Fondskosten = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace('.', ',');
                    if (col.Naziv.Substring(0, 5) == "07130")
                        value_betrag_Transaktionskosten = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace('.', ',');
                    if (col.Naziv.Substring(0, 5) == "07140")
                        value_betrag_Anlassbez_Kosten = Math.Round(Double.Parse(col.Redovi[i]) * 100, 5).ToString("F5").Replace('.', ',');
                }
                betrag_Lauf_Fondskosten.Add(value_betrag_Lauf_Fondskosten);
                betrag_Transaktionskosten.Add(value_betrag_Transaktionskosten);
                betrag_Anlassbez_Kosten.Add(value_betrag_Anlassbez_Kosten);
                status.Add("01");
                nullmeldung_Lauf_Fondskosten.Add(value_betrag_Lauf_Fondskosten == "" ? "" : "1");
                nullmeldung_Transaktionskosten.Add(value_betrag_Transaktionskosten == "" ? "" : "1");
                nullmeldung_Anlassbez_Kosten.Add(value_betrag_Anlassbez_Kosten == "" ? "" : "1");

            }
            return (
                betrag_Lauf_Fondskosten,
                status,
                betrag_Transaktionskosten,
                betrag_Anlassbez_Kosten,
                nullmeldung_Lauf_Fondskosten,
                nullmeldung_Transaktionskosten,
                nullmeldung_Anlassbez_Kosten
            );
        }

        public static (List<string>, List<string>, List<string>) WriteDatum(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> datum_Lauf_Fondskosten = new List<string>();
            List<string> datum_Transaktionskosten = new List<string>();
            List<string> datum_Anlassbez_Kosten = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "07160")
                    {
                        value = DateTime.ParseExact(col.Redovi[i].TrimEnd('.'), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                    }
                }
                datum_Lauf_Fondskosten.Add(value);
                datum_Transaktionskosten.Add(value);
                datum_Anlassbez_Kosten.Add(value);
            }
            return (datum_Lauf_Fondskosten, datum_Transaktionskosten, datum_Anlassbez_Kosten);
        }


        public static void SaveKTFDataToCSV(KTF.KTFData data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Zapisujemo zaglavlje kolona
                writer.WriteLine("Status,WKN,ISIN,Betrag_Lauf_Fondskosten,Datum_Lauf_Fondskosten,Nullmeldung_Lauf_Fondskosten,Betrag_Transaktionskosten,Datum_Transaktionskosten,Nullmeldung_Transaktionskosten,Betrag_Anlassbez_Kosten,Datum_Anlassbez_Kosten,Nullmeldung_Anlassbez_Kosten,Betrag_Tat_Ruecknahmekosten,Datum_Tat_Ruecknahmekosten,Nullmeldung_Tat_Ruecknahmekosten,Swing_Pricing");

                // Nalazimo najveći broj redova među svim listama
                int maxRows = new[]
                {
            data.Status,
            data.WKN,
            data.ISIN,
            data.Betrag_Lauf_Fondskosten,
            data.Datum_Lauf_Fondskosten,
            data.Nullmeldung_Lauf_Fondskosten,
            data.Betrag_Transaktionskosten,
            data.Datum_Transaktionskosten,
            data.Nullmeldung_Transaktionskosten,
            data.Betrag_Anlassbez_Kosten,
            data.Datum_Anlassbez_Kosten,
            data.Nullmeldung_Anlassbez_Kosten,
            data.Betrag_Tat_Ruecknahmekosten,
            data.Datum_Tat_Ruecknahmekosten,
            data.Nullmeldung_Tat_Ruecknahmekosten,
            data.Swing_Pricing
        }.Max(list => list.Count);

                for (int i = 0; i < maxRows; i++)
                {
                    // Pravljenje reda sa vrednostima iz svih listi
                    var row = new List<string>
            {
                GetListValueAtIndex(data.Status, i),
                GetListValueAtIndex(data.WKN, i),
                GetListValueAtIndex(data.ISIN, i),
                GetListValueAtIndex(data.Betrag_Lauf_Fondskosten, i),
                GetListValueAtIndex(data.Datum_Lauf_Fondskosten, i),
                GetListValueAtIndex(data.Nullmeldung_Lauf_Fondskosten, i),
                GetListValueAtIndex(data.Betrag_Transaktionskosten, i),
                GetListValueAtIndex(data.Datum_Transaktionskosten, i),
                GetListValueAtIndex(data.Nullmeldung_Transaktionskosten, i),
                GetListValueAtIndex(data.Betrag_Anlassbez_Kosten, i),
                GetListValueAtIndex(data.Datum_Anlassbez_Kosten, i),
                GetListValueAtIndex(data.Nullmeldung_Anlassbez_Kosten, i),
                GetListValueAtIndex(data.Betrag_Tat_Ruecknahmekosten, i),
                GetListValueAtIndex(data.Datum_Tat_Ruecknahmekosten, i),
                GetListValueAtIndex(data.Nullmeldung_Tat_Ruecknahmekosten, i),
                GetListValueAtIndex(data.Swing_Pricing, i)
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
