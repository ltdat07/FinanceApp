using Bdz_01_Kpo.Factories;
using Bdz_01_Kpo.Models;
using Bdz_01_Kpo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Services
{
    // Центральный фасад объединяет все фасады и предоставляет дополнительные методы.
    public class FinanceFacade
    {
        public BankAccountFacade BankAccounts { get; }
        public CategoryFacade Categories { get; }
        public OperationFacade Operations { get; }
        public AnalyticsFacade Analytics { get; }
        private readonly DomainFactory factory;

        public FinanceFacade()
        {
            factory = new DomainFactory();
            BankAccounts = new BankAccountFacade(factory);
            Categories = new CategoryFacade(factory);
            Operations = new OperationFacade(BankAccounts, Categories, factory);
            Analytics = new AnalyticsFacade(Operations);
        }

        // Пересчет баланса по операциям для указанного счета.
        public void RecalculateBalance(int bankAccountId)
        {
            var account = BankAccounts.GetBankAccount(bankAccountId);
            if (account == null)
                throw new InvalidOperationException("Счет не найден.");

            decimal newBalance = 0;
            var ops = Operations.GetAllOperations().Where(o => o.BankAccountId == bankAccountId);
            foreach (var op in ops)
            {
                if (op.Type == "доход")
                    newBalance += op.Amount;
                else if (op.Type == "расход")
                    newBalance -= op.Amount;
            }
            account.Balance = newBalance;
            Console.WriteLine($"Новый баланс счета {account.Id} ({account.Name}) установлен равным {account.Balance}₽");
        }

        // Методы для работы с репозиторием (с использованием прокси)
        public void LoadFromRepository(IRepository repository)
        {
            FinanceData data = repository.LoadData();
            // Если в базе есть счета, категории и операции, интегрируем их:
            foreach (var acc in data.BankAccounts)
                BankAccounts.CreateBankAccount(acc.Name, acc.Balance);
            foreach (var cat in data.Categories)
                Categories.CreateCategory(cat.Name, cat.Type);
            foreach (var op in data.Operations)
                Operations.CreateOperation(op.Type, op.BankAccountId, op.Amount, op.CategoryId, op.Description);
            Console.WriteLine("Данные загружены из базы (через прокси).");
        }

        public void SaveToRepository(IRepository repository)
        {
            FinanceData data = new FinanceData
            {
                BankAccounts = BankAccounts.GetAllBankAccounts(),
                Categories = Categories.GetAllCategories(),
                Operations = Operations.GetAllOperations()
            };
            repository.SaveData(data);
            Console.WriteLine("Данные сохранены в базу (через прокси).");
        }
    }
}
