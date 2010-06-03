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
    public partial class ProcessReport : System.Web.UI.Page
    {
        HomeReportBLL bll = new HomeReportBLL();
        UserModel user = new UserModel();
        HomeReportModel homereport = new HomeReportModel();
        protected void Page_Load(object sender, EventArgs e)
        {            
            homereport.ID = Request.QueryString["ID"].ToString();
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                homereport = bll.ShowHomeReportByID(homereport.ID);
                lbReportText.Text = homereport.ReportText;
                lbReportDate.Text = homereport.ReportDate.ToString("yyy年MM月dd日");
                lbReportMemo.Text = homereport.ReportMemo;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            homereport.FinshText = txtProcessResult.Text.Trim();
            bool flag = bll.UpdateHomeReportForProcess(homereport);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交成功');location.href='HomeReportInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('提交失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeReportInfo.aspx");
        }
    }
}
