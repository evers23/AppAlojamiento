using AppAloj.Datos;
using AppAloj.Entidades;
using AppAloj.WebAPI.Models;
using AppAloj.WebAPI.Models.TipoUsuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/TipoUsuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoUsuarioViewModel>> Listar()
        {
            var tipoUsuarios = await _context.TipoUsuarios.ToListAsync();

            return tipoUsuarios.Select(c => new TipoUsuarioViewModel
            {
                idtipousuario = c.idtipousuario,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion
            });
        }

        // GET: api/TipoUsuarios/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoUsuarioSelectViewModel>> Select()
        {
            var tipoUsuarios = await _context.TipoUsuarios.Where(c => c.condicion == true).ToListAsync();

            return tipoUsuarios.Select(c => new TipoUsuarioSelectViewModel
            {
                idtipousuario = c.idtipousuario,
                nombre = c.nombre
            });
        }

        // GET: api/TipoUsuarios/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<TipoUsuario>> Mostrar(int id)
        {
            var tipoUsuario = await _context.TipoUsuarios.FindAsync(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return Ok(new TipoUsuarioViewModel
            {
                idtipousuario = tipoUsuario.idtipousuario,
                nombre = tipoUsuario.nombre,
                descripcion = tipoUsuario.descripcion,
                condicion = tipoUsuario.condicion
            });
        }

        // PUT: api/TipoUsuarios/Editar
        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(TipoUsuarioUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idtipousuario <= 0)
                return BadRequest();

            var tipoUsuario = await _context.TipoUsuarios.FirstOrDefaultAsync(c => c.idtipousuario == model.idtipousuario);

            if (tipoUsuario == null)
                return NotFound();

            tipoUsuario.nombre = model.nombre;
            tipoUsuario.descripcion = model.descripcion;

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

        // POST: api/TipoUsuarios/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult<TipoUsuario>> Crear(TipoUsuarioCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TipoUsuario tipoUsuario = new TipoUsuario
            {
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
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

        // PUT: api/TipoUsuarios/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var tipoUsuario = await _context.TipoUsuarios.FirstOrDefaultAsync(c => c.idtipousuario == id);

            if (tipoUsuario == null)
                return NotFound();

            tipoUsuario.condicion = false;

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

        // PUT: api/TipoUsuarios/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var tipoUsuario = await _context.TipoUsuarios.FirstOrDefaultAsync(c => c.idtipousuario == id);

            if (tipoUsuario == null)
                return NotFound();

            tipoUsuario.condicion = true;

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

        private bool TipoUsuarioExists(int id)
        {
            return _context.TipoUsuarios.Any(e => e.idtipousuario == id);
        }
    }
}
