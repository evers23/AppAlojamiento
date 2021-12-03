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

namespace AppAloj.WebAPI.Controllers
{
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
        [HttpGet]
        public async Task<IEnumerable<ReservaViewModel>> GetReservas()
        {
            var reserva =  await _context.Reservas.ToListAsync();

            return reserva.Select(c => new ReservaViewModel
            {
                IdReserva = c.idreserva,
                IdCowork = c.idcowork,
                Nombre = c.nombre,
                Email = c.email,
                Horas = c.horas,
                Fecha = c.fechainicio,
                Mensaje = c.mensaje,
                indice = c.indice
            });
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(new ReservaViewModel 
            {
                IdReserva = reserva.idreserva,
                IdCowork = reserva.idcowork,
                Nombre = reserva.nombre,
                Email = reserva.email,
                Horas = reserva.horas,
                Fecha = reserva.fechainicio,
                Mensaje = reserva.mensaje,
                indice = reserva.indice
            });
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut()]
        public async Task<IActionResult> PutReserva(ReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.IdReserva <= 0)
                return BadRequest();

            var reserva = await _context.Reservas.FirstOrDefaultAsync(c => c.idreserva == model.IdReserva);

            if (reserva == null)
                return NotFound();

            reserva.idcowork = model.IdCowork;
            reserva.nombre = model.Nombre;
            reserva.email = model.Email;
            reserva.horas = model.Horas;
            reserva.fechainicio = model.Fecha;
            reserva.mensaje = model.Mensaje;
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

        // POST: api/Reservas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(ReservaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Reserva reserva = new Reserva
            {
                idcowork = model.IdCowork,
                nombre = model.Nombre,
                email = model.Email,
                horas = model.Horas,
                fechainicio = model.Fecha,
                mensaje = model.Mensaje,
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

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reserva>> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return reserva;
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.idreserva == id);
        }
    }
}
