using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers.Models.Almacen.Articulo
{
    //modelo que refleja que columnas muestro en el listado
    public class ArticuloViewModel
    {
        public int iddistribuidor { get; set;}
        public string nombre { get; set; }
        public string ciudad { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }

        /*
        public int idarticulo { get; set; }
        public int idcategoria { get; set; }
        public string categoria{ get; set; }//almacenamos nombre categoria con el id categoria
        public string codigo { get; set; }       
        public string nombre { get; set; }    
        public decimal precio_venta { get; set; } 
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    */
    }
}
