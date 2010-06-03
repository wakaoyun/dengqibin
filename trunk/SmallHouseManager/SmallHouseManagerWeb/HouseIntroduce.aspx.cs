using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb
{
    public partial class HouseIntroduce : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseInfoBLL bll = new BaseInfoBLL();            
            List<BaseInfoModel> list = bll.ShowBaseInfo();            
            if (list.Count != 0)
            {
                BaseName.Text = list[0].HomeName;
                MainHead.Text = list[0].MainHead;
                BuildDate.Text = list[0].BuildDate.ToString("yyyy年MM月dd日");
                BuildArea.Text = list[0].BuildArea.ToString();
                Amount.Text = list[0].Amount.ToString();
                ParkingArea.Text = list[0].ParkingArea.ToString();
                RoadArea.Text = list[0].RoadArea.ToString();
                GreenArea.Text = list[0].GreenArea.ToString();
                Tel.Text = list[0].Tel;
                Address.Text = list[0].Address;
                Introduce.Text = list[0].Memo;
            }
        }
    }
}
