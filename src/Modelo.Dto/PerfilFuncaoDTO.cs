using Modelo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Dto
{
    public class PerfilFuncaoDTO : PerfilFuncao
    {
        public string FuncaoDescricao { get; set; }
        public bool FuncaoManutencao { get; set; }
        public int ModuloId { get; set; }
        public string ModuloDescricao { get; set; }
        public bool Selecionado { get; set; }
    }
}
