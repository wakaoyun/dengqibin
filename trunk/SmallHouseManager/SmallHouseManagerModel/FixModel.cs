using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class FixModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string FixID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Factory { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime FactoryDate { get; set; }
    }
}
