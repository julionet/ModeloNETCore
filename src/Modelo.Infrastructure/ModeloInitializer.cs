using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Infrastructure
{
    public static class ModeloInitializer
    {
        public static void Seed(ModeloContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Usuarios.Any())
            {
                Usuario usuario = new Usuario()
                {
                    Administrador = true,
                    AlterarSenha = true,
                    Bloqueado = false,
                    DataAlteracao = DateTime.Today,
                    DiasExpirar = 90,
                    Id = 0,
                    Login = "ADMIN",
                    Master = true,
                    Nome = "Adminsitrador do Sistema",
                    NuncaExpira = false,
                    Senha = "!7<:3"
                };
            }
        }
    }
}
