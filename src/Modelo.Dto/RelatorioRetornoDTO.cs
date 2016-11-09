using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Dto
{
    public class RelatorioRetornoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public int Tamanho { get; set; }
        public DateTime Modificado { get; set; }
        public string Parametro { get; set; }
        public bool Matricial { get; set; }
        public bool QuebraPagina { get; set; }
        public bool GraficoTexto { get; set; }
        public bool LinhaBranco { get; set; }
        public bool Visualizar { get; set; }
        public double EscalaX { get; set; }
        public double EscalaY { get; set; }
    }
}
