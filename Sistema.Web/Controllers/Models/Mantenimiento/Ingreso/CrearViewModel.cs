using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Controllers.Models.Almacen.Ingreso
{
    public class CrearViewModel
    {
        //Propiedades maestro
        [Required]
        public int idproveedor { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }
        [Required]
        public string num_comprobante { get; set; }
        [Required]
        public decimal impuesto { get; set; }
        [Required]
        public decimal total { get; set; }
        //Propiedades detalle id articulo cantidad precio un ingreso puede tener varios detalles
        [Required]
        public List<DetalleViewModel> detalles { get; set; }

    }
}
