using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class ModuloMapping : EntityTypeConfiguration<Modulo>
    {
        private const string tableName = "Modulo";

        public override void Map(EntityTypeBuilder<Modulo> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Codigo).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Grupo).HasMaxLength(40);
            builder.Property(c => c.Cor).IsRequired();
            builder.Property(c => c.Imagem);
            builder.Property(c => c.Administracao).IsRequired();
            builder.Property(c => c.Navegacao).IsRequired();
            builder.Property(c => c.SistemaId).IsRequired();
            builder.HasOne(c => c.Sistema).WithMany(c => c.Modulo).HasForeignKey(c => c.SistemaId);
            builder.Ignore(c => c.Flag);
            builder.Ignore(c => c.QuantidadeFuncao);
        }
    }
}
