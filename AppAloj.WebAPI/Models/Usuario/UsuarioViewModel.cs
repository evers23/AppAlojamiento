using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models
{
    public class UsuarioViewModel
    {
        public int idusuario { get; set; }

        public int idtipousuario { get; set; }

        public string tipousuario { get; set; }

        public string rut { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string email { get; set; }

        public byte[] passwordhash { get; set; }

        public bool condicion { get; set; }
    }
}
