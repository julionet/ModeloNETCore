using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class DominioMapping : EntityTypeConfiguration<Dominio>
    {
        private const string tableName = "Dominio";

        public override void Map(EntityTypeBuilder<Dominio> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Descricao).HasMaxLength(60).IsRequired();
        }
    }
}
