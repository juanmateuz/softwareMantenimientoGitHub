using System;

namespace Sistema.Web.Controllers.Models.Almacen.Ingreso
{
    public class IngresoViewModel  //obtiene datos y los envia a la vista
    {
        public int idingreso { get; set; }  
        public int idproveedor { get; set; }
        public string proveedor { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }        
        public string num_comprobante { get; set; }       
        public DateTime fecha_hora { get; set; }        
        public decimal impuesto { get; set; }        
        public decimal total { get; set; }        
        public string estado { get; set; }
    }
}
