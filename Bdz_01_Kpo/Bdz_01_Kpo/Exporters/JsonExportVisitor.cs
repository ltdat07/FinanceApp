using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Exporters
{
    public class JsonExportVisitor : IExportVisitor
    {
        private readonly List<string> items = new List<string>();
        public void Visit(BankAccount account) => items.Add($"{{\"type\":\"BankAccount\",\"id\":{account.Id},\"name\":\"{account.Name}\",\"balance\":{account.Balance}}}");
        public void Visit(Category category) => items.Add($"{{\"type\":\"Category\",\"id\":{category.Id},\"name\":\"{category.Name}\",\"categoryType\":\"{category.Type}\"}}");
        public void Visit(Operation operation) => items.Add($"{{\"type\":\"Operation\",\"id\":{operation.Id},\"opType\":\"{operation.Type}\",\"bankAccountId\":{operation.BankAccountId},\"amount\":{operation.Amount},\"date\":\"{operation.Date:O}\",\"description\":\"{operation.Description}\",\"categoryId\":{operation.CategoryId}}}");
        public string GetResult() => "[\n" + string.Join(",\n", items) + "\n]";
    }
}
