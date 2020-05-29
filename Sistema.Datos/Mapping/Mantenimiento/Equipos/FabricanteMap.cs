using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

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
