using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Dto;
using Modelo.Repository;
using Modelo.Service.Security;
using Microsoft.Extensions.Caching.Memory;
using Modelo.Entity;

namespace Modelo.Service.Controllers
{
    [Route("api/autenticacao")]
    public class AutenticacaoController : Controller, IAutenticacaoController
    {
        private IMemoryCache _memoryCache;
        public AutenticacaoController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("gettoken")]
        public string GetToken()
        {
            return Token.GerarToken(_memoryCache);
        }

        [HttpPost]
        [Route("validarlogin")]
        public string ValidarLogin([FromBody] AutenticacaoDTO login)
        {
            if (Token.ValidarToken(login.Hash, _memoryCache))
                return new UsuarioRepository().ValidarLogin(login.Login, login.Senha);
            else
                return "Falha na autenticação do serviço!";
        }

        [HttpPost]
        [Route("gethashusuario")]
        public string GetHashUsuario([FromBody] AutenticacaoDTO login)
        {
            if (Token.ValidarToken(login.Hash, _memoryCache))
                return HashApi.GerarHash(login.Login, _memoryCache);
            else
                return null;
        }

        [HttpGet]
        [Route("getversaosistema")]
        public string GetVersaoSistema()
        {
            return new ParametroRepository().SelecionarValorParametro("999");
        }

        [HttpGet]
        [Route("selecionarsistema/{id}")]
        public Sistema SelecionarSistema(int id)
        {
            return new SistemaRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionarsistemasativosportipo/{tipo}")]
        public List<Sistema> SelecionarSistemasAtivosPorTipo(string tipo)
        {
            return new SistemaRepository().SelecionarAtivosPorTipo(tipo).ToList();
        }
    }
}
