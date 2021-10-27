using EfEx.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfEx
{
    public interface IDataService
    {
        IList<Category> GetCategories();
        Category CreateCategory(string newName, string newDescription);
        IList<Category> GetCategory(int valId);
        IList<Product> GetProducts();
    }

    public class DataService : IDataService
    {
        /*public bool CreateCategory(Category category)
        {
            var ctx = new NorthwindContext();
            category.Id = ctx.Categories.Max(x => x.Id) + 1;
            ctx.Add(category);
            return ctx.SaveChanges() > 0;
        }*/

        public IList<Category> GetCategory(int valId)
        {
            var ctx = new NorthwindContext();
            return ctx.Categories
                .Where(x => x.Id == valId)
                .Select(x => x).ToList();
        }

        public Category CreateCategory(string newName, string newDescription)
        {
            var ctx = new NorthwindContext();
            if (!ctx.Categories.Select(x => x.Name).Contains(newName))
            {
                var oldId = ctx.Categories.Select(x => x.Id).ToList();
                var newId = 1;

                while (oldId.Contains(newId))
                {
                    newId++;
                }

                Category newCategory = new Category();
                newCategory.Id = newId;
                newCategory.Name = newName;
                newCategory.Description = newDescription;

                ctx.Categories.Add(newCategory);
                ctx.SaveChanges();

                return newCategory;
            }
            else
            {
                return null;
            }
        }

        public IList<Category> GetCategories()
        {
            var ctx = new NorthwindContext();

            return ctx.Categories.ToList();
        }
        
        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }
    }
}