using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class PavilionModel
    {
        /// <summary>
        /// 楼宇号
        /// </summary>
        public string PaID { get; set; }
        /// <summary>
        /// 楼宇名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 楼宇层数
        /// </summary>
        public int Layer { get; set; }
        /// <summary>
        /// 楼宇高度
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 楼宇面积
        /// </summary>
        public double Area { get; set; }
        /// <summary>
        /// 建成日期
        /// </summary>
        public DateTime BuildDate { get; set; }
        /// <summary>
        /// 说明描述
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
