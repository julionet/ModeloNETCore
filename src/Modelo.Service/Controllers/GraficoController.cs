using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entity;
using Modelo.Repository;

namespace Modelo.Service.Controllers
{
    [Route("api/grafico")]
    public class GraficoController : Controller
    {
        [HttpGet]
        [Route("selecionar/{id}")]
        public Grafico Selecionar(int id)
        {
            return new GraficoRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionarporcodigo/{codigo}")]
        public Grafico SelecionarPorCodigo(string codigo)
        {
            return new GraficoRepository().SelecionarPorCodigo(codigo);
        }

        [HttpGet]
        [Route("selecionartodos")]
        public List<Grafico> SelecionarTodos()
        {
            return new GraficoRepository().SelecionarTodos().ToList();
        }

        [HttpGet]
        [Route("selecionarseries/{grafico}")]
        public List<GraficoSerie> SelecionarSeries(int grafico)
        {
            return new GraficoSerieRepository().SelecionarPorGrafico(grafico).ToList();
        }
    }
}
