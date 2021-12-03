using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models.TipoUsuario
{
    public class TipoUsuarioCreateViewModel
    {
        public string nombre { get; set; }

        public string descripcion { get; set; }

        public bool condicion { get; set; }
    }
}
