using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modelo.Infrastructure.Mapping
{
    public class BloqueioMapping : EntityTypeConfiguration<Bloqueio>
    {
        private const string tableName = "Bloqueio";

        public override void Map(EntityTypeBuilder<Bloqueio> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Classe).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Usuario).HasMaxLength(60).IsRequired();
            builder.Property(c => c.Computador).HasMaxLength(60).IsRequired();
            builder.Property(c => c.DataHora).IsRequired();
            builder.Property(c => c.Referencia).IsRequired();
        }
    }
}
