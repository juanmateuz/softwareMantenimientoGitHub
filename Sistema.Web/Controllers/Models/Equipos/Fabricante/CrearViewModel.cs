using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Controllers.Models.Ventas.Fabricante
{
    public class CrearViewModel
    {

        [Required]
        public string nombre { get; set; }
        [Required]
        public string pais { get; set; }
        [Required]
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
