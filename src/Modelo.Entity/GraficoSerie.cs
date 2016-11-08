using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class GraficoSerie : BaseClass
    {
        public string Nome { get; set; }

        public string Tipo { get; set; }

        public string Query { get; set; }

        public string Serie { get; set; }

        public string Argumento { get; set; }

        public string Valor { get; set; }

        public int GraficoId { get; set; }

        public virtual Grafico Grafico { get; set; }
    }
}
