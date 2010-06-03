using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class AdModel
    {
        /// <summary>
        /// 广告编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 广告名
        /// </summary>
        public string AdName { get; set; }
        /// <summary>
        /// 广告图片路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 广告网址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 增加日期
        /// </summary>
        public DateTime AddDate { get; set; }
    }
}
