using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.AplicationServices;
using eshop.Models.Comments;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<HomeController> logger;
        readonly ISecurityApplicationService iSecure;

        public ProductsController(EshopDBContext EshopDBContext, ILogger<HomeController> logger, ISecurityApplicationService iSecure)
        {
            this.EshopDBContext = EshopDBContext;
            this.logger = logger;
            this.iSecure = iSecure;
        }

        [Route("Products/{id:int}")]
        public IActionResult Detail(int id)
        {
            logger.LogInformation("Detail clicked");
            Product productItem = EshopDBContext.Products.Where(prodI => prodI.ID == id)
                .Include(prodI => prodI.MainComments)
                .ThenInclude(c => c.User)
                .Include(prodI => prodI.MainComments)
                .ThenInclude(c => c.SubComments)
                .FirstOrDefault();
            if (productItem != null)
            {
                return View(productItem);
            }
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Comment(CommentsViewModel cvm)
        {
            bool approve = false;
            User currentUser = await iSecure.GetCurrentUser(User);
            if (User.IsInRole(nameof(Roles.Admin)) || User.IsInRole(nameof(Roles.Manager)))
            {
                approve = true;
            }
            if (cvm.MainCommentId == 0)
            {
                MainComment commentItem = new MainComment()
                {
                    CommentMessage = cvm.Message,
                    UserId = currentUser.Id,
                    ProductId = cvm.ProductId,
                    Approved = approve
                };

                EshopDBContext.Add(commentItem);

                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Detail), new { id = commentItem.ProductId });
            }
            else
            {
                SubComment commentItem = new SubComment()
                {
                    MainCommentId = cvm.MainCommentId,
                    CommentMessage = cvm.Message,
                    UserId = currentUser.Id,
                    Approved = approve
                };
                EshopDBContext.Add(commentItem);
                await EshopDBContext.SaveChangesAsync();
                commentItem.MainComment = EshopDBContext.MainComments.Where(c => c.ID == commentItem.MainCommentId).FirstOrDefault();
                return RedirectToAction(nameof(Detail), new { id = commentItem.MainComment.ProductId });
            }              
        }
    }
}
