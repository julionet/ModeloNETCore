using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dto;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Repository;
using Modelo.Entity;

namespace Modelo.Service.Controllers
{
    [Route("api/perfil")]
    public class PerfilController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]BasePostDTO<PerfilPerfilFuncaoDTO> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Perfil.Id == 0)
                    _mensagem = new PerfilRepository(_db, entity.Usuario).Incluir(entity.Classe.Perfil);
                else
                    _mensagem = new PerfilRepository(_db, entity.Usuario).Alterar(entity.Classe.Perfil);

                if (_mensagem == "")
                {
                    foreach (PerfilFuncao item in entity.Classe.PerfilFuncoes)
                    {
                        PerfilFuncao perfilfuncao = new PerfilFuncao();
                        perfilfuncao.Id = item.Id;
                        perfilfuncao.PerfilId = entity.Classe.Perfil.Id;
                        perfilfuncao.FuncaoId = item.FuncaoId;
                        perfilfuncao.PermiteIncluir = item.PermiteIncluir;
                        perfilfuncao.PermiteAlterar = item.PermiteAlterar;
                        perfilfuncao.PermiteExcluir = item.PermiteExcluir;
                        if (perfilfuncao.Id == 0)
                        {
                            _mensagem = new PerfilFuncaoRepository(_db, entity.Usuario).Incluir(perfilfuncao);
                            if (_mensagem != "")
                            {
                                transacao.Rollback();
                                return _mensagem;
                            }
                        }
                        else
                        {
                            _mensagem = new PerfilFuncaoRepository(_db, entity.Usuario).Alterar(perfilfuncao);
                            if (_mensagem != "")
                            {
                                transacao.Rollback();
                                return _mensagem;
                            }
                        }
                    }

                    bool bExclui = true;
                    foreach (PerfilFuncaoDTO pf in new PerfilFuncaoRepository().SelecionarPorPerfil(entity.Classe.Perfil.Id, entity.Classe.SistemaId))
                    {
                        bExclui = true;
                        PerfilFuncao perfilfuncao = new PerfilFuncaoRepository().Selecionar(pf.Id);
                        if (entity.Classe.PerfilFuncoes.Where(p => p.FuncaoId == perfilfuncao.FuncaoId).Count() > 0)
                            bExclui = false;
                        if (bExclui)
                        {
                            _mensagem = new PerfilFuncaoRepository(_db, entity.Usuario).Excluir(perfilfuncao);
                            if (_mensagem != "")
                            {
                                transacao.Rollback();
                                return _mensagem;
                            }
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
        public string Excluir([FromBody]BasePostDTO<Perfil> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new PerfilRepository(_db, entity.Usuario).Excluir(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Perfil Selecionar(int id)
        {
            return new PerfilRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Perfil> SelecionarTodos()
        {
            return new PerfilRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Perfil> Filtrar([FromBody]string condicao)
        {
            return new PerfilRepository().Filtrar(condicao).ToList();
        }

        [HttpGet]
        [Route("selecionarporperfil/{usuario}/{sistema}")]
        public List<PerfilFuncaoDTO> SelecionarPorPerfil(int usuario, int sistema)
        {
            return new PerfilFuncaoRepository().SelecionarPorPerfil(usuario, sistema).ToList();
        }
    }
}
