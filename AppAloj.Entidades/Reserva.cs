using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppAloj.Entidades
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Horas { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Mensaje { get; set; }

        [Required]
        public int IdCowork { get; set; }

        public string indice { get; set; }


        public Cowork Cowork { get; set; }

        public ICollection<Revision> Revisions { get; set; }
    }
}
