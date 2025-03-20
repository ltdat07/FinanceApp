using System;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using BigDz_01.Commands;
using BigDz_01.Domain;
using BigDz_01.Facades;
using BigDz_01.ImportExport;
using BigDz_01.Repositories;
using BigDz_01.Decorators;
using ICommand = BigDz_01.Commands.ICommand;

namespace BigDz_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use proxy repository for caching.
            IFinanceRepository repository = new FinanceRepositoryProxy();
            FinanceFacade facade = new FinanceFacade(repository);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create an account");
                Console.WriteLine("2. Create category");
                Console.WriteLine("3. Create an operation");
                Console.WriteLine("4. Show accounts");
                Console.WriteLine("5. Show categories");
                Console.WriteLine("6. Show operations");
                Console.WriteLine("7. Analytics: difference between income and expenses for the period");
                Console.WriteLine("8. Analytics: grouping transactions by categories");
                Console.WriteLine("9. Import data");
                Console.WriteLine("10. Export data to CSV");
                Console.WriteLine("0. Выход");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter account name: ");
                        string accountName = Console.ReadLine();
                        ICommand createAccCmd = new CommandTimerDecorator(
                            new CreateAccountCommand(facade, accountName)
                        );
                        createAccCmd.Execute();
                        break;

                    case "2":
                        Console.Write("Enter category name: ");
                        string catName = Console.ReadLine();
                        Console.Write("Enter type (0 - Income, 1 - Expense): ");
                        string typeInput = Console.ReadLine();
                        CategoryType catType = (typeInput == "0") ? CategoryType.Income : CategoryType.Expense;

                        ICommand createCatCmd = new CommandTimerDecorator(
                            new CreateCategoryCommand(facade, catType, catName)
                        );
                        createCatCmd.Execute();
                        break;

                    case "3":
                        Console.Write("Enter account ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int accId))
                        {
                            Console.WriteLine("Invalid account ID.");
                            break;
                        }

                        Console.Write("Enter category ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int catId))
                        {
                            Console.WriteLine("Invalid category ID.");
                            break;
                        }

                        Console.Write("Enter the transaction amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Invalid amount.");
                            break;
                        }

                        Console.Write("Enter a description: ");
                        string desc = Console.ReadLine();
                        DateTime date = DateTime.Now;

                        var category = repository.GetCategories().FirstOrDefault(c => c.Id == catId);
                        if (category == null)
                        {
                            Console.WriteLine("Category not found.");
                            break;
                        }

                        ICommand createOpCmd = new CommandTimerDecorator(
                            new CreateOperationCommand(facade, category.Type, accId, amount, date, desc, catId)
                        );
                        createOpCmd.Execute();
                        break;

                    case "4":
                        Console.WriteLine("Accounts:");
                        facade.ListAccounts();
                        break;

                    case "5":
                        Console.WriteLine("Categories:");
                        facade.ListCategories();
                        break;

                    case "6":
                        Console.WriteLine("Operations:");
                        facade.ListOperations();
                        break;

                    case "7":
                        Console.Write("Enter start date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
                        {
                            Console.WriteLine("Invalid date.");
                            break;
                        }

                        Console.Write("Enter end date (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
                        {
                            Console.WriteLine("Invalid date.");
                            break;
                        }

                        decimal net = facade.GetNetAmount(start, end);
                        Console.WriteLine($"Net amount for the period: {net}");
                        break;

                    case "8":
                        var groups = facade.GroupOperationsByCategory();
                        foreach (var g in groups)
                        {
                            Console.WriteLine($"Category {g.Key}: {g.Value}");
                        }
                        break;

                    case "9":
                        Console.Write("Select import format (csv, json, yaml): ");
                        string format = Console.ReadLine().ToLower();
                        DataImporter importer = format switch
                        {
                            "csv" => new CsvImporter(),
                            "json" => new JsonImporter(),
                            "yaml" => new YamlImporter(),
                            _ => null
                        };

                        if (importer == null)
                        {
                            Console.WriteLine("Invalid format.");
                            break;
                        }

                        Console.Write("Enter the path to the file: ");
                        string path = Console.ReadLine();

                        try
                        {
                            importer.Import(path, repository);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Import error: " + ex.Message);
                        }
                        break;

                    case "10":
                        // Export to CSV using the Visitor pattern.
                        var exportVisitor = new Export.CsvExportVisitor();
                        foreach (var a in repository.GetAccounts()) a.Accept(exportVisitor);
                        foreach (var c in repository.GetCategories()) c.Accept(exportVisitor);
                        foreach (var o in repository.GetOperations()) o.Accept(exportVisitor);

                        Console.Write("Enter the path for CSV export: ");
                        string exportPath = Console.ReadLine();
                        exportVisitor.SaveToFile(exportPath);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Wrong choice.");
                        break;
                }

                Thread.Sleep(500);
            }
        }
    }
}
