using Sistema.Entidades.Almacen;
using System.Collections.Generic;

namespace Sistema.Contrato
{
    public interface IControlRepuesto
    {
        void Crear(Repuesto usuario);
        Repuesto Mostrar(int id);
        List<Repuesto> Listar();
        void Eliminar(int id);
    }
}
