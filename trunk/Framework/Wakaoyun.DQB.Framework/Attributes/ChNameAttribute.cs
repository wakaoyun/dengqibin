//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 1:29:16 PM
//功能说明：对枚举类型进行注释，方便以后获取枚举类型的中文名
//-------------------------------------------------------------------
//修改  人：
//修改时间：
//修改内容：
//-------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Wakaoyun.DQB.Framework.Attributes
{
    /// <summary>
    /// 枚举值注释属性
    /// </summary>
    public class ChNameAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ChName"></param>
        public ChNameAttribute(string chName)
        {
            ChName = chName;
        }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ChName { get; set; }

        private static Hashtable _cache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 得到枚举值的中文名
        /// </summary>
        /// <param name="enumImpl"></param>
        /// <returns></returns>
        public static string GetEnumChName(System.Enum enumImpl)
        {
            string enumChName = "";
            if (_cache != null)
            {
                enumChName = (string)_cache[enumImpl];
            }
            if (!string.IsNullOrEmpty(enumChName))
                return (string)_cache[enumImpl];
            else
            {
                string names = string.Empty;
                Type type = enumImpl.GetType();
                string[] fieldNames = enumImpl.ToString().Split(',');
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    FieldInfo fd = type.GetField(fieldNames[i].Trim());

                    if (fd == null) return "ERROR！";

                    object[] attrs = fd.GetCustomAttributes(typeof(ChNameAttribute), false);
                    string name = string.Empty;
                    foreach (ChNameAttribute attr in attrs)
                    {
                        name = attr.ChName;
                    }
                    names += name;
                    if (i < fieldNames.Length - 1)
                    {
                        names += ",";
                    }
                }
                _cache.Add(enumImpl, names);
                return names;
            }
        }

        /// <summary>
        /// 获取枚举类型的所有中文名
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static List<EnumInfo> GetEnumAllChName(Type enumValue)
        {
            FieldInfo fd = null;
            object[] attrs = null;
            FieldInfo[] fields = null;
            string name = string.Empty;

            List<EnumInfo> list = new List<EnumInfo>();
            EnumInfo enumInfo = null;

            if (enumValue.IsEnum)
            {
                fields = enumValue.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    fd = fields[i];
                    attrs = fd.GetCustomAttributes(typeof(ChNameAttribute), false);
                    if (attrs != null)
                    {
                        foreach (ChNameAttribute attr in attrs)
                        {
                            enumInfo = new EnumInfo();
                            enumInfo.EnumChName = attr.ChName;
                            enumInfo.EnumValue = (int)fd.GetValue(Activator.CreateInstance(fd.FieldType));
                            enumInfo.EnumEnName = fd.Name;
                            list.Add(enumInfo);
                        }
                    }
                }
            }
            return list;
        }
    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    public class EnumInfo
    {
        /// <summary>
        /// 枚举值
        /// </summary>
        public int EnumValue { get; set; }
        /// <summary>
        /// 枚举中文名
        /// </summary>
        public string EnumChName { get; set; }
        /// <summary>
        /// 枚举英文名
        /// </summary>
        public string EnumEnName { get; set; }
    }
}
