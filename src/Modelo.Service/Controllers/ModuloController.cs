using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Repository;
using Modelo.Entity;

namespace Modelo.Service.Controllers
{
    [Route("api/modulo")]
    public class ModuloController : Controller
    {
        [HttpGet]
        [Route("selecionar/{id}")]
        public Modulo Selecionar(int id)
        {
            return new ModuloRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Modulo> SelecionarTodos()
        {
            return new ModuloRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarporsistema/{sistema}")]
        public List<Modulo> SelecionarPorSistema(int sistema)
        {
            return new ModuloRepository().SelecionarPorSistema(sistema).ToList();
        }

        [HttpGet]
        [Route("selecionarporsistemausuario/{sistema}/{usuario}")]
        public List<Modulo> SelecionarPorSistemaUsuario(int sistema, int usuario)
        {
            return new ModuloRepository().SelecionarPorSistemaUsuario(sistema, usuario).ToList();
        }

        [HttpPost]
        [Route("filtrar")]
        public List<Modulo> Filtrar([FromBody]string condicao)
        {
            return new ModuloRepository().Filtrar(condicao).ToList();
        }

        [HttpPost]
        [Route("validardados")]
        public string ValidarDados([FromBody]Modulo entity)
        {
            return new ModuloRepository().ValidarDados(entity);
        }
    }
}
