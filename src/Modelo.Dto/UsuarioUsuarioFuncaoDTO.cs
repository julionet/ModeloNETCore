using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class UsuarioUsuarioFuncaoDTO
    {
        public int SistemaId { get; set; }
        public Usuario Usuario { get; set; }
        public UsuarioFuncao[] UsuarioFuncoes { get; set; }
    }
}
