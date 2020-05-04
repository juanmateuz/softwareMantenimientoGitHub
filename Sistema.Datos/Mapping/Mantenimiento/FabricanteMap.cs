using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Datos.Mapping.Ventas;
using Sistema.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Datos.Mapping.Almacen
{
    public class FabricanteMap : IEntityTypeConfiguration<Fabricante>
    {
        //IEntityTypeConfiguration<DetalleIngreso>
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {
            builder.ToTable("fabricantes")
            .HasKey(f => f.idfabricante);
        }
    }
}
