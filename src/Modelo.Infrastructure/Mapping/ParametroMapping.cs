using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class ParametroMapping : EntityTypeConfiguration<Parametro>
    {
        private const string tableName = "Parametro";

        public override void Map(EntityTypeBuilder<Parametro> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Descricao).HasMaxLength(120).IsRequired();
            builder.Property(c => c.Observacao);
            builder.Property(c => c.Tipo).HasMaxLength(1).IsRequired();
            builder.Property(c => c.ValorPadrao);
            builder.Property(c => c.ValorPersonalizado);
            builder.Property(c => c.Lista);
            builder.Property(c => c.PermiteUsuario).IsRequired();
            builder.Property(c => c.Categoria).HasMaxLength(3).IsRequired();
        }
    }
}
