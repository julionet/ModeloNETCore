using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class DominioItemMapping : EntityTypeConfiguration<DominioItem>
    {
        private const string tableName = "DominioItem";

        public override void Map(EntityTypeBuilder<DominioItem> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Valor).HasMaxLength(10).IsRequired();
            builder.Property(c => c.DominioId).IsRequired();
            builder.HasOne(c => c.Dominio).WithMany(c => c.DominioItem).HasForeignKey(c => c.DominioId);
        }
    }
}
