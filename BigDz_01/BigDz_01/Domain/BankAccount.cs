using BigDz_01.Export;
using System;

namespace BigDz_01.Domain
{
    public class BankAccount : IExportable
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Balance { get; private set; }

        public BankAccount(int id, string name)
        {
            Id = id;
            Name = name;
            Balance = 0;
        }

        public void UpdateBalance(decimal amount, CategoryType type)
        {
            Balance += type == CategoryType.Income ? amount : -amount;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Balance: {Balance}";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
