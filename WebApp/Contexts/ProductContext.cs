using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Contexts
{
    public class ProductContext
    {
        private static readonly ProductContext instance = new ProductContext();

        public List<Product> Products { get; set; }

        private ProductContext()
        { }

        public static ProductContext GetInstance()
        {
            return instance;
        }
    }
}
