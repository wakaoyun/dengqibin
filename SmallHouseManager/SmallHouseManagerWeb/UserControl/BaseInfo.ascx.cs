using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.UserControl
{
    public partial class BaseInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseInfoBLL bll = new BaseInfoBLL();
            List<BaseInfoModel> list = bll.ShowBaseInfo();           
            if (list.Count != 0)
            {
                BaseName.Text = list[0].HomeName;
                Amount.Text = list[0].Amount.ToString();
                BuildArea.Text = list[0].BuildArea.ToString();
                GreenArea.Text = list[0].GreenArea.ToString();
                RoadArea.Text = list[0].RoadArea.ToString();
                MainHead.Text = list[0].MainHead;
                Tel.Text = list[0].Tel;
                Address.Text = list[0].Address;
            }
        }
    }
}