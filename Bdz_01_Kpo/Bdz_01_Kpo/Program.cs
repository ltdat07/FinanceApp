using Bdz_01_Kpo.Commands;
using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Decorators;
using Bdz_01_Kpo.Exporters;
using Bdz_01_Kpo.Importers;
using Bdz_01_Kpo.Models;
using Bdz_01_Kpo.Repositories;
using Bdz_01_Kpo.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Bdz_01_Kpo
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var serviceProvider = ConfigureServices();

            var finance = serviceProvider.GetRequiredService<FinanceFacade>();
            var repository = serviceProvider.GetRequiredService<IRepository>();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить счет");
                Console.WriteLine("2. Редактировать счет");
                Console.WriteLine("3. Удалить счет");
                Console.WriteLine("4. Показать все счета");
                Console.WriteLine("5. Добавить категорию");
                Console.WriteLine("6. Редактировать категорию");
                Console.WriteLine("7. Удалить категорию");
                Console.WriteLine("8. Показать все категории");
                Console.WriteLine("9. Добавить операцию");
                Console.WriteLine("10. Редактировать операцию");
                Console.WriteLine("11. Удалить операцию");
                Console.WriteLine("12. Показать все операции");
                Console.WriteLine("13. Аналитика: Разница доходов и расходов");
                Console.WriteLine("14. Аналитика: Группировка по категориям");
                Console.WriteLine("15. Аналитика: Операции за период");
                Console.WriteLine("16. Экспорт данных");
                Console.WriteLine("17. Импорт данных");
                Console.WriteLine("18. Пересчитать баланс");
                Console.WriteLine("19. Сохранить данные в БД (через прокси)");
                Console.WriteLine("20. Загрузить данные из БД (через прокси)");
                Console.WriteLine("21. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine()!;

                try
                {
                    ICommand command = choice switch
                    {
                        "1" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Название счета: ");
                            string accName = Console.ReadLine()!;
                            Console.Write("Начальный баланс: ");
                            decimal accBalance = decimal.Parse(Console.ReadLine()!);
                            var acc = finance.BankAccounts.CreateBankAccount(accName, accBalance);
                            Console.WriteLine("Создан " + acc);
                        })),
                        "2" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID счета для редактирования: ");
                            int editAccId = int.Parse(Console.ReadLine()!);
                            Console.Write("Новое название: ");
                            string newAccName = Console.ReadLine()!;
                            finance.BankAccounts.EditBankAccount(editAccId, newAccName);
                            Console.WriteLine("Счет обновлен.");
                        })),
                        "3" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID счета для удаления: ");
                            int delAccId = int.Parse(Console.ReadLine()!);
                            finance.BankAccounts.DeleteBankAccount(delAccId);
                            Console.WriteLine("Счет удален.");
                        })),
                        "4" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.WriteLine("Все счета:");
                            foreach (var account in finance.BankAccounts.GetAllBankAccounts())
                                Console.WriteLine(account);
                        })),
                        "5" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Название категории: ");
                            string catName = Console.ReadLine()!;
                            Console.Write("Тип (доход/расход): ");
                            string catType = Console.ReadLine()!;
                            var cat = finance.Categories.CreateCategory(catName, catType);
                            Console.WriteLine("Создана " + cat);
                        })),
                        "6" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID категории для редактирования: ");
                            int editCatId = int.Parse(Console.ReadLine()!);
                            Console.Write("Новое название: ");
                            string newCatName = Console.ReadLine()!;
                            finance.Categories.EditCategory(editCatId, newCatName);
                            Console.WriteLine("Категория обновлена.");
                        })),
                        "7" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID категории для удаления: ");
                            int delCatId = int.Parse(Console.ReadLine()!);
                            finance.Categories.DeleteCategory(delCatId);
                            Console.WriteLine("Категория удалена.");
                        })),
                        "8" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.WriteLine("Все категории:");
                            foreach (var category in finance.Categories.GetAllCategories())
                                Console.WriteLine(category);
                        })),
                        "9" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID счета: ");
                            int opAccId = int.Parse(Console.ReadLine()!);
                            Console.Write("ID категории: ");
                            int opCatId = int.Parse(Console.ReadLine()!);
                            Console.Write("Тип операции (доход/расход): ");
                            string opType = Console.ReadLine()!;
                            Console.Write("Сумма: ");
                            decimal opAmount = decimal.Parse(Console.ReadLine()!);
                            Console.Write("Описание (опционально): ");
                            string? opDesc = Console.ReadLine();
                            var op = finance.Operations.CreateOperation(opType, opAccId, opAmount, opCatId, opDesc);
                            Console.WriteLine("Создана " + op);
                        })),
                        "10" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID операции для редактирования: ");
                            int editOpId = int.Parse(Console.ReadLine()!);
                            Console.Write("Новый тип (доход/расход): ");
                            string newOpType = Console.ReadLine()!;
                            Console.Write("Новая сумма: ");
                            decimal newOpAmount = decimal.Parse(Console.ReadLine()!);
                            Console.Write("Новое описание: ");
                            string? newOpDesc = Console.ReadLine();
                            finance.Operations.EditOperation(editOpId, newOpType, newOpAmount, newOpDesc);
                            Console.WriteLine("Операция обновлена.");
                        })),
                        "11" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("ID операции для удаления: ");
                            int delOpId = int.Parse(Console.ReadLine()!);
                            finance.Operations.DeleteOperation(delOpId);
                            Console.WriteLine("Операция удалена.");
                        })),
                        "12" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.WriteLine("Все операции:");
                            foreach (var operation in finance.Operations.GetAllOperations())
                                Console.WriteLine(operation);
                        })),
                        "13" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Введите начальную дату (ГГГГ-ММ-ДД): ");
                            DateTime startDiff = DateTime.Parse(Console.ReadLine()!);
                            Console.Write("Введите конечную дату (ГГГГ-ММ-ДД): ");
                            DateTime endDiff = DateTime.Parse(Console.ReadLine()!);
                            decimal netDiff = finance.Analytics.GetNetDifference(startDiff, endDiff);
                            Console.WriteLine($"Чистая разница (доход - расход): {netDiff}₽");
                        })),
                        "14" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Введите начальную дату (ГГГГ-ММ-ДД): ");
                            DateTime startGroup = DateTime.Parse(Console.ReadLine()!);
                            Console.Write("Введите конечную дату (ГГГГ-ММ-ДД): ");
                            DateTime endGroup = DateTime.Parse(Console.ReadLine()!);
                            var groups = finance.Analytics.GroupByCategory(startGroup, endGroup);
                            Console.WriteLine("Группировка по категориям (ID: [Доход, Расход]):");
                            foreach (var kvp in groups)
                                Console.WriteLine($"{kvp.Key}: [ {kvp.Value.Income}₽, {kvp.Value.Expense}₽ ]");
                        })),
                        "15" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Введите начальную дату (ГГГГ-ММ-ДД): ");
                            DateTime startOps = DateTime.Parse(Console.ReadLine()!);
                            Console.Write("Введите конечную дату (ГГГГ-ММ-ДД): ");
                            DateTime endOps = DateTime.Parse(Console.ReadLine()!);
                            var ops = finance.Analytics.GetOperationsByPeriod(startOps, endOps);
                            Console.WriteLine("Операции за выбранный период:");
                            foreach (var operation in ops)
                                Console.WriteLine(operation);
                        })),
                        "16" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            var exportItems = new List<IVisitable>();
                            exportItems.AddRange(finance.BankAccounts.GetAllBankAccounts());
                            exportItems.AddRange(finance.Categories.GetAllCategories());
                            exportItems.AddRange(finance.Operations.GetAllOperations());

                            Console.WriteLine("Выберите формат экспорта: 1 - CSV, 2 - JSON, 3 - YAML");
                            string format = Console.ReadLine()!;
                            IExportVisitor visitor = format switch
                            {
                                "1" => new CsvExportVisitor(),
                                "2" => new JsonExportVisitor(),
                                "3" => new YamlExportVisitor(),
                                _ => new CsvExportVisitor()
                            };
                            Console.Write("Введите путь для сохранения файла: ");
                            string filePath = Console.ReadLine()!;
                            var exporter = new DataExporter();
                            exporter.ExportData(exportItems, visitor, filePath);
                        })),
                        "17" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.WriteLine("Выберите формат импорта: 1 - CSV, 2 - JSON, 3 - YAML");
                            string format = Console.ReadLine()!;
                            DataImporter importer = format switch
                            {
                                "1" => new CsvImporter(),
                                "2" => new JsonImporter(),
                                "3" => new YamlImporter(),
                                _ => new JsonImporter()
                            };
                            Console.Write("Введите путь к файлу для импорта: ");
                            string importFilePath = Console.ReadLine()!;
                            if (File.Exists(importFilePath))
                            {
                                ImportData importedData = importer.Import(importFilePath);
                                // Если нет импортированных счетов, создаём дефолтный счет.
                                if (importedData.BankAccounts.Count == 0)
                                {
                                    Console.WriteLine("Счета не импортированы. Создаём дефолтный счет.");
                                    importedData.BankAccounts.Add(new BankAccount(0, "Default Account", 0));
                                }
                                // Если нет импортированных категорий, создаём дефолтную категорию.
                                if (importedData.Categories.Count == 0)
                                {
                                    Console.WriteLine("Категории не импортированы. Создаём дефолтную категорию.");
                                    importedData.Categories.Add(new Category(0, "Default Category", "доход"));
                                }
                                foreach (var acc in importedData.BankAccounts)
                                    finance.BankAccounts.CreateBankAccount(acc.Name, acc.Balance);
                                foreach (var cat in importedData.Categories)
                                    finance.Categories.CreateCategory(cat.Name, cat.Type);
                                foreach (var op in importedData.Operations)
                                    finance.Operations.CreateOperation(op.Type, op.BankAccountId, op.Amount, op.CategoryId, op.Description);
                                Console.WriteLine("Импорт завершен и данные интегрированы.");
                            }
                            else
                            {
                                Console.WriteLine($"Файл {importFilePath} не найден.");
                            }
                        })),
                        "18" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.Write("Введите ID счета для пересчета баланса: ");
                            int accountId = int.Parse(Console.ReadLine()!);
                            finance.RecalculateBalance(accountId);
                        })),
                        "19" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            finance.SaveToRepository(repository);
                        })),
                        "20" => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            finance.LoadFromRepository(repository);
                        })),
                        "21" => new LambdaCommand(() => exit = true),
                        _ => new TimeMeasurementDecorator(new LambdaCommand(() =>
                        {
                            Console.WriteLine("Неверный выбор, попробуйте снова.");
                        }))
                    };
                    command.Execute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
            }
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        /// <summary>
        /// Настраивает DI-контейнер и регистрирует все необходимые зависимости.
        /// </summary>
        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<DatabaseRepository>(sp => new DatabaseRepository("db.json"));
            services.AddSingleton<IRepository, RepositoryProxy>();


            services.AddSingleton<FinanceFacade>();
            services.AddSingleton<BankAccountFacade>();
            services.AddSingleton<CategoryFacade>();
            services.AddSingleton<OperationFacade>();
            services.AddSingleton<AnalyticsFacade>();

            services.AddSingleton<DataImporter, CsvImporter>();  
            services.AddSingleton<CsvImporter>();
            services.AddSingleton<JsonImporter>();
            services.AddSingleton<YamlImporter>();

            services.AddSingleton<DataExporter>();
            services.AddSingleton<CsvExportVisitor>();
            services.AddSingleton<JsonExportVisitor>();
            services.AddSingleton<YamlExportVisitor>();

  
            services.AddSingleton<ICommand, LambdaCommand>();
            services.AddSingleton<TimeMeasurementDecorator>();

            return services.BuildServiceProvider();
        }
    }
}
