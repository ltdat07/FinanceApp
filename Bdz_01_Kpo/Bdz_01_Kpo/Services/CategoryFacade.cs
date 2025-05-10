using Bdz_01_Kpo.Factories;
using Bdz_01_Kpo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bdz_01_Kpo.Services
{
    public class CategoryFacade
    {
        private readonly List<Category> categories = new();
        private readonly DomainFactory factory;

        public CategoryFacade(DomainFactory factory)
        {
            this.factory = factory;
        }

        public Category CreateCategory(string name, string type)
        {
            var category = factory.CreateCategory(name, type);
            categories.Add(category);
            return category;
        }

        public void EditCategory(int id, string newName)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new InvalidOperationException("Категория не найдена.");
            category.Name = newName;
        }

        public void DeleteCategory(int id)
        {
            categories.RemoveAll(c => c.Id == id);
        }

        public Category? GetCategory(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public List<Category> GetAllCategories()
        {
            return new List<Category>(categories);
        }
    }
}
