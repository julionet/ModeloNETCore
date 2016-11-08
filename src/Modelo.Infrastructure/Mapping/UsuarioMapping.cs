using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure.Mapping
{
    public class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        private const string tableName = "Usuario";

        public override void Map(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(tableName);
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Login).HasMaxLength(25).IsRequired();
            builder.Property(c => c.Nome).HasMaxLength(40);
            builder.Property(c => c.Senha).HasMaxLength(10);
            builder.Property(c => c.Master).IsRequired();
            builder.Property(c => c.Bloqueado).IsRequired();
            builder.Property(c => c.Administrador).IsRequired();
            builder.Property(c => c.NuncaExpira).IsRequired();
            builder.Property(c => c.AlterarSenha).IsRequired();
            builder.Property(c => c.DiasExpirar);
            builder.Property(c => c.DataAlteracao);
            builder.Property(c => c.FuncionarioId);
            builder.Ignore(c => c.ListaPerfis);
        }
    }
}
