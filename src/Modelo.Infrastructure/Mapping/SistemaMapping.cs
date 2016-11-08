using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class SistemaMapping : EntityTypeConfiguration<Sistema>
    {
        private const string tableName = "Sistema";

        public override void Map(EntityTypeBuilder<Sistema> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Imagem);
            builder.Property(c => c.Tipo).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Interface).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Linha).IsRequired();
            builder.Property(c => c.Tamanho).IsRequired();
            builder.Property(c => c.Gerenciador).IsRequired();
            builder.Property(c => c.Ativo).IsRequired();
            builder.Ignore(c => c.QuantidadeModulo);
        }
    }
}
