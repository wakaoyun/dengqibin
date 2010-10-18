//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 10:41:44 AM
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
using System.Data;

namespace Wakaoyun.DQB.DataAccess.Interface
{
    public interface IEntityHelper
    {
        T FillEntity<T>(DataRow dr);
        T FillEntity<T>(IDataReader dr);
        List<T> FillEntityList<T>(DataTable dt);
        List<T> FillEntityList<T>(IDataReader dr);
    }
}
