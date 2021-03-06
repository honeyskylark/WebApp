using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.Administration.Controllers
{
    public class CatalogsController : Controller
    {
        private readonly WebAppContext _context;

        public CatalogsController(WebAppContext context)
        {
            _context = context;    
        }

        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var webAppContext = _context.Catalogs.Include(c => c.SubSection);
            return View(await webAppContext.ToListAsync());
        }

        // GET: Catalogs/Details/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.SubSection)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Catalogs/Create
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["SubSectionId"] = new SelectList(_context.SubSections, "Id", "Name");
            return View();
        }

        // POST: Catalogs/Create
        [HttpPost]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SubSectionId")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SubSectionId"] = new SelectList(_context.SubSections, "Id", "Name", catalog.SubSectionId);
            return View(catalog);
        }

        // GET: Catalogs/Edit/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }
            ViewData["SubSectionId"] = new SelectList(_context.SubSections, "Id", "Name", catalog.SubSectionId);
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SubSectionId")] Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            ViewData["SubSectionId"] = new SelectList(_context.SubSections, "Id", "Name", catalog.SubSectionId);
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(c => c.SubSection)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalog = await _context.Catalogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}
