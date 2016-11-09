using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Repository;
using Modelo.Dto;

namespace Modelo.Service.Controllers
{
    [Route("api/funcao")]
    public class FuncaoController : Controller
    {
        [HttpGet]
        [Route("selecionar/{id}")]
        public Funcao Selecionar(int id)
        {
            return new FuncaoRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionarcodigo/{codigo}")]
        public Funcao SelecionarCodigo(string codigo)
        {
            return new FuncaoRepository().Selecionar(codigo);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Funcao> SelecionarTodos()
        {
            return new FuncaoRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarpormodulo/{id}")]
        public List<Funcao> SelecionarPorModulo(int id)
        {
            return new FuncaoRepository().SelecionarPorModulo(id).ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Funcao> Filtrar([FromBody]string condicao)
        {
            return new FuncaoRepository().Filtrar(condicao).ToList();
        }

        [HttpPost]
        [Route("validardados")]
        public string ValidarDados(Funcao entity)
        {
            return new FuncaoRepository().ValidarDados(entity);
        }

        [HttpGet]
        [Route("selecionartodoscompleto")]
        public List<FuncaoDTO> SelecionarTodosCompleto()
        {
            return new FuncaoRepository().SelecionarTodosCompleto().ToList();
        }
    }
}
