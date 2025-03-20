using BigDz_01.Domain;
using System;

namespace BigDz_01.Domain
{
    public static class DomainFactory
    {
        private static int _bankAccountIdCounter = 1;
        private static int _categoryIdCounter = 1;
        private static int _operationIdCounter = 1;

        public static BankAccount CreateBankAccount(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Account name cannot be empty");

            return new BankAccount(_bankAccountIdCounter++, name);
        }

        public static Category CreateCategory(CategoryType type, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty");

            return new Category(_categoryIdCounter++, type, name);
        }

        public static Operation CreateOperation(
            CategoryType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            string description,
            int categoryId)
        {
            if (amount <= 0)
                throw new ArgumentException("The transaction amount must be positive.");

            return new Operation(
                _operationIdCounter++,
                type,
                bankAccountId,
                amount,
                date,
                description,
                categoryId
            );
        }
    }
}
