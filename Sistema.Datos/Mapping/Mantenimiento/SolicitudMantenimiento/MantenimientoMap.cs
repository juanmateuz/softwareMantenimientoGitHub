using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Mantenimiento;

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
