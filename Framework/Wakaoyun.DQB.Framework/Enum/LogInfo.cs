//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 1:08:06 PM
//功能说明：日志信息枚举
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wakaoyun.DQB.Framework.Attributes;

namespace Wakaoyun.DQB.Framework.Enum
{
    public enum LogType
    {
        /// <summary>
        /// 程序异常
        /// </summary>
        [ChName("程序异常")]
        ExceptionLog,
        /// <summary>
        /// 数据库操作
        /// </summary>
        [ChName("数据库操作")]
        DataBaseOperateLog,
        /// <summary>
        /// Windows服务
        /// </summary>
        [ChName("Windows服务")]
        WindowsServiceLog,
        /// <summary>
        /// Web服务
        /// </summary>
        [ChName("Web服务")]
        WebServiceLog,
        /// <summary>
        /// 数据库语句跟踪
        /// </summary>
        [ChName("数据库语句跟踪")]
        DBCommandTrackLog,
        /// <summary>
        /// 其它未知分类
        /// </summary>
        [ChName("其它未知分类")]
        OtherLog
    }

    /// <summary>
    /// 异常严重级别
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// 紧急 - 系统无法使用
        /// </summary>
        [ChName("紧急 - 系统无法使用")]
        Emergency,        
        /// <summary>
        /// 致命情况
        /// </summary>
        [ChName("致命情况")]
        Critical,
        /// <summary>
        /// 一般错误情况
        /// </summary>
        [ChName("一般错误情况")]
        Error,
        /// <summary>
        /// 警告情况
        /// </summary>
        [ChName("警告情况")]
        Warn,
        /// <summary>
        /// 一般通知情况
        /// </summary>
        [ChName("一般通知情况")]
        Notice,
        /// <summary>
        /// 普通信息
        /// </summary>
        [ChName("普通信息")]
        Info
    }

    public enum LogPosition
    {
        /// <summary>
        /// 系统事件日志
        /// </summary>
        [ChName("系统事件日志")]
        EventLog,
        /// <summary>
        /// txt文件日志
        /// </summary>
        [ChName("txt文件日志")]
        FileLog,
        /// <summary>
        /// 数据库日志
        /// </summary>
        [ChName("数据库日志")]
        DataBase,
        /// <summary>
        /// Xml文件日志
        /// </summary>
        [ChName("Xml文件日志")]
        XmlLog
    }
}
