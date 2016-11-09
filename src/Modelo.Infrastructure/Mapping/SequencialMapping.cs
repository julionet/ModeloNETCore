using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class SequencialMapping : EntityTypeConfiguration<Sequencial>
    {
        private const string tableName = "Sequencial";

        public override void Map(EntityTypeBuilder<Sequencial> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Nome).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Valor).IsRequired();
        }
    }
}
