﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class ReservaViewModel
    {
        public int idreserva { get; set; }

        public string nombre { get; set; }

        public string email { get; set; }

        public int horas { get; set; }

        public DateTime fechainicio { get; set; }

        public DateTime fechafin { get; set; }

        public string mensaje { get; set; }

        public int idcowork { get; set; }

        public string cowork { get; set; }

        public string indice { get; set; }
    }
}
