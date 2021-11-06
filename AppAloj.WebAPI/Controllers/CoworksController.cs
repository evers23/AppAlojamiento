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
    public class CoworksController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public CoworksController(AppAlojDbContext context)
        {
            _context = context;
        }

        // GET: api/Coworks
        [HttpGet]
        public async Task<IEnumerable<CoworkViewModel>> GetCoworks()
        {
            var cowork = await _context.Coworks.ToListAsync();

            return cowork.Select(c => new CoworkViewModel
            {
                IdCowork = c.IdCowork,
                Nombre = c.Nombre,
                Dueno = c.Dueno,
                Descripcion = c.Descripcion,
                Direccion = c.Direccion,
                IdCategoria = c.IdCategoria,
                Precio = c.Precio,
                Foto = c.Foto
            });
        }

        // GET: api/Coworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cowork>> GetCowork(int id)
        {
            var cowork = await _context.Coworks.FindAsync(id);

            if (cowork == null)
            {
                return NotFound();
            }

            return Ok(new CoworkViewModel 
            {
                IdCowork = cowork.IdCowork,
                Nombre = cowork.Nombre,
                Dueno = cowork.Dueno,
                Descripcion = cowork.Descripcion,
                Direccion = cowork.Direccion,
                IdCategoria = cowork.IdCategoria,
                Precio = cowork.Precio,
                Foto = cowork.Foto
            });
        }

        // PUT: api/Coworks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutCowork(CoworkViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.IdCowork <= 0)
                return BadRequest();

            var cowork = await _context.Coworks.FirstOrDefaultAsync(c => c.IdCowork == model.IdCowork);

            if (cowork == null)
                return NotFound();

            cowork.Nombre = model.Nombre;
            cowork.Dueno = model.Dueno;
            cowork.Descripcion = model.Descripcion;
            cowork.Direccion = model.Direccion;
            cowork.IdCategoria = model.IdCategoria;
            cowork.Precio = model.Precio;
            cowork.Foto = model.Foto;

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

        // POST: api/Coworks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cowork>> PostCowork(CoworkViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Cowork cowork = new Cowork
            {
                Nombre = model.Nombre,
                Dueno = model.Dueno,
                Descripcion = model.Descripcion,
                Direccion = model.Direccion,
                IdCategoria = model.IdCategoria,
                Precio = model.Precio,
                Foto = model.Foto
            };

            _context.Coworks.Add(cowork);

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

        // DELETE: api/Coworks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cowork>> DeleteCowork(int id)
        {
            var cowork = await _context.Coworks.FindAsync(id);
            if (cowork == null)
            {
                return NotFound();
            }

            _context.Coworks.Remove(cowork);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return cowork;
        }

        private bool CoworkExists(int id)
        {
            return _context.Coworks.Any(e => e.IdCowork == id);
        }
    }
}
