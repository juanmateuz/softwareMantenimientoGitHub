using Sistema.Contrato;
using Sistema.Datos;
using Sistema.Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Dato
{
    public class DatosRepuesto : IDatosRepuesto
    {
        DbContextSistema BaseDatos;

        public DatosRepuesto()
        {
        }

        public DatosRepuesto(DbContextSistema context)
        {
            BaseDatos = context;
        }       

        public void Crear(Repuesto usuario)
        {
            throw new NotImplementedException();
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
