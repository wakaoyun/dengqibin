//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 2:38:54 PM
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
using Wakaoyun.DQB.Framework.Attributes;

namespace Wakaoyun.DQB.DataAccess.Enum
{
    /// <summary>
    /// 链接关闭机制
    /// </summary>
    public enum DbConnectionOwnership
    {
        /// <summary>
        /// 内部关闭
        /// </summary>
        [ChName("内部关闭")]
        Internal,
        /// <summary>
        /// 外部关闭
        /// </summary>
        [ChName("外部关闭")]
        External
    }
}
