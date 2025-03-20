using System;
using System.Collections.Generic;
using BigDz_01.Domain;

namespace BigDz_01.ImportExport
{
    public class YamlImporter : DataImporter
    {
        protected override List<Operation> ParseData(string rawData)
        {
            Console.WriteLine("Parsing YAML data...");
            // For demonstration purposes, we return dummy data.
            return new List<Operation>
            {
                DomainFactory.CreateOperation(CategoryType.Income, 1, 750, DateTime.Now, "Import YAML", 1)
            };
        }
    }
}
