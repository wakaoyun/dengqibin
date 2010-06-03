using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.Admin
{
    public partial class BaseInfo : System.Web.UI.Page
    {
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                BaseInfoBLL bll = new BaseInfoBLL();
                List<BaseInfoModel> list = bll.ShowBaseInfo();
                if (list.Count != 0)
                {
                    Session["BaseInfoID"] = list[0].ID;
                    BaseName.Text = list[0].HomeName;
                    Amount.Text = list[0].Amount.ToString() + "栋";
                    BuildArea.Text = list[0].BuildArea.ToString() + "亩";
                    GreenArea.Text = list[0].GreenArea.ToString() + "亩";
                    RoadArea.Text = list[0].RoadArea.ToString() + "亩";
                    ParkArea.Text = list[0].ParkingArea.ToString() + "亩";
                    BuildDate.Text = list[0].BuildDate.ToString("yyy年MM月dd日");
                    MainHead.Text = list[0].MainHead;
                    Telephone.Text = list[0].Tel;
                    Address.Text = list[0].Address;
                    BaseMemo.Text = list[0].Memo;
                }
                else
                {
                    Response.Redirect("AddBaseInfo.aspx");
                }
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModifyBaseInfo.aspx");
        }
    }
}
