using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppContext _context;

        public HomeController(WebAppContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            ViewBag.IsHomePage = "true";
            ViewBag.Catalogs =  await _context.Catalogs.ToListAsync();
            var webAppContext = _context.Products.Include(p => p.Catalog).Include(p => p.Currency).Include(p => p.Unit);

            return View(await webAppContext.ToListAsync()); 
        }


    }
}
