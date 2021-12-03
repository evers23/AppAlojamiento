using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppAloj.Entidades
{
    public class Reserva
    {
        [Key]
        public int idreserva { get; set; }

        [Required]
        public int idcowork { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        public int horas { get; set; }

        [Required]
        public DateTime fechainicio { get; set; }

        [Required]
        public DateTime fechafin { get; set; }

        [Required]
        public string mensaje { get; set; }

        public string indice { get; set; }

        [ForeignKey("idcowork")]
        public Cowork Cowork { get; set; }

        public ICollection<Revision> Revisions { get; set; }
    }
}
