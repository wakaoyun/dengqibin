//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 11:22:10 AM
//功能说明：
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using Wakaoyun.DQB.Framework.Attributes;

namespace Wakaoyun.DQB.DataAccess.Enum
{
    internal enum DataBaseType
    {        
        /// <summary>
        /// MSSQL数据库
        /// </summary>
        [ChName("MSSQL数据库")]
        Sql,
        /// <summary>
        /// Oracle数据库
        /// </summary>
        [ChName("Oracle数据库")]
        Oracle,
        /// <summary>
        /// Mysql数据库
        /// </summary>
        [ChName("Mysql数据库")]
        Mysql,
        /// <summary>
        /// Access数据库
        /// </summary>
        [ChName("Access数据库")]
        Access,
        /// <summary>
        /// DB2数据库
        /// </summary>
        [ChName("DB2数据库")]
        DB2
    }
}
