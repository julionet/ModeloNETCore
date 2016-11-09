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
    public class AutenticacaoController : Controller
    {
        private IMemoryCache _memoryCache;
        public AutenticacaoController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
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

        [HttpGet]
        [Route("gettoken")]
        public string GetToken()
        {
            return Token.GerarToken(_memoryCache);
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
        [Route("selecionar/{id}")]
        public Sistema Selecionar(int id)
        {
            return new SistemaRepository().Selecionar(id);
        }

        [HttpGet]
        [Route("selecionarativosportipo/{tipo}")]
        public List<Sistema> SelecionarAtivosPorTipo(string tipo)
        {
            return new SistemaRepository().SelecionarAtivosPorTipo(tipo).ToList();
        }
    }
}
