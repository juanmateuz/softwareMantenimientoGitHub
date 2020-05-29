using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Sistema.Entidades.Almacen
{
    public class Distribuidor //1
    {
     //distribuidor  
     public int iddistribuidor { get; set; }    
     [StringLength(50, MinimumLength =3,ErrorMessage ="El nombre no debe tener mas de 50 caracteres, ni menos de 3")]
     public string nombre { get; set; }
     [Required]
     public string ciudad { get; set; }
     [Required]
     public int telefono { get; set; }
     public string email { get; set; }    
     public ICollection<Repuesto> repuestos { get; set; }  // Distribuidor tiene unac coleccion de repuestos OK
     //public Repuesto categoria { get; set; } //estoy haciendo estado a la clase categoria  
    }
}

