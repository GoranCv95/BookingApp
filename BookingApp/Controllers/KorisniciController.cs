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
    public class KorisniciController : Controller
    {
        private readonly BookingAppContext _context;

        public KorisniciController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Korisnici
        public async Task<IActionResult> Index()
        {
              return _context.Korisnicis != null ? 
                          View(await _context.Korisnicis.ToListAsync()) :
                          Problem("Entity set 'BookingAppContext.Korisnicis'  is null.");
        }

        // GET: Korisnici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Korisnicis == null)
            {
                return NotFound();
            }

            var korisnici = await _context.Korisnicis
                .FirstOrDefaultAsync(m => m.KorisnikId == id);
            if (korisnici == null)
            {
                return NotFound();
            }

            return View(korisnici);
        }

        // GET: Korisnici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisnici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KorisnikId,Ime,Prezime,KorisničkoIme,Email,BrojTelefona")] Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korisnici);
        }

        // GET: Korisnici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Korisnicis == null)
            {
                return NotFound();
            }

            var korisnici = await _context.Korisnicis.FindAsync(id);
            if (korisnici == null)
            {
                return NotFound();
            }
            return View(korisnici);
        }

        // POST: Korisnici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KorisnikId,Ime,Prezime,KorisničkoIme,Email,BrojTelefona")] Korisnici korisnici)
        {
            if (id != korisnici.KorisnikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisniciExists(korisnici.KorisnikId))
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
            return View(korisnici);
        }

        // GET: Korisnici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Korisnicis == null)
            {
                return NotFound();
            }

            var korisnici = await _context.Korisnicis
                .FirstOrDefaultAsync(m => m.KorisnikId == id);
            if (korisnici == null)
            {
                return NotFound();
            }

            return View(korisnici);
        }

        // POST: Korisnici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Korisnicis == null)
            {
                return Problem("Entity set 'BookingAppContext.Korisnicis'  is null.");
            }
            var korisnici = await _context.Korisnicis.FindAsync(id);
            if (korisnici != null)
            {
                _context.Korisnicis.Remove(korisnici);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisniciExists(int id)
        {
          return (_context.Korisnicis?.Any(e => e.KorisnikId == id)).GetValueOrDefault();
        }
    }
}
