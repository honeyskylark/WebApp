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
    public class ProductsController : Controller
    {
        private readonly WebAppContext _context;

        public ProductsController(WebAppContext context)
        {
            _context = context;    
        }

        // GET: Products
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Index()
        {
            var webAppContext = _context.Products.Include(p => p.Catalog).Include(p => p.Currency).Include(p => p.Unit);
            return View(await webAppContext.ToListAsync());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Currency)
                .Include(p => p.Unit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public IActionResult Create()
        {
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Name");
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,CurrencyId,UnitId,Img,CatalogId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", product.CatalogId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Name", product.CurrencyId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Administrator")]
        [Area("Administration")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", product.CatalogId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Name", product.CurrencyId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name", product.UnitId);
            return View(product);
        }

        // POST: Products/Edit/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,CurrencyId,UnitId,Img,CatalogId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", product.CatalogId);
            ViewData["CurrencyId"] = new SelectList(_context.Currency, "Id", "Name", product.CurrencyId);
            ViewData["UnitId"] = new SelectList(_context.Unit, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Currency)
                .Include(p => p.Unit)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
