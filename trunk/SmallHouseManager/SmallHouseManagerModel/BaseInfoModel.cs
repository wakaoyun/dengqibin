using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class BaseInfoModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 小区名
        /// </summary>
        public string HomeName { get; set; }
        /// <summary>
        /// 负责人名
        /// </summary>
        public string MainHead { get; set; }
        /// <summary>
        /// 建成日期
        /// </summary>
        public DateTime BuildDate { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }
        /// <summary>
        /// 楼宇数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 绿化面积
        /// </summary>
        public double GreenArea { get; set; }
        /// <summary>
        /// 道路面积
        /// </summary>
        public double RoadArea { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 停车场面积
        /// </summary>
        public double ParkingArea { get; set; }
        /// <summary>
        /// 小区介绍
        /// </summary>
        public string Memo { get; set; }
    }
}
