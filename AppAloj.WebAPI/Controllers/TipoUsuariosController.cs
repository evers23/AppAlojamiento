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
    public class TipoUsuariosController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public TipoUsuariosController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoUsuarios
        [HttpGet]
        public async Task<IEnumerable<TipoUsuarioViewModel>> GetTipoUsuarios()
        {
            var tipoUsuario = await _context.TipoUsuarios.ToListAsync();

            return tipoUsuario.Select(c => new TipoUsuarioViewModel
            {
                IdTipoUsuario = c.IdTipoUsuario,
                Nombre = c.Nombre
            });
        }

        // GET: api/TipoUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> GetTipoUsuario(int id)
        {
            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return Ok(new TipoUsuarioViewModel 
            {
                IdTipoUsuario = tipoUsuario.IdTipoUsuario,
                Nombre = tipoUsuario.Nombre
            });
        }

        // PUT: api/TipoUsuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutTipoUsuario(TipoUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.IdTipoUsuario <= 0)
                return BadRequest();

            var tipoUsuario = await _context.TipoUsuarios.FirstOrDefaultAsync(c => c.IdTipoUsuario == model.IdTipoUsuario);

            if (tipoUsuario == null)
                return NotFound();

            tipoUsuario.Nombre = model.Nombre;

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

        // POST: api/TipoUsuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> PostTipoUsuario(TipoUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TipoUsuario tipoUsuario = new TipoUsuario
            {
                Nombre = model.Nombre
            };

            _context.TipoUsuarios.Add(tipoUsuario);

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

        // DELETE: api/TipoUsuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoUsuario>> DeleteTipoUsuario(int id)
        {
            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            _context.TipoUsuarios.Remove(tipoUsuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(tipoUsuario);
        }

        private bool TipoUsuarioExists(int id)
        {
            return _context.TipoUsuarios.Any(e => e.IdTipoUsuario == id);
        }
    }
}
