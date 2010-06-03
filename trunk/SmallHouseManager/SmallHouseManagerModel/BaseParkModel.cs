using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class BaseParkModel
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        public int ParkID { get; set; }
        /// <summary>
        /// 停车场名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 停车位数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }
    }
}
