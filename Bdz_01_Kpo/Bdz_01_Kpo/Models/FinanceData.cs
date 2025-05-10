using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Models
{
    // Класс, объединяющий данные для персистентности.
    public class FinanceData
    {
        public List<BankAccount> BankAccounts { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Operation> Operations { get; set; } = new();
    }
}
