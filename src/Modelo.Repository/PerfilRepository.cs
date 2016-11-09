using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class PerfilRepository : IPadraoRepository<Perfil>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Perfil> _repository;
        private string _usuario = "";

        public PerfilRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Perfil>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Perfil entity)
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

        public string Alterar(Perfil entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                Perfil entityold = _db.Set<Perfil>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(Perfil entity)
        {
            string mensagem = this.ValidarExclusao(entity);
            if (mensagem == "")
            {
                mensagem = this.ExcluirCascata(entity);
                if (mensagem == "")
                {
                    mensagem = _repository.Delete(entity.Id);

                    //if (mensagem == "")
                    //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "E", entity.GetType().Name, ref mensagem);
                }
            }
            return mensagem;
        }

        public Perfil Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Perfil> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Perfil> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public string ExcluirCascata(Perfil entity)
        {
            string mensagem = "";
            foreach (PerfilFuncao registro in new PerfilFuncaoRepository().SelecionarPorPerfil(entity.Id).ToList())
                mensagem = new PerfilFuncaoRepository(_repository.GetContext() as ModeloContext, _usuario).Excluir(registro);
            return mensagem;
        }

        public string ValidarDados(Perfil entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Descricao))
                return "Descrição não informada!";
            else if (entity.QuantidadeFuncoes == 0)
                return "Nenhuma função foi associada ao perfil!";
            else
                return "";
        }

        public string ValidarExclusao(Perfil entity)
        {
            if ((from q in _db.UsuarioPerfis where q.PerfilId == entity.Id select q).Count() != 0)
                return "Não é permitido excluir um perfil associado a um ou mais usuários!";
            else
                return "";
        }
    }
}
