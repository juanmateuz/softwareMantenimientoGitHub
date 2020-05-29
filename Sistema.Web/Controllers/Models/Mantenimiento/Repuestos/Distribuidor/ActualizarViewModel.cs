
using System.ComponentModel.DataAnnotations;


namespace Sistema.Web.Controllers.Models.Almacen.Articulo
{
    public class ActualizarViewModel
    {

        public int iddistribuidor { get; set; }
        [Required]
        public string nombre { get; set; }
        public string ciudad { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }

        //public int idarticulo { get; set; }
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
