using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Entity
{
    public class Funcao : BaseClass
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public string Grupo { get; set; }

        public string Tipo { get; set; }

        public string NomeAssembly { get; set; }

        public string NomeFormulario { get; set; }

        public bool Manutencao { get; set; }

        public int? RelatorioId { get; set; }

        public int? GraficoId { get; set; }

        public int ModuloId { get; set; }

        public virtual Modulo Modulo { get; set; }

        public virtual Relatorio Relatorio { get; set; }

        public virtual Grafico Grafico { get; set; }

        public virtual ICollection<UsuarioFuncao> UsuarioFuncao { get; set; }

        public virtual ICollection<PerfilFuncao> PerfilFuncao { get; set; }

        public string Flag { get; set; }

        public Funcao()
        {
            this.UsuarioFuncao = new HashSet<UsuarioFuncao>();
            this.PerfilFuncao = new HashSet<PerfilFuncao>();
        }
    }
}
