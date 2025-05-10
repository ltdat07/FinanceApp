using Bdz_01_Kpo.Core;
using Bdz_01_Kpo.Exporters;
namespace Bdz_01_Kpo.Models
{
    public class Category : IVisitable
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Type { get; }

        public Category(int id, string name, string type)
        {
            // Валидация выполнена в фабрике.
            Id = id;
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"[Категория {Id}] {Name} ({Type})";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
