using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using SmallHouseManagerDAL;

namespace SmallHouseManagerBLL
{
    public class Ultility
    {
        UltilityDAL ultity = new UltilityDAL();
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <param name="queryitems">查询每件集合</param>
        /// <param name="yes">是否精确查询</param>
        /// <returns>查询条件</returns>
        public static string GetConditionClause(Hashtable queryitems, bool yes)
        {
            bool flag = true;
            string where = "";
            foreach (DictionaryEntry item in queryitems)
            {
                if (flag)
                {
                    where = " where ";
                }
                else
                {
                    where += " and ";
                }
                if (item.Value.GetType().ToString() == "System.String" || item.Value.GetType().ToString() == "System.DataTime")
                {
                    if (yes)
                    {
                        where += item.Key.ToString() + "='" + item.Value.ToString() + "'";
                    }
                    else
                    {
                        where += item.Key.ToString() + " like '%" + item.Value.ToString() + "%'";
                    }
                }
                else
                {
                    where += item.Key.ToString() + "=" + item.Value.ToString();
                }
                flag = false;
            }
            if (queryitems.Count == 0)
            {
                where += " where Tag=0";
            }
            else
            {
                where += " and Tag=0";
            }
            return where;
        }
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path">备份文件路径</param>
        /// <returns>true：备份成功 false：备份失败</returns>
        public bool BackupDataBase(string path)
        {                  
            return ultity.BackupDataBase(path);
        }
        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="path">备份文件路径</param>
        /// <returns>true：备份成功 false：备份失败</returns>
        public bool RestorDataBase(string path)
        {
            return ultity.RestoreDataBase(path);
        }
        /// <summary>
        /// 关闭有关进程
        /// </summary>
        /// <param name="dbname">数据库名</param>
        public void Killspid(string dbname)
        {
            ultity.Killspid(dbname);
        }
    }
}
