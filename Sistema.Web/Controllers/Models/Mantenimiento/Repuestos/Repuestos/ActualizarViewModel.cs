using System.ComponentModel.DataAnnotations;//restricciones ingresar a la bd
namespace Sistema.Web.Controllers.Models.Almacen.Repuesto
{
    public class ActualizarViewModel
    {
        [Required]
        public int idrepuestos { get; set; }       
        public string tipo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string referencia { get; set; }
        public int cantidad { get; set; }
        public int stockminimo { get; set; }

        /*
        [Required]
        public int idcategoria { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool estado { get; set; } */
    }
}
