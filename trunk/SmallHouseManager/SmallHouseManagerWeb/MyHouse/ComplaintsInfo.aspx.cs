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
    public partial class UnDisposeComplaints : System.Web.UI.Page
    {
        HomeReportBLL bll = new HomeReportBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            HomeReportModel homereport = new HomeReportModel();
            homereport.ID = Request.QueryString["ID"].ToString();
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                homereport = bll.ShowHomeReportByID(homereport.ID);
                lbReportText.Text = homereport.ReportText;
                lbReportDate.Text = homereport.ReportDate.ToString("yyy年MM月dd日");
                lbReportMemo.Text = homereport.ReportMemo;
                lbProcessResult.Text = homereport.FinshText;
            }
        }
    }
}
