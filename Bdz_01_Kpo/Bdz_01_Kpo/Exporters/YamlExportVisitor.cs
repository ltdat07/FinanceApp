using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Exporters
{
    public class YamlExportVisitor : IExportVisitor
    {
        private readonly List<string> lines = new List<string>();
        public void Visit(BankAccount account)
        {
            lines.Add("- BankAccount:");
            lines.Add($"    id: {account.Id}");
            lines.Add($"    name: {account.Name}");
            lines.Add($"    balance: {account.Balance}");
        }
        public void Visit(Category category)
        {
            lines.Add("- Category:");
            lines.Add($"    id: {category.Id}");
            lines.Add($"    name: {category.Name}");
            lines.Add($"    type: {category.Type}");
        }
        public void Visit(Operation operation)
        {
            lines.Add("- Operation:");
            lines.Add($"    id: {operation.Id}");
            lines.Add($"    type: {operation.Type}");
            lines.Add($"    bankAccountId: {operation.BankAccountId}");
            lines.Add($"    amount: {operation.Amount}");
            lines.Add($"    date: {operation.Date}");
            lines.Add($"    description: {operation.Description}");
            lines.Add($"    categoryId: {operation.CategoryId}");
        }
        public string GetResult() => string.Join(Environment.NewLine, lines);
    }
}
