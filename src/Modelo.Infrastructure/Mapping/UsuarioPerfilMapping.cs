using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class UsuarioPerfilMapping : EntityTypeConfiguration<UsuarioPerfil>
    {
        private const string tableName = "UsuarioPerfil";

        public override void Map(EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => new { p.UsuarioId, p.PerfilId });

            builder.Property(p => p.UsuarioId).IsRequired();
            builder.Property(p => p.PerfilId).IsRequired();
            builder.HasOne(p => p.Usuario).WithMany(p => p.UsuarioPerfil).HasForeignKey(p => p.UsuarioId);
            builder.HasOne(p => p.Perfil).WithMany(p => p.UsuarioPerfil).HasForeignKey(p => p.PerfilId);
        }
    }
}
