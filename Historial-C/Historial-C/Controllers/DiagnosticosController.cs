﻿using System;
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
    public class DiagnosticosController : Controller
    {
        private readonly HistorialContext _context;

        public DiagnosticosController(HistorialContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Diagnostico.ToListAsync());
        }

        // GET: Diagnosticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // GET: Diagnosticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EpicrisisId,Descripcion,Recomendacion")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnostico);
        }

        // GET: Diagnosticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico == null)
            {
                return NotFound();
            }
            return View(diagnostico);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EpicrisisId,Descripcion,Recomendacion")] Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticoExists(diagnostico.Id))
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
            return View(diagnostico);
        }

        // GET: Diagnosticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnostico == null)
            {
                return NotFound();
            }

            var diagnostico = await _context.Diagnostico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnostico == null)
            {
                return Problem("Entity set 'HistorialContext.Diagnostico'  is null.");
            }
            var diagnostico = await _context.Diagnostico.FindAsync(id);
            if (diagnostico != null)
            {
                _context.Diagnostico.Remove(diagnostico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticoExists(int id)
        {
          return _context.Diagnostico.Any(e => e.Id == id);
        }
    }
}
