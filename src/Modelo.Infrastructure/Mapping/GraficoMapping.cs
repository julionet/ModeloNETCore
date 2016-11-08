using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class GraficoMapping : EntityTypeConfiguration<Grafico>
    {
        private const string tableName = "Grafico";

        public override void Map(EntityTypeBuilder<Grafico> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Codigo).HasMaxLength(10).IsRequired();
            builder.Property(c => c.Nome).HasMaxLength(80).IsRequired();
            builder.Property(c => c.Descricao);
            builder.Property(c => c.Parametro);
        }
    }
}
