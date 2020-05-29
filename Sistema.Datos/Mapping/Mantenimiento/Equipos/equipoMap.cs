using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    class equipoMap : IEntityTypeConfiguration<Equipos>
    {
        public void Configure(EntityTypeBuilder<Equipos> builder)
        {
            builder.ToTable("equipo")
            .HasKey(e => e.idequipos);
        }
    }
}
