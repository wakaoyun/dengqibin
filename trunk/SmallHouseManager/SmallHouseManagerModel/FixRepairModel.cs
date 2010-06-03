using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class FixRepairModel
    {
        /// <summary>
        /// 维修编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string FixID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string FixName { get; set; }
        /// <summary>
        /// 维修日期
        /// </summary>
        public DateTime RepairDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 主要负责人
        /// </summary>
        public string MainHead { get; set; }
        /// <summary>
        /// 维修费
        /// </summary>
        public double ServiceFee { get; set; }
        /// <summary>
        /// 材料费
        /// </summary>
        public double MaterielFee { get; set; }
        /// <summary>
        /// 维修总费用
        /// </summary>
        public double RepairSum { get; set; }
        /// <summary>
        /// 维修说明
        /// </summary>
        public string RepairMemo { get; set; }
        /// <summary>
        /// 是否付费 0：已付 1：未付
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// 维修单位
        /// </summary>
        public string RepairUnit { get; set; }
    }
}
