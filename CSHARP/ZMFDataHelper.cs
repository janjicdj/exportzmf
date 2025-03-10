using EMT;

namespace ZMF
{
    public static class ZMFDataHelper
    {
        //Funkcije za Upisivanje podataka u ZMF atribute
        public static ZMFData EMTtoZMF(EMTData emtData, EMTData eetData, EMTData amtData)
        {
            ZMFData zmfData = new ZMFData();
            //zmfData.Status="01";
            //zmfData.Modus_Genehmigung="B";
            //Status and genehmigung su upisani dole u Horizont
            zmfData.WKN = WriteWKN(amtData);
            zmfData.ISIN = WriteISIN(emtData);
            zmfData.Kundenkat = WriteKundenkat(emtData, "Y");
            zmfData.Ziele = WriteZiele(emtData, "Y");
            (zmfData.Status, zmfData.Modus_Genehmigung, zmfData.Horizont) = WriteHorizont(emtData);
            zmfData.Tragfaehigkeit = WriteTragfaehigkeit(emtData, "Y");
            zmfData.Erfahrung = WriteErfahrung(emtData, "Y");
            (zmfData.Strategie, zmfData.Neg_Strategie) = WriteStrategie_and_Neg_Strategie(emtData);
            zmfData.Spez_Anford = WriteSpez_Anford(emtData, "Y");
            (zmfData.Risiko_Ind, zmfData.Methode, zmfData.R_R_Profil) = WriteRisiko_Ind_Methode_R_R_Profil(emtData);
            zmfData.Neg_Kundenkat = WriteKundenkat(emtData, "N");
            zmfData.Neg_Ziele = WriteZiele(emtData, "N");
            zmfData.Neg_Tragfaehigkeit = WriteTragfaehigkeit(emtData, "N");
            zmfData.Neg_Erfahrung = WriteErfahrung(emtData, "N");
            zmfData.Prodkat = WriteProdkat(emtData);
            zmfData.Genehmigungsp = WriteGenehmigungsp(emtData);
            zmfData.SustainPreference = WriteSustainPreference(eetData);
            zmfData.SustainMainFocus = WriteSustainMainFocus(eetData);
            zmfData.SustainLabel = WriteSustainLabel(eetData);
            return zmfData;
        }

        private static List<string> WriteWKN(EMTData amtData)
        {
            var amtRowNum = amtData.Kolone[1].Redovi.Count;
            List<string> wkn = new List<string>();
            for (int i = 6; i < amtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in amtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 10) == "OFST020015")
                    {
                        value = col.Redovi[i];
                    }
                }
                wkn.Add(value);
            }
            return wkn;
        }

        public static List<string> WriteISIN(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> isin = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "00010")
                    {
                        value += col.Redovi[i];
                    }
                }
                isin.Add(value);
            }
            return isin;
        }
        public static List<string> WriteKundenkat(EMTData emtData, string x)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> kundenkat = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA = false;
                bool IsB = false;
                bool IsC = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv == "01010_Investor_Type_Retail")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsA = true;
                        }
                    }
                    if (col.Naziv == "01020_Investor_Type_Professional")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsB = true;
                        }
                    }
                    if (col.Naziv == "01030_Investor_Type_Eligible_Counterparty")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsC = true;
                        }
                    }
                }
                if (IsA) value += "A#";
                if (IsB) value += "B#";
                if (IsC) value += "C#";
                kundenkat.Add(value);
            }
            return kundenkat;
        }
        public static List<string> WriteZiele(EMTData emtData, string x)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> ziele = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA = false;
                bool IsB = false;
                bool IsC = false;
                bool IsD = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "05070")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsA = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "05010" ||
                    col.Naziv.Substring(0, 5) == "05020" ||
                    col.Naziv.Substring(0, 5) == "05030")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsB = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "05050")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsC = true;
                        }

                    }
                    if (col.Naziv.Substring(0, 5) == "05040")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsD = true;
                        }
                    }
                }
                if (IsA) value += "A#";
                if (IsB) value += "B#";
                if (IsC) value += "C#";
                if (IsD) value += "D#";
                ziele.Add(value);
            }
            return ziele;
        }
        public static (List<string>, List<string>, List<string>) WriteHorizont(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> status = new List<string>();
            List<string> horizont = new List<string>();
            List<string> genehmigung = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA = false;
                bool IsB = false;
                bool IsC = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "05080")
                    {
                        if ("V" == col.Redovi[i] || "S" == col.Redovi[i] ||
                        (int.TryParse(col.Redovi[i], out int number) && number < 3))
                        {
                            IsA = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "05080")
                    {
                        if ("M" == col.Redovi[i] ||
                        (int.TryParse(col.Redovi[i], out int number) && number >= 3 && number <= 5))
                        {
                            IsB = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "05080")
                    {
                        if ("L" == col.Redovi[i] ||
                        (int.TryParse(col.Redovi[i], out int number) && number > 5))
                        {
                            IsC = true;
                        }
                    }
                }
                if (IsA) value += "A#";
                if (IsB) value += "B#";
                if (IsC) value += "C#";
                status.Add("01");
                genehmigung.Add("B");
                horizont.Add(value);
            }
            return (status, genehmigung, horizont);
        }
        public static List<string> WriteTragfaehigkeit(EMTData emtData, string x)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> tragfaehigkeit = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsY10 = false;
                bool IsY20 = false;
                bool IsB = false;
                bool IsC = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "03010")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsY10 = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "03020")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsY20 = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "03040")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsB = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "03050")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsC = true;
                        }
                    }
                }
                if (IsY10 || IsY20) value = "A";
                if (IsB) value = "B";
                if (IsC) value = "C";
                tragfaehigkeit.Add(value);
            }
            return tragfaehigkeit;
        }
        public static List<string> WriteErfahrung(EMTData emtData, string x)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> erfahrung = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA = false;
                bool IsB = false;
                bool IsC = false;
                bool IsD = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "02010")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsA = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "02020")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsB = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "02030")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsC = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "02040")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsD = true;
                        }
                    }
                }
                if (IsA) value = "A";
                if (IsB) value = "B";
                if (IsC) value = "C";
                if (IsD) value = "D";
                erfahrung.Add(value);
            }
            return erfahrung;
        }
        public static (List<string>, List<string>) WriteStrategie_and_Neg_Strategie(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> strategie = new List<string>();
            List<string> neg_strategie = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA1 = false;
                bool IsA2 = false;
                bool IsB = false;
                bool IsC = false;
                string value = "";
                bool IsA1neg = false;
                bool IsA2neg = false;
                bool IsBneg = false;
                bool IsCneg = false;
                string valueneg = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "06010")
                    {
                        if ("R" == col.Redovi[i] || "B" == col.Redovi[i])
                        {
                            IsA1 = true;
                        }
                        if ("Neither" == col.Redovi[i])
                        {
                            IsA1 = false;
                            IsB = false;
                            IsC = false;
                            IsA1neg = true;
                            break;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "06020")
                    {
                        if ("R" == col.Redovi[i] || "B" == col.Redovi[i])
                        {
                            IsA2 = true;
                        }
                        if ("Neither" == col.Redovi[i])
                        {
                            IsA1 = false;
                            IsB = false;
                            IsC = false;
                            IsA2neg = true;
                            break;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "06030")
                    {
                        if ("R" == col.Redovi[i] || "B" == col.Redovi[i])
                        {
                            IsC = true;
                        }
                        if ("Neither" == col.Redovi[i])
                        {
                            IsA1 = false;
                            IsB = false;
                            IsC = false;
                            IsCneg = true;
                            break;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "06040")
                    {
                        if ("R" == col.Redovi[i] || "B" == col.Redovi[i])
                        {
                            IsB = true;
                        }
                        if ("Neither" == col.Redovi[i])
                        {
                            IsA1 = false;
                            IsB = false;
                            IsC = false;
                            IsBneg = true;
                            break;
                        }
                    }
                }
                if (IsA1 && IsA2) value += "A#";
                if (IsB) value += "B#";
                if (IsC) value += "C#";
                strategie.Add(value);
                if (IsA1neg && IsA2neg) valueneg += "A#";
                if (IsBneg) valueneg += "B#";
                if (IsCneg) valueneg += "C#";
                neg_strategie.Add(valueneg);
            }
            return (strategie, neg_strategie);
        }
        public static List<string> WriteSpez_Anford(EMTData emtData, string x)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> spez_Anford = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                bool IsA = false;
                bool IsB = false;
                bool IsC = false;
                bool IsZ = false;
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "05105")
                    {
                        if (x == col.Redovi[i])
                        {
                            IsA = true;
                            IsB = true;
                        }
                    }
                    if (col.Naziv.Substring(0, 5) == "05115")
                    {
                        if (x == "N")
                        {
                            if ("N" == col.Redovi[i])
                                IsC = true;
                        }
                        else
                        {
                            if ("I" == col.Redovi[i])
                            {
                                IsC = true;
                            }
                            if ("N" == col.Redovi[i] || "O" == col.Redovi[i])
                            {
                                IsZ = true;
                            }
                        }
                    }
                }
                if (IsA) value += "A#";
                if (IsB) value += "B#";
                if (IsC) value += "C#";
                if (IsZ) value += "Z#";
                spez_Anford.Add(value);
            }
            return spez_Anford;
        }
        public static (List<string>, List<string>, List<string>) WriteRisiko_Ind_Methode_R_R_Profil(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> risiko_Ind = new List<string>();
            List<string> methode = new List<string>();
            List<string> r_R_Profil = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value10 = "";
                string value20 = "";
                string value30 = "";
                string value = "";
                string value_met = "B";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "04010")
                    {
                        value10 = col.Redovi[i];
                    }
                    if (col.Naziv.Substring(0, 5) == "04020")
                    {
                        value20 = col.Redovi[i];
                    }
                    if (col.Naziv.Substring(0, 5) == "04030")
                    {
                        value30 = col.Redovi[i];
                    }
                }
                if (value20 == "")
                {
                    value_met = "A";
                    if (value10 == "")
                    {
                        value_met = "C";
                        if (value30 == "")
                        {
                            value_met = "NA";
                        }
                    }
                    switch (value10)
                    {
                        case "1": value = "A"; break;
                        case "2": value = "B"; break;
                        case "3": value = "C"; break;
                        case "4": value = "D"; break;
                        case "5": value = "E"; break;
                        case "6": value = "F"; break;
                        case "7": value = "G"; break;
                        default: value = "N"; break;
                    }
                }
                else
                {
                    switch (value20)
                    {
                        case "1": value = "A"; break;
                        case "2": value = "B"; break;
                        case "3": value = "C"; break;
                        case "4": value = "D"; break;
                        case "5": value = "E"; break;
                        case "6": value = "F"; break;
                        case "7": value = "G"; break;
                        default: value = "N"; break;
                    }
                }

                risiko_Ind.Add(value);
                methode.Add(value_met);
                r_R_Profil.Add(value);
            }
            return (risiko_Ind, methode, r_R_Profil);
        }
        public static List<string> WriteProdkat(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> prodkat = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "00090")
                    {
                        switch (col.Redovi[i])
                        {
                            case "1": value = "A"; break;
                            case "2": value = "B"; break;
                            case "3": value = "C"; break;
                            case "4": value = "D"; break;
                            case "5": value = "E"; break;
                            case "6": value = "F"; break;
                            case "7": value = "G"; break;
                            case "8": value = "H"; break;
                            case "9": value = "I"; break;
                            case "10": value = "J"; break;
                            case "11": value = "K"; break;
                            case "12": value = "L"; break;
                            case "13": value = "M"; break;
                            case "14": value = "N"; break;
                            default: value = ""; break;
                        }
                    }
                }
                prodkat.Add(value);
            }
            return prodkat;
        }
        public static List<string> WriteGenehmigungsp(EMTData emtData)
        {
            var emtRowNum = emtData.Kolone[1].Redovi.Count;
            List<string> gnehmigungsp = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in emtData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "00075")
                    {
                        value = col.Redovi[i];
                        break;
                    }
                }
                gnehmigungsp.Add(value);
            }
            return gnehmigungsp;
        }
        public static List<string> WriteSustainPreference(EMTData eetData)
        {
            var emtRowNum = eetData.Kolone[1].Redovi.Count;
            List<string> sustainPreference = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in eetData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "60440")
                    {
                        value = col.Redovi[i];
                    }
                }
                if (value == "")
                {
                    sustainPreference.Add("A");
                }
                else
                {
                    sustainPreference.Add(value);
                }
            }
            return sustainPreference;
        }
        public static List<string> WriteSustainMainFocus(EMTData eetData)
        {
            var emtRowNum = eetData.Kolone[1].Redovi.Count;
            List<string> sustainMainFocus = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in eetData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "20090")
                    {
                        value = col.Redovi[i];
                    }
                }
                if (value == "")
                {
                    sustainMainFocus.Add("A");
                }
                else if (value == "MF")
                {
                    sustainMainFocus.Add("E/S/G");
                }
                else
                {
                    sustainMainFocus.Add(value);
                }
            }
            return sustainMainFocus;
        }
        public static List<string> WriteSustainLabel(EMTData eetData)
        {
            var emtRowNum = eetData.Kolone[1].Redovi.Count;
            List<string> sustainLabel = new List<string>();
            for (int i = 0; i < emtRowNum; i++)
            {
                string value = "";
                foreach (EMT.ColumnInfo col in eetData.Kolone)
                {
                    if (col.Naziv.Substring(0, 5) == "20060")
                    {
                        switch (col.Redovi[i])
                        {
                            case "A": value = "1"; break;
                            case "C": value = "3"; break;
                            case "D": value = "4"; break;
                            case "B": value = "2"; break;
                            case "E": value = "5"; break;
                            case "F": value = "6"; break;
                            case "G": value = "7"; break;
                            case "H": value = "8"; break;
                            case "I": value = "9"; break;
                            case "J": value = "10"; break;
                            case "K": value = "11"; break;
                            case "L": value = "12"; break;
                            case "M": value = "13"; break;
                            case "N": value = "14"; break;
                            default: value = ""; break;
                        }
                    }
                }
                sustainLabel.Add(value);
            }
            return sustainLabel;
        }
        //***********************************************************************************************

        public static void SaveZMFDataToCSV(ZMFData data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Prvo zapisujemo nazive kolona (zaglavlje)
                writer.WriteLine("Status,Modus_Genehmigung,WKN,ISIN,Kundenkat,Ziele,Horizont,Tragfaehigkeit,Erfahrung,Strategie,Spez_Anford,Text_zu_Spez_Anford,Risiko_Ind,Methode,R_R_Profil,Neg_Kundenkat,Neg_Ziele,Neg_Horizont,Neg_Tragfaehigkeit,Neg_Erfahrung,Neg_Strategie,Neg_Spez_Anford,Neg_Risiko_Ind,Neg_Risiko_Renditeprofil,Prodkat,Genehmigungsp,SustainPreference,SustainMainFocus,SustainLabel");

                // Prolazimo kroz sve podatke i zapisujemo ih u CSV
                int maxRows = new[]
                {
                    data.Status,
                    data.Modus_Genehmigung,
                    data.WKN,
                    data.ISIN,
                    data.Kundenkat,
                    data.Ziele,
                    data.Horizont,
                    data.Tragfaehigkeit,
                    data.Erfahrung,
                    data.Strategie,
                    data.Spez_Anford,
                    data.Text_zu_Spez_Anford,
                    data.Risiko_Ind,
                    data.Methode,
                    data.R_R_Profil,
                    data.Neg_Kundenkat,
                    data.Neg_Ziele,
                    data.Neg_Horizont,
                    data.Neg_Tragfaehigkeit,
                    data.Neg_Erfahrung,
                    data.Neg_Strategie,
                    data.Neg_Spez_Anford,
                    data.Neg_Risiko_Ind,
                    data.Neg_Risiko_Renditeprofil,
                    data.Prodkat,
                    data.Genehmigungsp,
                    data.SustainPreference,
                    data.SustainMainFocus,
                    data.SustainLabel
                }.Max(list => list.Count); // Najveći broj redova u svim listama

                for (int i = 0; i < maxRows; i++)
                {
                    // Zbirka za podatke svakog reda
                    var row = new List<string>
                    {
                        // Uzimamo vrednosti sa indeksom 'i' iz svake liste
                        GetListValueAtIndex(data.Status, i),
                        GetListValueAtIndex(data.Modus_Genehmigung, i),
                        GetListValueAtIndex(data.WKN, i),
                        GetListValueAtIndex(data.ISIN, i),
                        GetListValueAtIndex(data.Kundenkat, i),
                        GetListValueAtIndex(data.Ziele, i),
                        GetListValueAtIndex(data.Horizont, i),
                        GetListValueAtIndex(data.Tragfaehigkeit, i),
                        GetListValueAtIndex(data.Erfahrung, i),
                        GetListValueAtIndex(data.Strategie, i),
                        GetListValueAtIndex(data.Spez_Anford, i),
                        GetListValueAtIndex(data.Text_zu_Spez_Anford, i),
                        GetListValueAtIndex(data.Risiko_Ind, i),
                        GetListValueAtIndex(data.Methode, i),
                        GetListValueAtIndex(data.R_R_Profil, i),
                        GetListValueAtIndex(data.Neg_Kundenkat, i),
                        GetListValueAtIndex(data.Neg_Ziele, i),
                        GetListValueAtIndex(data.Neg_Horizont, i),
                        GetListValueAtIndex(data.Neg_Tragfaehigkeit, i),
                        GetListValueAtIndex(data.Neg_Erfahrung, i),
                        GetListValueAtIndex(data.Neg_Strategie, i),
                        GetListValueAtIndex(data.Neg_Spez_Anford, i),
                        GetListValueAtIndex(data.Neg_Risiko_Ind, i),
                        GetListValueAtIndex(data.Neg_Risiko_Renditeprofil, i),
                        GetListValueAtIndex(data.Prodkat, i),
                        GetListValueAtIndex(data.Genehmigungsp, i),
                        GetListValueAtIndex(data.SustainPreference, i),
                        GetListValueAtIndex(data.SustainMainFocus, i),
                        GetListValueAtIndex(data.SustainLabel, i)
                    };

                    // Zapisujemo red u CSV
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }

        // Pomocna funkcija
        public static string GetListValueAtIndex(List<string> list, int index)
        {
            return index < list.Count ? list[index] : "";
        }

    }
}
