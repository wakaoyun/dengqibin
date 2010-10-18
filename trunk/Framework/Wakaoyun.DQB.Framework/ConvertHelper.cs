//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010,Wakaoyun
//系统名称：
//作    者：dengqibin
//邮箱地址：wakaoyun@gmail.com
//创建日期：9/8/2010 11:38:47 AM
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

namespace Wakaoyun.DQB.Framework
{
    public class ConvertHelper
    {
        /// <summary>
        /// IP地址转long类型
        /// </summary>
        /// <param name="p_ip">IP地址</param>
        /// <returns></returns>
        public static long IPToLong(string p_ip)
        {
            string[] sitem = p_ip.Split(new char[] { '.' });
            if (sitem.Length != 4)
            {
                return -1L;
            }
            byte[] item = new byte[4];
            for (int i = 0; i < sitem.Length; i++)
            {
                item[i] = byte.Parse(sitem[i]);
            }
            long ipNum = item[3];
            ipNum |= item[2] << 8;
            ipNum |= item[1] << 0x10;
            return (ipNum | (item[0] << 0x18));
        }

        /// <summary>
        /// long类型转IP地址
        /// </summary>
        /// <param name="p_ipNum">IP地址long值</param>
        /// <returns></returns>
        public static string LongToIP(long p_ipNum)
        {
            long m_temp = p_ipNum;
            string m_ipStr = "";
            m_ipStr = m_ipStr + (m_temp % 0x100L);
            m_temp = (int)(m_temp / 0x100L);
            for (int i = 2; i <= 4; i++)
            {
                m_ipStr = (m_temp % 0x100L) + "." + m_ipStr;
                m_temp = (int)(m_temp / 0x100L);
            }
            return m_ipStr;
        }
        
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static bool ToBool(object value)
        {
            if (value != null)
            {
                switch (value.GetType().Name)
                {
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "UInt16":
                    case "UInt32":
                    case "UInt64":
                    case "Single":
                    case "Double":
                    case "Decimal":
                        return (((int)value) != 0);

                    case "String":
                        if ((value.ToString().ToLower() != "true") && !(value.ToString().ToLower() == "1"))
                        {
                            return false;
                        }
                        return true;

                    case "Boolean":
                        return (bool)value;
                }
            }
            return false;
        }

        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static bool ToBool(string value)
        {
            bool convertResult = false;
            bool.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的bool类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static bool? ToBoolNullable(object value)
        {
            if (value == null)
            {
                return null;
            }
            switch (value.GetType().Name)
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "Single":
                case "Double":
                case "Decimal":
                    return (((int)value) != 0);

                case "String":
                    if ((value.ToString().ToLower() != "true") && !(value.ToString().ToLower() == "1"))
                    {
                        if ((value.ToString().ToLower() == "false") || (value.ToString().ToLower() == "0"))
                        {
                            return false;
                        }
                        return null;
                    }
                    return true;

                case "Boolean":
                    return new bool?((bool)value);
            }
            return false;
        }

        /// <summary>
        /// 转换为时间类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value)
        {
            if (value == null)
            {
                return DateTime.Parse("1900-01-01 00:00:00");
            }
            try
            {
                return DateTime.Parse(ToString(value));
            }
            catch
            {
                return DateTime.Parse("1900-01-01 00:00:00");
            }
        }

        /// <summary>
        /// 转换为可为null时间类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(object value)
        {
            DateTime result = ToDateTime(value);
            if (result == new DateTime(1900, 1, 1))
            {
                return null;
            }
            return new DateTime?(result);
        }

        /// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static decimal ToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0M;
            }
        }

        /// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static decimal ToDecimal(string value)
        {
            decimal convertResult = 0M;
            decimal.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Decimal类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(object value)
        {
            try
            {
                return new decimal?(Convert.ToDecimal(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static double ToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static double ToDouble(string value)
        {
            double convertResult = 0.0;
            double.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Double类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static double? ToDoubleNullable(object value)
        {
            try
            {
                return new double?(Convert.ToDouble(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为Float类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static float ToFloat(object value)
        {
            try
            {
                return Convert.ToSingle(value);
            }
            catch
            {
                return 0f;
            }
        }

        /// <summary>
        /// 转换为Float类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static float ToFloat(string value)
        {
            float convertResult = 0f;
            float.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Float类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static float? ToFloatNullable(object value)
        {
            try
            {
                return new float?(Convert.ToSingle(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为Guid类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static Guid ToGuid(object value)
        {
            if (value == null)
            {
                return Guid.Empty;
            }
            try
            {
                return new Guid(ToString(value));
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 转换为可为null的Guid类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static Guid? ToGuidNullable(object value)
        {
            Guid result = ToGuid(value);
            if (result == Guid.Empty)
            {
                return null;
            }
            return new Guid?(result);
        }

        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static int ToInt(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为Int类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static int ToInt(string value)
        {
            int convertResult = 0;
            int.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Int类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static int? ToIntNullable(object value)
        {
            try
            {
                return new int?(Convert.ToInt32(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为Long类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static long ToLong(object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0L;
            }
        }

        /// <summary>
        /// 转换为Long类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static long ToLong(string value)
        {
            long convertResult = 0L;
            long.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Long类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static long? ToLongNullable(object value)
        {
            try
            {
                return new long?(Convert.ToInt64(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为Short类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static short ToShort(object value)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换为Short类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static short ToShort(string value)
        {
            short convertResult = 0;
            short.TryParse(value, out convertResult);
            return convertResult;
        }

        /// <summary>
        /// 转换为可为null的Short类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static short? ToShortNullable(object value)
        {
            try
            {
                return new short?(Convert.ToInt16(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为String类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            try
            {
                return value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 转换为String类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToString(object value, string replace)
        {
            string result = ToString(value);
            if (string.IsNullOrEmpty(result))
            {
                return replace;
            }
            return result;
        }

        /// <summary>
        /// 转换为可为null的String类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns></returns>
        public static string ToStringNullable(object value)
        {
            string result = ToString(value);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            return result;
        }
    }
}
