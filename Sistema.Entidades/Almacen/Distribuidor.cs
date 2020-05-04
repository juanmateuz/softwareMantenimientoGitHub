using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Sistema.Entidades.Almacen
{
    public class Distribuidor
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

        // public Repuesto categoria { get; set; } //estoy haciendo estado a la clase categoria
        public ICollection<Repuesto> repuesto { get; set; }  // distribuidor tiene coleccion de repuestos

     /*
     public int idarticulo { get; set; }
     [Required]
     public int idcategoria { get; set; }
     public string codigo { get; set; }
     [StringLength(50, MinimumLength =3,ErrorMessage ="El nombre no debe tener mas de 50 caracteres, ni menos de 3")]
     public string nombre { get; set; }
     [Required]
     public decimal precio_venta { get; set; }
     [Required]
     public int stock { get; set; }
     public string descripcion { get; set; }
     public bool condicion { get; set; }
     public Repuesto categoria { get; set; } //estoy haciendo estado a la clase categoria
      */
    }

}
