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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Historial_C.Controllers
{
    //[Authorize]
    public class EpisodiosController : Controller
    {
        private readonly HistorialContext _context;

        public EpisodiosController(HistorialContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public async Task<IActionResult> Index(int? empleadoId)
        {

            if(empleadoId != null)
            {
                Empleado empleado = await _context.Empleado.FirstOrDefaultAsync(e => e.Nombre.Equals(empleadoId));
                await _context.Episodio.Include(e => e.EmpleadoRegistra).Where(e => e.EmpleadoId == empleado.Id).ToArrayAsync();
            }
              return View(await _context.Episodio.ToListAsync());
        }

        // GET: Episodios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Episodio == null)
            {
                return NotFound();
            }

            List<Episodio> episodios;
            episodios = _context.Episodio.Include(e => e.Evoluciones).ToList();
            episodios = _context.Episodio.Include(e => e.Paciente).ToList();
            var episodio = await _context.Episodio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // GET: Episodios/Create
        public IActionResult Create(int? pacienteId)
        {
            if(pacienteId == null)
            {
                return NotFound();
            }

            Episodio episodio = new Episodio() { PacienteId = pacienteId.Value};

            return View(episodio);
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id , PacienteId,Motivo,Descripcion,EstadoAbierto, EmpleadoId, FechaYHoraInicio ")] Episodio episodio)
        { 
            if (ModelState.IsValid)
            {
                var empleado = _context.Episodio.Include(m => m.EmpleadoId);
                episodio.EmpleadoId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                _context.Add(episodio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id=episodio.Id});

             
                
             
                
            }
            return View(episodio);
        }

        

        // GET: Episodios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Episodio == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodio.FindAsync(id);
            if (episodio == null)
            {
                return NotFound();
            }
            return View(episodio);
        }


        [Authorize(Roles = "Medico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> cerrarEpisodio(int id, [Bind("EstadoAbierto,FechaYHoraCierre")] Episodio episodio, int idEpisodio)
        {
            if (id != episodio.Id)
            {
       
                return Content($"El episodio con el id {id}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            return Content("El episodio fue cerrado");
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Motivo,Descripcion,EstadoAbierto")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            return View(episodio);
        }



        // GET: Episodios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Episodio == null)
            {
                return NotFound();
            }

            var episodio = await _context.Episodio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }


        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Episodio == null)
            {
                return Problem("Entity set 'HistorialContext.Episodio'  is null.");
            }
            var episodio = await _context.Episodio.FindAsync(id);
            if (episodio != null)
            {
                _context.Episodio.Remove(episodio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
          return _context.Episodio.Any(e => e.Id == id);
        }
    }
}
