using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class HomeReportModel
    {
        /// <summary>
        /// 投诉编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 投诉日期
        /// </summary>
        public DateTime ReportDate { get; set; }
        /// <summary>
        /// 投诉内容
        /// </summary>
        public string ReportText { get; set; }
        /// <summary>
        /// 投诉说明
        /// </summary>
        public string ReportMemo { get; set; }
        /// <summary>
        /// 处理状态（0：未处理1：已处理）
        /// </summary>
        public int Sign { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        public string FinshText { get; set; }
        /// <summary>
        /// 单元ID
        /// </summary>
        public string RoomID { get; set; }
    }
}
