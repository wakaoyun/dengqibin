using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class EmployeeModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string EmlpoyeeName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 工作安排
        /// </summary>
        public string Arrage { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 图像路径
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
