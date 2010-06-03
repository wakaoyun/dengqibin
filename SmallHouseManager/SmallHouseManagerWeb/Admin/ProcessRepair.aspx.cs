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
    public partial class ProcessRepair : System.Web.UI.Page
    {
        HomeRepairBLL bll = new HomeRepairBLL();
        UserModel user = new UserModel();
        HomeRepairModel homerepair = new HomeRepairModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            homerepair.ID = Request.QueryString["ID"].ToString();
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                homerepair = bll.ShowHomeRepairByID(homerepair.ID);
                lbRepairText.Text = homerepair.RepairText;
                lbRepairDate.Text = homerepair.RepairDate.ToString("yyy年MM月dd日");
                lbRepairMemo.Text = homerepair.RepairMemo;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            homerepair.Varperson = user.Name;
            homerepair.VarText = txtProcessResult.Text.Trim();
            homerepair.RepairUnit = txtRepairUnit.Text.Trim();
            bool flag = bll.UpdateHomeRepairForProcess(homerepair);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交成功');location.href='HomeRepairInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeRepairInfo.aspx");
        }
    }
}
