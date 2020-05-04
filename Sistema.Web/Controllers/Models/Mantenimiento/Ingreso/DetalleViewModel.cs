using System;
using System.ComponentModel.DataAnnotations;


namespace Sistema.Web.Controllers.Models.Almacen.Ingreso
{
    public class DetalleViewModel// un ingreso puede tener varios detalles
    {
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
    }
}
