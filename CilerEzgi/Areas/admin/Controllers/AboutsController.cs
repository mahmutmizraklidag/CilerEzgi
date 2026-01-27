using CilerEzgi.Data;
using CilerEzgi.Entities;
using CilerEzgi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CilerEzgi.Areas.admin.Controllers
{
    [Area("admin"),Authorize]
    public class AboutsController : Controller
    {
        private readonly DatabaseContext _context;

        public AboutsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: admin/Abouts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abouts.ToListAsync());
        }

        

        // GET: admin/Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About about ,IFormFile? Image,IFormFile? MobileImage)
        {
            if (ModelState.IsValid)
            {
                if (Image is not null) about.Image = await FileHelper.FileLoaderAsync(Image);
                if (MobileImage is not null) about.MobileImage = await FileHelper.FileLoaderAsync(MobileImage);
                _context.Add(about);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: admin/Abouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: admin/Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,About about, IFormFile? Image, IFormFile? MobileImage)
        {
            if (id != about.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return View(about);

            var dbAbout = await _context.Abouts.FindAsync(id);
            if (dbAbout == null) return NotFound();

            dbAbout.Title = about.Title;
            dbAbout.Description = about.Description;
            if (Image is not null)
            {
                if (!string.IsNullOrEmpty(dbAbout.Image))
                {
                    FileHelper.DeleteFile(dbAbout.Image);
                }
                dbAbout.Image = await FileHelper.FileLoaderAsync(Image);
            }
            if (MobileImage is not null)
            {
                if (!string.IsNullOrEmpty(dbAbout.MobileImage))
                {
                    FileHelper.DeleteFile(dbAbout.MobileImage);
                }
                dbAbout.MobileImage = await FileHelper.FileLoaderAsync(MobileImage);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Abouts", new { area = "Admin" });
        }

        // GET: admin/Abouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: admin/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                if (!string.IsNullOrEmpty(about.Image))
                {
                    FileHelper.DeleteFile(about.Image);
                }
                if (!string.IsNullOrEmpty(about.MobileImage))
                {
                    FileHelper.DeleteFile(about.MobileImage);
                }

                _context.Abouts.Remove(about);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
            return _context.Abouts.Any(e => e.Id == id);
        }
    }
}
