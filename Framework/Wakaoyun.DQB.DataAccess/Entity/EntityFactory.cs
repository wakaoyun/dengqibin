//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/9/2010 11:55:36 AM
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

namespace Wakaoyun.DQB.DataAccess.Entity
{
    public sealed class EntityFactory
    {
        public static IEntityHelper CreateInstance()
        {
            return new ReflectorEntityHelper();
        }
    }
}
