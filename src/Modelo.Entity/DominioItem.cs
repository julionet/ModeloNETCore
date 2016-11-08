using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class DominioItem : BaseClass
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Valor { get; set; }

        public int DominioId { get; set; }

        public Dominio Dominio { get; set; }
    }
}
