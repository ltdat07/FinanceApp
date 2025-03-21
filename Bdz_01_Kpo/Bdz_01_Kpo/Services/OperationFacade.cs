using Bdz_01_Kpo.Factories;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Services
{
    public class OperationFacade
    {
        private readonly List<Operation> operations = new();
        private readonly DomainFactory factory;
        private readonly BankAccountFacade bankAccountFacade;
        private readonly CategoryFacade categoryFacade;

        public OperationFacade(BankAccountFacade accountFacade, CategoryFacade catFacade, DomainFactory factory)
        {
            bankAccountFacade = accountFacade;
            categoryFacade = catFacade;
            this.factory = factory;
        }

        public Operation CreateOperation(string type, int bankAccountId, decimal amount, int categoryId, string? description = null)
        {
            // Проверяем наличие счета и категории.
            var account = bankAccountFacade.GetBankAccount(bankAccountId);
            var category = categoryFacade.GetCategory(categoryId);
            if (account == null || category == null)
                throw new InvalidOperationException("Счет или категория не найдены.");

            var operation = factory.CreateOperation(type, bankAccountId, amount, categoryId, description);
            operations.Add(operation);

            if (type == "доход")
                account.UpdateBalance(amount);
            else if (type == "расход")
                account.UpdateBalance(-amount);
            else
                throw new ArgumentException("Неверный тип операции.");

            return operation;
        }

        public void EditOperation(int id, string newType, decimal newAmount, string? newDescription)
        {
            var operation = operations.FirstOrDefault(o => o.Id == id);
            if (operation == null)
                throw new InvalidOperationException("Операция не найдена.");

            var account = bankAccountFacade.GetBankAccount(operation.BankAccountId);
            if (account == null)
                throw new InvalidOperationException("Связанный счет не найден.");

            // Отмена эффекта старой операции.
            if (operation.Type == "доход")
                account.UpdateBalance(-operation.Amount);
            else if (operation.Type == "расход")
                account.UpdateBalance(operation.Amount);

            if (newType != "доход" && newType != "расход")
                throw new ArgumentException("Тип операции должен быть 'доход' или 'расход'.");
            if (newAmount <= 0)
                throw new ArgumentException("Сумма операции должна быть положительной.");

            operation.Type = newType;
            operation.Amount = newAmount;
            operation.Description = newDescription;

            // Применение эффекта новой операции.
            if (newType == "доход")
                account.UpdateBalance(newAmount);
            else
                account.UpdateBalance(-newAmount);
        }

        public void DeleteOperation(int id)
        {
            var operation = operations.FirstOrDefault(o => o.Id == id);
            if (operation == null)
                throw new InvalidOperationException("Операция не найдена.");

            var account = bankAccountFacade.GetBankAccount(operation.BankAccountId);
            if (account == null)
                throw new InvalidOperationException("Связанный счет не найден.");

            if (operation.Type == "доход")
                account.UpdateBalance(-operation.Amount);
            else
                account.UpdateBalance(operation.Amount);

            operations.Remove(operation);
        }

        public List<Operation> GetAllOperations()
        {
            return new List<Operation>(operations);
        }
    }
}
