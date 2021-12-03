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

        // GET: api/Revisions
        [HttpGet]
        public async Task<IEnumerable<RevisionViewModel>> GetRevisions()
        {
            var revision = await _context.Revisions.ToListAsync();

            return revision.Select(c => new RevisionViewModel
            {
                IdRevision = c.idrevision,
                Nombre = c.nombre,
                Email = c.email,
                Fecha = c.fecha,
                Mensaje = c.mensaje,
                IdReserva = c.idreserva,
                indice = c.indice
            });
        }

        // GET: api/Revisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Revision>> GetRevision(int id)
        {
            var revision = await _context.Revisions.FindAsync(id);

            if (revision == null)
            {
                return NotFound();
            }

            return Ok(new RevisionViewModel
            {
                IdRevision = revision.idrevision,
                Nombre = revision.nombre,
                Email = revision.email,
                Fecha = revision.fecha,
                Mensaje = revision.mensaje,
                IdReserva = revision.idreserva,
                indice = revision.indice
            });
        }

        // PUT: api/Revisions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutRevision(RevisionViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.IdRevision <= 0)
                return BadRequest();

            var revision = await _context.Revisions.FirstOrDefaultAsync(c => c.idrevision == model.IdRevision);

            if (revision == null)
                return NotFound();

            revision.nombre = model.Nombre;
            revision.email = model.Email;
            revision.fecha = model.Fecha;
            revision.mensaje = model.Mensaje;
            revision.idreserva = model.IdReserva;
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

        // POST: api/Revisions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Revision>> PostRevision(RevisionViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Revision revision = new Revision
            {
                nombre = model.Nombre,
                email = model.Email,
                fecha = model.Fecha,
                mensaje = model.Mensaje,
                idreserva = model.IdReserva,
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

        // DELETE: api/Revisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Revision>> DeleteRevision(int id)
        {
            var revision = await _context.Revisions.FindAsync(id);
            if (revision == null)
            {
                return NotFound();
            }

            _context.Revisions.Remove(revision);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return revision;
        }

        private bool RevisionExists(int id)
        {
            return _context.Revisions.Any(e => e.idrevision == id);
        }
    }
}
