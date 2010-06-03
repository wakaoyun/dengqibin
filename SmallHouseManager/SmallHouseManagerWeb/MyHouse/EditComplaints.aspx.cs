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
    public partial class DesposeComplaints : System.Web.UI.Page
    {
        HomeReportBLL bll = new HomeReportBLL();
        HomeReportModel homereport = new HomeReportModel();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["ID"].ToString();
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    homereport = bll.ShowHomeReportByID(id);
                    lbID.Text = homereport.ID;
                    txtTitle.Text = homereport.ReportText;
                    txtMemo.Text = homereport.ReportMemo;
                }
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            homereport.ID = lbID.Text;
            homereport.ReportText = txtTitle.Text.Trim();
            homereport.ReportMemo = txtMemo.Text.Trim();
            homereport.ReportDate = DateTime.Now;
            bool flag = bll.UpdateHomeReport(homereport);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='MyComplaints.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyComplaints.aspx");
        }
    }
}
