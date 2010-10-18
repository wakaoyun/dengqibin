//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 11:26:52 AM
//功能说明：数据库访问基类
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wakaoyun.DQB.Framework;
using Wakaoyun.DQB.Framework.Entity;
using Wakaoyun.DQB.Framework.Enum;
using Wakaoyun.DQB.Framework.Log;

namespace Wakaoyun.DQB.DataAccess
{
    /// <summary>
    /// 数据库访问基类
    /// </summary>
    public abstract class DbHelperBase
    {
        protected static TrackConfigInfo trackInfo = new TrackConfigInfo();
        protected static ExceptionConfigInfo exceptionConfigInfo = new ExceptionConfigInfo();

        protected DbHelperBase()
        {
        }

        /// <summary>
        /// 记录数据库执行异常日志
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="commandText">sql语句</param>
        protected void ExceptionLog(Exception exception, string commandText)
        {
            if (exceptionConfigInfo.IsExceptionLogOpen)
            {
                LogEntry logEntry = new LogEntry();
                logEntry.Message = exception.Message + ";错误语句:" + commandText;
                logEntry.Title = "数据库操作异常";
                logEntry.TimeStamp = DateTime.Now;
                logEntry.LogTypeInfo = LogType.ExceptionLog;
                logEntry.ErrorLevelInfo = ErrorLevel.Error;
                logEntry.StackTrace = exception.StackTrace;
                SysLogger.Write(logEntry, exceptionConfigInfo.LogPositionInfo, exceptionConfigInfo.LogPath);
            }
        }

        /// <summary>
        /// 跟踪数据执行语句
        /// </summary>
        /// <param name="commandText">数据执行语句</param>
        protected void CommandTrackLog(string commandText)
        {
            if (trackInfo.IsTrackOpen)
            {
                LogEntry logEntry = new LogEntry();
                logEntry.Message = commandText;
                logEntry.Title = "数据库语句";
                logEntry.TimeStamp = DateTime.Now;
                logEntry.ErrorLevelInfo = ErrorLevel.Info;
                logEntry.LogTypeInfo = LogType.DBCommandTrackLog;
                LogPosition logPosition = trackInfo.LogPositionInfo;
                SysLogger.Write(logEntry, trackInfo.LogPositionInfo, trackInfo.LogPath);
            }
        }
    }

    #region 配置信息类
    public class TrackConfigInfo
    {
        public TrackConfigInfo()
        {

        }
        /// <summary>
        /// 是否开户数据库语句跟踪
        /// </summary>
        public bool IsTrackOpen { get { return ConvertHelper.ToBool(SysConfig.GetAppSetting("IsTrackLogOpen")); } }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LogPath
        {
            get
            {
                if (LogPositionInfo == LogPosition.DataBase)
                {
                    return ConvertHelper.ToString(SysConfig.GetConnectionString("TrackLogConnectString"));
                }
                return ConvertHelper.ToString(SysConfig.GetAppSetting("TrackLogPath"));
            }
        }

        /// <summary>
        /// 记录日志的方式
        /// </summary>
        public LogPosition LogPositionInfo
        {
            get
            {
                return (LogPosition)System.Enum.Parse(typeof(LogPosition),
                ConvertHelper.ToString(SysConfig.GetAppSetting("TrackLogType")), true);
            }
        }
    }

    public class ExceptionConfigInfo
    {
        public ExceptionConfigInfo()
        {

        }
        /// <summary>
        /// 是否开户数据库语句执行异常日志
        /// </summary>
        public bool IsExceptionLogOpen { get { return ConvertHelper.ToBool(SysConfig.GetAppSetting("IsExceptionLogOpen")); } }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LogPath
        {
            get
            {
                if (LogPositionInfo == LogPosition.DataBase)
                {
                    return ConvertHelper.ToString(SysConfig.GetConnectionString("ExceptionLogConnectString"));
                }
                return ConvertHelper.ToString(SysConfig.GetAppSetting("ExceptionLogPath"));
            }
        }

        /// <summary>
        /// 记录日志的方式
        /// </summary>
        public LogPosition LogPositionInfo
        {
            get
            {
                return (LogPosition)System.Enum.Parse(typeof(LogPosition),
                ConvertHelper.ToString(SysConfig.GetAppSetting("ExceptionLogType")), true);
            }
        }
    } 
    #endregion
}
