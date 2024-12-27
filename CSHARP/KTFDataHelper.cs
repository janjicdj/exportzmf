using EMT;

namespace KTF
{
    public static class KTFDataHelper
    {

        public static KTFData EMTtoKTF(EMTData emtData)
        {
            KTFData ktfData = new KTFData();
            //ktfData.Status = WriteStatus(emtData);
            //ktfData.WKN = WriteWKN(emtData);
            //ktfData.ISIN = WriteISIN(emtData);
            //ktfData.Betrag_Lauf_Fondskosten = WriteBetrag_Lauf_Fondskosten(emtData);
            return ktfData;
        }

        //public static List<string> WriteStatus(EMTData emtData)

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
