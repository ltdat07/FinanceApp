using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Repositories
{
    // Прокси, использующий in-memory кэш.
    public class RepositoryProxy : IRepository
    {
        private readonly DatabaseRepository dbRepository;
        private FinanceData? cache;

        public RepositoryProxy(DatabaseRepository repository)
        {
            dbRepository = repository;
            // При инициализации загружаем данные в кэш.
            cache = dbRepository.LoadData();
        }

        public FinanceData LoadData()
        {
            return cache ?? new FinanceData();
        }

        public void SaveData(FinanceData data)
        {
            // Обновляем кэш и сохраняем в базу.
            cache = data;
            dbRepository.SaveData(data);
        }
    }

}
