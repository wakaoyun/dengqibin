using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class HomeRepairModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 户主编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 报修日期
        /// </summary>
        public DateTime RepairDate { get; set; }
        /// <summary>
        /// 报修内容
        /// </summary>
        public string RepairText { get; set; }
        /// <summary>
        /// 报修说明
        /// </summary>
        public string RepairMemo { get; set; }
        /// <summary>
        /// 处理状态（0：未处理1：已处理）
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string Varperson { get; set; }
        /// <summary>
        /// 处理说明
        /// </summary>
        public string VarText { get; set; }
        /// <summary>
        /// 维修单位
        /// </summary>
        public string RepairUnit { get; set; }
        /// <summary>
        /// 业主验收意见
        /// </summary>
        public string OwnerText { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public string RoomID { get; set; }
    }
}
