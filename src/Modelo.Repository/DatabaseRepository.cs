using Microsoft.EntityFrameworkCore;
using Modelo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Modelo.Repository
{
    public class DatabaseRepository
    {
        public int GetDatabaseType()
        {
            ModeloContext db = new ModeloContext();
            if (db.Database.GetDbConnection() is SqlConnection)
                return 1;
            else
                return -1;
        }

        public DateTime GetDateTimeServer()
        {
            ModeloContext db = new ModeloContext();
            if (db.Database.GetDbConnection() is SqlConnection)
            {
                using (var connection = db.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "select GETDATE()";
                        return (DateTime)command.ExecuteScalar();
                    }
                }
            }
            return DateTime.Now;
        }

        public string GetSerialNumberHD()
        {
            ModeloContext db = new ModeloContext();
            if (db.Database.GetDbConnection() is SqlConnection)
                using (var connection = db.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "declare @hd varchar(1000) " +
                                              "create table #serialhd(data varchar(1000)) " +
                                              " " +
                                              "insert into #serialhd " +
                                              "exec xp_cmdshell 'vol' " +
                                              " " +
                                              "select @hd = substring(data, charindex('-', data, 1) - 4, 4) + substring(data, charindex('-', data, 1) + 1, 4) " +
                                              "from #serialhd " +
                                              "where data like '%-%' " +
                                              " " +
                                              "drop table #serialhd " +
                                              "select @hd";
                        return command.ExecuteScalar().ToString();
                    }
                }
            else
                return "12345678";
        }

        public DbDataReader ExecutarSQL<T>(string sql)
        {
            ModeloContext db = new ModeloContext();
            using (var connection = db.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                
            }
        }

        public string ExecutarComandoSQL(string sql)
        {
            try
            {
                int erro = new ModeloContext().Database.ExecuteSqlCommand(sql);
                return "";
            }
            catch (Exception erro)
            {
                return erro.Message;
            }
        }
    }
}
