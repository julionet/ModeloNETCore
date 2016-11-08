using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class UsuarioFuncaoMapping : EntityTypeConfiguration<UsuarioFuncao>
    {
        private const string tableName = "UsuarioFuncao";

        public override void Map(EntityTypeBuilder<UsuarioFuncao> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.UsuarioId).IsRequired();
            builder.Property(c => c.FuncaoId).IsRequired();
            builder.Property(c => c.PermiteIncluir).IsRequired();
            builder.Property(c => c.PermiteAlterar).IsRequired();
            builder.Property(c => c.PermiteExcluir).IsRequired();
            builder.HasOne(c => c.Usuario).WithMany(c => c.UsuarioFuncao).HasForeignKey(c => c.UsuarioId);
            builder.HasOne(c => c.Funcao).WithMany(c => c.UsuarioFuncao).HasForeignKey(c => c.FuncaoId);
        }
    }
}
