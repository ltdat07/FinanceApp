using System;
using System.Collections.Generic;
using System.IO;
using BigDz_01.Repositories;
using BigDz_01.Domain;

namespace BigDz_01.ImportExport
{
    // Abstract class for importing data (Template method).
    public abstract class DataImporter
    {
        public void Import(string filePath, IFinanceRepository repository)
        {
            var rawData = ReadFile(filePath);
            var data = ParseData(rawData);
            ValidateData(data);
            SaveData(data, repository);
        }

        protected virtual string ReadFile(string filePath)
        {
            Console.WriteLine("Reading file: " + filePath);
            return File.Exists(filePath) ? File.ReadAllText(filePath) : "";
        }

        protected abstract List<Operation> ParseData(string rawData);

        protected virtual void ValidateData(List<Operation> data)
        {
            foreach (var op in data)
            {
                if (op.Amount <= 0)
                    throw new Exception("Invalid transaction amount");
            }
        }

        protected virtual void SaveData(List<Operation> data, IFinanceRepository repository)
        {
            foreach (var op in data)
            {
                repository.AddOperation(op);
            }
            Console.WriteLine("Data imported successfully.");
        }
    }
}
