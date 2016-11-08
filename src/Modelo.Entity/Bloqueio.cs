using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Bloqueio : BaseClass
    {
        public string Classe { get; set; }

        public string Usuario { get; set; }

        public string Computador { get; set; }

        public DateTime DataHora { get; set; }

        public int Referencia { get; set; }
    }
}
