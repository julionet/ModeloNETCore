using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class UsuarioFuncao : BaseClass
    {
        public int UsuarioId { get; set; }

        public int FuncaoId { get; set; }

        public bool PermiteIncluir { get; set; }

        public bool PermiteAlterar { get; set; }

        public bool PermiteExcluir { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Funcao Funcao { get; set; }
    }
}
