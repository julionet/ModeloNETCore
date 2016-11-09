using Microsoft.EntityFrameworkCore;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Repository
{
    public class AtualizacaoRepository : IPadraoRepository<Atualizacao>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Atualizacao> _repository;
        private string _usuario = "";

        public AtualizacaoRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Atualizacao>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Atualizacao entity)
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

        public string Alterar(Atualizacao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                Atualizacao entityold = _db.Set<Atualizacao>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(Atualizacao entity)
        {
            string mensagem = this.ValidarExclusao(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Delete(entity.Id);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "E", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public Atualizacao Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Atualizacao> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public bool ExisteAtualizacao(int numero)
        {
            return this.SelecionarTodos().Where(p => p.Numero == numero).Count() != 0;
        }

        public IQueryable<Atualizacao> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public string ValidarDados(Atualizacao entity)
        {
            return "";
        }

        public string ValidarExclusao(Atualizacao entity)
        {
            return "";
        }

        public int Contar()
        {
            return this.SelecionarTodos().Count();
        }

        public int ContarPendente()
        {
            return this.SelecionarTodosPendente().Count();
        }

        public IQueryable<Atualizacao> SelecionarTodosPendente()
        {
            if (_db.Database.GetDbConnection() is System.Data.SqlClient.SqlConnection)
                return this.SelecionarTodos().Where(p => (p.Banco == "S" || p.Banco == "T" || p.Banco == "Q") && (p.Status == "P" || p.Status == "E"));
            else
                return null;
        }

        public string AtualizarVersao(Atualizacao atualizacao)
        {
            string mensagem = new DatabaseRepository().ExecutarComandoSQL(atualizacao.Sql);
            if (mensagem == "")
            {
                atualizacao.Status = "O";
                atualizacao.Mensagem = "Atualização realizada com sucesso!";
            }
            else
            {
                atualizacao.Status = "E";
                atualizacao.Mensagem = mensagem;
            }
            this.Alterar(atualizacao);
            return mensagem;
        }

        public string FinalizarAtualizacoes()
        {
            string mensagem = "";
            foreach (Atualizacao atualizacao in this.SelecionarTodosPendente())
            {
                atualizacao.Status = "O";
                atualizacao.Mensagem = "Não necessário executar atualização";
                mensagem = this.Alterar(atualizacao);
                if (mensagem != "")
                    break;
            }
            return mensagem;
        }
    }
}
