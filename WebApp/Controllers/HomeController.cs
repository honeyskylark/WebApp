using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //ViewBag.Catalogs =  await _context.Catalogs.ToListAsync();
            //var webAppContext = _context.Products.Include(p => p.Catalog).Include(p => p.Currency).Include(p => p.Unit);

            //return View(await webAppContext.ToListAsync());
            return View();
        }
    }
}
