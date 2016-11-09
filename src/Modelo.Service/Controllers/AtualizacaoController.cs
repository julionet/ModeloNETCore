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
    [Route("api/atualizacao")]
    public class AtualizacaoController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]BasePostDTO<Atualizacao> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Id == 0)
                    _mensagem = new AtualizacaoRepository(_db, entity.Usuario).Incluir(entity.Classe);
                else
                    _mensagem = new AtualizacaoRepository(_db, entity.Usuario).Alterar(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir([FromBody]BasePostDTO<Atualizacao> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new AtualizacaoRepository(_db, entity.Usuario).Excluir(entity.Classe);
                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Atualizacao Selecionar(int id)
        {
            return new AtualizacaoRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Atualizacao> SelecionarTodos()
        {
            return new AtualizacaoRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Atualizacao> Filtrar([FromBody]string condicao)
        {
            return new AtualizacaoRepository().Filtrar(condicao).ToList();
        }

        [HttpGet]
        [Route("contar")]
        public int Contar()
        {
            return new AtualizacaoRepository().Contar();
        }

        [HttpGet]
        [Route("contarpendente")]
        public int ContarPendente()
        {
            return new AtualizacaoRepository().ContarPendente();
        }

        [HttpGet]
        [Route("existeatualizacao")]
        public bool ExisteAtualizacao(int numero)
        {
            return new AtualizacaoRepository().ExisteAtualizacao(numero);
        }

        [HttpPost]
        [Route("importar")]
        public string Importar([FromBody]BasePostDTO<Atualizacao[]> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                foreach (Atualizacao atualizacao in entity.Classe)
                {
                    if (new AtualizacaoRepository().SelecionarTodos().Where(p => p.Id == atualizacao.Id).Count() == 0)
                        _mensagem = new AtualizacaoRepository(_db, entity.Usuario).Incluir(atualizacao);

                    if (_mensagem != "")
                        break;
                }

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionartodospendente")]
        public List<Atualizacao> SelecionarTodosPendente()
        {
            return new AtualizacaoRepository().SelecionarTodosPendente().ToList();
        }

        [HttpPost]
        [Route("atualizarversao")]
        public string AtualizarVersao([FromBody]Atualizacao atualizacao)
        {
            return new AtualizacaoRepository().AtualizarVersao(atualizacao);
        }

        [HttpGet]
        [Route("finalizaratualizacoes")]
        public string FinalizarAtualizacoes()
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new AtualizacaoRepository(_db, "").FinalizarAtualizacoes();
                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }
    }
}
