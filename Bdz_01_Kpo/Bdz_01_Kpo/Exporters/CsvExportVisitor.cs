using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Exporters
{
    public class CsvExportVisitor : IExportVisitor
    {
        private readonly List<string> lines = new List<string>();
        public void Visit(BankAccount account) => lines.Add($"BankAccount,{account.Id},{account.Name},{account.Balance}");
        public void Visit(Category category) => lines.Add($"Category,{category.Id},{category.Name},{category.Type}");
        public void Visit(Operation operation) => lines.Add($"Operation,{operation.Id},{operation.Type},{operation.BankAccountId},{operation.Amount},{operation.Date},{operation.Description},{operation.CategoryId}");
        public string GetResult() => string.Join(Environment.NewLine, lines);
    }
}
