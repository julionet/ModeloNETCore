using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Repository;
using Modelo.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Modelo.Service.Controllers
{
    [Route("api/database")]
    public class DatabaseController : Controller
    {
        [HttpGet]
        [Route("getdatahoraservidor")]
        public DateTime GetDataHoraServidor()
        {
            return new DatabaseRepository().GetDateTimeServer();
        }

        [HttpGet]
        [Route("getnumeroseriehd")]
        public string GetNumeroSerieHD()
        {
            return new DatabaseRepository().GetSerialNumberHD();
        }

        [HttpGet]
        [Route("getdatabasetype")]
        public int GetDatabaseType()
        {
            return new DatabaseRepository().GetDatabaseType();
        }

        [HttpPost]
        [Route("executarcomandosql")]
        public string ExecutarComandoSQL([FromBody]string sql)
        {
            return new DatabaseRepository().ExecutarComandoSQL(sql);
        }

        [HttpGet]
        [Route("conectarbanco")]
        public string ConectarBanco()
        {
            try
            {
                new ModeloContext().Database.GetDbConnection().Open();
                return "";
            }
            catch (Exception erro)
            {
                if (erro.InnerException != null)
                    return erro.InnerException.Message;
                else
                    return erro.Message;
            }
        }
    }
}
