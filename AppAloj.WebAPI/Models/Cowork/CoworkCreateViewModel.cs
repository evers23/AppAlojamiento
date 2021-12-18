using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class CoworkCreateViewModel
    {
        public string nombre { get; set; }

        public string dueno { get; set; }

        public string descripcion { get; set; }

        public string direccion { get; set; }

        [Required]
        public int idcategoria { get; set; }

        public decimal precio { get; set; }

        public string foto { get; set; }

        public bool condicion { get; set; }
    }
}
