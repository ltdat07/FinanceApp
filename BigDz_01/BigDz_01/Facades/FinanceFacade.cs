using System;
using System.Linq;
using System.Collections.Generic;
using BigDz_01.Domain;
using BigDz_01.Repositories;

namespace BigDz_01.Facades
{
    public class FinanceFacade
    {
        private readonly IFinanceRepository _repository;

        public FinanceFacade(IFinanceRepository repository)
        {
            _repository = repository;
        }

        public BankAccount CreateAccount(string name)
        {
            var account = DomainFactory.CreateBankAccount(name);
            _repository.AddAccount(account);
            return account;
        }

        public Category CreateCategory(CategoryType type, string name)
        {
            var category = DomainFactory.CreateCategory(type, name);
            _repository.AddCategory(category);
            return category;
        }

        public Operation CreateOperation(CategoryType type, int accountId, decimal amount, DateTime date, string description, int categoryId)
        {
            var op = DomainFactory.CreateOperation(type, accountId, amount, date, description, categoryId);
            _repository.AddOperation(op);

            var account = _repository.GetAccountById(accountId);
            account?.UpdateBalance(amount, type);

            return op;
        }

        // Analytics: the difference between income and expenses for the period.
        public decimal GetNetAmount(DateTime start, DateTime end)
        {
            var ops = _repository.GetOperations().Where(o => o.Date >= start && o.Date <= end);
            decimal net = 0;
            foreach (var op in ops)
            {
                net += op.Type == CategoryType.Income ? op.Amount : -op.Amount;
            }
            return net;
        }

        // Analytics: grouping operations by categories.
        public Dictionary<int, decimal> GroupOperationsByCategory()
        {
            return _repository.GetOperations()
                .GroupBy(op => op.CategoryId)
                .ToDictionary(g => g.Key,
                              g => g.Sum(op => op.Type == CategoryType.Income ? op.Amount : -op.Amount));
        }

        // Methods for outputting lists.
        public void ListAccounts()
        {
            var accounts = _repository.GetAccounts();
            accounts.ForEach(a => Console.WriteLine(a));
        }

        public void ListCategories()
        {
            var categories = _repository.GetCategories();
            categories.ForEach(c => Console.WriteLine(c));
        }

        public void ListOperations()
        {
            var operations = _repository.GetOperations();
            operations.ForEach(o => Console.WriteLine(o));
        }
    }
}
