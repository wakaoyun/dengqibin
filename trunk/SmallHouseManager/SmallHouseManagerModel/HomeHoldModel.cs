using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class HomeHoldModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 住房号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 房产证号
        /// </summary>
        public string OwnerID { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomID { get; set; }
    }
}
