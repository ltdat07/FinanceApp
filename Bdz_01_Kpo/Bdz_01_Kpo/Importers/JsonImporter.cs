using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Importers
{
    public class JsonImporter : DataImporter
    {
        protected override ImportData ParseData(string content)
        {
            Console.WriteLine("Парсинг JSON данных...");
            var data = new ImportData();
            data.BankAccounts.Add(new BankAccount(1, "JSON Account", 1000));
            data.Categories.Add(new Category(1, "JSON Category", "доход"));
            data.Operations.Add(new Operation(1, "доход", 1, 500, 1, "JSON Operation"));
            return data;
        }
    }
}
