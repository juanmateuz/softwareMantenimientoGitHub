using Sistema.Contrato;
using Sistema.Dato;
using Sistema.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Control
{
    public class ControlRepuesto : IControlRepuesto
    {
        IDatosRepuesto datos;

        public ControlRepuesto()
        {
            datos = new DatosRepuesto();
        }
        public void Crear(Repuesto usuario)
        {
            
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Repuesto> Listar()
        {
            throw new NotImplementedException();
        }

        public Repuesto Mostrar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
