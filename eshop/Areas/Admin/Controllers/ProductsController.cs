using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models;
using eshop.Models.Database_Fake;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductsController : Controller
    {
        IHostingEnvironment Env;
        private readonly EshopDBContext EshopDBContext;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<ProductsController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            this.logger = logger;
        }

        public async Task<IActionResult> SelectProducts()
        {
            logger.LogInformation("Products selected");
            ProductViewModel productVM = new ProductViewModel();
            productVM.Products = await EshopDBContext.Products.ToListAsync();
            return View(productVM);
        }

        public IActionResult Edit(int id)
        {
            logger.LogInformation("Product edit");
            Product productItem = EshopDBContext.Products.Where(prodI => prodI.ID == id).FirstOrDefault();
            if (productItem != null)
            {
                return View(productItem);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            logger.LogInformation("Product edit");
            Product productItem = EshopDBContext.Products.Where(prodI => prodI.ID == product.ID).FirstOrDefault();
            if (productItem != null)
            {
                if (ModelState.IsValid)
                {
                    productItem.ImageAlt = product.ImageAlt;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                    if (String.IsNullOrWhiteSpace(product.ImageSrc = await fup.FileUploadAsync(product.Image)) == false)
                    {
                        productItem.ImageSrc = product.ImageSrc;
                    }

                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(SelectProducts));
                }
                else
                {
                    return View(product);
                }
            }
            else
                return NotFound();
        }

        public IActionResult Create()
        {
            logger.LogInformation("Product create");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            logger.LogInformation("Product create");
            if (ModelState.IsValid)
            {
                product.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);

                EshopDBContext.Products.Add(product);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(SelectProducts));
            }
            else
            {
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Product delete");
            Product productItem = EshopDBContext.Products.Where(product => product.ID == id).FirstOrDefault();
            if (productItem != null)
            {
                EshopDBContext.Products.Remove(productItem);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(SelectProducts));
            }
            else
                return NotFound();
        }
    }
}
