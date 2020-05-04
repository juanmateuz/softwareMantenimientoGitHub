using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Almacen
{
    public class MantenimientoMap : IEntityTypeConfiguration<Mantenimiento>
    {
        public void Configure(EntityTypeBuilder<Mantenimiento> builder)
        {
            builder.ToTable("solicitud_mante")
            .HasKey(m => m.idsolicitud);
        }
    }
}
