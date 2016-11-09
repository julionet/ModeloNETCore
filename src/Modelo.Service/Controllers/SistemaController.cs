using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Modelo.Repository;
using Modelo.Entity;
using Modelo.Dto;

namespace Modelo.Service.Controllers
{
    [Route("api/sistema")]
    public class SistemaController : Controller
    {
        private string _mensagem = "";

        [HttpPost]
        [Route("salvar")]
        public string Salvar([FromBody]SistemaModuloFuncaoDTO entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                if (entity.Sistema.Id == 0)
                    _mensagem = new SistemaRepository(_db).Incluir(entity.Sistema);
                else
                    _mensagem = new SistemaRepository(_db).Alterar(entity.Sistema);

                if (_mensagem == "")
                {
                    foreach (Modulo itemModulo in entity.Modulos)
                    {
                        if (itemModulo.Flag == "I")
                        {
                            int iModulo = itemModulo.Id;
                            itemModulo.SistemaId = entity.Sistema.Id;
                            _mensagem = new ModuloRepository(_db).Incluir(itemModulo);

                            if (string.IsNullOrWhiteSpace(_mensagem))
                            {
                                foreach (Funcao itemFuncao in entity.Funcoes.Where(p => p.ModuloId == iModulo))
                                {
                                    itemFuncao.ModuloId = itemModulo.Id;
                                    _mensagem = new FuncaoRepository(_db).Incluir(itemFuncao);
                                }
                            }
                        }
                        else if (itemModulo.Flag == "A")
                        {
                            _mensagem = new ModuloRepository(_db).Alterar(itemModulo);

                            if (string.IsNullOrWhiteSpace(_mensagem))
                            {
                                foreach (Funcao itemFuncao in entity.Funcoes.Where(p => p.ModuloId == itemModulo.Id))
                                {
                                    if (itemFuncao.Flag == "I")
                                    {
                                        itemFuncao.ModuloId = itemModulo.Id;
                                        _mensagem = new FuncaoRepository(_db).Incluir(itemFuncao);
                                    }
                                    else if (itemFuncao.Flag == "A")
                                        _mensagem = new FuncaoRepository(_db).Alterar(itemFuncao);
                                    else if (itemFuncao.Flag == "E")
                                        _mensagem = new FuncaoRepository(_db).Excluir(itemFuncao);
                                }
                            }
                        }
                        else if (itemModulo.Flag == "E")
                            _mensagem = new ModuloRepository(_db).Excluir(itemModulo);
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
        public string Excluir([FromBody]Sistema entity)
        {
            ModeloContext _db = new ModeloContext();
            using (IDbContextTransaction transacao = _db.Database.BeginTransaction())
            {
                _mensagem = new SistemaRepository(_db).Excluir(entity);

                if (_mensagem == "")
                    transacao.Commit();
                else
                    transacao.Rollback();
            }
            return _mensagem;
        }

        [HttpGet]
        [Route("selecionar/{id}")]
        public Sistema Selecionar(int id)
        {
            return new SistemaRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Sistema> SelecionarTodos()
        {
            return new SistemaRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarativos")]
        public List<Sistema> SelecionarAtivos()
        {
            return new SistemaRepository().SelecionarAtivos().ToList();
        }

        [HttpGet]
        [Route("selecionarativosportipo/{tipo}")]
        public List<Sistema> SelecionarAtivosPorTipo(string tipo)
        {
            return new SistemaRepository().SelecionarAtivosPorTipo(tipo).ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Sistema> Filtrar([FromBody]string condicao)
        {
            return new SistemaRepository().Filtrar(condicao).ToList();
        }
    }
}
