using BigDz_01.Domain;
using BigDz_01.Export;
using System;

namespace BigDz_01.Domain
{
    public class Operation : IExportable
    {
        public int Id { get; }
        public CategoryType Type { get; }
        public int BankAccountId { get; }
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public int CategoryId { get; }

        public Operation(
            int id,
            CategoryType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            string description,
            int categoryId)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            Description = description;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"[{Id}] {Type} {Amount} rub. from {Date.ToShortDateString()} (Account: {BankAccountId}, Category: {CategoryId})";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
