using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Factories
{
    // Фабрика для создания доменных объектов с валидацией и автоматическим присвоением ID.
    public class DomainFactory
    {
        private int nextBankAccountId = 1;
        private int nextCategoryId = 1;
        private int nextOperationId = 1;

        public BankAccount CreateBankAccount(string name, decimal balance = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя счета не может быть пустым.");
            var account = new BankAccount(nextBankAccountId++, name, balance);
            return account;
        }

        public Category CreateCategory(string name, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя категории не может быть пустым.");
            if (type != "доход" && type != "расход")
                throw new ArgumentException("Тип категории должен быть 'доход' или 'расход'.");
            var category = new Category(nextCategoryId++, name, type);
            return category;
        }

        public Operation CreateOperation(string type, int bankAccountId, decimal amount, int categoryId, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма операции должна быть положительной.");
            if (type != "доход" && type != "расход")
                throw new ArgumentException("Тип операции должен быть 'доход' или 'расход'.");
            var operation = new Operation(nextOperationId++, type, bankAccountId, amount, categoryId, description);
            return operation;
        }
    }
}
