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
    public class ZbiorkaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZbiorkaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zbiorka
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zbiorka.Include(z => z.Klasa).Include(z => z.Skarbnik);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexKlasa()
        {
            var applicationDbContext = _context.Zbiorka.Include(z => z.Klasa).Include(z => z.Skarbnik);
                
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zbiorka/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zbiorka == null)
            {
                return NotFound();
            }

            var zbiorka = await _context.Zbiorka
                .Include(z => z.Klasa)
                .Include(z => z.Skarbnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zbiorka == null)
            {
                return NotFound();
            }

            return View(zbiorka);
        }
        //[Authorize(Roles = "Skarbnik")]
        // GET: Zbiorka/Create
        public IActionResult Create()
        {
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID");
            ViewData["SkarbnikID"] = new SelectList(_context.Skarbnik, "ID", "ID");
            return View();
        }
        //[Authorize(Roles = "Skarbnik")]
        // POST: Zbiorka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa,Opis,Kwota,DataOd,DataDo,KlasaID,SkarbnikID")] Zbiorka zbiorka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zbiorka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorka.KlasaID);
            ViewData["SkarbnikID"] = new SelectList(_context.Skarbnik, "ID", "ID", zbiorka.SkarbnikID);
            return View(zbiorka);
        }
        //[Authorize(Roles = "Skarbnik")]
        // GET: Zbiorka/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zbiorka == null)
            {
                return NotFound();
            }

            var zbiorka = await _context.Zbiorka.FindAsync(id);
            if (zbiorka == null)
            {
                return NotFound();
            }
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorka.KlasaID);
            ViewData["SkarbnikID"] = new SelectList(_context.Skarbnik, "ID", "ID", zbiorka.SkarbnikID);
            return View(zbiorka);
        }
        //[Authorize(Roles = "Skarbnik")]
        // POST: Zbiorka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa,Opis,Kwota,DataOd,DataDo,KlasaID,SkarbnikID")] Zbiorka zbiorka)
        {
            if (id != zbiorka.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zbiorka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZbiorkaExists(zbiorka.ID))
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
            ViewData["KlasaID"] = new SelectList(_context.Klasa, "ID", "ID", zbiorka.KlasaID);
            ViewData["SkarbnikID"] = new SelectList(_context.Skarbnik, "ID", "ID", zbiorka.SkarbnikID);
            return View(zbiorka);
        }
        //[Authorize(Roles = "Skarbnik")]
        // GET: Zbiorka/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zbiorka == null)
            {
                return NotFound();
            }

            var zbiorka = await _context.Zbiorka
                .Include(z => z.Klasa)
                .Include(z => z.Skarbnik)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zbiorka == null)
            {
                return NotFound();
            }

            return View(zbiorka);
        }
        //[Authorize(Roles = "Skarbnik")]
        // POST: Zbiorka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zbiorka == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zbiorka'  is null.");
            }
            var zbiorka = await _context.Zbiorka.FindAsync(id);
            if (zbiorka != null)
            {
                _context.Zbiorka.Remove(zbiorka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZbiorkaExists(int id)
        {
          return (_context.Zbiorka?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
