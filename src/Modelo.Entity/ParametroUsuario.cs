using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class ParametroUsuario : BaseClass
    {
        public int Id { get; set; }

        public int ParametroId { get; set; }

        public int UsuarioId { get; set; }

        public string Valor { get; set; }

        public virtual Parametro Parametro { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
