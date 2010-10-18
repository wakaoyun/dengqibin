//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 11:34:26 AM
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
using System.Configuration;

namespace Wakaoyun.DQB.Framework
{
    public class SysConfig
    {
        /// <summary>
        /// 得到配置节点的值
        /// </summary>
        /// <param name="name">节点名</param>
        /// <returns></returns>
        public static string GetAppSetting(string name)
        {
            return ConvertHelper.ToString(ConfigurationManager.AppSettings[name]);
        }

        /// <summary>
        /// 得到数据库链接节点的值
        /// </summary>
        /// <param name="name">数据库链接节点名</param>
        /// <returns></returns>
        public static string GetConnectionString(string name)
        {
            return ConvertHelper.ToString(ConfigurationManager.ConnectionStrings[name].ConnectionString);
        }

        /// <summary>
        /// 默认数据库链接
        /// </summary>
        public static string DefaultConnectionString
        {
            get
            {
                return GetConnectionString("Default");
            }
        }
    }
}
