using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historial_C.Data;
using Historial_C.Models;
using Microsoft.AspNetCore.Identity;
using Historial_C.Helpers;
using Microsoft.AspNetCore.Authorization;
//using AspNetCore;

namespace Historial_C.Controllers
{
    
    public class PacientesController : Controller
    {
        private readonly HistorialContext _context;
        private readonly UserManager<Persona> _userManager;

        public PacientesController(HistorialContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }
        
        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Paciente.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(paciente);
                //await _context.SaveChangesAsync();
                paciente.UserName = paciente.Email;

                var resultado = await _userManager.CreateAsync(paciente, Configs.PasswordGenerica);
                //si pude crear 
                if (resultado.Succeeded)
                {
                    //si pude crear el empleado entonces le agrego un rol
                    await _userManager.AddToRoleAsync(paciente, "Usuario");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObraSocial,Id,Nombre,Apellido,Dni,Telefono,Direccion,Email")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paciente == null)
            {
                return Problem("Entity set 'HistorialContext.Paciente'  is null.");
            }
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return _context.Paciente.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<IActionResult> BuscarPaciente(string pacienteDni)
        {
            if (pacienteDni == null) {
                return NotFound();
            }

            Paciente paciente = _context.Paciente.First(p => p.Dni.Equals(pacienteDni));

            if (paciente != null)
            {
                return View("Details", paciente);
            }
            return NotFound();
        }

       
        public async Task<IActionResult> HistorialClinicaPaciente(string pacienteUserName)
        {
            if(pacienteUserName == null || _context.Paciente == null)//Validar si acá tiene que ir persona o paciente
            {
                return NotFound();
            }
            List<Paciente> pacientes;
            pacientes = await _context.Paciente.Include(m => m.Episodios).ToListAsync();
            Paciente paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.UserName ==pacienteUserName);
            if(paciente == null)
            {
                return NotFound();
            }
            return View("HistorialClinicaPaciente", paciente);
        }

    }
}
