using Microsoft.EntityFrameworkCore;
using Microsoft.Framework.Configuration;
using Modelo.Entity;
using Modelo.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure
{
    public class ModeloContext : DbContext
    {
        public ModeloContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            string setting = builder.Build()["ConnectionStrings:ModeloConnection"].ToString();
            optionsBuilder.UseSqlServer(setting);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new BloqueioMapping());
            modelBuilder.AddConfiguration(new DominioItemMapping());
            modelBuilder.AddConfiguration(new DominioMapping());
            modelBuilder.AddConfiguration(new FuncaoMapping());
            modelBuilder.AddConfiguration(new GraficoMapping());
            modelBuilder.AddConfiguration(new GraficoSerieMapping());
            modelBuilder.AddConfiguration(new ModuloMapping());
            modelBuilder.AddConfiguration(new ParametroMapping());
            modelBuilder.AddConfiguration(new ParametroUsuarioMapping());
            modelBuilder.AddConfiguration(new PerfilFuncaoMapping());
            modelBuilder.AddConfiguration(new PerfilMapping());
            modelBuilder.AddConfiguration(new RelatorioMapping());
            modelBuilder.AddConfiguration(new SistemaMapping());
            modelBuilder.AddConfiguration(new UsuarioFuncaoMapping());
            modelBuilder.AddConfiguration(new UsuarioMapping());
        }
    }
}
