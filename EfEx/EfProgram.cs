using EfEx.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using EfEx.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace EfEx
{
    internal class EfProgram
    {
        static void Main(string[] args)
        
        {
            var service = new DataService();

            foreach (var categories in service.GetCategories())
            {
                Console.WriteLine(categories);
            }
            
            static void FirstTake()
                {
                    var ctx = new NorthwindContext();

                    var products = ctx.Product.Include(x => x.Category);

                    foreach (var product in products)
                    {
                        Console.WriteLine(product);
                    }
                }
            }
        }
    }


