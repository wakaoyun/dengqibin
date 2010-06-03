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
    public partial class AddBaseInfo : System.Web.UI.Page
    {
        BaseInfoBLL bll = new BaseInfoBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            if (Session["BaseInfoID"] != null)
                Response.Redirect("Default.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            BaseInfoModel baseinfo = new BaseInfoModel();
            baseinfo.HomeName = txtBaseName.Text.Trim();
            baseinfo.MainHead = txtMainHead.Text.Trim();
            baseinfo.BuildDate = DateTime.Parse(txtBuildDate.Value.Trim().ToString());
            baseinfo.BuildArea = Double.Parse(txtBuildArea.Text.Trim());
            baseinfo.RoadArea = Double.Parse(txtRoadArea.Text.Trim());
            baseinfo.GreenArea = Double.Parse(txtGreenArea.Text.Trim());
            baseinfo.ParkingArea = Double.Parse(txtParkArea.Text.Trim());
            baseinfo.Amount = int.Parse(txtAmount.Text.Trim());
            baseinfo.Tel = txtTelphone.Text.Trim();
            baseinfo.Address = txtAddress.Text.Trim();
            baseinfo.Memo = txtMemo.Text.Trim();
            bool flag = bll.InsertBaseInfo(baseinfo);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='BaseInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
