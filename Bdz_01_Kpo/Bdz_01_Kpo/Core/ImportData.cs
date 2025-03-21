using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Core
{
    public class ImportData
    {
        public List<BankAccount> BankAccounts { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Operation> Operations { get; set; } = new();
    }
}
