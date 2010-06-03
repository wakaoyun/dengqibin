using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.UserControl
{
    public partial class Login : System.Web.UI.UserControl
    {
        UsersBLL bll = new UsersBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Login_Pannel.Visible = false;
                UserModel user = (UserModel)Session["User"];
                MyUserName.Text = user.Name;
                if (user.UserType == 0)
                    Url.PostBackUrl = "../MyHouse/Default.aspx";
                else
                    Url.PostBackUrl = "../Admin/Default.aspx";
                Loginned_Pannel.Visible = true;
            }
            else
            {
                Login_Pannel.Visible = true;
                Loginned_Pannel.Visible = false;
            }
        }

        protected void Submit_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(Code.Text))
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('输入不能为空')</script>");
            else
            {
                if (Code.Text.ToLower() == Session["checkCode"].ToString())
                {                  
                    List<UserModel> list = bll.UserLogin(UserName.Text, Password.Text);                   
                    if (list.Count == 0)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名或密码错误')</script>");
                    }
                    else
                    {
                        Session["User"] = list[0];
                        Login_Pannel.Visible = false;
                        MyUserName.Text = list[0].Name;
                        if (list[0].UserType == 0)
                            Url.PostBackUrl = "../MyHouse/Default.aspx";
                        else
                            Url.PostBackUrl = "../Admin/Default.aspx";
                        Loginned_Pannel.Visible = true;
                    }
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('验证码错误')</script>");
            }
        }
    }
}