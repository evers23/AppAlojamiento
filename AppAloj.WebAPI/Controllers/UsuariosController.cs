using AppAloj.Datos;
using AppAloj.Entidades;
using AppAloj.WebAPI.Models;
using AppAloj.WebAPI.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppAlojDbContext _context;
        private readonly IConfiguration _config;

        public UsuariosController(AppAlojDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(a => a.TipoUsuario).ToListAsync();

            return usuario.Select(c => new UsuarioViewModel
            {
                idusuario = c.idusuario,
                idtipousuario = c.idtipousuario,
                tipousuario = c.TipoUsuario.nombre,
                rut = c.rut,
                nombre = c.nombre,
                apellido = c.apellido,
                email = c.email,
                passwordhash = c.passwordhash,
                condicion = c.condicion
            });
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Usuario>> Mostrar(int id)
        {
            var usuario = await _context.Usuarios.Include(c => c.TipoUsuario).SingleOrDefaultAsync(a => a.idusuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioViewModel
            {
                idusuario = usuario.idusuario,
                idtipousuario = usuario.idtipousuario,
                tipousuario = usuario.TipoUsuario.nombre,
                rut = usuario.rut,
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                email = usuario.email,
                passwordhash = usuario.passwordhash,
                condicion = usuario.condicion
            });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(UsuarioUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idusuario <= 0)
                return BadRequest();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.idusuario == model.idusuario);

            if (usuario == null)
                return NotFound();

            usuario.idtipousuario = model.idtipousuario;
            usuario.rut = model.rut;
            usuario.nombre = model.nombre;
            usuario.apellido = model.apellido;
            usuario.email = model.email;

            if (model.pass_update == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.passwordhash = passwordHash;
                usuario.passwordsalt = passwordSalt;
            }


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
        public async Task<ActionResult<Usuario>> Crear(UsuarioCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = model.email.ToLower();
            if (await _context.Usuarios.AnyAsync(u => u.email == email))
                return BadRequest("Email usado por otro usuario.");

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
            Usuario usuario = new Usuario
            {
                idtipousuario = model.idtipousuario,
                rut = model.rut,
                nombre = model.nombre,
                apellido = model.apellido,
                email = model.email.ToLower(),
                passwordhash = passwordHash,
                passwordsalt = passwordSalt,
                condicion = true
            };

            _context.Usuarios.Add(usuario);

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

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.idusuario == id);

            if (usuario == null)
                return NotFound();

            usuario.condicion = false;

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

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(c => c.idusuario == id);

            if (usuario == null)
                return NotFound();

            usuario.condicion = true;

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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.Email.ToLower();
            var usuario = await _context.Usuarios.Where(u => u.condicion == true).Include(u => u.TipoUsuario).FirstOrDefaultAsync(u => u.email == email);

            if (usuario == null)
                return NotFound();

            if (!VerificarPassword(model.password, usuario.passwordhash, usuario.passwordsalt))
                return NotFound();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.nombre),
                new Claim("idusuario", usuario.idusuario.ToString()),
                new Claim("tipousuario", usuario.TipoUsuario.nombre),
                new Claim("nombre", usuario.nombre)
            };

            return Ok(new { token = generarToken(claims) });

        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idusuario == id);
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarPassword(string password, byte[] passwordAlmacenadoHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordNuevoHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordAlmacenadoHash).SequenceEqual(new ReadOnlySpan<byte>(passwordNuevoHash));
            }
        }

        private string generarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
