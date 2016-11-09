using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class SistemaModuloFuncaoDTO
    {
        public Sistema Sistema { get; set; }
        public Modulo[] Modulos { get; set; }
        public Funcao[] Funcoes { get; set; }
    }
}
