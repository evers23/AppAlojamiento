using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAloj.Entidades
{
    public class TipoUsuario
    {
        [Key]
        public int idtipousuario { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        [StringLength(256)]
        public string descripcion { get; set; }

        public bool condicion { get; set; }


        public ICollection<Usuario> Usuarios { get; set; }
    }
}
