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
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Historial_C.Controllers
{
    //[Authorize]
    public class EvolucionesController : Controller
    {
        private readonly HistorialContext _context;

        public EvolucionesController(HistorialContext context)
        {
            _context = context;
        }

        // GET: Evoluciones
        public async Task<IActionResult> Index()
        {
            var historialContext = _context.Evolucion.Include(e => e.Medico);
            return View(await historialContext.ToListAsync());
        }

        // GET: Evoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evolucion == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evolucion
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Create
        public IActionResult Create(int? episodioId)
        {
            if(episodioId == null )
            {
                return NotFound();
            }
            //ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "Id", "Apellido");
           
            Evolucion evolucion = new Evolucion() { EpisodioId = episodioId.Value };
           
            return View(evolucion);
        }

        // POST: Evoluciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Medico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, MedicoId,EpisodioId,DescripcionAtencion,EstadoAbierto,NotaId")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                evolucion.MedicoId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                _context.Add(evolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = evolucion.Id });
            }

            return View(evolucion);
        }

        // GET: Evoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evolucion == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evolucion.FindAsync(id);
            if (evolucion == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "Id", "Apellido", evolucion.MedicoId);
            return View(evolucion);
        }

        // POST: Evoluciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicoId,EpisodioId,DescripcionAtencion,EstadoAbierto,NotaId")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
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
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "Id", "Apellido", evolucion.MedicoId);
            return View(evolucion);
        }

        // GET: Evoluciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evolucion == null)
            {
                return NotFound();
            }

            var evolucion = await _context.Evolucion
                .Include(e => e.Medico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // POST: Evoluciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evolucion == null)
            {
                return Problem("Entity set 'HistorialContext.Evolucion'  is null.");
            }
            var evolucion = await _context.Evolucion.FindAsync(id);
            if (evolucion != null)
            {
                _context.Evolucion.Remove(evolucion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvolucionExists(int id)
        {
          return _context.Evolucion.Any(e => e.Id == id);
        }
    }
}
