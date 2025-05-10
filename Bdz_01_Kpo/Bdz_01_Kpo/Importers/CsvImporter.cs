using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Importers
{
    public class CsvImporter : DataImporter
    {
        protected override ImportData ParseData(string content)
        {
            Console.WriteLine("Парсинг CSV данных...");
            var data = new ImportData();
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
                return data;
            // Пропускаем заголовок.
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length < 6)
                    continue;
                if (!decimal.TryParse(parts[3], out decimal amount))
                    continue;
                if (!DateTime.TryParse(parts[4], out DateTime date))
                    continue;
                // Здесь в CSV указаны только данные для операции.
                var op = new Operation(
                    id: int.Parse(parts[0]),
                    type: parts[2].Trim(),
                    bankAccountId: 1,
                    amount: amount,
                    categoryId: 1,
                    description: parts[5].Trim()
                );
                op.Date = date;
                data.Operations.Add(op);
            }
            return data;
        }
    }
}
