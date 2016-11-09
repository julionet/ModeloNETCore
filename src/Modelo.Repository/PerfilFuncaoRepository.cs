using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class PerfilFuncaoRepository : IPadraoRepository<PerfilFuncao>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<PerfilFuncao> _repository;
        private string _usuario = "";

        public PerfilFuncaoRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<PerfilFuncao>(context == null ? new ModeloContext() : context);
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

        public string Incluir(PerfilFuncao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                entity.Id = 0;
                mensagem = _repository.Insert(entity);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "I", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Alterar(PerfilFuncao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                PerfilFuncao entityold = _db.Set<PerfilFuncao>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(PerfilFuncao entity)
        {
            string mensagem = _repository.Delete(entity.Id);

            //if (mensagem == "")
            //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "E", entity.GetType().Name, ref mensagem);
            return mensagem;
        }

        public PerfilFuncao Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<PerfilFuncao> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<PerfilFuncao> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public PerfilFuncao Selecionar(int perfil, int funcao)
        {
            return _repository.GetAll().Where(p => p.PerfilId == perfil && p.FuncaoId == funcao).FirstOrDefault();
        }

        public IQueryable<PerfilFuncao> SelecionarPorPerfil(int id)
        {
            return _repository.GetAll().Where(p => p.PerfilId == id);
        }

        public IQueryable<PerfilFuncaoDTO> SelecionarPorPerfil(int perfil, int sistema)
        {
            return (from q in _db.PerfilFuncoes
                    join f in _db.Funcoes on q.FuncaoId equals f.Id
                    join m in _db.Modulos on f.ModuloId equals m.Id
                    where q.PerfilId == perfil && m.SistemaId == sistema
                    select new PerfilFuncaoDTO
                    {
                        FuncaoDescricao = f.Descricao,
                        FuncaoId = q.FuncaoId,
                        FuncaoManutencao = f.Manutencao,
                        Id = q.Id,
                        ModuloDescricao = m.Descricao,
                        ModuloId = f.ModuloId,
                        PermiteAlterar = q.PermiteAlterar,
                        PermiteExcluir = q.PermiteExcluir,
                        PermiteIncluir = q.PermiteIncluir,
                        PerfilId = q.PerfilId,
                        Selecionado = true
                    });
        }

        public string ValidarDados(PerfilFuncao entity)
        {
            if (entity.PerfilId == 0)
                return "Perfil não informado!";
            else if (entity.FuncaoId == 0)
                return "Funão não informada!";
            else
                return "";
        }
    }
}
