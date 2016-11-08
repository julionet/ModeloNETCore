using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Interface
{
    public interface IPadraoRepository<T> where T : class
    {
        string Incluir(T entity);
        string Alterar(T entity);
        string Excluir(T entity);
        T Selecionar(int id);
        IQueryable<T> SelecionarTodos();
        IQueryable<T> Filtrar(string condicao);
    }
}
