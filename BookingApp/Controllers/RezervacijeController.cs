using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;

namespace BookingApp.Controllers
{
    public class RezervacijeController : Controller
    {
        private readonly BookingAppContext _context;

        public RezervacijeController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Rezervacije
        public async Task<IActionResult> Index()
        {
            var bookingAppContext = _context.Rezervacijes.Include(r => r.Korisnik).Include(r => r.Smještaj);
            return View(await bookingAppContext.ToListAsync());
        }

        // GET: Rezervacije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rezervacijes == null)
            {
                return NotFound();
            }

            var rezervacije = await _context.Rezervacijes
                .Include(r => r.Korisnik)
                .Include(r => r.Smještaj)
                .FirstOrDefaultAsync(m => m.RezervacijaId == id);
            if (rezervacije == null)
            {
                return NotFound();
            }

            return View(rezervacije);
        }

        // GET: Rezervacije/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId");
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId");
            return View();
        }

        // POST: Rezervacije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervacijaId,KorisnikId,SmještajId,BrojGostiju,DatumRezervacije,PočetakRezervacije,KrajRezervacije")] Rezervacije rezervacije)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacije);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", rezervacije.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", rezervacije.SmještajId);
            return View(rezervacije);
        }

        // GET: Rezervacije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rezervacijes == null)
            {
                return NotFound();
            }

            var rezervacije = await _context.Rezervacijes.FindAsync(id);
            if (rezervacije == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", rezervacije.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", rezervacije.SmještajId);
            return View(rezervacije);
        }

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervacijaId,KorisnikId,SmještajId,BrojGostiju,DatumRezervacije,PočetakRezervacije,KrajRezervacije")] Rezervacije rezervacije)
        {
            if (id != rezervacije.RezervacijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacije);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijeExists(rezervacije.RezervacijaId))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", rezervacije.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", rezervacije.SmještajId);
            return View(rezervacije);
        }

        // GET: Rezervacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rezervacijes == null)
            {
                return NotFound();
            }

            var rezervacije = await _context.Rezervacijes
                .Include(r => r.Korisnik)
                .Include(r => r.Smještaj)
                .FirstOrDefaultAsync(m => m.RezervacijaId == id);
            if (rezervacije == null)
            {
                return NotFound();
            }

            return View(rezervacije);
        }

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rezervacijes == null)
            {
                return Problem("Entity set 'BookingAppContext.Rezervacijes'  is null.");
            }
            var rezervacije = await _context.Rezervacijes.FindAsync(id);
            if (rezervacije != null)
            {
                _context.Rezervacijes.Remove(rezervacije);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijeExists(int id)
        {
          return (_context.Rezervacijes?.Any(e => e.RezervacijaId == id)).GetValueOrDefault();
        }
    }
}
