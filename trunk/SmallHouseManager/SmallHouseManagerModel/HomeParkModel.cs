using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class HomeParkModel
    {
        /// <summary>
        /// 用户停车编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 停车场编号
        /// </summary>
        public int ParkID { get; set; }
        /// <summary>
        /// 停车场名
        /// </summary>
        public string ParkName { get; set; }
        /// <summary>
        /// 单元编号
        /// </summary>
        public string RoomID { get; set; }
        /// <summary>
        /// 户主编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarID { get; set; }
        /// <summary>
        /// 车类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime BuyDate { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
    }
}
