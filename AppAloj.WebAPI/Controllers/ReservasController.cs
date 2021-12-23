using AppAloj.Datos;
using AppAloj.Entidades;
using AppAloj.WebAPI.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Controllers
{
    [Authorize(Roles = "Administrador,Cliente")]
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
        [HttpGet("[action]")]
        public async Task<IEnumerable<ReservaViewModel>> Listar()
        {
            var reservas = await _context.Reservas.Include(a => a.Cowork).ToListAsync();

            return reservas.Select(c => new ReservaViewModel
            {
                idreserva = c.idreserva,
                idcowork = c.idcowork,
                cowork = c.Cowork.nombre,
                nombre = c.nombre,
                email = c.email,
                horas = c.horas,
                fechainicio = c.fechainicio,
                fechafin = c.fechafin,
                mensaje = c.mensaje,
                indice = c.indice
            });
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Reserva>> Mostrar(int id)
        {
            var reserva = await _context.Reservas.Include(c => c.Cowork).SingleOrDefaultAsync(a => a.idreserva == id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(new ReservaViewModel
            {
                idreserva = reserva.idreserva,
                idcowork = reserva.idcowork,
                cowork = reserva.Cowork.nombre,
                nombre = reserva.nombre,
                email = reserva.email,
                horas = reserva.horas,
                fechainicio = reserva.fechainicio,
                fechafin = reserva.fechafin,
                mensaje = reserva.mensaje,
                indice = reserva.indice
            });
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Editar(ReservaUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.idreserva <= 0)
                return BadRequest();

            var reserva = await _context.Reservas.FirstOrDefaultAsync(c => c.idreserva == model.idreserva);

            if (reserva == null)
                return NotFound();

            reserva.idcowork = model.idcowork;
            reserva.nombre = model.nombre;
            reserva.email = model.email;
            reserva.horas = model.horas;
            reserva.fechainicio = model.fechainicio;
            reserva.fechafin = model.fechafin;
            reserva.mensaje = model.mensaje;
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

        [HttpPost("[action]")]
        public async Task<ActionResult<Reserva>> Crear(ReservaCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Reserva reserva = new Reserva
            {
                idcowork = model.idcowork,
                nombre = model.nombre,
                email = model.email,
                horas = model.horas,
                fechainicio = model.fechainicio,
                fechafin = model.fechafin,
                mensaje = model.mensaje,
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

        /*
        public void Send(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("darkgravem@gmail.com", "DarkDr34m");
            smtp.Send(email);
            smtp.Disconnect(true);
        }*/

        /*
        [HttpPost("[action]")]
        public Task<ActionResult<Reserva>> sendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Reserva AppCowork", "darkgravem@gmail.com"));
            message.To.Add(MailboxAddress.Parse("aroblesrodrigues@gmail.com"));
            message.Subject = "Wtf";
            message.Body = new TextPart("plain")
            {
                Text = "Estimado(a) "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, false);
                client.Authenticate("darkgravem@gmail.com", "DarkDr34m");
                client.Send(message);
                client.Disconnect(true);
            }

            return Ok();
        }*/

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.idreserva == id);
        }
    }
}
