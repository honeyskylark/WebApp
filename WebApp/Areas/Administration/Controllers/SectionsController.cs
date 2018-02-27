using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Administration.Controllers
{
    public class SectionsController : Controller
    {
        private readonly WebAppContext _context;

        public SectionsController(WebAppContext context)
        {
            _context = context;    
        }

        // GET: Equipments
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.ToListAsync());
        }

        // GET: Equipments/Details/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Sections
                .SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipments/Create
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name")] Section equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        [HttpPost]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Section equipment)
        {
            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Sections
                .SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sections.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EquipmentExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}
