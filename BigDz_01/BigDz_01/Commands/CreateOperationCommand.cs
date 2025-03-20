using System;
using BigDz_01.Domain;
using BigDz_01.Facades;

namespace BigDz_01.Commands
{
    public class CreateOperationCommand : ICommand
    {
        private readonly FinanceFacade _facade;
        private readonly CategoryType _type;
        private readonly int _accountId;
        private readonly decimal _amount;
        private readonly DateTime _date;
        private readonly string _description;
        private readonly int _categoryId;

        public CreateOperationCommand(
            FinanceFacade facade,
            CategoryType type,
            int accountId,
            decimal amount,
            DateTime date,
            string description,
            int categoryId)
        {
            _facade = facade;
            _type = type;
            _accountId = accountId;
            _amount = amount;
            _date = date;
            _description = description;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            var op = _facade.CreateOperation(_type, _accountId, _amount, _date, _description, _categoryId);
            Console.WriteLine("Operation created: " + op);
        }
    }
}
