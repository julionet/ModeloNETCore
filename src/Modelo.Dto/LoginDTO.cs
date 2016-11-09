using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Dto
{
    public class LoginDTO
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Confirmacao { get; set; }
        public string Computador { get; set; }
        public string Hash { get; set; }
    }
}
