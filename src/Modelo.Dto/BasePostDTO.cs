using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Dto
{
    public class BasePostDTO<T>
    {
        public T Classe { get; set; }
        public string Usuario { get; set; }
    }
}
