﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class PacienteController : Controller
    {

        private readonly TurnosContext _context;

        public PacienteController(TurnosContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paciente.ToListAsync());
        }

        public async Task<IActionResult> Detalle(int? id)
        {
            if(id == null)
                return NotFound();

            var paciente = await _context.Paciente.FirstOrDefaultAsync(p => p.IdPaciente == id);
            if(paciente == null)
                return NotFound();

            return View(paciente);
        }


        public IActionResult CrearPaciente()
        { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Previene alterar data fuera del formulario
        public async Task<IActionResult> CrearPaciente([Bind("IdPaciente, Nombre, Apellido, Direccion, Telefono, Email")]Paciente paciente)
        {
            if(ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(paciente);
        }

        public async Task<IActionResult> EditarPaciente(int? id)
        {
            if(id == null)
                return NotFound();

            var paciente = await _context.Paciente.FindAsync(id);

            if(paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPaciente(int id, [Bind("IdPaciente, Nombre, Apellido, Direccion, Telefono, Email")] Paciente paciente)
        {
            if (id != paciente.IdPaciente)
                return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        public async Task<IActionResult> EliminarPaciente(int? id)
        {
            if(id == null) 
                return NotFound();

            var paciente = await _context.Paciente.FirstOrDefaultAsync(
                    p => p.IdPaciente == id
                );

            if(paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpPost, ActionName("EliminarPaciente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarPacienteConfirmado(int? id)
        {
            if ( id==null)
                return NotFound();

            var paciente = await _context.Paciente.FindAsync(id);
            if(paciente == null)
                return NotFound();

            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
