using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class OrderItemsController : Controller
    {
        private readonly EshopDBContext _context;
        private readonly ILogger<OrderItemsController> logger;

        public OrderItemsController(EshopDBContext context, ILogger<OrderItemsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: Admin/OrderItems
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("OrderItem index");
            var eshopDBContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            return View(await eshopDBContext.ToListAsync());
        }

        // GET: Admin/OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            logger.LogInformation("OrderItem details");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: Admin/OrderItems/Create
        public IActionResult Create()
        {
            logger.LogInformation("OrderItem create");
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID");
            return View();
        }

        // POST: Admin/OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,ProductID,Amount,Price,ID,DateTimeCreated")] OrderItem orderItem)
        {
            logger.LogInformation("OrderItem create");
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            logger.LogInformation("OrderItem edit");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", orderItem.ProductID);
            return View(orderItem);
        }

        // POST: Admin/OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,ProductID,Amount,Price,ID,DateTimeCreated")] OrderItem orderItem)
        {
            logger.LogInformation("OrderItem edit");
            if (id != orderItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "OrderNumber", orderItem.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", orderItem.ProductID);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            logger.LogInformation("OrderItem delete");
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: Admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            logger.LogInformation("OrderItem delete confirmation");
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.ID == id);
        }
    }
}
