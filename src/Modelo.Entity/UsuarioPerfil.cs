using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class UsuarioPerfil
    {
        public int UsuarioId { get; set; }

        public int PerfilId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Perfil Perfil { get; set; }
    }
}
