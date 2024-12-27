using System;
using EMT;
using ZMF;
using KTF;

class Program
{
    static void Main(string[] args)
    {
        // Putanja do Excel fajla
        string filePath = @"D:\C#\Test\Import\Orion-EMT_LU_4_2-20240930_CL v4.2.xlsx"; // Zamenite sa stvarnom putanjom do fajla

        // Kreiraj objekat koji će čuvati podatke o kolonama
        EMTData emtData = EMTDataHelper.LoadEMT(filePath); // Pozivamo funkciju iz EMTDataHelper klase

        // Meni za korisnika
        Console.WriteLine("Dobrodošli u program za obradu podataka!");

        Console.WriteLine("Molimo unesite ime ZMF fajla (bez ekstenzije):");
        string csvFileZMFName = Console.ReadLine() ?? string.Empty;
        string csvFileZMFPath = @$"D:\C#\Test\Export\{csvFileZMFName}.csv";

        //Console.WriteLine("Molimo unesite ime KTF fajla (bez ekstenzije):");
        //string csvFileKTFName = Console.ReadLine() ?? string.Empty;
        //string csvFileKTFPath = @$"C:\Users\HP\Documents\ZMF,KTF,JV - TEST\KTF{csvFileKTFName}.csv";

        //Console.WriteLine("Molimo unesite ime JV fajla (bez ekstenzije):");
        //string csvFileJVName = Console.ReadLine() ?? string.Empty;
        //string csvFileJVPath = @$"C:\Users\HP\Documents\ZMF,KTF,JV - TEST\JV{csvFileJVName}.csv";

        Console.WriteLine($"Vaš CSV fajl će biti sačuvan na lokaciji: {csvFileZMFPath}");

        ZMFData zmfData = ZMFDataHelper.EMTtoZMF(emtData);
        ZMFDataHelper.SaveZMFDataToCSV(zmfData, csvFileZMFPath);

        //KTFData ktfData = KTFDataHelper.EMTtoKTF(emtData);
        //KTFDataHelper.SaveKTFDataToCSV(ktfData, csvFileKTFPath);

        Console.WriteLine("Podaci su uspešno upisani u CSV fajl!");
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

