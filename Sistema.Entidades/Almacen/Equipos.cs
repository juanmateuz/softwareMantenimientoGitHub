using System;

namespace Sistema.Entidades.Almacen
{
    public class Equipos
    {
        public int idequipos { get; set; }
        public int idfabricante { get; set; }
        public string serie { get; set; }
        public string nombre  { get; set; }
        public string marca { get; set; }
        public string voltaje { get; set; }
        public decimal costo { get; set; }
        public DateTime year_adquisicion { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }

     //   public Fabricante fabricante { get; set; }
    }
}
