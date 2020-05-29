using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Controllers.Models.Almacen.Equipo
{
    public class CrearViewModel
    {
        [Required]       
        public int idfabricante { get; set; }
        [Required]
        public string serie { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string marca { get; set; }
        public string voltaje { get; set; }
        public decimal costo { get; set; }
        public DateTime year_adquisicion { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
