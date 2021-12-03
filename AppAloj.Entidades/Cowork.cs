using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppAloj.Entidades
{
    public class Cowork
    {
        [Key]
        public int idcowork { get; set; }

        [Required]
        public int idcategoria { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string dueno { get; set; }

        public string descripcion { get; set; }

        [Required]
        public string direccion { get; set; }

        [Required]
        public decimal precio { get; set; }

        public string foto { get; set; }

        public bool condicion { get; set; }

        [ForeignKey("idcategoria")]
        public Categoria Categoria { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
