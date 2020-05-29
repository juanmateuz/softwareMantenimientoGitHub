using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Controllers.Models.Almacen.Repuesto
{
    public class CrearViewModel
    {
        public int iddistribuidor { get; set; }        
        public string tipo { get; set; }
        [Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
        public string nombre { get; set; }      
        public string referencia { get; set; }
        public int cantidad { get; set; }
        public int stockminimo { get; set; }
        public bool correoEnviado { get; set; }

        //idrepuestos no es necesario porque se genera automaticamente
        /*  [Required]
          [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
          public string nombre { get; set; }
          [StringLength(256)]
          public string descripcion { get; set; }
          public bool estado { get; set; } */
    }
}
