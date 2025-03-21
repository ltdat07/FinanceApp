using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Exporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Models
{
    public class Operation : IVisitable
    {
        public int Id { get; }
        public string Type { get; set; }
        public int BankAccountId { get; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; }

        public Operation(int id, string type, int bankAccountId, decimal amount, int categoryId, string? description = null)
        {
            // Валидация выполнена в фабрике.
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = DateTime.Now;
            CategoryId = categoryId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[Операция {Id}] {Type}: {Amount}₽, Категория {CategoryId}, Счет {BankAccountId}, Дата: {Date.ToShortDateString()}, Описание: {Description}";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
