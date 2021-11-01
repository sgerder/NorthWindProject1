using System.Collections.Generic;

namespace EfEx.Domain
{
    public interface IDataService
    {
        // category methods
        List<Category> GetCategories();
        Category GetCategory(int inId);
        Category CreateCategory(string inName, string inDescription);
        bool UpdateCategory(int inId, string inName, string inDescription);
        bool DeleteCategory(int categoryId);
        
        // product methods
        Product GetProduct(int inId);
        List<Product> GetProductByCategory(int inId);
        List<Product> GetProductByName(string inName);
    }
}