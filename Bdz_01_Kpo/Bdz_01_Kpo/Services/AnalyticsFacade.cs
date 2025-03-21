using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Services
{
    public class AnalyticsFacade
    {
        private readonly OperationFacade operationFacade;

        public AnalyticsFacade(OperationFacade opFacade)
        {
            operationFacade = opFacade;
        }

        public decimal GetNetDifference(DateTime start, DateTime end)
        {
            var ops = operationFacade.GetAllOperations().Where(o => o.Date >= start && o.Date <= end);
            decimal income = ops.Where(o => o.Type == "доход").Sum(o => o.Amount);
            decimal expense = ops.Where(o => o.Type == "расход").Sum(o => o.Amount);
            return income - expense;
        }

        public Dictionary<int, (decimal Income, decimal Expense)> GroupByCategory(DateTime start, DateTime end)
        {
            var groups = new Dictionary<int, (decimal Income, decimal Expense)>();
            var ops = operationFacade.GetAllOperations().Where(o => o.Date >= start && o.Date <= end);
            foreach (var op in ops)
            {
                if (!groups.ContainsKey(op.CategoryId))
                    groups[op.CategoryId] = (0, 0);
                var (inc, exp) = groups[op.CategoryId];
                if (op.Type == "доход")
                    inc += op.Amount;
                else
                    exp += op.Amount;
                groups[op.CategoryId] = (inc, exp);
            }
            return groups;
        }

        public List<Operation> GetOperationsByPeriod(DateTime start, DateTime end)
        {
            return operationFacade.GetAllOperations().Where(o => o.Date >= start && o.Date <= end).ToList();
        }
    }
}
