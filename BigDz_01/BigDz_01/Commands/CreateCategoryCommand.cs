using BigDz_01.Domain;
using BigDz_01.Facades;

namespace BigDz_01.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly FinanceFacade _facade;
        private readonly CategoryType _type;
        private readonly string _name;

        public CreateCategoryCommand(FinanceFacade facade, CategoryType type, string name)
        {
            _facade = facade;
            _type = type;
            _name = name;
        }

        public void Execute()
        {
            var category = _facade.CreateCategory(_type, _name);
            Console.WriteLine("Category created: " + category);
        }
    }
}
