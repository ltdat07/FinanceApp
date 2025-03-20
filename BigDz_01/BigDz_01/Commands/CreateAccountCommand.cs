using BigDz_01.Facades;

namespace BigDz_01.Commands
{
    public class CreateAccountCommand : ICommand
    {
        private readonly FinanceFacade _facade;
        private readonly string _name;

        public CreateAccountCommand(FinanceFacade facade, string name)
        {
            _facade = facade;
            _name = name;
        }

        public void Execute()
        {
            var account = _facade.CreateAccount(_name);
            Console.WriteLine("Account created: " + account);
        }
    }
}
