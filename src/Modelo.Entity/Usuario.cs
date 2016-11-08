using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Usuario : BaseClass
    {
        public string Login { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public bool Master { get; set; }

        public bool Bloqueado { get; set; }

        public bool Administrador { get; set; }

        public bool NuncaExpira { get; set; }

        public bool AlterarSenha { get; set; }

        public Nullable<int> DiasExpirar { get; set; }

        public Nullable<DateTime> DataAlteracao { get; set; }

        public Nullable<int> FuncionarioId { get; set; }

        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }

        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }

        public virtual ICollection<ParametroUsuario> ParametroUsuario { get; set; }

        public string ListaPerfis { get; set; }

        public Usuario()
        {
            this.UsuarioFuncao = new HashSet<UsuarioFuncao>();
            this.UsuarioPerfil = new HashSet<UsuarioPerfil>();
            this.ParametroUsuario = new HashSet<ParametroUsuario>();
        }
    }
}
