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
    public class SmještajController : Controller
    {
        private readonly BookingAppContext _context;

        public SmještajController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Smještaj
        public async Task<IActionResult> Index()
        {
            var bookingAppContext = _context.Smještajs.Include(s => s.TipSmještaja);
            return View(await bookingAppContext.ToListAsync());
        }

        // GET: Smještaj/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Smještajs == null)
            {
                return NotFound();
            }

            var smještaj = await _context.Smještajs
                .Include(s => s.TipSmještaja)
                .FirstOrDefaultAsync(m => m.SmještajId == id);
            if (smještaj == null)
            {
                return NotFound();
            }

            return View(smještaj);
        }

        // GET: Smještaj/Create
        public IActionResult Create()
        {
            ViewData["TipSmještajaId"] = new SelectList(_context.TipSmještajas, "TipSmještajaId", "TipSmještaja1");
            return View();
        }

        // POST: Smještaj/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SmještajId,Naziv,Adresa,Cijena,Email,TipSmještajaId")] Smještaj smještaj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(smještaj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipSmještajaId"] = new SelectList(_context.TipSmještajas, "TipSmještajaId", "TipSmještajaId", smještaj.TipSmještajaId);
            return View(smještaj);
        }

        // GET: Smještaj/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Smještajs == null)
            {
                return NotFound();
            }

            var smještaj = await _context.Smještajs.FindAsync(id);
            if (smještaj == null)
            {
                return NotFound();
            }
            ViewData["TipSmještajaId"] = new SelectList(_context.TipSmještajas, "TipSmještajaId", "TipSmještajaId", smještaj.TipSmještajaId);
            return View(smještaj);
        }

        // POST: Smještaj/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SmještajId,Naziv,Adresa,Cijena,Email,TipSmještajaId")] Smještaj smještaj)
        {
            if (id != smještaj.SmještajId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smještaj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmještajExists(smještaj.SmještajId))
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
            ViewData["TipSmještajaId"] = new SelectList(_context.TipSmještajas, "TipSmještajaId", "TipSmještajaId", smještaj.TipSmještajaId);
            return View(smještaj);
        }

        // GET: Smještaj/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Smještajs == null)
            {
                return NotFound();
            }

            var smještaj = await _context.Smještajs
                .Include(s => s.TipSmještaja)
                .FirstOrDefaultAsync(m => m.SmještajId == id);
            if (smještaj == null)
            {
                return NotFound();
            }

            return View(smještaj);
        }

        // POST: Smještaj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Smještajs == null)
            {
                return Problem("Entity set 'BookingAppContext.Smještajs'  is null.");
            }
            var smještaj = await _context.Smještajs.FindAsync(id);
            if (smještaj != null)
            {
                _context.Smještajs.Remove(smještaj);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmještajExists(int id)
        {
          return (_context.Smještajs?.Any(e => e.SmještajId == id)).GetValueOrDefault();
        }
    }
}
