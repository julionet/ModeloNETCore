using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dto;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Entity;
using Modelo.Repository;

namespace Modelo.Service.Controllers
{
    [Route("api/usuariofuncao")]
    public class UsuarioFuncaoController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]BasePostDTO<UsuarioUsuarioFuncaoDTO> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new UsuarioRepository(_db, entity.Usuario).Alterar(entity.Classe.Usuario);

                foreach (UsuarioFuncao item in entity.Classe.UsuarioFuncoes)
                {
                    UsuarioFuncao usuariofuncao = new UsuarioFuncao();
                    usuariofuncao.Id = item.Id;
                    usuariofuncao.UsuarioId = entity.Classe.Usuario.Id;
                    usuariofuncao.FuncaoId = item.FuncaoId;
                    usuariofuncao.PermiteIncluir = item.PermiteIncluir;
                    usuariofuncao.PermiteAlterar = item.PermiteAlterar;
                    usuariofuncao.PermiteExcluir = item.PermiteExcluir;
                    if (new UsuarioFuncaoRepository().Selecionar(usuariofuncao.UsuarioId, usuariofuncao.FuncaoId) == null)
                    {
                        _mensagem = new UsuarioFuncaoRepository(_db, entity.Usuario).Incluir(usuariofuncao);
                        if (_mensagem != "")
                        {
                            transacao.Rollback();
                            return _mensagem;
                        }
                    }
                    else
                    {
                        _mensagem = new UsuarioFuncaoRepository(_db, entity.Usuario).Alterar(usuariofuncao);
                        if (_mensagem != "")
                        {
                            transacao.Rollback();
                            return _mensagem;
                        }
                    }
                }

                bool bExclui = true;
                foreach (UsuarioFuncaoDTO uf in new UsuarioFuncaoRepository().SelecionarPorUsuario(entity.Classe.Usuario.Id, entity.Classe.SistemaId))
                {
                    bExclui = true;
                    UsuarioFuncao usuariofuncao = new UsuarioFuncaoRepository().Selecionar(uf.Id);
                    if (entity.Classe.UsuarioFuncoes.Where(p => p.FuncaoId == usuariofuncao.FuncaoId).Count() > 0)
                        bExclui = false;
                    if (bExclui)
                    {
                        _mensagem = new UsuarioFuncaoRepository(_db, entity.Usuario).Excluir(usuariofuncao);
                        if (_mensagem != "")
                        {
                            transacao.Rollback();
                            return _mensagem;
                        }
                    }
                }

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir([FromBody]BasePostDTO<UsuarioFuncao> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new UsuarioFuncaoRepository(_db, entity.Usuario).Excluir(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public UsuarioFuncao Selecionar(int id)
        {
            return new UsuarioFuncaoRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionarfuncao/{usuario}/{funcao}")]
        public UsuarioFuncao SelecionarFuncao(int usuario, int funcao)
        {
            return new UsuarioFuncaoRepository().Selecionar(usuario, funcao);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<UsuarioFuncao> SelecionarTodos()
        {
            return new UsuarioFuncaoRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<UsuarioFuncao> Filtrar([FromBody]string condicao)
        {
            return new UsuarioFuncaoRepository().Filtrar(condicao).ToList();
        }

        [HttpGet]
        [Route("selecionarporusuario/{usuario}/{sistema}")]
        public List<UsuarioFuncaoDTO> SelecionarPorUsuario(int usuario, int sistema)
        {
            return new UsuarioFuncaoRepository().SelecionarPorUsuario(usuario, sistema).ToList();
        }

        [HttpGet]
        [Route("selecionaracessoporusuariomodulo/{usuario}/{modulo}/{sistema}")]
        public List<UsuarioFuncaoDTO> SelecionarAcessoPorUsuarioModulo(int usuario, int modulo, int sistema)
        {
            return new UsuarioFuncaoRepository().SelecionarAcessoPorUsuarioModulo(usuario, modulo, sistema).ToList();
        }
    }
}
