using BigDz_01.Export;

namespace BigDz_01.Domain
{
    public interface IExportable
    {
        void Accept(IExportVisitor visitor);
    }
}
