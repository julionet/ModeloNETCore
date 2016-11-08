using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Modulo : BaseClass
    {
        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public string Grupo { get; set; }

        public byte[] Imagem { get; set; }

        public int Cor { get; set; }

        public bool Administracao { get; set; }

        public bool Navegacao { get; set; }

        public int SistemaId { get; set; }

        public virtual Sistema Sistema { get; set; }

        public virtual ICollection<Funcao> Funcao { get; set; }

        public string Flag { get; set; }

        public int QuantidadeFuncao { get; set; }

        public Modulo()
        {
            this.Funcao = new HashSet<Funcao>();
        }
    }
}
