using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Repository;

namespace Modelo.Service.Controllers
{
    [Route("api/sequencial")]
    public class SequencialController : Controller
    {
        [HttpPost]
        [Route("buscar")]
        public int Buscar([FromBody]string nome)
        {
            return new SequencialRepository().Buscar(nome);
        }
    }
}
