﻿using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Almacen
{
    public class Fabricante
    {
        public int idfabricante { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string pais { get; set; }
        [Required]
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
