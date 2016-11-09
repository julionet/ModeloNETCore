using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class AtualizacaoMapping : EntityTypeConfiguration<Atualizacao>
    {
        private const string tableName = "Atualizacao";

        public override void Map(EntityTypeBuilder<Atualizacao> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Numero).IsRequired();
            builder.Property(c => c.Data).IsRequired();
            builder.Property(c => c.Descricao).IsRequired();
            builder.Property(c => c.Versao).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Banco).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Sql);
            builder.Property(c => c.SqlProcedimento).IsRequired();
            builder.Property(c => c.Status).HasMaxLength(1).IsRequired();
            builder.Property(c => c.Mensagem);
        }
    }
}
