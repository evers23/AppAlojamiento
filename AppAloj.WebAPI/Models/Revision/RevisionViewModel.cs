﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class RevisionViewModel
    {
        public int idrevision { get; set; }

        public string nombre { get; set; }

        public string email { get; set; }

        public DateTime fecha { get; set; }

        public string mensaje { get; set; }

        public int idreserva { get; set; }

        public string reserva { get; set; }

        public string indice { get; set; }
    }
}
