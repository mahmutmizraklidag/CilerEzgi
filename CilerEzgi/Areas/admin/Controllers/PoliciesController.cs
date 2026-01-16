using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CilerEzgi.Data;
using CilerEzgi.Entities;

namespace CilerEzgi.Areas.admin.Controllers
{
    [Area("admin")]
    public class PoliciesController : Controller
    {
        private readonly DatabaseContext _context;

        public PoliciesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/Policies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Policies.ToListAsync());
        }

        // GET: admin/Policies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policies = await _context.Policies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policies == null)
            {
                return NotFound();
            }

            return View(policies);
        }

        // GET: admin/Policies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Policies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Slug")] Policies policies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policies);
        }

        // GET: admin/Policies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policies = await _context.Policies.FindAsync(id);
            if (policies == null)
            {
                return NotFound();
            }
            return View(policies);
        }

        // POST: admin/Policies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Slug")] Policies policies)
        {
            if (id != policies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliciesExists(policies.Id))
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
            return View(policies);
        }

        // GET: admin/Policies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policies = await _context.Policies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policies == null)
            {
                return NotFound();
            }

            return View(policies);
        }

        // POST: admin/Policies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policies = await _context.Policies.FindAsync(id);
            if (policies != null)
            {
                _context.Policies.Remove(policies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliciesExists(int id)
        {
            return _context.Policies.Any(e => e.Id == id);
        }
    }
}
