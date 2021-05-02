using eshop.Models;
using eshop.Models.AplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerOrdersController : Controller
    {

        ISecurityApplicationService iSecure;
        EshopDBContext EshopDBContext;
        private readonly ILogger<CustomerOrdersController> logger;

        public CustomerOrdersController(ISecurityApplicationService iSecure, EshopDBContext eshopDBContext, ILogger<CustomerOrdersController> logger)
        {
            this.iSecure = iSecure;
            EshopDBContext = eshopDBContext;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            logger.LogInformation("CustomerOrders Index");
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await iSecure.GetCurrentUser(User);
                if (currentUser != null)
                {
                    IList<Order> userOrders = await this.EshopDBContext.Orders
                                                                        .Where(or => or.UserId == currentUser.Id)
                                                                        .Include(o => o.User)
                                                                        .Include(o => o.OrderItems)
                                                                        .ThenInclude(oi => oi.Product)
                                                                        .ToListAsync();
                    return View(userOrders);
                }
            }

            return NotFound();
        }
    }
}
