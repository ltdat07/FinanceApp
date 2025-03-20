using System.Collections.Generic;
using System.Linq;
using BigDz_01.Domain;
using BigDz_01.Repositories;

namespace BigDz_01.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly List<BankAccount> _accounts = new List<BankAccount>();
        private readonly List<Category> _categories = new List<Category>();
        private readonly List<Operation> _operations = new List<Operation>();

        public void AddAccount(BankAccount account) => _accounts.Add(account);
        public void AddCategory(Category category) => _categories.Add(category);
        public void AddOperation(Operation op) => _operations.Add(op);

        public BankAccount GetAccountById(int id) => _accounts.FirstOrDefault(a => a.Id == id);

        public List<BankAccount> GetAccounts() => _accounts;
        public List<Category> GetCategories() => _categories;
        public List<Operation> GetOperations() => _operations;
    }
}
