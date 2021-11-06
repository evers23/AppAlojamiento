using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class CoworkViewModel
    {
        public int IdCowork { get; set; }

        public string Nombre { get; set; }

        public string Dueno { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; }

        public int IdCategoria { get; set; }

        public decimal Precio { get; set; }

        public string Foto { get; set; }
    }
}
