using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class ParametroUsuarioRepository : IPadraoRepository<ParametroUsuario>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<ParametroUsuario> _repository;
        private string _usuario = "";

        public ParametroUsuarioRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<ParametroUsuario>(context == null ? new ModeloContext() : context);
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

        public string Incluir(ParametroUsuario entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "I", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Alterar(ParametroUsuario entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                ParametroUsuario entityold = _db.Set<ParametroUsuario>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);
                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(ParametroUsuario entity)
        {
            string mensagem = _repository.Delete(entity.Id);

            //if (mensagem == "")
            //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "E", entity.GetType().Name, ref mensagem);
            return mensagem;
        }

        public ParametroUsuario Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<ParametroUsuario> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<ParametroUsuario> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public IQueryable<ParametroUsuario> SelecionarPorParametro(int id)
        {
            return _repository.GetAll();
        }

        public ParametroUsuario SelecionarPorParametroUsuario(int parametro, int usuario)
        {
            return _repository.GetAll().Where(p => p.ParametroId == parametro && p.UsuarioId == usuario).FirstOrDefault();
        }

        public string ValidarDados(ParametroUsuario entity)
        {
            if (entity.ParametroId == 0)
                return "Parâmetro não informado!";
            else if (entity.UsuarioId == 0)
                return "Usuário não informado!";
            else if (string.IsNullOrWhiteSpace(entity.Valor))
                return "Valor não informado!";
            else
                return "";
        }
    }
}
