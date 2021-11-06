using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppAloj.Entidades
{
    public class TipoUsuario
    {
        [Key]
        public int IdTipoUsuario { get; set; }

        [Required]
        public string Nombre { get; set; }


        public ICollection<Usuario> Usuarios { get; set; }
    }
}
