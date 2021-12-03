using System.ComponentModel.DataAnnotations;

namespace AppAloj.WebAPI.Models.Usuario
{
    public class UsuarioUpdateViewModel
    {
        [Required]
        public int idusuario { get; set; }

        [Required]
        public int idtipousuario { get; set; }

        [Required]
        public string rut { get; set; }

        [Required]
        public string nombre { get; set; }

        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        public bool pass_update { get; set; }

    }
}
