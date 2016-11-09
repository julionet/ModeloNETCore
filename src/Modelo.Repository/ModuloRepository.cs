using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class ModuloRepository : IPadraoRepository<Modulo>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Modulo> _repository;
        private string _usuario = "";

        public ModuloRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Modulo>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Modulo entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);
            }
            return mensagem;
        }

        public string Alterar(Modulo entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Update(entity);
            }
            return mensagem;
        }

        public string Excluir(Modulo entity)
        {
            string mensagem = this.ValidarExclusao(entity);
            if (mensagem == "")
            {
                mensagem = this.ExcluirCascata(entity);
                if (mensagem == "")
                    mensagem = _repository.Delete(entity.Id);
            }
            return mensagem;
        }

        public Modulo Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Modulo> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Modulo> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public string ExcluirCascata(Modulo entity)
        {
            string mensagem = "";
            foreach (Funcao registro in new FuncaoRepository().SelecionarPorModulo(entity.Id).ToList())
                mensagem = new FuncaoRepository(_repository.GetContext() as ModeloContext).Excluir(registro);
            return mensagem;
        }

        public IQueryable<Modulo> SelecionarPorSistema(int id)
        {
            return _repository.GetAll().Where(p => p.SistemaId == id);
        }

        public IQueryable<Modulo> SelecionarPorSistemaUsuario(int sistema, int usuario)
        {
            Usuario us = new UsuarioRepository().Selecionar(usuario);

            IQueryable<int> dados;

            if (us.Master)
                dados = (from m in _db.Modulos
                         where !m.Administracao && m.SistemaId == sistema
                         select m.Id);
            else
                dados = (from q in _db.UsuarioFuncoes
                         join f in _db.Funcoes on q.FuncaoId equals f.Id
                         join m in _db.Modulos on f.ModuloId equals m.Id
                         where q.UsuarioId == usuario && m.SistemaId == sistema
                         select m.Id).Union((from pf in _db.PerfilFuncoes
                                          join f in _db.Funcoes on pf.FuncaoId equals f.Id
                                          join m in _db.Modulos on f.ModuloId equals m.Id
                                          where (from up in _db.UsuarioPerfis
                                                 where up.UsuarioId == usuario && m.SistemaId == sistema
                                                 select up.PerfilId).Contains(pf.PerfilId)
                                          select m.Id));

            if (us.Administrador)
                dados = dados.Union((from m in _db.Modulos
                                     where m.Administracao && m.SistemaId == sistema
                                     select m.Id));

            return (from m in _db.Modulos
                    where dados.Distinct().Contains(m.Id)
                    select m);
        }

        public string ValidarDados(Modulo entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Descricao))
                return "Descrição não informada!";
            else if (string.IsNullOrWhiteSpace(entity.Codigo))
                return "Código não informado!";
            else if (entity.Cor == 0)
                return "Cor não informada!";
            else if (entity.QuantidadeFuncao == 0)
                return "Nenhuma função associada ao módulo!";
            else
                return "";
        }

        public string ValidarExclusao(Modulo entity)
        {
            if (new UsuarioFuncaoRepository().SelecionarTodos().Where(p => p.Funcao.ModuloId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais usuários!";
            else if (new PerfilFuncaoRepository().SelecionarTodos().Where(p => p.Funcao.ModuloId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais perfis!";
            else
                return "";
        }
    }
}
