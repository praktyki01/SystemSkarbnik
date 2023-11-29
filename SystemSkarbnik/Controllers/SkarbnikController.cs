using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemSkarbnik.Data;
using SystemSkarbnik.Models;

namespace SystemSkarbnik.Controllers
{
    [Authorize(Roles = "Skarbnik")]
    public class SkarbnikController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkarbnikController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skarbnik
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Skarbnik.Include(s => s.Klasa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Skarbnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Skarbnik == null)
            {
                return NotFound();
            }

            var skarbnik = await _context.Skarbnik
                .Include(s => s.Klasa)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skarbnik == null)
            {
                return NotFound();
            }

            return View(skarbnik);
        }

        // GET: Skarbnik/Create
        public IActionResult Create()
        {
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID");
            return View();
        }

        // POST: Skarbnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Imię,Nazwisko,KlasaID")] Skarbnik skarbnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skarbnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", skarbnik.KlasaID);
            return View(skarbnik);
        }

        // GET: Skarbnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Skarbnik == null)
            {
                return NotFound();
            }

            var skarbnik = await _context.Skarbnik.FindAsync(id);
            if (skarbnik == null)
            {
                return NotFound();
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", skarbnik.KlasaID);
            return View(skarbnik);
        }

        // POST: Skarbnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Imię,Nazwisko,KlasaID")] Skarbnik skarbnik)
        {
            if (id != skarbnik.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skarbnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkarbnikExists(skarbnik.ID))
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
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", skarbnik.KlasaID);
            return View(skarbnik);
        }

        // GET: Skarbnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Skarbnik == null)
            {
                return NotFound();
            }

            var skarbnik = await _context.Skarbnik
                .Include(s => s.Klasa)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (skarbnik == null)
            {
                return NotFound();
            }

            return View(skarbnik);
        }

        // POST: Skarbnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Skarbnik == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Skarbnik'  is null.");
            }
            var skarbnik = await _context.Skarbnik.FindAsync(id);
            if (skarbnik != null)
            {
                _context.Skarbnik.Remove(skarbnik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkarbnikExists(int id)
        {
          return (_context.Skarbnik?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
