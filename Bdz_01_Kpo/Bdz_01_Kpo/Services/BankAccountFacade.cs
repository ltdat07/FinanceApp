using Bdz_01_Kpo.Factories;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Services
{
    public class BankAccountFacade
    {
        private readonly List<BankAccount> bankAccounts = new();
        private readonly DomainFactory factory;

        public BankAccountFacade(DomainFactory factory)
        {
            this.factory = factory;
        }

        public BankAccount CreateBankAccount(string name, decimal balance = 0)
        {
            var account = factory.CreateBankAccount(name, balance);
            bankAccounts.Add(account);
            return account;
        }

        public void EditBankAccount(int id, string newName)
        {
            var account = bankAccounts.FirstOrDefault(a => a.Id == id);
            if (account == null)
                throw new InvalidOperationException("Счет не найден.");
            account.Name = newName;
        }

        public void DeleteBankAccount(int id)
        {
            bankAccounts.RemoveAll(a => a.Id == id);
        }

        public BankAccount? GetBankAccount(int id)
        {
            return bankAccounts.FirstOrDefault(a => a.Id == id);
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            return new List<BankAccount>(bankAccounts);
        }
    }
}
