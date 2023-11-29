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
    // [Authorize(Roles = "Skarbnik")]
    public class UczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uczen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Uczen.Include(u => u.Klasa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uczen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .Include(u => u.Klasa)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // GET: Uczen/Create
        public IActionResult Create()
        {
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID");
            return View();
        }

        // POST: Uczen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Imię,Nazwisko,KlasaID")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", uczen.KlasaID);
            return View(uczen);
        }

        // GET: Uczen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen.FindAsync(id);
            if (uczen == null)
            {
                return NotFound();
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", uczen.KlasaID);
            return View(uczen);
        }

        // POST: Uczen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Imię,Nazwisko,KlasaID")] Uczen uczen)
        {
            if (id != uczen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UczenExists(uczen.ID))
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
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", uczen.KlasaID);
            return View(uczen);
        }

        // GET: Uczen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Uczen == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .Include(u => u.Klasa)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // POST: Uczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uczen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Uczen'  is null.");
            }
            var uczen = await _context.Uczen.FindAsync(id);
            if (uczen != null)
            {
                _context.Uczen.Remove(uczen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UczenExists(int id)
        {
          return (_context.Uczen?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
