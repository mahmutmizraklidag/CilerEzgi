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
    public class WhyChooseUsController : Controller
    {
        private readonly DatabaseContext _context;

        public WhyChooseUsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/WhyChooseUs
        public async Task<IActionResult> Index()
        {
            return View(await _context.WhyChooseUs.ToListAsync());
        }

        // GET: admin/WhyChooseUs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyChooseUs = await _context.WhyChooseUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyChooseUs == null)
            {
                return NotFound();
            }

            return View(whyChooseUs);
        }

        // GET: admin/WhyChooseUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/WhyChooseUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] WhyChooseUs whyChooseUs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(whyChooseUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(whyChooseUs);
        }

        // GET: admin/WhyChooseUs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyChooseUs = await _context.WhyChooseUs.FindAsync(id);
            if (whyChooseUs == null)
            {
                return NotFound();
            }
            return View(whyChooseUs);
        }

        // POST: admin/WhyChooseUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] WhyChooseUs whyChooseUs)
        {
            if (id != whyChooseUs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(whyChooseUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhyChooseUsExists(whyChooseUs.Id))
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
            return View(whyChooseUs);
        }

        // GET: admin/WhyChooseUs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyChooseUs = await _context.WhyChooseUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyChooseUs == null)
            {
                return NotFound();
            }

            return View(whyChooseUs);
        }

        // POST: admin/WhyChooseUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var whyChooseUs = await _context.WhyChooseUs.FindAsync(id);
            if (whyChooseUs != null)
            {
                _context.WhyChooseUs.Remove(whyChooseUs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WhyChooseUsExists(int id)
        {
            return _context.WhyChooseUs.Any(e => e.Id == id);
        }
    }
}
