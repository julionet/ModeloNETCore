using Modelo.Dto;
using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class UsuarioFuncaoRepository : IPadraoRepository<UsuarioFuncao>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<UsuarioFuncao> _repository;
        private string _usuario = "";

        public UsuarioFuncaoRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<UsuarioFuncao>(context == null ? new ModeloContext() : context);
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

        public string Incluir(UsuarioFuncao entity)
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

        public string Alterar(UsuarioFuncao entity)
        {
            string mensagem = this.ValidarDados(entity);
            if (mensagem == "")
            {
                UsuarioFuncao entityold = _db.Set<UsuarioFuncao>().First(p => p.Id == entity.Id);

                mensagem = _repository.Update(entity);
                //if (mensagem == "")
                //    new AuditoriaRepository().RegistrarAuditoria(entityold, entity, _usuario, "A", entity.GetType().Name, ref mensagem);
            }
            return mensagem;
        }

        public string Excluir(UsuarioFuncao entity)
        {
            string mensagem = _repository.Delete(entity.Id);
            //if (mensagem == "")
            //    new AuditoriaRepository().RegistrarAuditoria(entity, entity, _usuario, "E", entity.GetType().Name, ref mensagem);
            return mensagem;
        }

        public UsuarioFuncao Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<UsuarioFuncao> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<UsuarioFuncao> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public IQueryable<UsuarioFuncaoDTO> SelecionarPorUsuario(int usuario, int sistema)
        {
            return (from q in _db.UsuarioFuncoes
                    join f in _db.Funcoes on q.FuncaoId equals f.Id
                    join m in _db.Modulos on f.ModuloId equals m.Id
                    where q.UsuarioId == usuario && m.SistemaId == sistema
                    select new UsuarioFuncaoDTO
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
                        UsuarioId = q.UsuarioId,
                        Selecionado = true
                    });
        }

        public IQueryable<UsuarioFuncao> SelecionarPorUsuario(int usuario)
        {
            return this.SelecionarTodos().Where(p => p.UsuarioId == usuario);
        }

        public IQueryable<UsuarioFuncaoDTO> SelecionarAcessoPorUsuarioModulo(int usuario, int modulo, int sistema)
        {
            bool admin = false;
            bool master = false;
            if (usuario != 0)
            {
                Usuario us = new UsuarioRepository().Selecionar(usuario);
                admin = us.Administrador;
                master = us.Master;
            }

            IQueryable<UsuarioFuncaoDTO> dados;

            if (!master)
                dados =
                    (from q in _db.UsuarioFuncoes
                     join u in _db.Usuarios on q.UsuarioId equals u.Id
                     join f in _db.Funcoes on q.FuncaoId equals f.Id
                     join m in _db.Modulos on f.ModuloId equals m.Id
                     where q.UsuarioId == usuario
                     select new UsuarioFuncaoDTO
                     {
                         FuncaoId = q.FuncaoId,
                         FuncaoDescricao = f.Descricao,
                         FuncaoGrupo = f.Grupo,
                         ModuloId = f.ModuloId,
                         ModuloDescricao = m.Descricao,
                         ModuloCor = m.Cor,
                         SistemaId = m.SistemaId,
                         PermiteIncluir = q.PermiteIncluir,
                         PermiteAlterar = q.PermiteAlterar,
                         PermiteExcluir = q.PermiteExcluir
                     }).Union((from pf in _db.PerfilFuncoes
                               join f in _db.Funcoes on pf.FuncaoId equals f.Id
                               join m in _db.Modulos on f.ModuloId equals m.Id
                               where (from up in _db.UsuarioPerfis
                                      where up.UsuarioId == usuario
                                      select up.PerfilId).Contains(pf.PerfilId)
                               select new UsuarioFuncaoDTO
                               {
                                   FuncaoId = pf.FuncaoId,
                                   FuncaoDescricao = f.Descricao,
                                   FuncaoGrupo = f.Grupo,
                                   ModuloId = f.ModuloId,
                                   ModuloDescricao = m.Descricao,
                                   ModuloCor = m.Cor,
                                   SistemaId = m.SistemaId,
                                   PermiteIncluir = pf.PermiteIncluir,
                                   PermiteAlterar = pf.PermiteAlterar,
                                   PermiteExcluir = pf.PermiteExcluir
                               }));
            else
                dados = (from f in _db.Funcoes
                         join m in _db.Modulos on f.ModuloId equals m.Id
                         where !m.Administracao && m.SistemaId == sistema
                         select new UsuarioFuncaoDTO
                         {
                             FuncaoId = f.Id,
                             FuncaoDescricao = f.Descricao,
                             FuncaoGrupo = f.Grupo,
                             ModuloId = f.ModuloId,
                             ModuloDescricao = m.Descricao,
                             ModuloCor = m.Cor,
                             SistemaId = m.SistemaId,
                             PermiteIncluir = true,
                             PermiteAlterar = true,
                             PermiteExcluir = true
                         });

            if (admin)
                dados = dados.Union(
                        (from f in _db.Funcoes
                         join m in _db.Modulos on f.ModuloId equals m.Id
                         where m.Administracao && m.SistemaId == sistema
                         select new UsuarioFuncaoDTO
                         {
                             FuncaoId = f.Id,
                             FuncaoDescricao = f.Descricao,
                             FuncaoGrupo = f.Grupo,
                             ModuloId = f.ModuloId,
                             ModuloDescricao = m.Descricao,
                             ModuloCor = m.Cor,
                             SistemaId = m.SistemaId,
                             PermiteIncluir = true,
                             PermiteAlterar = true,
                             PermiteExcluir = true
                         }));

            if (modulo != 0)
                dados = dados.Where(p => p.ModuloId == modulo);

            return dados.Distinct();
        }

        public UsuarioFuncao Selecionar(int usuario, int funcao)
        {
            return this.SelecionarTodos().Where(p => p.UsuarioId == usuario && p.FuncaoId == funcao).FirstOrDefault();
        }

        public string ValidarDados(UsuarioFuncao entity)
        {
            if (entity.UsuarioId == 0)
                return "Usuário não informado!";
            else if (entity.FuncaoId == 0)
                return "Função não informada!";
            else
                return "";
        }
    }
}
