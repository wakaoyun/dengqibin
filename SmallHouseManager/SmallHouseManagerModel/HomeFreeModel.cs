using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class HomeFreeModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 费用类型ID
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 费用类型名
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 单元名
        /// </summary>        
        public string RoomID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public double Payment { get; set; }
        /// <summary>
        /// 已付金额
        /// </summary>
        public double FactPayment { get; set; }
        /// <summary>
        /// 未付金额
        /// </summary>
        public double NotPayment { get; set; }
        /// <summary>
        /// 计费日期
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 收费人
        /// </summary>
        public string HandleName { get; set; }
        /// <summary>
        /// 登记众人
        /// </summary>
        public string AddName { get; set; }
    }
}
