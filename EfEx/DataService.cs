using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EfEx.Domain
{
    public class DataService : IDataService
    {

        public Category GetCategory(int idIn)
        {
            using var ctx = new NorthwindContext();
            return ctx.Category
                .Where(x => x.CategoryId == idIn)
                .Select(x => x).ToList().FirstOrDefault();
        }

        public IList<Category> GetCategories()
        {
            using var ctx = new NorthwindContext();
            IList<Category> list = ctx.Category.ToList();
            return list;
        }

        public Category CreateCategory(string inName, string inDescription)
        {
            using var ctx = new NorthwindContext();
            if (!ctx.Category.Select(x => x.CategoryName).Contains(inName))
            {
                var existingIds = ctx.Category.Select(x => x.CategoryId).ToList();
                var newid = 1;

                while (existingIds.Contains(newid))
                {
                    newid++;
                }

                Category newCategory = new Category();
                newCategory.CategoryId = newid;
                newCategory.CategoryName = inName;
                newCategory.CategoryDescription = inDescription;

                ctx.Category.Add(newCategory);
                ctx.SaveChanges();

                return newCategory;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteCategory(int inId)
        {
            using var ctx = new NorthwindContext();
            if (ctx.Category.Select(x => x.CategoryId).Contains(inId))
            {
                ctx.Category.Remove(ctx.Category.Where(x => x.CategoryId == inId).Select(x => x).ToList().First());
                ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool UpdateCategory(int inId, string inName, string inDescription)
        {
            using var ctx = new NorthwindContext();
            var category = ctx.Category.Find(inId);
            if (category != null)
            {
                category.CategoryName = inName;
                category.CategoryDescription = inDescription;
                ctx.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Product GetProduct(int inId)
        {
            using var ctx = new NorthwindContext();
            var product = ctx.Product.Find(inId);
            product.Category = ctx.Category.Find(product.CategoryId);
            product.CategoryName = ctx.Category.Find(product.CategoryId).CategoryName;
            return product;
        }

        public List<Product> GetProductByCategory(int inId)
        {
            using var ctx = new NorthwindContext();
            var products = ctx.Product.Where(x => x.CategoryId == inId).Select(x => x).ToList();
            foreach (var product in products)
            {
                product.Category = ctx.Category.Find(product.CategoryId);
                product.CategoryName = ctx.Category.Find(product.CategoryId).CategoryName;
            }

            return products;
        }

        public List<Product> GetProductByName(string inName)
        {
            using var ctx = new NorthwindContext();
            var products = ctx.Product.Where(x => x.Name.Contains(inName)).Select(x => x).ToList();
            foreach (var product in products)
            {
                product.Category = ctx.Category.Find(product.CategoryId);
                product.CategoryName = ctx.Category.Find(product.CategoryId).CategoryName;
            }

            return products;
        }

        public Order GetOrder(int inId)
        {
            using var ctx = new NorthwindContext();
            var order = ctx.Order.Find(inId);
            order.OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == order.Id).Select(x => x).ToList();
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Order = order;
                orderDetail.Product = GetProduct(orderDetail.ProductId);
            }

            return order;

        }

        public List<Order> GetOrders()
        {
            using var ctx = new NorthwindContext();
            var orders = ctx.Order.ToList();
            foreach (var order in orders)
            {
                order.OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == order.Id).Select(x => x).ToList();
                foreach (var orderDetail in order.OrderDetails)
                {
                    orderDetail.Order = order;
                    orderDetail.Product = GetProduct(orderDetail.ProductId);
                }
            }
            return orders;
        }

        public List<OrderDetails> GetOrderDetailsByOrderId(int inId)
        {
            using var ctx = new NorthwindContext();
            var order = ctx.Order.Find(inId);
            order.OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == order.Id).Select(x => x).ToList();
            foreach (var orderDetail in order.OrderDetails)
            {
                orderDetail.Order = order;
                orderDetail.Product = GetProduct(orderDetail.ProductId);
            }
            return order.OrderDetails;
        }
        
        public List<OrderDetails> GetOrderDetailsByProductId(int inId)
        {
            using var ctx = new NorthwindContext();
            var orderDetails = ctx.OrderDetails.Where(x => x.ProductId == inId).Select(x => x).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Order = GetOrder(orderDetail.OrderId);
                orderDetail.Product = GetProduct(orderDetail.ProductId);
            }
            return orderDetails;
        }
    }
}