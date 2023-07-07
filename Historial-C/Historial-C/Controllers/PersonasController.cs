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

namespace Historial_C.Controllers
{
    [Authorize]
    public class PersonasController : Controller
    {
        private readonly HistorialContext _context;
        private readonly UserManager<Persona> _userManager;

        public PersonasController(HistorialContext context, UserManager<Persona> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
              return View(await _context.Persona.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        public async Task<IActionResult> DetailsPerfil(string? personaName)
        {
            if (personaName == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.UserName == personaName);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,Email,UserName,Password")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(persona);
                //await _context.SaveChangesAsync();
                persona.UserName = persona.Email;

                var resultado = await _userManager.CreateAsync(persona, Configs.PasswordGenerica);
                //si pude crear 
                if (resultado.Succeeded)
                {
                    //si pude crear el empleado entonces le agrego un rol
                    await _userManager.AddToRoleAsync(persona, "Usuario");
                }
                    return RedirectToAction(nameof(Index));

            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,Email,UserName,Password")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //buscamos la persona y le hacemos el mapeo de datos para actualizar
                    //y sea controlado por Entity
                    var personaEnDb = _context.Persona.Find(persona.Id);
                    if(personaEnDb == null)
                    {
                        return NotFound();
                    }

                    //Actualizamos los datos 
                    personaEnDb.Dni = persona.Dni;
                    personaEnDb.Nombre = persona.Nombre;
                    personaEnDb.Apellido = persona.Apellido;
                    personaEnDb.Telefono = persona.Telefono;
                    personaEnDb.Direccion = persona.Direccion;
                    personaEnDb.Email = persona.Email;

                    //Actualizamos el email y el username 
                    if(!ActualizarEmail(persona, personaEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya esta en uso");
                        return View(persona);
                    }

                    //actualizamos el contexto
                    _context.Update(personaEnDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("Details",persona);
            }
            return View(persona);
        }


        private bool ActualizarEmail(Persona p, Persona pDb)
        {
            bool resultado = true;

            try
            {
                if (!pDb.NormalizedEmail.Equals(p.Email.ToUpper()))
                {
                    if (ExistEmail(p.Email))
                    {
                        resultado = false;
                    }
                    else
                    {
                        pDb.Email = p.Email;
                        pDb.NormalizedEmail = p.Email.ToUpper();
                        pDb.UserName = p.Email;
                        pDb.NormalizedUserName = pDb.NormalizedEmail;
                    }
                }
                else
                {

                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }

        private bool ExistEmail(string email)
        {
            return _context.Persona.Any(p => p.NormalizedEmail == email.ToUpper());
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Persona == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Persona == null)
            {
                return Problem("Entity set 'HistorialContext.Persona'  is null.");
            }
            var persona = await _context.Persona.FindAsync(id);
            if (persona != null)
            {
                _context.Persona.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return _context.Persona.Any(e => e.Id == id);
        }
    }
}
