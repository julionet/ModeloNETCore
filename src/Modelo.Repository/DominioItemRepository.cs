using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System.Linq;

namespace Modelo.Repository
{
    public class DominioItemRepository
    {
        private IRepository<DominioItem> _repository;

        public DominioItemRepository()
        {
            _repository = new Repository<DominioItem>(new ModeloContext());
        }

        public IQueryable<DominioItem> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<DominioItem> SelecionarPorDominio(int id)
        {
            return _repository.GetAll().Where(p => p.DominioId == id);
        }

        public bool ValidarDominioItem(int dominio, string valor)
        {
            return this.SelecionarPorDominio(dominio).Where(p => p.Valor == valor).Count() != 0;
        }
    }
}
