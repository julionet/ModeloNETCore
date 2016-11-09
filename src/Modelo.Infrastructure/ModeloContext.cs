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
            string connectionString = builder.Build()["ConnectionStrings:ModeloConnection"].ToString();
            optionsBuilder.UseSqlServer(connectionString);
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
            modelBuilder.AddConfiguration(new UsuarioPerfilMapping());
        }

        public DbSet<Bloqueio> Bloqueios { get; set; }
        public DbSet<Dominio> Dominios { get; set; }
        public DbSet<DominioItem> DominiosItens { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Grafico> Graficos { get; set; }
        public DbSet<GraficoSerie> GraficoSeries { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<ParametroUsuario> ParametroUsuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilFuncao> PerfilFuncoes { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}
