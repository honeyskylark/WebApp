using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebAppContext _context;

        public HomeController(WebAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Include(p => p.Catalog).Include(p => p.Currency).Include(p => p.Unit);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FeedbackViewModel model)
        {
            Feedback feedback = new Feedback {
                Email = model.Email,
                Message = model.Message,
                Name = model.Name,
                Phone = model.Phone,                
            };          
            _context.Add(feedback);
            await _context.SaveChangesAsync();
            return View();
        }

    }
}
