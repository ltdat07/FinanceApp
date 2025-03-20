using BigDz_01.Domain;


namespace BigDz_01.Export
{
    public interface IExportVisitor
    {
        void Visit(BankAccount account);
        void Visit(Category category);
        void Visit(Operation operation);
    }
}
