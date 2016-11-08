using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class ParametroUsuarioMapping : EntityTypeConfiguration<ParametroUsuario>
    {
        private const string tableName = "ParametroUsuario";

        public override void Map(EntityTypeBuilder<ParametroUsuario> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.ParametroId).IsRequired();
            builder.Property(c => c.UsuarioId).IsRequired();
            builder.Property(c => c.Valor);
            builder.HasOne(c => c.Parametro).WithMany(c => c.ParametroUsuario).HasForeignKey(c => c.ParametroId);
            builder.HasOne(c => c.Usuario).WithMany(c => c.ParametroUsuario).HasForeignKey(c => c.UsuarioId);
        }
    }
}
