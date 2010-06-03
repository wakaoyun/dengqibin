using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class NewsModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 新闻名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻日期
        /// </summary>
        public DateTime NoticeDate { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NoticeContent { get; set; }
        /// <summary>
        /// 发布者
        /// </summary>
        public string NoticePerson { get; set; }
    }
}
