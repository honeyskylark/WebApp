using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Areas.eCore.Controllers
{
    [Area("eCore")]
    public class DealsController : Controller
    {
        private readonly WebAppContext _context;

        public DealsController(WebAppContext context)
        {
            _context = context;
        }

        // GET: eCore/Deals
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Index()
        {
            var filteredProcesses = from process in _context.Processes
                                    orderby process.Order
                                    select process;
            var processes = filteredProcesses.Distinct();
            ViewData["Processes"] = processes;
            ViewData["Count"] = processes.Count();

            var webAppContext = _context.Deals.Include(d => d.Contact).Include(d => d.From).Include(d => d.Process).Include(d => d.Product).Include(d => d.User);
            return View(await webAppContext.ToListAsync());
        }

        // GET: eCore/Deals/Details/5
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .Include(d => d.Contact)
                .Include(d => d.Contact.Company)
                .Include(d => d.From)
                .Include(d => d.Process)
                .Include(d => d.Product)
                .Include(d => d.Product.Currency)
                .Include(d => d.Product.Unit)
                .Include(d => d.Product.Catalog)
                .Include(d => d.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // GET: eCore/Deals/Create
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Create(int? id)
        {
            List<object> _contactsViewList = new List<object>();
            foreach (var contact in _context.Contacts)
            {
                _contactsViewList.Add(new
                {
                    Id = contact.Id,
                    Name = $"{contact.FirstName} {contact.LastName} {contact.Patronymic}"
                });
            }

            List<object> _usersViewList = new List<object>();
            foreach (var user in _context.Users)
            {
                _usersViewList.Add(new
                {
                    Id = user.Id,
                    Name = $"{user.FirstName} {user.Patronymic} {user.LastName}"
                });
            }

            ViewData["ContactId"] = new SelectList(_contactsViewList, "Id", "Name");
            ViewData["FromId"] = new SelectList(_context.Froms, "Id", "Name");
            var process = await _context.Processes.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            var processes = new List<Process> { new Process { Id = process.Id, Order = process.Order, Title = process.Title } };

            ViewData["ProcessId"] = new SelectList(processes, "Id", "Title");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");

            ViewData["UserId"] = new SelectList(_usersViewList, "Id", "Name");
            return View();
        }

        // POST: eCore/Deals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]       
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Create([Bind("Title,Description,ProcessId,ContactId,FromId,ProductId,UserId")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "FirstName", deal.ContactId);
            ViewData["FromId"] = new SelectList(_context.Froms, "Id", "Name", deal.FromId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Title", deal.ProcessId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", deal.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", deal.UserId);
            return View(deal);
        }

        // GET: eCore/Deals/Edit/5
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals.SingleOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            List<object> _contactsViewList = new List<object>();
            foreach (var contact in _context.Contacts)
            {
                _contactsViewList.Add(new
                {
                    Id = contact.Id,
                    Name = $"{contact.FirstName} {contact.LastName} {contact.Patronymic}"
                });
            }

            List<object> _usersViewList = new List<object>();
            foreach (var user in _context.Users)
            {
                _usersViewList.Add(new
                {
                    Id = user.Id,
                    Name = $"{user.FirstName} {user.Patronymic} {user.LastName}"
                });
            }

            ViewData["ContactId"] = new SelectList(_contactsViewList, "Id", "Name", deal.ContactId);
            ViewData["FromId"] = new SelectList(_context.Froms, "Id", "Name", deal.FromId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Title", deal.ProcessId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", deal.ProductId);
            ViewData["UserId"] = new SelectList(_usersViewList, "Id", "Name", deal.UserId);
            return View(deal);
        }

        // POST: eCore/Deals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ProcessId,ContactId,FromId,ProductId,UserId")] Deal deal)
        {
            if (id != deal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.Id))
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
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "FirstName", deal.ContactId);
            ViewData["FromId"] = new SelectList(_context.Froms, "Id", "Name", deal.FromId);
            ViewData["ProcessId"] = new SelectList(_context.Processes, "Id", "Title", deal.ProcessId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", deal.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", deal.UserId);
            return View(deal);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Forward(int? id)
        {
            var processes = from process in _context.Processes
                            select process;

            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .Include(d => d.Contact)
                .Include(d => d.From)
                .Include(d => d.Process)
                .Include(d => d.Product)
                .Include(d => d.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            try
            {
                var nextOrder = deal.Process.Order + 1;
                var selectedProcess = from process in processes
                                      where process.Order == nextOrder
                                      select process;

                deal.ProcessId = selectedProcess.FirstOrDefault().Id;
                _context.Update(deal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealExists(deal.Id))
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

        // GET: eCore/Deals/Delete/5
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .Include(d => d.Contact)
                .Include(d => d.From)
                .Include(d => d.Process)
                .Include(d => d.Product)
                .Include(d => d.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        // POST: eCore/Deals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deal = await _context.Deals.SingleOrDefaultAsync(m => m.Id == id);
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealExists(int id)
        {
            return _context.Deals.Any(e => e.Id == id);
        }
    }
}
