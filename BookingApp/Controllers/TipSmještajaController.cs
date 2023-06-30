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
    public class TipSmještajaController : Controller
    {
        private readonly BookingAppContext _context;

        public TipSmještajaController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: TipSmještaja
        public async Task<IActionResult> Index()
        {
              return _context.TipSmještajas != null ? 
                          View(await _context.TipSmještajas.ToListAsync()) :
                          Problem("Entity set 'BookingAppContext.TipSmještajas'  is null.");
        }

        // GET: TipSmještaja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipSmještajas == null)
            {
                return NotFound();
            }

            var tipSmještaja = await _context.TipSmještajas
                .FirstOrDefaultAsync(m => m.TipSmještajaId == id);
            if (tipSmještaja == null)
            {
                return NotFound();
            }

            return View(tipSmještaja);
        }

        // GET: TipSmještaja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipSmještaja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipSmještajaId,TipSmještaja1")] TipSmještaja tipSmještaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipSmještaja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipSmještaja);
        }

        // GET: TipSmještaja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipSmještajas == null)
            {
                return NotFound();
            }

            var tipSmještaja = await _context.TipSmještajas.FindAsync(id);
            if (tipSmještaja == null)
            {
                return NotFound();
            }
            return View(tipSmještaja);
        }

        // POST: TipSmještaja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipSmještajaId,TipSmještaja1")] TipSmještaja tipSmještaja)
        {
            if (id != tipSmještaja.TipSmještajaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipSmještaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipSmještajaExists(tipSmještaja.TipSmještajaId))
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
            return View(tipSmještaja);
        }

        // GET: TipSmještaja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipSmještajas == null)
            {
                return NotFound();
            }

            var tipSmještaja = await _context.TipSmještajas
                .FirstOrDefaultAsync(m => m.TipSmještajaId == id);
            if (tipSmještaja == null)
            {
                return NotFound();
            }

            return View(tipSmještaja);
        }

        // POST: TipSmještaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipSmještajas == null)
            {
                return Problem("Entity set 'BookingAppContext.TipSmještajas'  is null.");
            }
            var tipSmještaja = await _context.TipSmještajas.FindAsync(id);
            if (tipSmještaja != null)
            {
                _context.TipSmještajas.Remove(tipSmještaja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipSmještajaExists(int id)
        {
          return (_context.TipSmještajas?.Any(e => e.TipSmještajaId == id)).GetValueOrDefault();
        }
    }
}
