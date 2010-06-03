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
    public partial class EditNews : System.Web.UI.Page
    {
        NewsBLL bll = new NewsBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    int id = int.Parse(Request.QueryString["ID"]);
                    NewsModel news = bll.ShowNewsByID(id);
                    txtNoticeTitle.Text = news.Title;
                    ftbContent.Text = news.NoticeContent;
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            NewsModel news = new NewsModel();
            news.Title = txtNoticeTitle.Text.Trim();
            news.NoticeContent = ftbContent.Text.Trim();
            news.NoticePerson = user.Name;
            news.ID = int.Parse(Request.QueryString["ID"].ToString());
            bool flag = bll.UpdateNews(news);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='NewsInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsInfo.aspx");
        }
    }
}
