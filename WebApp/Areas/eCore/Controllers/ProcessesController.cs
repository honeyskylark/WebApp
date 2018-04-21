using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Areas.eCore.Controllers
{
    [Area("eCore")]
    public class ProcessesController : Controller
    {
        private readonly WebAppContext _context;

        public ProcessesController(WebAppContext context)
        {
            _context = context;
        }

        // GET: eCore/Processes
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Processes.ToListAsync());
        }

        // GET: eCore/Processes/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // GET: eCore/Processes/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: eCore/Processes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Order")] Process process)
        {
            if (ModelState.IsValid)
            {
                _context.Add(process);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Deals");
            }
            return View(process);
        }

        // GET: eCore/Processes/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes.SingleOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }
            return View(process);
        }

        // POST: eCore/Processes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Order")] Process process)
        {
            if (id != process.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Processes.AsNoTracking().Any(m => m.Order == process.Order))
                    {
                        var processWithExistingOrder = await _context.Processes.AsNoTracking().SingleOrDefaultAsync(m => m.Order == process.Order);
                        if (processWithExistingOrder != null)
                        {
                            if (processWithExistingOrder.Id != process.Id)
                            {
                                var processLegacy = await _context.Processes.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
                                if (processLegacy != null)
                                {
                                    processWithExistingOrder.Order = processLegacy.Order;
                                    _context.Update(processWithExistingOrder);
                                }                           
                            }
                        }                                               
                    }

                    _context.Update(process);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessExists(process.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Deals");
            }
            return View(process);
        }

        // GET: eCore/Processes/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var process = await _context.Processes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (process == null)
            {
                return NotFound();
            }

            return View(process);
        }

        // POST: eCore/Processes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var process = await _context.Processes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Deals");
        }

        private bool ProcessExists(int id)
        {
            return _context.Processes.Any(e => e.Id == id);
        }
    }
}
