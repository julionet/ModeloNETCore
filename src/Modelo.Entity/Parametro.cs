using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Parametro : BaseClass
    {
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public string Tipo { get; set; }

        public string ValorPadrao { get; set; }

        public string ValorPersonalizado { get; set; }

        public string Lista { get; set; }

        public bool PermiteUsuario { get; set; }

        public string Categoria { get; set; }

        public virtual ICollection<ParametroUsuario> ParametroUsuario { get; set; }

        public Parametro()
        {
            this.ParametroUsuario = new HashSet<ParametroUsuario>();
        }
    }
}
