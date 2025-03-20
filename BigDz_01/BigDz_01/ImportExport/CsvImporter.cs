using System;
using System.Collections.Generic;
using BigDz_01.Domain;


namespace BigDz_01.ImportExport
{
    public class CsvImporter : DataImporter
    {
        protected override List<Operation> ParseData(string rawData)
        {
            Console.WriteLine("Parsing CSV data...");
            // For demonstration purposes we return dummy data.
            return new List<Operation>
            {
                DomainFactory.CreateOperation(CategoryType.Income, 1, 1000, DateTime.Now, "CSV import", 1)
            };
        }
    }
}
