using Modelo.Entity;
using Modelo.Infrastructure;
using Modelo.Interface;
using System;
using System.Linq;

namespace Modelo.Repository
{
    public class BloqueioRepository : IPadraoRepository<Bloqueio>, IDisposable
    {
        private ModeloContext _db = new ModeloContext();
        private IRepository<Bloqueio> _repository;
        private string _usuario = "";

        public BloqueioRepository(ModeloContext context = null, string usuario = "")
        {
            _repository = new Repository<Bloqueio>(context == null ? new ModeloContext() : context);
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

        public string Incluir(Bloqueio entity)
        {
            return _repository.Insert(entity);
        }

        public string Alterar(Bloqueio entity)
        {
            return _repository.Update(entity);
        }

        public string Excluir(Bloqueio entity)
        {
            return _repository.Delete(entity.Id);
        }

        public Bloqueio Selecionar(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Bloqueio> SelecionarTodos()
        {
            return _repository.GetAll();
        }

        public IQueryable<Bloqueio> Filtrar(string condicao)
        {
            return _repository.Filter(condicao);
        }

        public Bloqueio SelecionarRegistroBloqueado(string classe, int referencia)
        {
            return _repository.GetAll().Where(p => p.Classe == classe && p.Referencia == referencia).FirstOrDefault();
        }

        public string BloquearRegistro(string classe, int usuario, int referencia)
        {
            bool bloqueado = false;
            Bloqueio bloqueio = this.SelecionarRegistroBloqueado(classe, referencia);

            if (bloqueio != null)
            {
                int iTempo = 0;
                if (int.TryParse(new ParametroRepository().SelecionarValorParametro("001", 0), out iTempo))
                {
                    if (DateTime.Now.Subtract(bloqueio.DataHora).TotalSeconds > iTempo)
                        this.Excluir(bloqueio);
                    else
                        bloqueado = true;
                }
                else
                    bloqueado = true;
            }

            if (bloqueado)
                return string.Format("Registro bloqueado por outro usuário!\r\nTente acessá-lo novamente mais tarde.\r\n\r\nUsuario: {0}\r\nComputador: {1}\r\nData/hora: {2}", bloqueio.Usuario, bloqueio.Computador, bloqueio.DataHora.ToString("dd/MM/yyyy HH:mm:ss"));
            else
                return this.Incluir(new Bloqueio()
                {
                    Classe = classe,
                    Computador = Environment.MachineName,
                    DataHora = new DatabaseRepository().GetDateTimeServer(),
                    Referencia = referencia,
                    Usuario = new UsuarioRepository().Selecionar(usuario).Login
                });
        }

        public void AtualizarBloqueio(string classe, int referencia)
        {
            Bloqueio bloqueio = this.SelecionarRegistroBloqueado(classe, referencia);   
            bloqueio.DataHora = new DatabaseRepository().GetDateTimeServer();
            this.Alterar(bloqueio);
        }

        public void ExcluirBloqueio(string classe, int referencia)
        {
            Bloqueio bloqueio = this.SelecionarRegistroBloqueado(classe, referencia);
            if (bloqueio != null)
                this.Excluir(bloqueio);
        }

        public string ConsultarBloqueio(string classe, int referencia)
        {
            bool bloqueado = false;
            Bloqueio bloqueio = this.SelecionarRegistroBloqueado(classe, referencia);

            if (bloqueio != null)
            {
                int iTempo = 0;
                if (int.TryParse(new ParametroRepository().SelecionarValorParametro("001", 0), out iTempo))
                {
                    if (DateTime.Now.Subtract(bloqueio.DataHora).TotalSeconds > iTempo)
                        this.Excluir(bloqueio);
                    else
                        bloqueado = true;
                }
                else
                    bloqueado = true;
            }

            if (bloqueado)
                return string.Format("Registro bloqueado por outro usuário!\r\nTente acessá-lo novamente mais tarde.\r\n\r\nUsuario: {0}\r\nComputador: {1}\r\nData/hora: {2}", bloqueio.Usuario, bloqueio.Computador, bloqueio.DataHora.ToString("dd/MM/yyyy HH:mm:ss"));
            else
                return "";
        }
    }
}
