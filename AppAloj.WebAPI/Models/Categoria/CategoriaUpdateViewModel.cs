using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAloj.WebAPI.Models.Categoria
{
    public class CategoriaUpdateViewModel
    {
        public int idcategoria { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }
    }
}
