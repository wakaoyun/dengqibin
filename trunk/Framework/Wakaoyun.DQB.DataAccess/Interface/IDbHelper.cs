//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 10:35:25 AM
//功能说明：
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Wakaoyun.DQB.DataAccess.Enum;

namespace Wakaoyun.DQB.DataAccess.Interface
{
    public interface IDbHelper
    {
        DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText);
        DataSet ExecuteDataset(IDbConnection connection, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText);
        DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText);
        DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        int ExecuteNonQuery(CommandType commandType, string commandText);
        int ExecuteNonQuery(string spName, params object[] parameterValues);
        int ExecuteNonQuery(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText);
        int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText);
        int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText);
        int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        IDataReader ExecuteReader(string spName, params object[] parameterValues);
        IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText);
        IDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText);
        IDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText);
        IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters);
        IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction, CommandType commandType,
            string commandText, IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership);
        //Hashtable ExecuteReturnCacheTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler);
        //object ExecuteReturnTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler);
        object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText);
        object ExecuteScalar(IDbConnection connection, string spName, params object[] parameterValues);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText);
        object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues);
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText);
        object ExecuteScalar(string connectionString, string spName, params object[] parameterValues);
        object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        //bool ExecuteTransation(IList<TransationParam> paramList, string conString, IExceptionHandler exceptionHandler);
        int GetCount(string tblName, string condition);
        T GetEntity<T>(string spName, params object[] parameterValues) where T : class, new();
        T GetEntity<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T : class, new();
        T GetEntity<T>(IDbConnection connection, CommandType commandType, string commandText) where T : class, new();
        T GetEntity<T>(IDbConnection connection, string spName, params object[] parameterValues) where T : class, new();
        T GetEntity<T>(IDbTransaction transaction, CommandType commandType, string commandText) where T : class, new();
        T GetEntity<T>(IDbTransaction transaction, string spName, params object[] parameterValues) where T : class, new();
        T GetEntity<T>(string connectionString, CommandType commandType, string commandText) where T : class, new();
        T GetEntity<T>(string connectionString, string spName, params object[] parameterValues) where T : class, new();
        T GetEntity<T>(IDbConnection connection, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        T GetEntity<T>(IDbTransaction transaction, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        T GetEntity<T>(string connectionString, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        T GetEntity<T>(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText,
            IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership) where T : class, new();
        List<T> GetList<T>(string spName, params object[] parameterValues) where T : class, new();
        List<T> GetList<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters) where T : class, new();
        List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText) where T : class, new();
        List<T> GetList<T>(IDbConnection connection, string spName, params object[] parameterValues) where T : class, new();
        List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText) where T : class, new();
        List<T> GetList<T>(IDbTransaction transaction, string spName, params object[] parameterValues) where T : class, new();
        List<T> GetList<T>(string connectionString, CommandType commandType, string commandText) where T : class, new();
        List<T> GetList<T>(string connectionString, string spName, params object[] parameterValues) where T : class, new();
        List<T> GetList<T>(IDbConnection connection, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        List<T> GetList<T>(IDbTransaction transaction, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        List<T> GetList<T>(string connectionString, CommandType commandType, string commandText,
            params IDbDataParameter[] commandParameters) where T : class, new();
        List<T> GetList<T>(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText,
            IDbDataParameter[] commandParameters, DbConnectionOwnership connectionOwnership) where T : class, new();
        IDataReader GetPageList(string tblName, int pageSize, int pageIndex, string fldSort, bool sort, string condition);
        string ConnectionString { get; set; }
        IExceptionHandler GetListExceptionHandler { get; set; }
    }
}
