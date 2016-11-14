using Cartsys.Entity;
using Modelo.Library;
using System;
using System.Linq;
using System.Reflection;

namespace Cartsys.Repository
{
    public class AuditoriaRepository
    {
        public AuditoriaRepository()
        {
        }

        public bool RegistrarAuditoria(object classeoriginal, object classecurrent, string usuario, string acao, string entidade, ref string mensagem)
        {
            string[] tipoauditoria = { "Int32", "String", "Boolean", "DateTime", "Double", "Nullable`1" };

            if (usuario == "") return true;

            bool bModificou = true;
            if (acao == "A")
                bModificou = Classe.HasChanges(classeoriginal, classecurrent);

            if (!bModificou) 
                return true;

            bool bAuditarIncluir = true;
            bool bAuditarAlterar = true;
            bool bAuditarExcluir = true;

            AuditoriaEntidade auditoriaentidade = null;
            if (((acao == "I") && (bAuditarIncluir)) || ((acao == "A") && (bAuditarAlterar)) || ((acao == "E") && (bAuditarExcluir)))
                auditoriaentidade = new AuditoriaEntidade()
                {
                    DataHora = DateTime.Now,
                    Acao = acao,
                    Entidade = entidade,
                    Usuario = usuario
                };

            if ((acao == "I") && (bAuditarIncluir))
            {
                foreach (PropertyInfo prop in classecurrent.GetType().GetProperties())
                    if (prop.Name.ToLower() == "id")
                    {
                        auditoriaentidade.RegistroId = Convert.ToInt32(prop.GetValue(classecurrent, null));
                        break;
                    }

                mensagem = new AuditoriaEntidadeRepository().Incluir(auditoriaentidade);
                if (mensagem != "")
                    return false;
                else
                {
                    foreach (PropertyInfo propriedade in classeoriginal.GetType().GetProperties())
                    {
                        if (propriedade.GetCustomAttributes(typeof(NoAudit), true).Length > 0) 
                            continue;

                        Type colType = propriedade.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];

                        foreach (PropertyInfo propriedade2 in classecurrent.GetType().GetProperties())
                        {
                            if ((propriedade2.Name == propriedade.Name) && (tipoauditoria.Contains(colType.Name)))
                            {
                                AuditoriaAtributo auditoriaatributo = new AuditoriaAtributo();
                                auditoriaatributo.AuditoriaEntidadeId = auditoriaentidade.Id;
                                auditoriaatributo.Atributo = propriedade.Name;
                                auditoriaatributo.ValorAntigo = null;
                                if (propriedade2.GetValue(classecurrent, null) != null)
                                    auditoriaatributo.ValorNovo = propriedade2.GetValue(classecurrent, null).ToString();
                                else
                                    auditoriaatributo.ValorNovo = "";

                                mensagem = new AuditoriaAtributoRepository().Incluir(auditoriaatributo);
                                if (mensagem != "")
                                    return false;
                            }
                        }
                    }
                }
            }
            else if ((acao == "A") && (bAuditarAlterar))
            {
                foreach (PropertyInfo prop in classeoriginal.GetType().GetProperties())
                    if (prop.Name.ToLower() == "id")
                    {
                        auditoriaentidade.RegistroId = Convert.ToInt32(prop.GetValue(classeoriginal, null));
                        break;
                    }

                mensagem = new AuditoriaEntidadeRepository().Incluir(auditoriaentidade);
                if (mensagem != "")
                    return false;
                else
                {
                    foreach (PropertyInfo propriedade in classeoriginal.GetType().GetProperties())
                    {
                        if (propriedade.GetCustomAttributes(typeof(NoAudit), true).Length > 0) continue;

                        Type colType = propriedade.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];

                        foreach (PropertyInfo propriedade2 in classecurrent.GetType().GetProperties())
                        {
                            if ((propriedade2.Name == propriedade.Name) && (tipoauditoria.Contains(colType.Name)))
                            {
                                AuditoriaAtributo auditoriaatributo = new AuditoriaAtributo();
                                auditoriaatributo.AuditoriaEntidadeId = auditoriaentidade.Id;
                                auditoriaatributo.Atributo = propriedade.Name;
                                if (propriedade.GetValue(classeoriginal, null) != null)
                                    auditoriaatributo.ValorAntigo = propriedade.GetValue(classeoriginal, null).ToString();
                                else
                                    auditoriaatributo.ValorAntigo = "";
                                if (propriedade2.GetValue(classecurrent, null) != null)
                                    auditoriaatributo.ValorNovo = propriedade2.GetValue(classecurrent, null).ToString();
                                else
                                    auditoriaatributo.ValorNovo = "";

                                mensagem = new AuditoriaAtributoRepository().Incluir(auditoriaatributo);
                                if (mensagem != "")
                                    return false;
                            }
                        }
                    }
                }
            }
            else if ((acao == "E") && (bAuditarExcluir))
            {
                foreach (PropertyInfo prop in classeoriginal.GetType().GetProperties())
                    if (prop.Name.ToLower() == "id")
                    {
                        auditoriaentidade.RegistroId = Convert.ToInt32(prop.GetValue(classeoriginal, null));
                        break;
                    }

                mensagem = new AuditoriaEntidadeRepository().Incluir(auditoriaentidade);
                if (mensagem != "")
                    return false;
                else
                {
                    foreach (PropertyInfo propriedade in classeoriginal.GetType().GetProperties())
                    {
                        if (propriedade.GetCustomAttributes(typeof(NoAudit), true).Length > 0) continue;

                        Type colType = propriedade.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            colType = colType.GetGenericArguments()[0];

                        foreach (PropertyInfo propriedade2 in classecurrent.GetType().GetProperties())
                        {
                            if ((propriedade2.Name == propriedade.Name) && (tipoauditoria.Contains(colType.Name)))
                            {
                                AuditoriaAtributo auditoriaatributo = new AuditoriaAtributo();
                                auditoriaatributo.AuditoriaEntidadeId = auditoriaentidade.Id;
                                auditoriaatributo.Atributo = propriedade.Name;
                                if (propriedade.GetValue(classeoriginal, null) != null)
                                    auditoriaatributo.ValorAntigo = propriedade.GetValue(classeoriginal, null).ToString();
                                else
                                    auditoriaatributo.ValorAntigo = "";
                                auditoriaatributo.ValorNovo = null;

                                mensagem = new AuditoriaAtributoRepository().Incluir(auditoriaatributo);
                                if (mensagem != "")
                                    return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
