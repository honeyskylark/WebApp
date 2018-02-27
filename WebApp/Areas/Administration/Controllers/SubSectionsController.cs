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
    public class SubSectionsController : Controller
    {
        private readonly WebAppContext _context;

        public SubSectionsController(WebAppContext context)
        {
            _context = context;    
        }

        // GET: SubSections
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Index()
        {
            var webAppContext = _context.SubSection.Include(s => s.Section);
            return View(await webAppContext.ToListAsync());
        }

        // GET: SubSections/Details/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSection
                .Include(s => s.Section)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subSection == null)
            {
                return NotFound();
            }

            return View(subSection);
        }

        // GET: SubSections/Create
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name");
            return View();
        }

        // POST: SubSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,SectionId")] SubSection subSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subSection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name", subSection.SectionId);
            return View(subSection);
        }

        // GET: SubSections/Edit/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSection.SingleOrDefaultAsync(m => m.Id == id);
            if (subSection == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name", subSection.SectionId);
            return View(subSection);
        }

        // POST: SubSections/Edit/5
        [Area("Administration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SectionId")] SubSection subSection)
        {
            if (id != subSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubSectionExists(subSection.Id))
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
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name", subSection.SectionId);
            return View(subSection);
        }

        // GET: SubSections/Delete/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subSection = await _context.SubSection
                .Include(s => s.Section)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (subSection == null)
            {
                return NotFound();
            }

            return View(subSection);
        }

        // POST: SubSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subSection = await _context.SubSection.SingleOrDefaultAsync(m => m.Id == id);
            _context.SubSection.Remove(subSection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubSectionExists(int id)
        {
            return _context.SubSection.Any(e => e.Id == id);
        }
    }
}
