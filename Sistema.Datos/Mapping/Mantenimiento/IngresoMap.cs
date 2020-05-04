using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class IngresoMap : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            //mapeamiento table ingreso
            builder.ToTable("ingreso")
            .HasKey(i => i.idingreso);
            //relacion con la entidad persona no por idpersona sino por idproveedor
            builder.HasOne(i => i.persona) //relacion ingreso que tiene una entidad persona
            .WithMany(p => p.ingresos) // mi tabla persona puede tener collecion de ingresos
            .HasForeignKey(i => i.idproveedor);
            //relaciono ingreso con la entidad persona mediante el campo idproveedor
        }
    }
}
