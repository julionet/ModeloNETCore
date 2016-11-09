using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class SistemaRepository : IPadraoRepository<Sistema>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Sistema> _repository;
        private string _usuario = "";

        public SistemaRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Sistema>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Sistema entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);
            }
            return mensagem;
        }

        public string Alterar(Sistema entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Update(entity);
            }
            return mensagem;
        }

        public string Excluir(Sistema entity)
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

        public Sistema Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Sistema> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Sistema> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public string ExcluirCascata(Sistema entity)
        {
            string mensagem = "";
            foreach (Modulo registro in new ModuloRepository().SelecionarPorSistema(entity.Id).ToList())
                mensagem = new ModuloRepository(_repository.GetContext() as ModeloContext).Excluir(registro);
            return mensagem;
        }

        public IQueryable<Sistema> SelecionarAtivos()
        {
            return _repository.GetAll().Where(p => p.Ativo && !p.Gerenciador);
        }

        public IQueryable<Sistema> SelecionarAtivosPorTipo(string tipo)
        {
            return this.SelecionarAtivos().Where(p => p.Tipo == tipo);
        }

        public string ValidarDados(Sistema entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Descricao))
                return "Descrição não informada!";
            else if (string.IsNullOrWhiteSpace(entity.Codigo))
                return "Código não informado!";
            else if (this.SelecionarTodos().Where(p => p.Codigo == entity.Codigo && p.Id != entity.Id).Count() != 0)
                return "Código é uma informação exclusiva!";
            else if (string.IsNullOrWhiteSpace(entity.Tipo))
                return "Tipo não informado!";
            else if (string.IsNullOrWhiteSpace(entity.Interface))
                return "Interface não informada!";
            else if (entity.Linha == 0)
                return "Número de linhas não informado!";
            else if (entity.Tamanho == 0)
                return "Tamanho não informado!";
            else if (entity.QuantidadeModulo == 0)
                return "Nenhum módulo associado ao sistema!";
            else
                return "";
        }

        public string ValidarExclusao(Sistema entity)
        {
            if (new UsuarioFuncaoRepository().SelecionarTodos().Where(p => p.Funcao.Modulo.SistemaId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais usuários!";
            else if (new PerfilFuncaoRepository().SelecionarTodos().Where(p => p.Funcao.Modulo.SistemaId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais perfis!";
            else
                return "";
        }
    }
}
