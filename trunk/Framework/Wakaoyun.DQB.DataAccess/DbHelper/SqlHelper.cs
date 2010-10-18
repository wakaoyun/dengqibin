//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 11:24:41 AM
//功能说明：Sql数据库执行帮助类
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wakaoyun.DQB.DataAccess.Interface;
using System.Data.SqlClient;
using System.Data;
using Wakaoyun.DQB.DataAccess.Enum;
using System.Xml;
using Wakaoyun.DQB.Framework;
using Wakaoyun.DQB.DataAccess.Entity;

namespace Wakaoyun.DQB.DataAccess.DbHelper
{
    /// <summary>
    /// Sql数据库执行帮助类
    /// </summary>
    public sealed class SqlHelper : DbHelperBase, IDbHelper
    {
        private string _ConnectionString;
        private IExceptionHandler _ExceptionHandler;
        //private static IDbCacheDependencyHelper Current;
        private bool LogDBException;
        private const string ParameterChar = "@";
        private bool TrackSql;

        private SqlHelper()
        {            
            this._ConnectionString = string.Empty;
            this._ExceptionHandler = null;
        }

        private SqlHelper(string connectionString)
        {           
            this._ExceptionHandler = null;
            this.ConnectionString = connectionString;
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters != null) && (dataRow != null))
            {
                int i = 0;
                foreach (SqlParameter commandParameter in commandParameters)
                {
                    if ((commandParameter.ParameterName == null) || (commandParameter.ParameterName.Length <= 1))
                    {
                        throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", i, commandParameter.ParameterName));
                    }
                    if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                    {
                        commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
                    }
                    i++;
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int i = 0;
                int j = commandParameters.Length;
                while (i < j)
                {
                    if (parameterValues[i] is IDbDataParameter)
                    {
                        IDbDataParameter paramInstance = (IDbDataParameter) parameterValues[i];
                        if (paramInstance.Value == null)
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = paramInstance.Value;
                        }
                    }
                    else if (parameterValues[i] == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[i];
                    }
                    i++;
                }
            }
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if (((p.Direction == ParameterDirection.InputOutput) || (p.Direction == ParameterDirection.Input)) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        public SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            SqlCommand cmd = new SqlCommand(spName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                for (int index = 0; index < sourceColumns.Length; index++)
                {
                    commandParameters[index].SourceColumn = sourceColumns[index];
                }
                AttachParameters(cmd, commandParameters);
            }
            return cmd;
        }

        //public static IDbCacheDependencyHelper CreateInstance(string connectionString)
        //{
        //    Current = DbHelperCache.Get(connectionString);
        //    if (Current == null)
        //    {
        //        Current = new MySqlHelper1(connectionString);
        //        DbHelperCache.Add(connectionString, Current);
        //    }
        //    return Current;
        //}

        public DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteDataset(connection, commandType, commandText, null);
        }

        public DataSet ExecuteDataset(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteDataset(transaction, commandType, commandText, null);
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return this.ExecuteDataset(connectionString, commandType, commandText, null);
        }

        public DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) connection, null, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                return ds;
            }
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) transaction.Connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return this.ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public DataSet ExecuteDatasetTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public DataSet ExecuteDatasetTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(this.ConnectionString, commandType, commandText, null);
        }

        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            string connectionString = this.ConnectionString;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                return this.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(connectionString, commandType, commandText, null);
        }

        public int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) connection, null, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) transaction.Connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return this.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        public int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public IDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(this.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteReader(this.ConnectionString, CommandType.StoredProcedure, spName);
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            IDataReader dr;
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("ConnectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                dr = this.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return dr;
        }

        public IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteReader(connection, commandType, commandText, null);
        }

        public IDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteReader(transaction, commandType, commandText, null);
        }

        public IDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return this.ExecuteReader(connectionString, commandType, commandText, null);
        }

        public IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        public IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            return this.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        public IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return this.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            IDataReader dr;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                dr = this.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return dr;
        }

        public IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership)
        {
            IDataReader dr;
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataReader dataReader;
                PrepareCommand(cmd, (SqlConnection) connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
                if (connectionOwnership == DbConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                dr = dataReader;
            }
            catch (Exception)
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            finally
            {
                if (mustCloseConnection && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
                connection.Dispose();
            }
            return dr;
        }

        public SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return (SqlDataReader)ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return (SqlDataReader)ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return (SqlDataReader)ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return (SqlDataReader)ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public SqlDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return (SqlDataReader)ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return (SqlDataReader)ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        //public Hashtable ExecuteReturnCacheTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler)
        //{
        //    SqlConnection transationConn = new SqlConnection(conString);
        //    SqlTransaction tran = null;
        //    SqlCommand sqlCmd = null;
        //    object execResult = null;
        //    Hashtable newTempHashCahe = null;
        //    int itemIndex = 0;
        //    try
        //    {
        //        transationConn.Open();
        //        tran = transationConn.BeginTransaction(IsolationLevel.Serializable);
        //        sqlCmd = new SqlCommand();
        //        sqlCmd.Connection = transationConn;
        //        sqlCmd.Transaction = tran;
        //        int reuslt = 0;
        //        newTempHashCahe = new Hashtable();
        //        foreach (TransationParam transationParam in paramList)
        //        {
        //            itemIndex++;
        //            sqlCmd.CommandText = transationParam.CommandText;
        //            sqlCmd.CommandType = transationParam.CommandType;
        //            sqlCmd.Parameters.Clear();
        //            if (transationParam.SqlParameterList.Length > 0)
        //            {
        //                AttachParameters(sqlCmd, (SqlParameter[]) transationParam.SqlParameterList);
        //                if ((transationParam.FieldHashKey != null) && (transationParam.FieldHashKey.Count > 0))
        //                {
        //                    foreach (string fieldName in transationParam.FieldHashKey.Keys)
        //                    {
        //                        sqlCmd.Parameters["@" + fieldName].Value = newTempHashCahe[transationParam.FieldHashKey[fieldName]];
        //                    }
        //                }
        //            }
        //            reuslt = sqlCmd.ExecuteNonQuery();
        //            if ((itemIndex == paramList.Count) || transationParam.IsNeedTransfer)
        //            {
        //                if (transationParam.CommandType == CommandType.StoredProcedure)
        //                {
        //                    execResult = transationParam.SqlParameterList[0].Value;
        //                }
        //                if (transationParam.TempCacheKey != null)
        //                {
        //                    newTempHashCahe.Add(transationParam.TempCacheKey, execResult);
        //                }
        //                else
        //                {
        //                    newTempHashCahe.Add(transationParam.SqlParameterList[0].ParameterName, execResult);
        //                }
        //            }
        //        }
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        execResult = null;
        //        try
        //        {
        //            tran.Rollback();
        //        }
        //        catch (MySqlException e)
        //        {
        //            if ((tran.Connection != null) && (exceptionHandler != null))
        //            {
        //                exceptionHandler.WriteLog(e);
        //            }
        //        }
        //        if (exceptionHandler != null)
        //        {
        //            exceptionHandler.WriteLog(ex);
        //        }
        //    }
        //    finally
        //    {
        //        if (transationConn.State != ConnectionState.Closed)
        //        {
        //            transationConn.Close();
        //            transationConn.Dispose();
        //        }
        //    }
        //    return newTempHashCahe;
        //}

        //public object ExecuteReturnTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler)
        //{
        //    SqlConnection transationConn = new SqlConnection(conString);
        //    SqlTransaction tran = null;
        //    SqlCommand sqlCmd = null;
        //    object execResult = null;
        //    Hashtable newTempHashCahe = null;
        //    int itemIndex = 0;
        //    try
        //    {
        //        transationConn.Open();
        //        tran = transationConn.BeginTransaction(IsolationLevel.Serializable);
        //        sqlCmd = new SqlCommand();
        //        sqlCmd.Connection = transationConn;
        //        sqlCmd.Transaction = tran;
        //        int reuslt = 0;
        //        newTempHashCahe = new Hashtable();
        //        foreach (TransationParam transationParam in paramList)
        //        {
        //            itemIndex++;
        //            sqlCmd.CommandText = transationParam.CommandText;
        //            sqlCmd.CommandType = transationParam.CommandType;
        //            sqlCmd.Parameters.Clear();
        //            if (transationParam.SqlParameterList.Length > 0)
        //            {
        //                AttachParameters(sqlCmd, (SqlParameter[]) transationParam.SqlParameterList);
        //                if ((transationParam.FieldHashKey != null) && (transationParam.FieldHashKey.Count > 0))
        //                {
        //                    foreach (string fieldName in transationParam.FieldHashKey.Keys)
        //                    {
        //                        sqlCmd.Parameters["@" + fieldName].Value = newTempHashCahe[transationParam.FieldHashKey[fieldName]];
        //                    }
        //                }
        //            }
        //            reuslt = sqlCmd.ExecuteNonQuery();
        //            if ((itemIndex == paramList.Count) || transationParam.IsNeedTransfer)
        //            {
        //                if (transationParam.CommandType == CommandType.StoredProcedure)
        //                {
        //                    execResult = transationParam.SqlParameterList[0].Value;
        //                }
        //                if (transationParam.TempCacheKey != null)
        //                {
        //                    newTempHashCahe.Add(transationParam.TempCacheKey, execResult);
        //                }
        //                else
        //                {
        //                    newTempHashCahe.Add(transationParam.SqlParameterList[0].ParameterName, execResult);
        //                }
        //            }
        //        }
        //        tran.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        execResult = null;
        //        try
        //        {
        //            tran.Rollback();
        //        }
        //        catch (MySqlException e)
        //        {
        //            if ((tran.Connection != null) && (exceptionHandler != null))
        //            {
        //                exceptionHandler.WriteLog(e);
        //            }
        //        }
        //        if (exceptionHandler != null)
        //        {
        //            exceptionHandler.WriteLog(ex);
        //        }
        //    }
        //    finally
        //    {
        //        if (transationConn.State != ConnectionState.Closed)
        //        {
        //            transationConn.Close();
        //            transationConn.Dispose();
        //        }
        //    }
        //    return execResult;
        //}

        public object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(connection, commandType, commandText, null);
        }

        public object ExecuteScalar(IDbConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(transaction, commandType, commandText, null);
        }

        public object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return this.ExecuteScalar(connectionString, commandType, commandText, null);
        }

        public object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        public object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) connection, null, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, (SqlConnection) transaction.Connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return this.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public object ExecuteScalarTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public object ExecuteScalarTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, (IDbDataParameter[]) commandParameters);
            }
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, dataRow);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        //public bool ExecuteTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler)
        //{
        //    SqlConnection transationConn = new SqlConnection(conString);
        //    SqlTransaction tran = null;
        //    SqlCommand sqlCmd = null;
        //    bool execResult = false;
        //    try
        //    {
        //        transationConn.Open();
        //        tran = transationConn.BeginTransaction();
        //        sqlCmd = new SqlCommand();
        //        sqlCmd.Connection = transationConn;
        //        sqlCmd.Transaction = tran;
        //        foreach (TransationParam transationParam in paramList)
        //        {
        //            sqlCmd.CommandText = transationParam.CommandText;
        //            sqlCmd.CommandType = transationParam.CommandType;
        //            sqlCmd.Parameters.Clear();
        //            if (transationParam.SqlParameterList.Length > 0)
        //            {
        //                AttachParameters(sqlCmd, (SqlParameter[]) transationParam.SqlParameterList);
        //            }
        //            int reuslt = 0;
        //            reuslt = sqlCmd.ExecuteNonQuery();
        //        }
        //        tran.Commit();
        //        execResult = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            tran.Rollback();
        //        }
        //        catch (MySqlException e)
        //        {
        //            if ((tran.Connection != null) && (exceptionHandler != null))
        //            {
        //                exceptionHandler.WriteLog(e);
        //            }
        //        }
        //        if (exceptionHandler != null)
        //        {
        //            exceptionHandler.WriteLog(ex);
        //        }
        //    }
        //    finally
        //    {
        //        if (transationConn.State != ConnectionState.Closed)
        //        {
        //            transationConn.Close();
        //            transationConn.Dispose();
        //        }
        //    }
        //    return execResult;
        //}

        public void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            this.FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataset(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                this.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                this.FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        public void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            this.FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataset(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                this.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
            else
            {
                this.FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        public void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                this.FillDataset(connection, commandType, commandText, dataSet, tableNames);
            }
        }

        public void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                this.FillDataset(connection, spName, dataSet, tableNames, parameterValues);
            }
        }

        public void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            this.FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            this.FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                this.FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
            }
        }

        private void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                if ((tableNames != null) && (tableNames.Length > 0))
                {
                    string tableName = "Table";
                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if ((tableNames[index] == null) || (tableNames[index].Length == 0))
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName = tableName + ((index + 1)).ToString();
                    }
                }
                dataAdapter.Fill(dataSet);
                command.Parameters.Clear();
            }
            if (mustCloseConnection)
            {
                connection.Close();
            }
        }

        public int GetCount(string tblName, string condition)
        {
            StringBuilder sql = new StringBuilder("select count(*) from " + tblName);
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(" where " + condition);
            }
            return ConvertHelper.ToInt(this.ExecuteScalar(this.ConnectionString, CommandType.Text, sql.ToString(), null).ToString());
        }

        public T GetEntity<T>(string spName, params object[] parameterValues) where T: class, new()
        {
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(this.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetEntity<T>(this.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetEntity<T>(this.ConnectionString, CommandType.StoredProcedure, spName);
        }

        public T GetEntity<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            T t;
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                t = this.GetEntity<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return t;
        }

        public T GetEntity<T>(IDbConnection connection, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetEntity<T>(connection, commandType, commandText, null);
        }

        public T GetEntity<T>(IDbConnection connection, string spName, params object[] parameterValues) where T: class, new()
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetEntity<T>(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetEntity<T>(connection, CommandType.StoredProcedure, spName);
        }

        public T GetEntity<T>(IDbTransaction transaction, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetEntity<T>(transaction, commandType, commandText, null);
        }

        public T GetEntity<T>(IDbTransaction transaction, string spName, params object[] parameterValues) where T: class, new()
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetEntity<T>(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetEntity<T>(transaction, CommandType.StoredProcedure, spName);
        }

        public T GetEntity<T>(string connectionString, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetEntity<T>(connectionString, commandType, commandText, null);
        }

        public T GetEntity<T>(string connectionString, string spName, params object[] parameterValues) where T: class, new()
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetEntity<T>(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetEntity<T>(connectionString, CommandType.StoredProcedure, spName);
        }

        public T GetEntity<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            return this.GetEntity<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        public T GetEntity<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return this.GetEntity<T>(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        public T GetEntity<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            T t;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                t = this.GetEntity<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return t;
        }

        public T GetEntity<T>(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership) where T: class, new()
        {
            T returnEntity = default(T);
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataReader dataReader;
                PrepareCommand(cmd, (SqlConnection) connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
                if (connectionOwnership == DbConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                returnEntity = default(T);
                returnEntity = EntityFactory.CreateInstance().FillEntity<T>(dataReader);
            }
            catch (Exception)
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            finally
            {
                if (mustCloseConnection && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
                connection.Dispose();
            }
            return returnEntity;
        }

        public List<T> GetList<T>(string spName, params object[] parameterValues) where T: class, new()
        {
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(this.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetList<T>(this.ConnectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetList<T>(this.ConnectionString, CommandType.StoredProcedure, spName);
        }

        public List<T> GetList<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            List<T> list;
            if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                base.CommandTrackLog(commandText);
                list = this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception ex)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw ex;
            }
            return list;
        }

        public List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetList<T>(connection, commandType, commandText, (IDbDataParameter[]) null);
        }

        public List<T> GetList<T>(IDbConnection connection, string spName, params object[] parameterValues) where T: class, new()
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetList<T>(connection, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetList<T>(connection, CommandType.StoredProcedure, spName);
        }

        public List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetList<T>(transaction, commandType, commandText, (IDbDataParameter[]) null);
        }

        public List<T> GetList<T>(IDbTransaction transaction, string spName, params object[] parameterValues) where T: class, new()
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetList<T>(transaction, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetList<T>(transaction, CommandType.StoredProcedure, spName);
        }

        public List<T> GetList<T>(string connectionString, CommandType commandType, string commandText) where T: class, new()
        {
            return this.GetList<T>(connectionString, commandType, commandText, (IDbDataParameter[]) null);
        }

        public List<T> GetList<T>(string connectionString, string spName, params object[] parameterValues) where T: class, new()
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(commandParameters, parameterValues);
                return this.GetList<T>(connectionString, CommandType.StoredProcedure, spName, commandParameters);
            }
            return this.GetList<T>(connectionString, CommandType.StoredProcedure, spName);
        }

        //public List<T> GetList<T>(string spName, ref CacheDependency cacheDependency, params object[] parameterValues) where T: class, new()
        //{
        //    if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
        //    {
        //        throw new ArgumentNullException("connectionString");
        //    }
        //    if ((spName == null) || (spName.Length == 0))
        //    {
        //        throw new ArgumentNullException("spName");
        //    }
        //    if ((parameterValues != null) && (parameterValues.Length > 0))
        //    {
        //        SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(this.ConnectionString, spName);
        //        AssignParameterValues(commandParameters, parameterValues);
        //        return this.GetList<T>(this.ConnectionString, CommandType.StoredProcedure, spName, ref cacheDependency, commandParameters);
        //    }
        //    return this.GetList<T>(this.ConnectionString, CommandType.StoredProcedure, spName);
        //}

        //public List<T> GetList<T>(CommandType commandType, string commandText, ref CacheDependency cacheDependency, params IDbDataParameter[] commandParameters) where T: class, new()
        //{
        //    List<T> list;
        //    if ((this.ConnectionString == null) || (this.ConnectionString.Length == 0))
        //    {
        //        throw new ArgumentNullException("connectionString");
        //    }
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(this.ConnectionString);
        //        connection.Open();
        //        base.LogSql(commandText);
        //        list = this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal, ref cacheDependency);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //        }
        //        throw ex;
        //    }
        //    return list;
        //}

        //public List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText, ref CacheDependency cacheDependency) where T: class, new()
        //{
        //    return this.GetList<T>(connection, commandType, commandText, ref cacheDependency, null);
        //}

        public List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            return this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        //public List<T> GetList<T>(IDbConnection connection, string spName, ref CacheDependency cacheDependency, params object[] parameterValues) where T: class, new()
        //{
        //    if (connection == null)
        //    {
        //        throw new ArgumentNullException("connection");
        //    }
        //    if ((spName == null) || (spName.Length == 0))
        //    {
        //        throw new ArgumentNullException("spName");
        //    }
        //    if ((parameterValues != null) && (parameterValues.Length > 0))
        //    {
        //        SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connection.ConnectionString, spName);
        //        AssignParameterValues(commandParameters, parameterValues);
        //        return this.GetList<T>(connection, CommandType.StoredProcedure, spName, ref cacheDependency, commandParameters);
        //    }
        //    return this.GetList<T>(connection, CommandType.StoredProcedure, spName, ref cacheDependency);
        //}

        //public List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText, ref CacheDependency cacheDependency) where T: class, new()
        //{
        //    return this.GetList<T>(transaction, commandType, commandText, ref cacheDependency, null);
        //}

        public List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return this.GetList<T>(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External);
        }

        //public List<T> GetList<T>(IDbTransaction transaction, string spName, ref CacheDependency cacheDependency, params object[] parameterValues) where T: class, new()
        //{
        //    if (transaction == null)
        //    {
        //        throw new ArgumentNullException("transaction");
        //    }
        //    if ((transaction != null) && (transaction.Connection == null))
        //    {
        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
        //    }
        //    if ((spName == null) || (spName.Length == 0))
        //    {
        //        throw new ArgumentNullException("spName");
        //    }
        //    if ((parameterValues != null) && (parameterValues.Length > 0))
        //    {
        //        SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
        //        AssignParameterValues(commandParameters, parameterValues);
        //        return this.GetList<T>(transaction, CommandType.StoredProcedure, spName, ref cacheDependency, commandParameters);
        //    }
        //    return this.GetList<T>(transaction, CommandType.StoredProcedure, spName, ref cacheDependency);
        //}

        public List<T> GetList<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T: class, new()
        {
            List<T> list;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                base.CommandTrackLog(commandText);
                list = this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal);
            }
            catch (Exception ex)
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw ex;
            }
            return list;
        }

        //public List<T> GetList<T>(string connectionString, CommandType commandType, string commandText, ref CacheDependency cacheDependency) where T: class, new()
        //{
        //    return this.GetList<T>(connectionString, commandType, commandText, ref cacheDependency, null);
        //}

        //public List<T> GetList<T>(string connectionString, string spName, ref CacheDependency cacheDependency, params object[] parameterValues) where T: class, new()
        //{
        //    if ((connectionString == null) || (connectionString.Length == 0))
        //    {
        //        throw new ArgumentNullException("connectionString");
        //    }
        //    if ((spName == null) || (spName.Length == 0))
        //    {
        //        throw new ArgumentNullException("spName");
        //    }
        //    if ((parameterValues != null) && (parameterValues.Length > 0))
        //    {
        //        SqlParameter[] commandParameters = SqlParameterCacheHelper.GetSpParameterSet(connectionString, spName);
        //        AssignParameterValues(commandParameters, parameterValues);
        //        return this.GetList<T>(connectionString, CommandType.StoredProcedure, spName, ref cacheDependency, commandParameters);
        //    }
        //    return this.GetList<T>(connectionString, CommandType.StoredProcedure, spName, ref cacheDependency);
        //}

        //public List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText, ref CacheDependency cacheDependency, params IDbDataParameter[] commandParameters) where T: class, new()
        //{
        //    return this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.External, ref cacheDependency);
        //}

        //public List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText, ref CacheDependency cacheDependency, params IDbDataParameter[] commandParameters) where T: class, new()
        //{
        //    if (transaction == null)
        //    {
        //        throw new ArgumentNullException("transaction");
        //    }
        //    if ((transaction != null) && (transaction.Connection == null))
        //    {
        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
        //    }
        //    return this.GetList<T>(transaction.Connection, transaction, commandType, commandText, commandParameters, DbConnectionOwnership.External, ref cacheDependency);
        //}

        //public List<T> GetList<T>(string connectionString, CommandType commandType, string commandText, ref CacheDependency cacheDependency, params IDbDataParameter[] commandParameters) where T: class, new()
        //{
        //    List<T> list;
        //    if ((connectionString == null) || (connectionString.Length == 0))
        //    {
        //        throw new ArgumentNullException("connectionString");
        //    }
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(connectionString);
        //        connection.Open();
        //        base.LogSql(commandText);
        //        list = this.GetList<T>(connection, null, commandType, commandText, commandParameters, DbConnectionOwnership.Internal, ref cacheDependency);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (connection != null)
        //        {
        //            connection.Close();
        //        }
        //        throw ex;
        //    }
        //    return list;
        //}

        public List<T> GetList<T>(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership) where T: class, new()
        {
            List<T> returnList = null;
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                SqlDataReader dataReader;
                PrepareCommand(cmd, (SqlConnection) connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
                base.CommandTrackLog(cmd.CommandText);
                if (connectionOwnership == DbConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                    }
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                returnList = new List<T>();
                returnList = EntityFactory.CreateInstance().FillEntityList<T>(dataReader);
            }
            catch (Exception ex)
            {
                base.ExceptionLog(ex, cmd.CommandText);
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw ex;
            }
            finally
            {
                if (mustCloseConnection && (connection.State != ConnectionState.Closed))
                {
                    connection.Close();
                }
                connection.Dispose();
            }
            return returnList;
        }

        //public List<T> GetList<T>(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership, ref CacheDependency cacheDependency) where T: class, new()
        //{
        //    List<T> returnList = null;
        //    if (connection == null)
        //    {
        //        throw new ArgumentNullException("connection");
        //    }
        //    bool mustCloseConnection = false;
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {
        //        SqlDataReader dataReader;
        //        PrepareCommand(cmd, (SqlConnection) connection, (SqlTransaction) transaction, commandType, commandText, (SqlParameter[]) commandParameters, out mustCloseConnection);
        //        base.LogSql(cmd.CommandText);
        //        if (connectionOwnership == DbConnectionOwnership.External)
        //        {
        //            dataReader = cmd.ExecuteReader();
        //        }
        //        else
        //        {
        //            dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        }
        //        bool canClear = true;
        //        foreach (SqlParameter commandParameter in cmd.Parameters)
        //        {
        //            if (commandParameter.Direction != ParameterDirection.Input)
        //            {
        //                canClear = false;
        //            }
        //        }
        //        if (canClear)
        //        {
        //            cmd.Parameters.Clear();
        //        }
        //        cacheDependency = null;
        //        returnList = new List<T>();
        //        returnList = EntityHelper.FillList<T>(dataReader, this.GetListExceptionHandler);
        //    }
        //    catch (Exception ex)
        //    {
        //        base.LogException(ex, cmd.CommandText);
        //        if (mustCloseConnection)
        //        {
        //            connection.Close();
        //        }
        //        throw new DataBaseException("数据库操作异常", cmd.CommandText, ex);
        //    }
        //    finally
        //    {
        //        if (mustCloseConnection && (connection.State != ConnectionState.Closed))
        //        {
        //            connection.Close();
        //        }
        //        connection.Dispose();
        //    }
        //    return returnList;
        //}

        public IDataReader GetPageList(string tblName, int pageSize, int pageIndex, string fldSort, bool sort, string condition)
        {
            string sql = this.GetPagerSQL(condition, pageSize, pageIndex, fldSort, tblName, sort);
            return this.ExecuteReader(this.ConnectionString, CommandType.Text, sql, null);
        }

        private string GetPagerSQL(string condition, int pageSize, int pageIndex, string fldSort, string tblName, bool sort)
        {
            string strSort = sort ? " DESC" : " ASC";
            StringBuilder strSql = new StringBuilder("select * from " + tblName);
            if (!string.IsNullOrEmpty(condition))
            {
                strSql.AppendFormat(" where {0} order by {1}{2}", condition, fldSort, strSort);
            }
            else
            {
                strSql.AppendFormat(" order by {0}{1}", fldSort, strSort);
            }
            strSql.AppendFormat(" limit {0},{1}", pageSize * (pageIndex - 1), pageSize);
            return strSql.ToString();
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
            {
                throw new ArgumentNullException("insertCommand");
            }
            if (deleteCommand == null)
            {
                throw new ArgumentNullException("deleteCommand");
            }
            if (updateCommand == null)
            {
                throw new ArgumentNullException("updateCommand");
            }
            if ((tableName == null) || (tableName.Length == 0))
            {
                throw new ArgumentNullException("tableName");
            }
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
            set
            {
                this._ConnectionString = value;
            }
        }

        public IExceptionHandler GetListExceptionHandler
        {
            get
            {
                return this._ExceptionHandler;
            }
            set
            {
                this._ExceptionHandler = value;
            }
        }
    }
}
