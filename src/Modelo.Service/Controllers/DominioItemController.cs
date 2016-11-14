using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modelo.Service.Controllers
{
    [Route("api/dominioitem")]
    public class DominioItemController : Controller
    {
        [HttpGet]
        [Route("selecionartodos")]
        public List<DominioItem> SelecionarTodos()
        {
            return new DominioItemRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarpordominio/{id}")]
        public List<DominioItem> SelecionarPorDominio(int id)
        {
            return new DominioItemRepository().SelecionarPorDominio(id).ToList();
        }
    }
}
