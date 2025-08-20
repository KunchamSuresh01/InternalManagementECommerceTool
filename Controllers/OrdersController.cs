using InternalManagementECommerceTool.Data;
using InternalManagementECommerceTool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternalManagementECommerceTool.Controllers
{
    public class OrdersController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(x => x.OrderProducts)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(order);
        }
    }
}