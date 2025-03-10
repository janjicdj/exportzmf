using System;
using EMT;
using ZMF;
using KTF;
using JV;

class Program
{
    static void Main(string[] args)
    {
        // Putanja do Excel fajla
        string EMTfilePath = @"..\Pareto Files for Test\Import\20241120_ EMT-V42 _ PARETO SICAV _ 20241119.xlsx"; // Zamenite sa stvarnom putanjom do fajla
        string EETfilePath = @"..\Pareto Files for Test\Import\20241212_ EET-V112 _ PARETO SICAV _ 20241211.xlsx"; // Zamenite sa stvarnom putanjom do fajla
        string AMTfilePath = @"..\Pareto Files for Test\Import\AMT_Pareto AMT_20241107_150008_571.xlsx"; // Zamenite sa stvarnom putanjom do fajla

        // Kreiraj objekat koji će čuvati podatke o kolonama
        //Tip podatka je EMTData ali u taj tip podatka cuvamo i amt i eet
        EMTData emtData = EMTDataHelper.LoadEMT(EMTfilePath); // Pozivamo funkciju iz EMTDataHelper klase
        EMTData eetData = EMTDataHelper.LoadEMT(EETfilePath); // Pozivamo funkciju iz EMTDataHelper klase
        EMTData amtData = EMTDataHelper.LoadEMT(AMTfilePath); // Pozivamo funkciju iz EMTDataHelper klase

        // Meni za korisnika
        Console.WriteLine("Dobrodošli u program za obradu podataka!");

        Console.WriteLine("Molimo unesite ime fajlova (bez ekstenzije):");
        string subname = Console.ReadLine() ?? string.Empty;
        string csvFileZMFPath = @$"..\ZMF,KTF,JV - TEST\ZMF{subname}.csv";

        string csvFileKTFPath = @$"..\ZMF,KTF,JV - TEST\KTF{subname}.csv";

        string csvFileJVPath = @$"..\ZMF,KTF,JV - TEST\JV{subname}.csv";

        Console.WriteLine($"Vaši CSV fajlovi će biti sačuvan na lokaciji: {csvFileZMFPath}");

        ZMFData zmfData = ZMFDataHelper.EMTtoZMF(emtData, eetData, amtData);
        ZMFDataHelper.SaveZMFDataToCSV(zmfData, csvFileZMFPath);

        KTFData ktfData = KTFDataHelper.EMTtoKTF(emtData, amtData);
        KTFDataHelper.SaveKTFDataToCSV(ktfData, csvFileKTFPath);

        JVData jvData = JVDataHelper.EMTtoJV(amtData, emtData);
        JVDataHelper.SaveJVDataToCSV(jvData, csvFileJVPath);

        Console.WriteLine("Podaci su uspešno upisani u CSV fajlove!");
    }
}











// Funkcija za ispis podataka iz EMTData objekta
//static void IspisiPodatke(EMTData emtData)
//{
//    Console.WriteLine("Podaci sačuvani u EMTData objektu:");
//    Console.WriteLine("-------------------------------");
//
//    // Ispis svih kolona i njihovih redova
//    foreach (var column in emtData.Kolone)
//    {
//        Console.WriteLine($"ID: {column.ID}, Naziv: {column.Naziv}");
//        for (int i = 0; i < column.Redovi.Count; i++)
//        {
//            Console.WriteLine($"    Red {i + 1}: {column.Redovi[i]}");
//        }
//        Console.WriteLine();  // Dodaje prazan red između kolona
//    }
//}

