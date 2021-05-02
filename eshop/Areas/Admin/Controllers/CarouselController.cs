using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<CarouselController> logger;

        public CarouselController(EshopDBContext eshopDBContext,IHostingEnvironment env, ILogger<CarouselController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            this.logger = logger;
        }
        public async Task<IActionResult> Carousel()
        {
            logger.LogInformation("Carousel select");
            CarouselViewModel carouselVM = new CarouselViewModel();
            carouselVM.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(carouselVM);
        }

        public IActionResult Create()
        {
            logger.LogInformation("Create carousel");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            logger.LogInformation("Create carousel");
            if (ModelState.IsValid)
            { 
            carousel.ImageSrc = String.Empty;

            FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels","image");
            carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);

            EshopDBContext.Carousels.Add(carousel);
            await EshopDBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Carousel));
            }
            else
            {
                return View(carousel);
            }
        }

        public IActionResult Edit(int id)
        {
            logger.LogInformation("Edit carousel");
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == id).FirstOrDefault();
            if (carouselItem != null)
            {
                return View(carouselItem);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            logger.LogInformation("Edit carousel");
            Carousel carouselItem = EshopDBContext.Carousels.Where(carI => carI.ID == carousel.ID).FirstOrDefault();
            if (carouselItem != null)
            {
                if (ModelState.IsValid)
                {
                    carouselItem.DataTarget = carousel.DataTarget;
                    carouselItem.ImageAlt = carousel.ImageAlt;
                    carouselItem.CarouselContent = carousel.CarouselContent;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                    if (String.IsNullOrWhiteSpace(carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image)) == false)
                    {
                        carouselItem.ImageSrc = carousel.ImageSrc;
                    }

                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Carousel));
                }
                else
                {
                    return View(carousel);
                }
            }
            else
                return NotFound();
        }

        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Delete carousel");
            Carousel carouselItem = EshopDBContext.Carousels.Where(carousel => carousel.ID == id).FirstOrDefault();
            if (carouselItem != null)
            {
                EshopDBContext.Carousels.Remove(carouselItem);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Carousel));
            }
            else
                return NotFound();
        }
    }
}
