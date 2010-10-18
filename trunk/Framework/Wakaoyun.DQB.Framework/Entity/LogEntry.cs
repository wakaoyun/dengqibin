//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 1:06:02 PM
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
using Wakaoyun.DQB.Framework.Enum;
using Wakaoyun.DQB.Framework.Attributes;

namespace Wakaoyun.DQB.Framework.Entity
{
    public class LogEntry
    {
        public override string ToString()
        {
            string logType = string.Format("{0}日志", ChNameAttribute.GetEnumChName(LogTypeInfo));
            string errorLevel = ChNameAttribute.GetEnumChName(ErrorLevelInfo);
            
            StringBuilder totalMessage = new StringBuilder();
            totalMessage.Append("\r\n----------------header------------------------\r\n");
            totalMessage.Append("标题：").Append(this.Title).Append("\r\n");
            totalMessage.Append("信息：").Append(this.Message).Append("\r\n");
            totalMessage.Append("日志时间：").Append(this.TimeStamp).Append("\r\n");
            totalMessage.Append("日志等级：").Append(errorLevel).Append("\r\n");
            totalMessage.Append("日志分类：").Append(logType).Append("\r\n");
            totalMessage.Append("日志来源：").Append(this.Source).Append("\r\n");
            totalMessage.Append("Session信息：").Append(this.SessionInfo).Append("\r\n");
            totalMessage.Append("远程主机：").Append(this.UserHostAddress).Append("\r\n");
            totalMessage.Append("请求地址：").Append(this.RequestUrl).Append("\r\n");
            totalMessage.Append("上次请求地址：").Append(this.UrlReferrer).Append("\r\n");
            totalMessage.Append("堆栈跟踪：").Append(this.StackTrace).Append("\r\n");
            totalMessage.Append("\r\n----------------footer------------------------\r\n");
            return totalMessage.ToString();
        }
                
        private LogType _logType = LogType.OtherLog;
        /// <summary>
        /// 日志分类
        /// </summary>
        public LogType LogTypeInfo
        {
            get { return this._logType; }
            set { this._logType = value; }
        }

        private ErrorLevel _errorLevel = ErrorLevel.Info;
        /// <summary>
        /// 日志等级
        /// </summary>
        public ErrorLevel ErrorLevelInfo
        {
            get { return this._errorLevel; }
            set { this._errorLevel = value; }
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Session信息
        /// </summary>
        public string SessionInfo { get; set; }

        /// <summary>
        /// 日志来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 上次请求地址
        /// </summary>
        public string UrlReferrer { get; set; }

        /// <summary>
        /// 远程主机
        /// </summary>
        public string UserHostAddress { get; set; }
    }
}
