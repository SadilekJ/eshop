using eshop.Models.Database_Fake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class DatabaseFake
    {
        public static IList<Carousel> Carousels { get; set; }
        public static IList<Product> Products { get; set; }

        static DatabaseFake()
        {
            Carousels = CarouselHelper.GenerateCarousel();
            Products = ProductsHelper.GenerateProducts();
        }
    }
}
