using System.Collections.Generic;
using System.IO;
using BigDz_01.Domain;

namespace BigDz_01.Export
{
    public class CsvExportVisitor : IExportVisitor
    {
        public List<string> Lines { get; } = new List<string>();

        public void Visit(BankAccount account)
        {
            Lines.Add($"Account,{account.Id},{account.Name},{account.Balance}");
        }

        public void Visit(Category category)
        {
            Lines.Add($"Category,{category.Id},{category.Name},{category.Type}");
        }

        public void Visit(Operation operation)
        {
            Lines.Add($"Operation,{operation.Id},{operation.BankAccountId},{operation.Amount},{operation.Date.ToShortDateString()},{operation.Description},{operation.CategoryId}");
        }

        public void SaveToFile(string filePath)
        {
            File.WriteAllLines(filePath, Lines);
            Console.WriteLine("CSV export complete. File: " + filePath);
        }
    }
}
