using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class GraficoSerieRepository : IPadraoRepository<GraficoSerie>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<GraficoSerie> _repository;
        private string _usuario = "";

        public GraficoSerieRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<GraficoSerie>(context == null ? new ModeloContext() : context);
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

        public string Incluir(GraficoSerie entity)
        {
            return _repository.Insert(entity);
        }

        public string Alterar(GraficoSerie entity)
        {
            return _repository.Update(entity);
        }

        public string Excluir(GraficoSerie entity)
        {
            return _repository.Delete(entity.Id);
        }

        public GraficoSerie Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<GraficoSerie> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<GraficoSerie> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public IQueryable<GraficoSerie> SelecionarPorGrafico(int id)
        {
            return this.SelecionarTodos().Where(p => p.GraficoId == id);
        }
    }
}
