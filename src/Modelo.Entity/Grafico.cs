using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Grafico : BaseClass
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Parametro { get; set; }

        public virtual ICollection<GraficoSerie> GraficoSerie { get; set; }

        public virtual ICollection<Funcao> Funcao { get; set; }

        public Grafico()
        {
            this.GraficoSerie = new HashSet<GraficoSerie>();
            this.Funcao = new HashSet<Funcao>();
        }
    }
}
