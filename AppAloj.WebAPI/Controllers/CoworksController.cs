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
    [Route("api/[controller]")]
    [ApiController]
    public class CoworksController : ControllerBase
    {
        private readonly AppAlojDbContext _context;

        public CoworksController(AppAlojDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador,Cliente")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CoworkViewModel>> Listar()
        {
            var cowork = await _context.Coworks.Include(a => a.Categoria).ToListAsync();

            return cowork.Select(c => new CoworkViewModel
            {
                idcowork = c.idcowork,
                nombre = c.nombre,
                categoria = c.Categoria.nombre,
                dueno = c.dueno,
                descripcion = c.descripcion,
                direccion = c.direccion,
                idcategoria = c.idcategoria,
                precio = c.precio,
                foto = c.foto,
                condicion = c.condicion
            });
        }

        [Authorize(Roles = "Administrador,Cliente")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Cowork>> Mostrar(int id)
        {
            var cowork = await _context.Coworks.Include(c => c.Categoria).SingleOrDefaultAsync(a => a.idcowork == id);

            if (cowork == null)
            {
                return NotFound();
            }

            return Ok(new CoworkViewModel
            {
                idcowork = cowork.idcowork,
                nombre = cowork.nombre,
                categoria = cowork.Categoria.nombre,
                dueno = cowork.dueno,
                descripcion = cowork.descripcion,
                direccion = cowork.direccion,
                idcategoria = cowork.idcategoria,
                precio = cowork.precio,
                foto = cowork.foto,
                condicion = cowork.condicion
            });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(CoworkUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idcowork <= 0)
                return BadRequest();

            var cowork = await _context.Coworks.FirstOrDefaultAsync(c => c.idcowork == model.idcowork);

            if (cowork == null)
                return NotFound();

            cowork.nombre = model.nombre;
            cowork.dueno = model.dueno;
            cowork.descripcion = model.descripcion;
            cowork.direccion = model.direccion;
            cowork.idcategoria = model.idcategoria;
            cowork.precio = model.precio;
            cowork.foto = model.foto;

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

        [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<ActionResult<Cowork>> Crear(CoworkCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Cowork cowork = new Cowork
            {
                nombre = model.nombre,
                dueno = model.dueno,
                descripcion = model.descripcion,
                direccion = model.direccion,
                idcategoria = model.idcategoria,
                precio = model.precio,
                foto = model.foto,
                condicion = true
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

        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var cowork = await _context.Coworks.FirstOrDefaultAsync(c => c.idcowork == id);

            if (cowork == null)
                return NotFound();

            cowork.condicion = false;

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

        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var cowork = await _context.Coworks.FirstOrDefaultAsync(c => c.idcowork == id);

            if (cowork == null)
                return NotFound();

            cowork.condicion = true;

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


        private bool CoworkExists(int id)
        {
            return _context.Coworks.Any(e => e.idcowork == id);
        }
    }
}
