﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Mantenimiento
{
    public class Mantenimiento
    {
        public IEnumerable<object> mantenimientos;
        public int idsolicitud { get; set; }
        public int idequipos{ get; set; }
        public int idusuario { get; set; }
        public DateTime fechasolicitud { get; set; }
        public string equipo { get; set; }
        public string tipomante { get; set; }
        public string solicitado_por { get; set; }
        public string recibido_por { get; set; }
        public string prioridad { get; set; }
        public string descripcion_ingeniero { get; set; }
        public string descripcion_mecanico { get; set; }
        public string estado2 { get; set; }
        public bool estado { get; set; }
        public string ejecuta { get; set; }


    }
}
