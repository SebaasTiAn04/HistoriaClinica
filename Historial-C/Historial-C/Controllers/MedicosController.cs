using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historial_C.Data;
using Historial_C.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Historial_C.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace Historial_C.Controllers
{
    
    public class MedicosController : Controller
    {
        private readonly HistorialContext _context;
        private readonly UserManager<Persona> _userManager;

        public MedicosController(HistorialContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Medicos
    

        public async Task<IActionResult> Index(string? especialidadBuscada)
        {
            List<Medico> medicos;

            if (especialidadBuscada != null)
            {
                Especialidad especialidad = await _context.Especialidad.FirstOrDefaultAsync(e => e.Nombre.Equals(especialidadBuscada));
                if(especialidad != null)
                {
                    medicos = await _context.Medico.Include(m => m.Especialidad).Where(m => m.EspecialidadId == especialidad.Id).ToListAsync();
                }
                else
                {
                    medicos = _context.Medico.Include(m => m.Especialidad).ToList();
                }

            }
            else
            {
                medicos = _context.Medico.Include(m => m.Especialidad).ToList();
            }
            return View(medicos);
        }
        

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        [Authorize(Roles= "Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,Legajo,Id,Nombre,Apellido,Dni,Telefono,Direccion,Email")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                // _context.Add(medico);
                //await _context.SaveChangesAsync();
                medico.UserName = medico.Email;

                var resultado = await _userManager.CreateAsync(medico, Configs.PasswordGenerica);

                if (resultado.Succeeded) {
                    await _userManager.AddToRoleAsync(medico, "Medico");
                }
                

                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matricula,Legajo,Id,Nombre,Apellido,Dni,Telefono,Direccion,Email")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medico == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medico == null)
            {
                return Problem("Entity set 'HistorialContext.Medico'  is null.");
            }
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.Id == id);
        }

       
    }
}
