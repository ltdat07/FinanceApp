using Bdz_01_Kpo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Exporters
{
    public class DataExporter
    {
        public void ExportData(IEnumerable<IVisitable> data, IExportVisitor visitor, string filePath)
        {
            foreach (var item in data)
                item.Accept(visitor);
            string result = visitor.GetResult();
            File.WriteAllText(filePath, result);
            Console.WriteLine($"Данные экспортированы в {filePath}");
        }
    }
}
