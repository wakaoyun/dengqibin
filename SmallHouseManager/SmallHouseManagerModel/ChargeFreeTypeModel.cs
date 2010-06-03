using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class ChargeFreeTypeModel
    {
        /// <summary>
        /// 费用类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string Format { get; set; }
    }
}
