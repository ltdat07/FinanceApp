using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Importers
{
    public class YamlImporter : DataImporter
    {
        protected override ImportData ParseData(string content)
        {
            Console.WriteLine("Парсинг YAML данных...");
            var data = new ImportData();
            data.BankAccounts.Add(new BankAccount(1, "YAML Account", 3000));
            data.Categories.Add(new Category(1, "YAML Category", "доход"));
            data.Operations.Add(new Operation(1, "доход", 1, 700, 1, "YAML Operation"));
            return data;
        }
    }
}
