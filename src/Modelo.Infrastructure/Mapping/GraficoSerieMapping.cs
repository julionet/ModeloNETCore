using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class GraficoSerieMapping : EntityTypeConfiguration<GraficoSerie>
    {
        private const string tableName = "GraficoSerie";

        public override void Map(EntityTypeBuilder<GraficoSerie> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Nome).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Tipo).HasMaxLength(2).IsRequired();
            builder.Property(c => c.Query).IsRequired();
            builder.Property(c => c.Serie).HasMaxLength(60);
            builder.Property(c => c.Argumento).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Valor).HasMaxLength(60).IsRequired();
            builder.Property(c => c.GraficoId).IsRequired();
            builder.HasOne(c => c.Grafico).WithMany(c => c.GraficoSerie).HasForeignKey(c => c.GraficoId);
        }
    }
}
