using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Sistema : BaseClass
    {
        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public byte[] Imagem { get; set; }

        public string Tipo { get; set; }

        public string Interface { get; set; }

        public int Linha { get; set; }

        public int Tamanho { get; set; }

        public bool Gerenciador { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Modulo> Modulo { get; set; }

        public int QuantidadeModulo { get; set; }

        public Sistema()
        {
            this.Modulo = new HashSet<Modulo>();
        }
    }
}
