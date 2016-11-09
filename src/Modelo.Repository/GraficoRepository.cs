using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class GraficoRepository : IPadraoRepository<Grafico>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Grafico> _repository;
        private string _usuario = "";

        public GraficoRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Grafico>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Grafico entity)
        {
            return _repository.Insert(entity);
        }

        public string Alterar(Grafico entity)
        {
            return _repository.Update(entity);
        }

        public string Excluir(Grafico entity)
        {
            return _repository.Delete(entity.Id);
        }

        public Grafico Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Grafico> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Grafico> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public Grafico SelecionarPorCodigo(string codigo)
        {
            return this.SelecionarTodos().Where(p => p.Codigo == codigo).FirstOrDefault();
        }
    }
}
