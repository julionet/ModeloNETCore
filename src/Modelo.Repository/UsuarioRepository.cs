using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using Modelo.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Modelo.Repository
{
    public class UsuarioRepository : IPadraoRepository<Usuario>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Usuario> _repository;
        private string _usuario = "";

        public UsuarioRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Usuario>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Usuario entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                mensagem = _repository.Insert(entity);

                //if (mensagem == "")
                //    mensagem = _repository.JoinEntity<Perfil>(entity.Perfil, entity.ListaPerfis);

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "I", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Alterar(Usuario entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                Usuario entityold = _db.Set<Usuario>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);

                //if (mensagem == "")
                //{
                //    _repository.GetContext().Entry(entity).Collection(c => c.Perfil).Load();
                //    mensagem = _repository.JoinEntity<Perfil>(entity.Perfil, entity.ListaPerfis);
                //}

                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(Usuario entity)
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

        public Usuario Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Usuario> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Usuario> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public IQueryable<UsuarioDTO> Filtrar_(string condicao)
        {
            return (from q in _db.Usuarios
                    select new UsuarioDTO
                    {
                        Administrador = q.Administrador,
                        AlterarSenha = q.AlterarSenha,
                        Bloqueado = q.Bloqueado,
                        DataAlteracao = q.DataAlteracao,
                        DiasExpirar = q.DiasExpirar,
                        FuncionarioId = q.FuncionarioId,
                        FuncionarioNome = "",
                        Id = q.Id,
                        Login = q.Login,
                        Master = q.Master,
                        Nome = q.Nome,
                        NuncaExpira = q.NuncaExpira,
                        Senha = q.Senha
                    }).Where(condicao);
        }

        public string ExcluirCascata(Usuario entity)
        {
            string mensagem = "";
            foreach (UsuarioFuncao registro in new UsuarioFuncaoRepository().SelecionarPorUsuario(entity.Id).ToList())
                mensagem = new UsuarioFuncaoRepository(_repository.GetContext() as ModeloContext, _usuario).Excluir(registro);
            return mensagem;
        }

        public Usuario Selecionar(string login)
        {
            return _repository.GetAll().Where(p => p.Login == login).FirstOrDefault();
        }

        public IQueryable<Usuario> SelecionarNaoMaster()
        {
            return SelecionarTodos().Where(p => !p.Master);
        }

        public IQueryable<Perfil> SelecionarPerfis(int usuario)
        {
            return (from q in _db.UsuarioPerfis
                    join p in _db.Perfis on q.PerfilId equals p.Id
                    where q.UsuarioId == usuario select p);
        }

        public string ValidarDados(Usuario entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Login))
                return "Login não informado!";
            else if (SelecionarTodos().Where(p => p.Login == entity.Login && p.Id != entity.Id).Count() != 0)
                return "Login já informado para outro usuário!";
            else if (string.IsNullOrWhiteSpace(entity.Senha))
                return "Senha não informada!";
            else
                return "";
        }

        public string ValidarExclusao(Usuario entity)
        {
            return "";
        }

        public string ValidarLogin(string usuario, string senha)
        {
            Usuario u = this.Selecionar(usuario.ToUpper());

            if (string.IsNullOrWhiteSpace(usuario))
                return "Usuário não informado!";
            else if (string.IsNullOrWhiteSpace(senha))
                return "Senha não informada!";
            else if (u == null)
                return "Usuário não cadastrado!";
            else if (u.Bloqueado)
                return "Usuário bloqueado!";
            else if (Security.Decrypt("Texto", u.Senha).ToUpper() != senha.ToUpper())
                return "Senha incorreta!";
            else
                return "";
        }

        public bool DeveAlterarSenha(string usuario)
        {
            Usuario u = this.Selecionar(usuario);
            return (u.AlterarSenha || (u.DataAlteracao.Value.AddDays(u.DiasExpirar ?? 0) < DateTime.Now));
        }

        public string AlterarSenha(string usuario, string senhaantiga, string novasenha, string confirmacao)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return "Usuário não informado!";
            else if (string.IsNullOrWhiteSpace(senhaantiga))
                return "Senha antiga não informada!";
            else if (string.IsNullOrWhiteSpace(novasenha))
                return "Nova senha não informada!";
            else if (string.IsNullOrWhiteSpace(confirmacao))
                return "Confirmação não informada!";
            else if (novasenha != confirmacao)
                return "Confirmação não confere com a nova senha!";
            else
            {
                Usuario u = this.Selecionar(usuario);
                if (u == null)
                    return "Usuário não cadastrado!";
                else if (Security.Decrypt("Texto", u.Senha).ToUpper() != senhaantiga.ToUpper())
                    return "Senha antiga está incorreta!";
                else if (Security.Decrypt("Texto", u.Senha).ToUpper() == novasenha.ToUpper())
                    return "Nova senha deve ser diferente da senha antiga!";
                else
                {
                    u.Senha = Security.Encrypt("Texto", novasenha);
                    u.AlterarSenha = false;
                    u.DataAlteracao = DateTime.Today;
                    return this.Alterar(u);
                }
            }
        }
    }
}
