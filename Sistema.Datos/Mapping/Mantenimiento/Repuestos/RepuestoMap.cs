using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class RepuestoMap : IEntityTypeConfiguration<Repuesto>
    {
        public void Configure(EntityTypeBuilder<Repuesto> builder)
        {
            builder.ToTable("repuesto")  //mappeamos el nombre de la tabla
            .HasKey(c => c.idrepuestos);//campo en la bd es necesario el nombre tabla y su llave primaria
            //builder.Property(c => c.estado);
            //builder.HasOne(i => i.persona) //relacion ingreso que tiene una entidad persona
            // .WithMany(p => p.ingresos) // mi tabla persona puede tener collecion de ingresos
            // .HasForeignKey(i => i.idproveedor);
            ////relaciono ingreso con la entidad persona mediante el campo idproveedor
        }
    }
}
