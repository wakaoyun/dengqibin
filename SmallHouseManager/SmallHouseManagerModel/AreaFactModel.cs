using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class AreaFactModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 设施名称
        /// </summary>
        public string FactName { get; set; }
        /// <summary>
        /// 主要负责人
        /// </summary>
        public string MainHead { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; }
    }
}
