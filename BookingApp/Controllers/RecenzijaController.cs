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
    public class RecenzijaController : Controller
    {
        private readonly BookingAppContext _context;

        public RecenzijaController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Recenzija
        public async Task<IActionResult> Index()
        {
            var bookingAppContext = _context.Recenzijas.Include(r => r.Korisnik).Include(r => r.Smještaj);
            return View(await bookingAppContext.ToListAsync());
        }

        // GET: Recenzija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recenzijas == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzijas
                .Include(r => r.Korisnik)
                .Include(r => r.Smještaj)
                .FirstOrDefaultAsync(m => m.RecenzijaId == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // GET: Recenzija/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisničkoIme");
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "Naziv");
            return View();
        }

        // POST: Recenzija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecenzijaId,KorisnikId,SmještajId,Ocjena,Komentar")] Recenzija recenzija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", recenzija.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", recenzija.SmještajId);
            return View(recenzija);
        }

        // GET: Recenzija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recenzijas == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzijas.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", recenzija.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", recenzija.SmještajId);
            return View(recenzija);
        }

        // POST: Recenzija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecenzijaId,KorisnikId,SmještajId,Ocjena,Komentar")] Recenzija recenzija)
        {
            if (id != recenzija.RecenzijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.RecenzijaId))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnicis, "KorisnikId", "KorisnikId", recenzija.KorisnikId);
            ViewData["SmještajId"] = new SelectList(_context.Smještajs, "SmještajId", "SmještajId", recenzija.SmještajId);
            return View(recenzija);
        }

        // GET: Recenzija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recenzijas == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzijas
                .Include(r => r.Korisnik)
                .Include(r => r.Smještaj)
                .FirstOrDefaultAsync(m => m.RecenzijaId == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // POST: Recenzija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recenzijas == null)
            {
                return Problem("Entity set 'BookingAppContext.Recenzijas'  is null.");
            }
            var recenzija = await _context.Recenzijas.FindAsync(id);
            if (recenzija != null)
            {
                _context.Recenzijas.Remove(recenzija);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(int id)
        {
          return (_context.Recenzijas?.Any(e => e.RecenzijaId == id)).GetValueOrDefault();
        }
    }
}
