using System;

namespace Sistema.Web.Controllers.Models.solicitudMantenimiento
{
    public class SelectViewModel
    {
        public int idsolicitud { get; set; }
        public int idequipos { get; set; }
        public int idusuario { get; set; }
        public DateTime fechasolicitud { get; set; }
        public string equipo { get; set; }
    }
}
