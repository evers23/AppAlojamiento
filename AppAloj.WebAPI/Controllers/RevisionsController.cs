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
                IdRevision = c.IdRevision,
                Nombre = c.Nombre,
                Email = c.Email,
                Fecha = c.Fecha,
                Mensaje = c.Mensaje,
                IdReserva = c.IdReserva,
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
                IdRevision = revision.IdRevision,
                Nombre = revision.Nombre,
                Email = revision.Email,
                Fecha = revision.Fecha,
                Mensaje = revision.Mensaje,
                IdReserva = revision.IdReserva,
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

            var revision = await _context.Revisions.FirstOrDefaultAsync(c => c.IdRevision == model.IdRevision);

            if (revision == null)
                return NotFound();

            revision.Nombre = model.Nombre;
            revision.Email = model.Email;
            revision.Fecha = model.Fecha;
            revision.Mensaje = model.Mensaje;
            revision.IdReserva = model.IdReserva;
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
                Nombre = model.Nombre,
                Email = model.Email,
                Fecha = model.Fecha,
                Mensaje = model.Mensaje,
                IdReserva = model.IdReserva,
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
            return _context.Revisions.Any(e => e.IdRevision == id);
        }
    }
}
