using BigDz_01.Export;

namespace BigDz_01.Domain
{
    public class Category : IExportable
    {
        public int Id { get; }
        public CategoryType Type { get; }
        public string Name { get; }

        public Category(int id, CategoryType type, string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - Type: {Type}";
        }

        public void Accept(IExportVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
