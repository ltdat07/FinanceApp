using System.Collections.Generic;
using System.Linq;
using BigDz_01.Domain;
using BigDz_01.Repositories;

namespace BigDz_01.Repositories
{
    // Proxy repository with in-memory cache.
    public class FinanceRepositoryProxy : IFinanceRepository
    {
        private readonly FinanceRepository _repository = new FinanceRepository();
        private readonly List<BankAccount> _accountsCache;
        private readonly List<Category> _categoriesCache;
        private readonly List<Operation> _operationsCache;

        public FinanceRepositoryProxy()
        {
            // When initializing, we cache the data.
            _accountsCache = new List<BankAccount>(_repository.GetAccounts());
            _categoriesCache = new List<Category>(_repository.GetCategories());
            _operationsCache = new List<Operation>(_repository.GetOperations());
        }

        public void AddAccount(BankAccount account)
        {
            _repository.AddAccount(account);
            _accountsCache.Add(account);
        }

        public void AddCategory(Category category)
        {
            _repository.AddCategory(category);
            _categoriesCache.Add(category);
        }

        public void AddOperation(Operation op)
        {
            _repository.AddOperation(op);
            _operationsCache.Add(op);
        }

        public BankAccount GetAccountById(int id) => _accountsCache.FirstOrDefault(a => a.Id == id);

        public List<BankAccount> GetAccounts() => _accountsCache;
        public List<Category> GetCategories() => _categoriesCache;
        public List<Operation> GetOperations() => _operationsCache;
    }
}
