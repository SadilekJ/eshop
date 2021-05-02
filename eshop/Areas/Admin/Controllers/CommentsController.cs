using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Comments;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CommentsController : Controller
    {
        private readonly EshopDBContext _context;
        private readonly ILogger<CommentsController> logger;

        public CommentsController(EshopDBContext context, ILogger<CommentsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public async Task<IActionResult> CommentsShow()
        {
            logger.LogInformation("MainComments show");
            CommentsViewModel cvm = new CommentsViewModel();
            cvm.MainComments = await _context.MainComments.Include(c => c.SubComments).ToListAsync();
            return View(cvm);
        }


        public async Task<IActionResult> ApproveMainComment(int id)
        {
            logger.LogInformation("MainComment approval");
            MainComment mainCommentItem = _context.MainComments.Where(mcI => mcI.ID == id).FirstOrDefault();
            if (mainCommentItem != null)
            {
                mainCommentItem.Approved = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CommentsShow));
            }
            else
            return NotFound();
        }

        public async Task<IActionResult> ApproveSubComment(int id)
        {
            logger.LogInformation("SubComment approval");
            SubComment subCommentItem = _context.SubComments.Where(scI => scI.ID == id).FirstOrDefault();
            if (subCommentItem != null)
            {
                subCommentItem.Approved = true;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CommentsShow));
            }
            else
                return NotFound();
        }
    }
}
