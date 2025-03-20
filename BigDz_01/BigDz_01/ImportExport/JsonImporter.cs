using System;
using System.Collections.Generic;
using BigDz_01.Domain;

namespace BigDz_01.ImportExport
{
    public class JsonImporter : DataImporter
    {
        protected override List<Operation> ParseData(string rawData)
        {
            Console.WriteLine("Parsing JSON data...");
            // For demonstration purposes, we return dummy data.
            return new List<Operation>
            {
                DomainFactory.CreateOperation(CategoryType.Expense, 1, 500, DateTime.Now, "JSON import", 2)
            };
        }
    }
}
