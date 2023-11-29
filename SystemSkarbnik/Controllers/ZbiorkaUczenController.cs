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
    //[Authorize(Roles = "Skarbnik")]
    public class ZbiorkaUczenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZbiorkaUczenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZbiorkaUczen
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ZbiorkaUczen.Include(z => z.Klasa).Include(z => z.Uczen).Include(z => z.Zbiorka);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ZbiorkaUczen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ZbiorkaUczen == null)
            {
                return NotFound();
            }

            var zbiorkaUczen = await _context.ZbiorkaUczen
                .Include(z => z.Klasa)
                .Include(z => z.Uczen)
                .Include(z => z.Zbiorka)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zbiorkaUczen == null)
            {
                return NotFound();
            }

            return View(zbiorkaUczen);
        }

        // GET: ZbiorkaUczen/Create
        public IActionResult Create()
        {
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID");
            ViewData["UczenID"] = new SelectList(_context.Uczen, "ID", "ID");
            ViewData["ZbiorkaID"] = new SelectList(_context.Zbiorka, "ID", "ID");
            return View();
        }

        // POST: ZbiorkaUczen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ZbiorkaID,KlasaID,UczenID,CzyZaplacil,KiedyZaplacil")] ZbiorkaUczen zbiorkaUczen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zbiorkaUczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorkaUczen.KlasaID);
            ViewData["UczenID"] = new SelectList(_context.Uczen, "ID", "ID", zbiorkaUczen.UczenID);
            ViewData["ZbiorkaID"] = new SelectList(_context.Zbiorka, "ID", "ID", zbiorkaUczen.ZbiorkaID);
            return View(zbiorkaUczen);
        }

        // GET: ZbiorkaUczen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ZbiorkaUczen == null)
            {
                return NotFound();
            }

            var zbiorkaUczen = await _context.ZbiorkaUczen.FindAsync(id);
            if (zbiorkaUczen == null)
            {
                return NotFound();
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorkaUczen.KlasaID);
            ViewData["UczenID"] = new SelectList(_context.Uczen, "ID", "ID", zbiorkaUczen.UczenID);
            ViewData["ZbiorkaID"] = new SelectList(_context.Zbiorka, "ID", "ID", zbiorkaUczen.ZbiorkaID);
            return View(zbiorkaUczen);
        }

        // POST: ZbiorkaUczen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ZbiorkaID,KlasaID,UczenID,CzyZaplacil,KiedyZaplacil")] ZbiorkaUczen zbiorkaUczen)
        {
            if (id != zbiorkaUczen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zbiorkaUczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZbiorkaUczenExists(zbiorkaUczen.ID))
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
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorkaUczen.KlasaID);
            ViewData["UczenID"] = new SelectList(_context.Uczen, "ID", "ID", zbiorkaUczen.UczenID);
            ViewData["ZbiorkaID"] = new SelectList(_context.Zbiorka, "ID", "ID", zbiorkaUczen.ZbiorkaID);
            return View(zbiorkaUczen);
        }

        // GET: ZbiorkaUczen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ZbiorkaUczen == null)
            {
                return NotFound();
            }

            var zbiorkaUczen = await _context.ZbiorkaUczen
                .Include(z => z.Klasa)
                .Include(z => z.Uczen)
                .Include(z => z.Zbiorka)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zbiorkaUczen == null)
            {
                return NotFound();
            }

            return View(zbiorkaUczen);
        }

        // POST: ZbiorkaUczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ZbiorkaUczen == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ZbiorkaUczen'  is null.");
            }
            var zbiorkaUczen = await _context.ZbiorkaUczen.FindAsync(id);
            if (zbiorkaUczen != null)
            {
                _context.ZbiorkaUczen.Remove(zbiorkaUczen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZbiorkaUczenExists(int id)
        {
          return (_context.ZbiorkaUczen?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
