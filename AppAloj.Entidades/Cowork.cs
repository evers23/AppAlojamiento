using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppAloj.Entidades
{
    public class Cowork
    {
        [Key]
        public int IdCowork { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Dueno { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        [Required]
        public decimal Precio { get; set; }

        public string Foto { get; set; }


        public Categoria Categoria { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
