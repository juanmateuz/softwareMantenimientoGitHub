using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers.Models.Almacen.Articulo
{
    public class CrearViewModel
    {
        
        public string nombre { get; set; }
        public string ciudad { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }

        //[Required]
        //public int idcategoria { get; set; }
        //[Required]
        //public string codigo { get; set; }
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
        //public string nombre { get; set; }
        //public decimal precio_venta { get; set; }
        //[Required]
        //public int stock { get; set; }
        //public string descripcion { get; set; }
    }
}
