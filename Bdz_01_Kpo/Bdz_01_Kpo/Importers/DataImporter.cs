using Bdz_01_Kpo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Importers
{
    public abstract class DataImporter
    {
        public ImportData Import(string filePath)
        {
            string content = File.ReadAllText(filePath);
            ImportData data = ParseData(content);
            Console.WriteLine("Импортированные данные:");
            Console.WriteLine($"  Счетов: {data.BankAccounts.Count}");
            Console.WriteLine($"  Категорий: {data.Categories.Count}");
            Console.WriteLine($"  Операций: {data.Operations.Count}");
            return data;
        }

        protected abstract ImportData ParseData(string content);
    }
}
