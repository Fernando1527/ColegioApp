using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColegioApp.Data;
using ColegioApp.Models;

namespace ColegioApp.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpedientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expedientes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // GET: Expedientes/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Nombre");
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre");
            return View();
        }

        // POST: Expedientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AlumnoId,MateriaId,Nota,Observacion")] Expediente expediente)
        {
            _context.Add(expediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Expedientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes.FindAsync(id);

            if (expediente == null)
            {
                return NotFound();
            }

            ViewData["AlumnoId"] = new SelectList(_context.Alumnos, "Id", "Nombre", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materias, "Id", "Nombre", expediente.MateriaId);

            return View(expediente);
        }

        // POST: Expedientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AlumnoId,MateriaId,Nota,Observacion")] Expediente expediente)
        {
            if (id != expediente.Id)
            {
                return NotFound();
            }

            _context.Update(expediente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Expedientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expedientes
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.Expedientes.FindAsync(id);

            if (expediente != null)
            {
                _context.Expedientes.Remove(expediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 🔥 NUEVO MÉTODO (PROMEDIOS)
        public IActionResult Promedios()
        {
            var promedios = _context.Expedientes
                .Include(e => e.Alumno)
                .GroupBy(e => e.Alumno.Nombre)
                .Select(g => new
                {
                    Alumno = g.Key,
                    Promedio = g.Average(x => x.Nota)
                })
                .ToList();

            return View(promedios);
        }

        private bool ExpedienteExists(int id)
        {
            return _context.Expedientes.Any(e => e.Id == id);
        }
    }
}