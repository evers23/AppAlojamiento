using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppAloj.Entidades
{
    public class Revision
    {
        [Key]
        public int idrevision { get; set; }

        [Required]
        public int idreserva { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public DateTime fecha { get; set; }

        [Required]
        public string mensaje { get; set; }

        public string indice { get; set; }

        [ForeignKey("idreserva")]
        public Reserva Reserva { get; set; }
    }
}
