using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class UserModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 子编号
        /// </summary>
        public int SubID { get; set; }
        /// <summary>
        /// 登陆名
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string Code { get; set; }
    }
}
