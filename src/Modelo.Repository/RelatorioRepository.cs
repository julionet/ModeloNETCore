using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class RelatorioRepository : IPadraoRepository<Relatorio>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Relatorio> _repository;
        private string _usuario = "";

        public RelatorioRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Relatorio>(context == null ? new ModeloContext() : context);
            _usuario = usuario;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string Incluir(Relatorio entity)
        {
            return _repository.Insert(entity);
        }

        public string Alterar(Relatorio entity)
        {
            return _repository.Update(entity);
        }

        public string Excluir(Relatorio entity)
        {
            return _repository.Delete(entity.Id);
        }

        public Relatorio Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Relatorio> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Relatorio> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public RelatorioRetornoDTO SelecionarDTO(int id)
        {
            return this.SelecionarTodosDTO().Where(p => p.Id == id).FirstOrDefault();
        }

        public RelatorioRetornoDTO Selecionar(string nome)
        {
            return this.SelecionarTodosDTO().Where(p => p.Nome == nome).FirstOrDefault();
        }

        public IQueryable<RelatorioRetornoDTO> SelecionarTodosDTO()
        {
            return (from q in _db.Relatorios
                    select new RelatorioRetornoDTO
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Nome = q.Nome,
                        Origem = q.Origem,
                        Tamanho = q.Tamanho,
                        Modificado = q.Modificado,
                        Parametro = q.Parametro,
                        LinhaBranco = q.LinhaBranco,
                        Matricial = q.Matricial,
                        QuebraPagina = q.QuebraPagina,
                        GraficoTexto = q.GraficoTexto,
                        Visualizar = q.Visualizar,
                        EscalaX = q.EscalaX,
                        EscalaY = q.EscalaY
                    });
        }

        public RelatorioRetornoDTO SelecionarPorCodigoDTO(string codigo)
        {
            return this.SelecionarTodosDTO().Where(p => p.Codigo == codigo).FirstOrDefault();
        }

        public Relatorio SelecionarPorCodigo(string codigo)
        {
            return this.SelecionarTodos().Where(p => p.Codigo == codigo).FirstOrDefault();
        }
    }
}
