using System.Collections.Generic;
using BigDz_01.Domain;

namespace BigDz_01.Repositories
{
    public interface IFinanceRepository
    {
        void AddAccount(BankAccount account);
        void AddCategory(Category category);
        void AddOperation(Operation op);

        BankAccount GetAccountById(int id);

        List<BankAccount> GetAccounts();
        List<Category> GetCategories();
        List<Operation> GetOperations();
    }
}
