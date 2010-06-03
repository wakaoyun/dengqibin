using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class TypeModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        public int TypeID { get; set; }
    }
}
