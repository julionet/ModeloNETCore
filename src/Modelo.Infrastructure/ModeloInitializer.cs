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
                context.Usuarios.Add(new Usuario() { Login = "ADMIN", Nome = "Administrado do sistema", Senha = "!7<:3", Master = true, Bloqueado = false, Administrador = true, NuncaExpira = false, AlterarSenha = true, DiasExpirar = 90, DataAlteracao = DateTime.Today });
                context.SaveChanges();
            }

            if (!context.Sistemas.Any())
            {
                context.Sistemas.Add(new Sistema() { Codigo = "GERENCIADOR", Descricao = "Gerenciador de Sistemas", Tipo = "D", Interface = "M", Linha = 2, Tamanho = 120, Gerenciador = true, Ativo = true });
                context.SaveChanges();

                context.Modulos.Add(new Modulo() { Codigo = "GERENCIADOR", Descricao = "Gerenciador", Cor = -16748352, Navegacao = true, Administracao = false, SistemaId = 1 });
                context.SaveChanges();

                context.Funcoes.Add(new Funcao() { Codigo = "SISTEMA", Descricao = "Definição de Sistemas", Tipo = "F", NomeAssembly = "Cartsys.Gerenciador", NomeFormulario = "FrmSistema", Manutencao = true, ModuloId = 1 });
                context.Funcoes.Add(new Funcao() { Codigo = "PARAMETRO", Descricao = "Definição de Parâmetros", Tipo = "F", NomeAssembly = "Cartsys.Gerenciador", NomeFormulario = "FrmParametro", Manutencao = true, ModuloId = 1 });
                context.SaveChanges();
            }

            if (!context.Dominios.Any())
            {
                context.Dominios.Add(new Dominio() { Descricao = "Categoria de Parâmetro" });
                context.SaveChanges();

                context.DominioItens.Add(new DominioItem() { Descricao = "Administração do Sistema", Valor = "01", DominioId = 1 });
                context.SaveChanges();
            }

            if (!context.Parametros.Any())
            {
                context.Parametros.Add(new Parametro() { Descricao = "Tempo máximo de duração de bloqueios (segundos)", Codigo = "001", Tipo = "N", ValorPadrao = "30", PermiteUsuario = false, Categoria = "01" });
                context.Parametros.Add(new Parametro() { Descricao = "Versão do banco de dados", Codigo = "999", Tipo = "N", ValorPadrao = "1", PermiteUsuario = false, Categoria = "01" });
                context.SaveChanges();
            }
        }
    }
}
