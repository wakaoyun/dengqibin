using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.MyHouse
{
    public partial class HomeReapirInfo : System.Web.UI.Page
    {
        HomeRepairBLL bll = new HomeRepairBLL();
        HomeRepairModel homerepair = new HomeRepairModel();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            homerepair.ID = Request.QueryString["ID"].ToString();
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {                  
                    homerepair = bll.ShowHomeRepairByID(homerepair.ID);                    
                    lbRepairText.Text = homerepair.RepairText;
                    lbRepairDate.Text = homerepair.RepairDate.ToString("yyy年MM月dd日");
                    lbRepairMemo.Text = homerepair.RepairMemo;
                    lbProcessName.Text = homerepair.Varperson;
                    lbRepairUnit.Text = homerepair.RepairUnit;
                    lbProcessResult.Text = homerepair.VarText;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyHomeRepair.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            homerepair.OwnerText = txtCheckResult.Text.Trim();           
            bool flag = bll.UpdateHomeRepairCheck(homerepair);            
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交成功');location.href='MyHomeRepair.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交失败');</script>");
            }
        }
    }
}
