using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Repository;

namespace Modelo.Service.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar(BasePostDTO<Usuario> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Id == 0)
                    _mensagem = new UsuarioRepository(_db, entity.Usuario).Incluir(entity.Classe);
                else
                    _mensagem = new UsuarioRepository(_db, entity.Usuario).Alterar(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir(BasePostDTO<Usuario> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new UsuarioRepository(_db, entity.Usuario).Excluir(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Usuario Selecionar(int id)
        {
            return new UsuarioRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Usuario> SelecionarTodos()
        {
            return new UsuarioRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<UsuarioDTO> Filtrar([FromBody]string condicao)
        {
            return new UsuarioRepository().Filtrar_(condicao).ToList();
        }

        [HttpGet]
        [Route("selecionarlogin/{login}")]
        public Usuario SelecionarLogin(string login)
        {
            return new UsuarioRepository().Selecionar(login);
        }

        [HttpGet]
        [Route("selecionarnaomaster")]
        public List<Usuario> SelecionarNaoMaster()
        {
            return new UsuarioRepository().SelecionarNaoMaster().ToList();
        }

        [HttpGet]
        [Route("devealterarsenha/{usuario}")]
        public bool DeveAlterarSenha(string usuario)
        {
            return new UsuarioRepository().DeveAlterarSenha(usuario);
        }

        [HttpPost]
        [Route("alterarsenha")]
        public string AlterarSenha(LoginDTO login)
        {
            return new UsuarioRepository().AlterarSenha(login.Usuario, login.Senha, login.NovaSenha, login.Confirmacao);
        }

        [HttpGet]
        [Route("selecionarperfis/{usuario}")]
        public List<Perfil> SelecionarPerfis(int usuario)
        {
            return new UsuarioRepository().SelecionarPerfis(usuario).ToList();
        }
    }
}
