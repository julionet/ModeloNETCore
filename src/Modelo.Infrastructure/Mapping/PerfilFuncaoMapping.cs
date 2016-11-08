using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class PerfilFuncaoMapping : EntityTypeConfiguration<PerfilFuncao>
    {
        private const string tableName = "PerfilFuncao";

        public override void Map(EntityTypeBuilder<PerfilFuncao> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.PerfilId).IsRequired();
            builder.Property(c => c.FuncaoId).IsRequired();
            builder.Property(c => c.PermiteIncluir).IsRequired();
            builder.Property(c => c.PermiteAlterar).IsRequired();
            builder.Property(c => c.PermiteExcluir).IsRequired();
            builder.HasOne(c => c.Perfil).WithMany(c => c.PerfilFuncao).HasForeignKey(c => c.PerfilId);
            builder.HasOne(c => c.Funcao).WithMany(c => c.PerfilFuncao).HasForeignKey(c => c.FuncaoId);
        }
    }
}
