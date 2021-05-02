using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {
            IList<Carousel> carousel = new List<Carousel>()
            {
                new Carousel() { DataTarget = "#myCarousel", ImageSrc = "/images/banner1.svg", ImageAlt = "ASP.NET", CarouselContent = "Learn how to build ASP.NET apps that can run anywhere." +
                    "<a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409\">" +
                        "Learn More </a>"                       
                },
                new Carousel() {DataTarget = "#myCarousel", ImageSrc = "/images/banner2.svg", ImageAlt = "Visual Studio", CarouselContent = "There are powerful new features in Visual Studio for building modern web apps." +
                    "<a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525030&clcid=0x409\">" +
                        "Learn More </a>"
                    },
                new Carousel() { DataTarget = "#myCarousel", ImageSrc = "/images/banner3.svg", ImageAlt = "Microsoft Azure", CarouselContent = "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps." +
                    "<a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525027&clcid=0x409\">" +
                        "Learn More </a>"
                    },
                new Carousel() { DataTarget = "#myCarousel", ImageSrc = "/images/img_lights.jpg", ImageAlt = "North Pole", CarouselContent = "North Pole Scenery" +
                "<a class=\"btn btn-default\" href=\"https://google.com\">" +
                "Learn More </a>" 
                }
            };
            return carousel;
        }
    }
}
