using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using EMT;  // Koristimo prostor imena EMT da bismo koristili EMTData i ColumnInfo

namespace EMT
{
    public class EMTDataHelper
    {
        // Funkcija koja učitava Excel fajl i popunjava EMTData objekat
        public static EMTData LoadEMT(string filePath)
        {
            // Kreiranje objekta za čuvanje podataka
            EMTData emtData = new EMTData();

            // Učitavanje Excel fajla
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                // Uzmi prvi radni list
                var worksheet = package.Workbook.Worksheets[0];

                // Prolazimo kroz sve kolone
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    // Pronađi naziv kolone (prva ćelija u koloni)
                    string columnName = worksheet.Cells[1, col].Text;

                    // Kreiraj objekat sa informacijama o koloni
                    ColumnInfo columnInfo = new ColumnInfo
                    {
                        ID = col,  // ID kolone (početni broj je 1)
                        Naziv = columnName
                    };

                    // Dodaj vrednosti svih redova u ovu kolonu
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)  // Počinjemo od 2. reda (pretpostavljamo da je prvi red zaglavlje)
                    {
                        string cellValue = worksheet.Cells[row, col].Text;
                        columnInfo.Redovi.Add(cellValue);
                    }

                    // Dodaj ovu kolonu u EMTData objekat
                    emtData.Kolone.Add(columnInfo);
                }
            }

            return emtData;
        }
        
    }
    
}
