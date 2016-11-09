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
    [Route("api/parametrousuario")]
    public class ParametroUsuarioController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]BasePostDTO<ParametroUsuario> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Id == 0)
                    _mensagem = new ParametroUsuarioRepository(_db, entity.Usuario).Incluir(entity.Classe);
                else
                    _mensagem = new ParametroUsuarioRepository(_db, entity.Usuario).Alterar(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir([FromBody]BasePostDTO<ParametroUsuario> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new ParametroUsuarioRepository(_db, entity.Usuario).Excluir(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public ParametroUsuario Selecionar(int id)
        {
            return new ParametroUsuarioRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<ParametroUsuario> SelecionarTodos()
        {
            return new ParametroUsuarioRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarporparametro/{id}")]
        public List<ParametroUsuario> SelecionarPorParametro(int id)
        {
            return new ParametroUsuarioRepository().SelecionarPorParametro(id).ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<ParametroUsuario> Filtrar([FromBody]string condicao)
        {
            return new ParametroUsuarioRepository().Filtrar(condicao).ToList();
        }
    }
}
