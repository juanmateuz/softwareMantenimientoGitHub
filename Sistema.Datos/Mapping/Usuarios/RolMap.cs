using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Usuarios
{
    public class RolMap : IEntityTypeConfiguration<Rol>//mapeamos entidad rol con la tabla rol
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("roles")
            .HasKey(r => r.idrol);
            //en db context sistema aplicamos la configuracion     
        }
    }
}
