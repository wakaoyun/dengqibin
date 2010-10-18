//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 4:27:53 PM
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
using System.Collections;
using Wakaoyun.DQB.DataAccess.Interface;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Wakaoyun.DQB.DataAccess.DbHelper
{
    public sealed class SqlParameterCacheHelper
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        private SqlParameterCacheHelper()
        {
        }

        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string hashKey = connectionString + ":" + commandText;
            paramCache[hashKey] = commandParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];
            int i = 0;
            int j = originalParameters.Length;
            while (i < j)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
                i++;
            }
            return clonedParameters;
        }

        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            string hashKey = connectionString + ":" + commandText;
            SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];
            if (cachedParameters == null)
            {
                return null;
            }
            return CloneParameters(cachedParameters);
        }

        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter = false)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            string hashKey = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] cachedParameters = (SqlParameter[]) paramCache[hashKey];
            if (cachedParameters == null)
            {
                paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
                cachedParameters = (SqlParameter[])paramCache[hashKey];
            }
            return CloneParameters(cachedParameters);
        }

        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(spName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                connection.Close();
                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }
                SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(discoveredParameters, 0);
                foreach (SqlParameter discoveredParameter in discoveredParameters)
                {
                    discoveredParameter.Value = DBNull.Value;
                }
                return discoveredParameters;
            }
        }
    }
}
