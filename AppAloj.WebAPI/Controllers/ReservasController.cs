using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAloj.Datos;
using AppAloj.Entidades;
using AppAloj.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppAloj.WebAPI.Controllers
{
    [Authorize(Roles = "Administrador,Cliente")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public ReservasController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet("[action]")]
        public async Task<IEnumerable<ReservaViewModel>> Listar()
        {
            var reservas = await _context.Reservas.Include(a => a.Cowork).ToListAsync();

            return reservas.Select(c => new ReservaViewModel
            {
                idreserva = c.idreserva,
                idcowork = c.idcowork,
                cowork = c.Cowork.nombre,
                nombre = c.nombre,
                email = c.email,
                horas = c.horas,
                fechainicio = c.fechainicio,
                fechafin = c.fechafin,
                mensaje = c.mensaje,
                indice = c.indice
            });
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Reserva>> Mostrar(int id)
        {
            var reserva = await _context.Reservas.Include(c => c.Cowork).SingleOrDefaultAsync(a => a.idreserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(new ReservaViewModel
            {
                idreserva = reserva.idreserva,
                idcowork = reserva.idcowork,
                cowork = reserva.Cowork.nombre,
                nombre = reserva.nombre,
                email = reserva.email,
                horas = reserva.horas,
                fechainicio = reserva.fechainicio,
                fechafin = reserva.fechafin,
                mensaje = reserva.mensaje,
                indice = reserva.indice
            });
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(ReservaUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idreserva <= 0)
                return BadRequest();

            var reserva = await _context.Reservas.FirstOrDefaultAsync(c => c.idreserva == model.idreserva);

            if (reserva == null)
                return NotFound();

            reserva.idcowork = model.idcowork;
            reserva.nombre = model.nombre;
            reserva.email = model.email;
            reserva.horas = model.horas;
            reserva.fechainicio = model.fechainicio;
            reserva.fechafin = model.fechafin;
            reserva.mensaje = model.mensaje;
            reserva.indice = model.indice;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Reserva>> Crear(ReservaCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Reserva reserva = new Reserva
            {
                idcowork = model.idcowork,
                nombre = model.nombre,
                email = model.email,
                horas = model.horas,
                fechainicio = model.fechainicio,
                fechafin = model.fechafin,
                mensaje = model.mensaje,
                indice = model.indice
            };

            _context.Reservas.Add(reserva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.idreserva == id);
        }
    }
}
