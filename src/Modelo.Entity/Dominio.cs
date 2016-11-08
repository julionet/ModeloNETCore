using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Dominio : BaseClass
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<DominioItem> DominioItem { get; set; }

        public Dominio()
        {
            this.DominioItem = new HashSet<DominioItem>();
        }
    }
}
