using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class RelatorioMapping : EntityTypeConfiguration<Relatorio>
    {
        private const string tableName = "Relatorio";

        public override void Map(EntityTypeBuilder<Relatorio> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Nome).HasMaxLength(80).IsRequired();
            builder.Property(c => c.Origem).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Tamanho).IsRequired();
            builder.Property(c => c.Modificado).IsRequired();
            builder.Property(c => c.Modelo);
            builder.Property(c => c.Parametro);
            builder.Property(c => c.Matricial).IsRequired();
            builder.Property(c => c.QuebraPagina).IsRequired();
            builder.Property(c => c.GraficoTexto).IsRequired();
            builder.Property(c => c.LinhaBranco).IsRequired();
            builder.Property(c => c.Visualizar).IsRequired();
            builder.Property(c => c.EscalaX).IsRequired();
            builder.Property(c => c.EscalaY).IsRequired();
        }
    }
}
