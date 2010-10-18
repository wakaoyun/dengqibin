//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/9/2010 12:05:57 PM
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
using Wakaoyun.DQB.DataAccess.Interface;
using System.Data;
using System.Reflection;

namespace Wakaoyun.DQB.DataAccess.Entity
{
    internal class ReflectorEntityHelper : IEntityHelper
    {
        /// <summary>
        /// 用数据行来填冲一个数据实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dr">数据行</param>
        /// <returns>已填冲值的实体</returns>
        public T FillEntity<T>(DataRow dr)
        {
            Type type = typeof(T);
            T t = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if ((dr[prop.Name] != null) && (dr[prop.Name] != DBNull.Value))
                    {
                        prop.SetValue(t, dr[prop.Name], null);
                    }
                }
                catch
                {
                }
            }
            return t;
        }

        /// <summary>
        /// 用DataReader来填冲一个数据实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="dr">DataReader</param>
        /// <returns>已填冲值的实体</returns>
        public T FillEntity<T>(IDataReader dr)
        {
            Type type = typeof(T);
            T t = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if ((dr[prop.Name] != null) && (dr[prop.Name] != DBNull.Value))
                    {
                        prop.SetValue(t, dr[prop.Name], null);
                    }
                }
                catch
                {
                }
            }
            return t;
        }
       
        public List<T> FillEntityList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            T t;
            foreach (DataRow dr in dt.Rows)
            {
                t = FillEntity<T>(dr);
                list.Add(t);
            }

            return list;
        }

        public List<T> FillEntityList<T>(IDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
