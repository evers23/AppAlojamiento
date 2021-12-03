using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppAloj.Entidades
{
    public class Categoria
    {
        [Key]
        public int idcategoria { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nombre debe tener entre 3 y 50 catacteres")]
        public string nombre { get; set; }

        [StringLength(256)]
        public string descripcion { get; set; }

        public bool condicion { get; set; }


        public ICollection<Cowork> Coworks { get; set; }
    }
}
