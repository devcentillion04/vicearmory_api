using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ViceArmory.Bal.Database
{
    public class MySqlService : IDatabaseService, IDisposable
    {
        private MySqlConnection connection;

        private MySqlTransaction transaction;

        public MySqlService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConnectionString");
            this.connection = new MySqlConnection(connectionString);
        }

        public void BeginTransaction()
        {
            if (this.transaction == null)
            {
                this.transaction = this.connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
            }
        }

        public void Open()
        {
            if (this.connection.State == ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        public void Close()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }

        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            try
            {
                this.Open();
                return this.connection.Query<T>(query);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.Close();
            }
        }

        public IEnumerable<T> ExecuteStoreProcedure<T>(string storeProcName, object parameters)
        {
            try
            {
                this.Open();
                return this.connection.Query<T>(storeProcName, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.Close();
            }
        }

        public IEnumerable<T> RecordList<T>(string storeProcName, object parameters)
        {
            return this.ExecuteStoreProcedure<T>(storeProcName, parameters);
        }

        public string RecordAddUpdateDelete(string storeProcName, object parameters)
        {
            try
            {
                this.Open();
                return Convert.ToString(this.connection.Execute(storeProcName, parameters, commandType: CommandType.StoredProcedure));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.Close();
            }
        }

        public void Dispose()
        {
            this.transaction = null;
            this.connection = null;
        }
    }

}
