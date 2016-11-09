using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Repository;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Dto;

namespace Modelo.Service.Controllers
{
    [Route("api/parametro")]
    public class ParametroController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar(BasePostDTO<Parametro> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Classe.Id == 0)
                    _mensagem = new ParametroRepository(_db, entity.Usuario).Incluir(entity.Classe);
                else
                    _mensagem = new ParametroRepository(_db, entity.Usuario).Alterar(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpPost]
        [Route("excluir")]
        public string Excluir(BasePostDTO<Parametro> entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new ParametroRepository(_db, entity.Usuario).Excluir(entity.Classe);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Parametro Selecionar(int id)
        {
            return new ParametroRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Parametro> SelecionarTodos()
        {
            return new ParametroRepository().SelecionarTodos().ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Parametro> Filtrar([FromBody]string condicao)
        {
            return new ParametroRepository().Filtrar(condicao).ToList();
        }

        [HttpGet]
        [Route("selecionarvalorparametro/{codigo}/{usuario}")]
        public string SelecionarValorParametro(string codigo, int usuario)
        {
            return new ParametroRepository().SelecionarValorParametro(codigo, usuario);
        }

        [HttpGet]
        [Route("selecionarporcategoria/{categoria}")]
        public List<Parametro> SelecionarPorCategoria(string categoria)
        {
            return new ParametroRepository().SelecionarPorCategoria(categoria).ToList();
        }

        [HttpPost]
        [Route("gravarparametro")]
        public string GravarParametro(BasePostDTO<Parametro[]> parametros)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                foreach (Parametro item in parametros.Classe)
                {
                    Parametro parametro = new ParametroRepository().SelecionarPorCodigo(item.Codigo);
                    if (parametro != null)
                    {
                        parametro.ValorPersonalizado = string.IsNullOrWhiteSpace(item.ValorPersonalizado) || (item.ValorPersonalizado.Trim().Length == 0) ? null : item.ValorPersonalizado;
                        _mensagem = new ParametroRepository(_db, parametros.Usuario).Alterar(parametro);
                        if (_mensagem != "")
                        {
                            transacao.Rollback();
                            return _mensagem;
                        }
                    }
                }
                transacao.Commit();
            }
            return _mensagem;
        }
    }
}
