using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class FuncaoRepository : IPadraoRepository<Funcao>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Funcao> _repository;
        private string _usuario = "";

        public FuncaoRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Funcao>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Funcao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);
            }
            return mensagem;
        }

        public string Alterar(Funcao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Update(entity);
            }
            return mensagem;
        }

        public string Excluir(Funcao entity)
        {
            string mensagem = this.ValidarExclusao(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Delete(entity.Id);
            }
            return mensagem;
        }

        public Funcao Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Funcao> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Funcao> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public Funcao Selecionar(string codigo)
        {
            return _repository.GetAll().Where(p => p.Codigo == codigo).FirstOrDefault();
        }

        public IQueryable<FuncaoDTO> SelecionarTodosCompleto()
        {
            return (from q in _db.Funcoes
                    join m in _db.Modulos on q.ModuloId equals m.Id
                    where !m.Administracao && !m.Sistema.Gerenciador
                    select new FuncaoDTO
                    {
                        Codigo = q.Codigo,
                        Descricao = q.Descricao,
                        Grupo = q.Grupo,
                        Id = q.Id,
                        Manutencao = q.Manutencao,
                        ModuloDescricao = m.Descricao,
                        ModuloId = q.ModuloId,
                        NomeAssembly = q.NomeAssembly,
                        NomeFormulario = q.NomeFormulario,
                        RelatorioId = q.RelatorioId,
                        Tipo = q.Tipo
                    });
        }

        public IQueryable<Funcao> SelecionarPorModulo(int id)
        {
            return _repository.GetAll().Where(p => p.ModuloId == id);
        }

        public string ValidarDados(Funcao entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Descricao))
                return "Descrição não informada!";
            else if (string.IsNullOrWhiteSpace(entity.Codigo))
                return "Código não informado!";
            else if (string.IsNullOrWhiteSpace(entity.Tipo))
                return "Tipo não informado!";
            else if (entity.Tipo.Equals("F") && string.IsNullOrWhiteSpace(entity.NomeAssembly))
                return "Nome do assembly não informado!";
            else if (entity.Tipo.Equals("F") && string.IsNullOrWhiteSpace(entity.NomeFormulario))
                return "Nome do formulário não informado!";
            else if (entity.Tipo.Equals("R") && entity.RelatorioId == 0)
                return "Relatório não informado!";
            else
                return "";
        }

        public string ValidarExclusao(Funcao entity)
        {
            if (new UsuarioFuncaoRepository().SelecionarTodos().Where(p => p.FuncaoId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais usuários!";
            else if (new PerfilFuncaoRepository().SelecionarTodos().Where(p => p.FuncaoId == entity.Id).Count() != 0)
                return "Não é permitido excluir uma função associada a um ou mais perfis!";
            else
                return "";
        }
    }
}
