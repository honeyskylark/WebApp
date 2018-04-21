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
    [Area("Administration")]
    public class FromsController : Controller
    {
        private readonly WebAppContext _context;

        public FromsController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Administration/Froms
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Froms.ToListAsync());
        }

        // GET: Administration/Froms/Details/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @from = await _context.Froms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@from == null)
            {
                return NotFound();
            }

            return View(@from);
        }

        // GET: Administration/Froms/Create
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administration/Froms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name")] From @from)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@from);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@from);
        }

        // GET: Administration/Froms/Edit/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @from = await _context.Froms.SingleOrDefaultAsync(m => m.Id == id);
            if (@from == null)
            {
                return NotFound();
            }
            return View(@from);
        }

        // POST: Administration/Froms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] From @from)
        {
            if (id != @from.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@from);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FromExists(@from.Id))
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
            return View(@from);
        }

        // GET: Administration/Froms/Delete/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @from = await _context.Froms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@from == null)
            {
                return NotFound();
            }

            return View(@from);
        }

        // POST: Administration/Froms/Delete/5
        [HttpPost, ActionName("Delete")]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @from = await _context.Froms.SingleOrDefaultAsync(m => m.Id == id);
            _context.Froms.Remove(@from);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FromExists(int id)
        {
            return _context.Froms.Any(e => e.Id == id);
        }
    }
}
