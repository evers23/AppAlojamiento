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
    public class RevisionsController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public RevisionsController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet("[action]")]
        public async Task<IEnumerable<RevisionViewModel>> Listar()
        {
            var revision = await _context.Revisions.Include(a => a.Reserva).ToListAsync();

            return revision.Select(c => new RevisionViewModel
            {
                idrevision  =c.idrevision,
                idreserva = c.idreserva,
                reserva = c.Reserva.nombre,
                nombre = c.nombre,
                email = c.email,
                fecha = c.fecha,
                mensaje = c.mensaje,
                indice = c.indice
            });
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Revision>> Mostrar(int id)
        {
            var revision = await _context.Revisions.Include(c => c.Reserva).SingleOrDefaultAsync(a => a.idrevision == id);

            if (revision == null)
            {
                return NotFound();
            }

            return Ok(new RevisionViewModel
            {
                idrevision = revision.idrevision,
                idreserva = revision.idreserva,
                reserva = revision.Reserva.nombre,
                nombre = revision.nombre,
                email = revision.email,
                fecha = revision.fecha,
                mensaje = revision.mensaje,
                indice = revision.indice
            });
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(RevisionUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idrevision <= 0)
                return BadRequest();

            var revision = await _context.Revisions.FirstOrDefaultAsync(c => c.idrevision == model.idrevision);

            if (revision == null)
                return NotFound();

            revision.idreserva = model.idreserva;
            revision.nombre = model.nombre;
            revision.email = model.email;
            revision.fecha = model.fecha;
            revision.mensaje = model.mensaje;
            revision.indice = model.indice;

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
        public async Task<ActionResult<Revision>> Crear(RevisionCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Revision revision = new Revision
            {
                idreserva = model.idreserva,
                nombre = model.nombre,
                email = model.email,
                fecha = model.fecha,
                mensaje = model.mensaje,
                indice = model.indice
            };

            _context.Revisions.Add(revision);

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

        private bool RevisionExists(int id)
        {
            return _context.Revisions.Any(e => e.idrevision == id);
        }
    }
}
