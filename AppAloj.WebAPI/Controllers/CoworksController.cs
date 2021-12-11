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
    [Authorize(Roles = "Cliente,Administrador")]
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
                IdCowork = c.idcowork,
                Nombre = c.nombre,
                Dueno = c.dueno,
                Descripcion = c.descripcion,
                Direccion = c.direccion,
                IdCategoria = c.idcategoria,
                Precio = c.precio,
                Foto = c.foto
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
                IdCowork = cowork.idcowork,
                Nombre = cowork.nombre,
                Dueno = cowork.dueno,
                Descripcion = cowork.descripcion,
                Direccion = cowork.direccion,
                IdCategoria = cowork.idcategoria,
                Precio = cowork.precio,
                Foto = cowork.foto
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

            var cowork = await _context.Coworks.FirstOrDefaultAsync(c => c.idcowork == model.IdCowork);

            if (cowork == null)
                return NotFound();

            cowork.nombre = model.Nombre;
            cowork.dueno = model.Dueno;
            cowork.descripcion = model.Descripcion;
            cowork.direccion = model.Direccion;
            cowork.idcategoria = model.IdCategoria;
            cowork.precio = model.Precio;
            cowork.foto = model.Foto;

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
                nombre = model.Nombre,
                dueno = model.Dueno,
                descripcion = model.Descripcion,
                direccion = model.Direccion,
                idcategoria = model.IdCategoria,
                precio = model.Precio,
                foto = model.Foto
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
            return _context.Coworks.Any(e => e.idcowork == id);
        }
    }
}
