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
    public partial class AddNews : System.Web.UI.Page
    {
        NewsBLL bll = new NewsBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            NewsModel news = new NewsModel();
            news.Title = txtNoticeTitle.Text.Trim();
            news.NoticeContent = ftbContent.Text.Trim();
            news.NoticePerson = user.Name;
            bool flag = bll.InsertNotice(news);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='NewsInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
