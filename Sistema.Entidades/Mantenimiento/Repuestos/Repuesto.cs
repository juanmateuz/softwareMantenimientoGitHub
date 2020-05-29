//Clase categoria sirve para restringir entrada a la base de datos
using System.ComponentModel.DataAnnotations;//validar campos y longitud

namespace Sistema.Entidades.Almacen // clase categoria pertenece proyecto entidades
{                                  
    public class Repuesto //*
    {
        //colocamos campos de la bd cambio por repuestos     
        public int idrepuestos { get; set; }
        public int iddistribuidor { get; set; }        
        public string tipo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener mas de 50 caracteres")]
        public string nombre { get; set; }
        [StringLength(256)] 
        public string referencia { get; set; }
        public int cantidad { get; set; }
        public int stockminimo { get; set; }       
        public bool correoEnviado { get; set;}
        public Distribuidor distribuidor { get; set; }//Un distribuidor tiene muchos repuestos "llave foranea" OK        
        // public bool estado { get; set; } 
        // public ICollection<Distribuidor> articulos { get;set; } ejemplo de uno a muchos
    }
}
