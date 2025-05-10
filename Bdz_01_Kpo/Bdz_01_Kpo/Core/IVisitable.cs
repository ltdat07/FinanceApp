using Bdz_01_Kpo.Exporters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Core
{
    public interface IVisitable
    {
        void Accept(IExportVisitor visitor);
    }
}
