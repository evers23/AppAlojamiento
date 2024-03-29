﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class CoworkViewModel
    {
        public int idcowork { get; set; }

        public string nombre { get; set; }

        public string dueno { get; set; }

        public string descripcion { get; set; }

        public string direccion { get; set; }

        public int idcategoria { get; set; }

        public string categoria { get; set; }

        public decimal precio { get; set; }

        public string foto { get; set; }

        public bool condicion { get; set; }
    }
}
