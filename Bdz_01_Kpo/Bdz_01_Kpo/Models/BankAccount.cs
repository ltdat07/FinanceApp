using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Exporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Models
{
    public class BankAccount : IVisitable
    {
        public int Id { get; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(int id, string name, decimal balance = 0)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        public void UpdateBalance(decimal amount)
        {
            Balance += amount;
        }

        public override string ToString()
        {
            return $"[Счет {Id}] {Name}, Баланс: {Balance}₽";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
