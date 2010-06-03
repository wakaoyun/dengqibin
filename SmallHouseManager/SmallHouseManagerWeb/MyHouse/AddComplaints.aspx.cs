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
    public partial class AddComplaints : System.Web.UI.Page
    {
        HomeReportBLL bll = new HomeReportBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                int i = bll.GetMaxID();
                if (i == -1)
                {
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "TS100001";
                }
                else
                {
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "TS" + (i + 1).ToString();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            HomeReportModel homereport = new HomeReportModel();
            homereport.ID = lbID.Text;
            homereport.Code = user.Code;
            homereport.ReportText = txtTitle.Text.Trim();
            homereport.ReportMemo = txtMemo.Text.Trim();
            bool flag = bll.InsertHomeReport(homereport);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='MyComplaints.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyComplaints.aspx");
        }
    }
}
