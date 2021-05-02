using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database_Fake
{
    public class ProductsHelper
    {
        public static IList<Product> GenerateProducts()
        {
            IList<Product> product = new List<Product>()
            {
               new Product()
               {
                   Price = 150.90, Name = "Product One", Category = "Others", ImageSrc = "/images/product_one.jpg", ImageAlt = "Product One"
               },
               new Product()
               {
                   Price = 190.90, Name = "Product Two", Category = "Others", ImageSrc = "/images/product_two.jpg", ImageAlt = "Product Two",
               },
               new Product()
               {
                   Price = 179.90, Name = "Product Three", Category = "Others", ImageSrc = "/images/product_three.jpg", ImageAlt = "Product Three",
               }
            };
            return product;
        }
    }
}
