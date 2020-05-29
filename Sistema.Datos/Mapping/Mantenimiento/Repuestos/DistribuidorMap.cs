using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;//se encarga de mapear la entidad articulo con la tabla articulo de la bd
namespace Sistema.Datos.Mapping.Almacen
{
    public class DistribuidorMap : IEntityTypeConfiguration<Distribuidor>//sea una configuracion de entity fram.. y el tipo es entidad articulo
    {
        public void Configure(EntityTypeBuilder<Distribuidor> builder)
        {
            //mapeamento de la tabla
            builder.ToTable("distribuidores")
            .HasKey(a => a.iddistribuidor);
        }
    }
}