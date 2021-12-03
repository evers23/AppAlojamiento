using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models.TipoUsuario
{
    public class TipoUsuarioUpdateViewModel
    {
        public int idtipousuario { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }
    }
}
