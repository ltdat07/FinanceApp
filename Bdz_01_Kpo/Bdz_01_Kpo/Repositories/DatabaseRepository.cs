using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Repositories
{
    // "База данных" реализована через файл (JSON).
    public class DatabaseRepository : IRepository
    {
        private readonly string filePath;
        public DatabaseRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public FinanceData LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<FinanceData>(json) ?? new FinanceData();
            }
            return new FinanceData();
        }

        public void SaveData(FinanceData data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
