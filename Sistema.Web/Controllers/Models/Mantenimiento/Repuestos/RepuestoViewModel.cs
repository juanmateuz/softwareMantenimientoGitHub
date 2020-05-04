//sirve para restringir los llamados a los servicios web

namespace Sistema.Web.Controllers.Models.Almacen.Repuesto
{
    public class RepuestoViewModel
    {
        //lo que quiero que el usuario vea
        public int idrepuestos { get; set; }
        public int iddistribuidor { get; set; }
        public int idequipo { get; set; }
        public string nombreDistribuidor { get; set; }
        public string nombreEquipo { get; set; }
        public string repuesto { get; set; }
        public string tipo { get; set; }      
        public string nombre { get; set; }
        public string referencia { get; set; }
        public int cantidad { get; set; } 
        public int stockminimo { get; set; }
        public bool correoEnviado { get; set; }
    }
}
