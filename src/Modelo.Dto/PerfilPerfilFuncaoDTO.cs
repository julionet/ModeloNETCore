using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class PerfilPerfilFuncaoDTO
    {
        public int SistemaId { get; set; }
        public Perfil Perfil { get; set; }
        public PerfilFuncao[] PerfilFuncoes { get; set; }
    }
}
