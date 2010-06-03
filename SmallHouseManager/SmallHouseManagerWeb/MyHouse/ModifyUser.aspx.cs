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
    public partial class ModifyUser : System.Web.UI.Page
    {
        UsersBLL bll = new UsersBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            UserName.Text = user.UID;
        }

        protected void CheckUser_Click(object sender, EventArgs e)
        {          
            bool flag = bll.CheckUserByUID(NewUserName.Text);           
            if(flag)
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名已存在！');</script>");
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('恭喜用户名可用');</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyInfo.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            bool flag = bll.CheckUserByUID(NewUserName.Text);
            if (flag)
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名已存在！');</script>");
            else
            {
                int result = bll.UpdateUID(user.ID, NewUserName.Text.Trim());
                if (result == 0)
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败！');</script>");
                else
                {
                    user.UID = NewUserName.Text.Trim();
                    Session["User"] = user;
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功！');location.href='MyInfo.aspx';</script>");
                }
            }
        }
    }
}
