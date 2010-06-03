using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallHouseManagerModel
{
    public class RoomModel
    {
        /// <summary>
        /// 房间编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomID { get; set; }
        /// <summary>
        /// 楼宇号
        /// </summary>
        public string PaID { get; set; }        
        /// <summary>
        /// 楼宇名
        /// </summary>
        public string PaName { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public int CellID { get; set; }
        /// <summary>
        /// 单元名
        /// </summary>
        public string CellName { get; set; }
        /// <summary>
        /// 朝向号
        /// </summary>
        public int SunnyID { get; set; }
        /// <summary>
        /// 朝向名
        /// </summary>
        public string SunnyName { get; set; }
        /// <summary>
        /// 装修标准号
        /// </summary>
        public int IndoorID { get; set; }
        /// <summary>
        /// 装修标准名
        /// </summary>
        public string IndoorName { get; set; }
        /// <summary>
        /// 房间功能号
        /// </summary>
        public int RoomUseID { get; set; }
        /// <summary>
        /// 房间功能名
        /// </summary>
        public string RoomUseName { get; set; }
        /// <summary>
        /// 房间规格号
        /// </summary>
        public int RoomFormatID { get; set; }
        /// <summary>
        /// 房间规格名
        /// </summary>
        public string RoomFormatName { get; set; }
        /// <summary>
        /// 房间面积
        /// </summary>
        public double BuildArea { get; set; }
        /// <summary>
        /// 可用面积
        /// </summary>
        public double UseArea { get; set; }
        /// <summary>
        /// 出售状态
        /// </summary>
        public int State { get; set; }
    }
}
