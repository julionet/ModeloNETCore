﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Perfil : BaseClass
    {
        public string Descricao { get; set; }

        public virtual ICollection<PerfilFuncao> PerfilFuncao { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }

        public int QuantidadeFuncoes { get; set; }

        public Perfil()
        {
            this.PerfilFuncao = new HashSet<PerfilFuncao>();
            this.Usuario = new HashSet<Usuario>();
        }
    }
}
