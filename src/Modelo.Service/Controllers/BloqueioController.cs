using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Dto;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Repository;

namespace Modelo.Service.Controllers
{
    [Route("api/bloqueio")]
    public class BloqueioController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]BasePostDTO<Bloqueio> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Id == 0)
                    _mensagem = new BloqueioRepository(_db, entity.Usuario).Incluir(entity.Classe);
                else
                    _mensagem = new BloqueioRepository(_db, entity.Usuario).Alterar(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir([FromBody]BasePostDTO<Bloqueio> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new BloqueioRepository(_db, entity.Usuario).Excluir(entity.Classe);
                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Bloqueio Selecionar(int id)
        {
            return new BloqueioRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Bloqueio> SelecionarTodos()
        {
            return new BloqueioRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Bloqueio> Filtrar([FromBody]string condicao)
        {
            return new BloqueioRepository().Filtrar(condicao).ToList();
        }

        [HttpGet]
        [Route("bloquearregistro/{classe}/{usuario}/{referencia}")]
        public string BloquearRegistro(string classe, int usuario, int referencia)
        {
            return new BloqueioRepository().BloquearRegistro(classe, usuario, referencia);
        }

        [HttpGet]
        [Route("excluirbloqueio/{classe}/{referencia}")]
        public void ExcluirBloqueio(string classe, int referencia)
        {
            new BloqueioRepository().ExcluirBloqueio(classe, referencia);
        }

        [HttpGet]
        [Route("atualizarbloqueio/{classe}/{referencia}")]
        public void AtualizarBloqueio(string classe, int referencia)
        {
            new BloqueioRepository().AtualizarBloqueio(classe, referencia);
        }

        [HttpGet]
        [Route("consultarbloqueio/{classe}/{referencia}")]
        public string ConsultarBloqueio(string classe, int referencia)
        {
            return new BloqueioRepository().ConsultarBloqueio(classe, referencia);
        }
    }
}
