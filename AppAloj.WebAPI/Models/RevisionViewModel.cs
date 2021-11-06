using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class RevisionViewModel
    {
        public int IdRevision { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public DateTime Fecha { get; set; }

        public string Mensaje { get; set; }

        public int IdReserva { get; set; }

        public string indice { get; set; }
    }
}
