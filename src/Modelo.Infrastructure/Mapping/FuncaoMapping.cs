using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class FuncaoMapping : EntityTypeConfiguration<Funcao>
    {
        private const string tableName = "Funcao";

        public override void Map(EntityTypeBuilder<Funcao> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Grupo).HasMaxLength(40);
            builder.Property(c => c.Tipo).HasMaxLength(1).IsRequired();
            builder.Property(c => c.NomeAssembly).HasMaxLength(60);
            builder.Property(c => c.NomeFormulario).HasMaxLength(60);
            builder.Property(c => c.RelatorioId);
            builder.Property(c => c.GraficoId);
            builder.Property(c => c.Manutencao).IsRequired();
            builder.Property(c => c.ModuloId).IsRequired();
            builder.HasOne(c => c.Relatorio).WithMany(c => c.Funcao).HasForeignKey(c => c.RelatorioId);
            builder.HasOne(c => c.Grafico).WithMany(c => c.Funcao).HasForeignKey(c => c.GraficoId);
            builder.HasOne(c => c.Modulo).WithMany(c => c.Funcao).HasForeignKey(c => c.ModuloId);
            builder.Ignore(c => c.Flag);
        }
    }
}
