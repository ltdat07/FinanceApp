using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Repositories
{
    public interface IRepository
    {
        FinanceData LoadData();
        void SaveData(FinanceData data);
    }
}
