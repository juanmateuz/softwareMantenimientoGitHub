using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Usuarios
{
    public class Rol
    {
        public int idrol { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre debe contener entre 3 y 30 caracteres")]
        public string nombre { get; set; }
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public ICollection<Usuario> usuarios { get; set; }//para obtener los usuarios que se le han asignado el rol especifico
    }
}
