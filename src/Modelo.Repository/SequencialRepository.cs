using Modelo.Entity;
using Modelo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Repository
{
    public class SequencialRepository : IDisposable
    {
        private ModeloContext _db = new ModeloContext();

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

        public int Buscar(string nome)
        {
            Sequencial sequencial = (from q in _db.Sequenciais where q.Nome == nome select q).FirstOrDefault();
            if (sequencial == null)
                return 0;

            try
            {
                sequencial.Valor += 1;
                _db.SaveChanges();
            }
            catch
            {
                return 0;
            }

            return sequencial.Valor;
        }
    }
}
