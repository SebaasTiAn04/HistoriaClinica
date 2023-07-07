using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historial_C.Data;
using Historial_C.Models;
using Microsoft.AspNetCore.Authorization;

namespace Historial_C.Controllers
{
    [Authorize]
    public class EpicrisisController : Controller
    {
        private readonly HistorialContext _context;

        public EpicrisisController(HistorialContext context)
        {
            _context = context;
        }

        // GET: Epicrisis
        public async Task<IActionResult> Index()
        {
              return View(await _context.Epicrisis.ToListAsync());
        }

        // GET: Epicrisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // GET: Epicrisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Epicrisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicoId,EpisodioId")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epicrisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(epicrisis);
        }

        // GET: Epicrisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis.FindAsync(id);
            if (epicrisis == null)
            {
                return NotFound();
            }
            return View(epicrisis);
        }

        // POST: Epicrisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicoId,EpisodioId")] Epicrisis epicrisis)
        {
            if (id != epicrisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epicrisis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpicrisisExists(epicrisis.Id))
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
            return View(epicrisis);
        }

        // GET: Epicrisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Epicrisis == null)
            {
                return NotFound();
            }

            var epicrisis = await _context.Epicrisis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // POST: Epicrisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Epicrisis == null)
            {
                return Problem("Entity set 'HistorialContext.Epicrisis'  is null.");
            }
            var epicrisis = await _context.Epicrisis.FindAsync(id);
            if (epicrisis != null)
            {
                _context.Epicrisis.Remove(epicrisis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpicrisisExists(int id)
        {
          return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
