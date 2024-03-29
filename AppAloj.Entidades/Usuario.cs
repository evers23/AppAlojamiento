﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAloj.Entidades
{
    public class Usuario
    {
        [Key]
        public int idusuario { get; set; }

        [Required]
        public int idtipousuario { get; set; }

        [Required]
        public string rut { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellido { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public byte[] passwordhash { get; set; }

        [Required]
        public byte[] passwordsalt { get; set; }

        public bool condicion { get; set; }

        [ForeignKey("idtipousuario")]
        public TipoUsuario TipoUsuario { get; set; }
    }
}
