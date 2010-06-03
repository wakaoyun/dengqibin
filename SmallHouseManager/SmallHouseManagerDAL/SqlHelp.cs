using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SmallHouseManagerDAL
{
    class SqlHelp
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        private static readonly string masterConnection = ConfigurationManager.ConnectionStrings["masterConnectionString"].ToString();

        #region private utility methods

        /// <summary>
        /// 向SqlCommand添加参数
        /// </summary>
        /// <param name="command">要添加参数的SqlCommand</param>
        /// <param name="commandParameters">参数数组</param>
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters == null || commandParameters.Length == 0) throw new ArgumentNullException("commandParameters");
            command.Parameters.Clear();
            foreach (SqlParameter p in commandParameters)
                command.Parameters.Add(p);

        }

        #endregion private utility methods & constructors

        #region ExecuteNonQuery

        /// <summary>
        /// 执行无参SQL语句
        /// </summary>
        /// <param name="query">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int result;
                SqlCommand cmd = new SqlCommand(commandText, conn);                
                cmd.CommandType = commandType;
                conn.Open();
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }           
        }
        /// <summary>
        /// 执行带参数SQL语句
        /// </summary>
        /// <param name="query">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int result;
                SqlCommand cmd = new SqlCommand(commandText, conn);                
                AttachParameters(cmd, commandParameters);
                cmd.CommandType = commandType;
                conn.Open();
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        /// <summary>
        /// 执行带参数SQL语句
        /// </summary>
        /// <param name="query">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQueryForMaster(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            using (SqlConnection conn = new SqlConnection(masterConnection))
            {
                int result;
                SqlCommand cmd = new SqlCommand(commandText, conn);
                AttachParameters(cmd, commandParameters);
                cmd.CommandType = commandType;
                conn.Open();
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        /// <summary>
        /// 执行不带参数的SQL语句事务
        /// </summary>
        /// <param name="query">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQueryTransaction(string[] commandText, CommandType commandType)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int result=0;
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = commandType;
                cmd.Transaction = transaction;
                try
                {
                    foreach (string str in commandText)
                    {
                        cmd.CommandText = str;
                        result = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                transaction.Commit();
                return result;
            }
        }
        /// <summary>
        /// 执行带参数的SQL语句事务
        /// </summary>
        /// <param name="query">存储过程名或SQL语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQueryTransaction(string[] commandText, CommandType commandType, params SqlParameter[][] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int result = 0;
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = commandType;
                cmd.Transaction = transaction;
                try
                {
                    for (int i = 0; i < commandText.Length;i++ )
                    {
                        cmd.CommandText = commandText[i];
                        AttachParameters(cmd, commandParameters[i]);
                        result = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                transaction.Commit();
                return result;
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteReader

        /// <summary>
        /// 不带参数的SQL语句,使用后要关闭SqlDataReader对象
        /// </summary>
        /// <param name="commandText">SQL语句或存储过程名</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            if(string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            SqlConnection conn = new SqlConnection(connectionString);           
            SqlCommand cmd = new SqlCommand(commandText, conn);            
            cmd.CommandType = commandType;
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;            
        }
        /// <summary>
        /// 带参数的SQL语句,使用后要关闭SqlDataReader对象
        /// </summary>
        /// <param name="commandText">SQL语句或存储过程名</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandParameters">参数数组</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");
            SqlConnection conn = new SqlConnection(connectionString);           
            SqlCommand cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = commandType;
            AttachParameters(cmd, commandParameters);
            SqlDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;            
        }

        #endregion ExecuteReader
    }
}
