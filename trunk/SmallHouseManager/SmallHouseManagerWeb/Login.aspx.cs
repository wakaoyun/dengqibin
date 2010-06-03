using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.MyHouse
{
    public partial class Login : System.Web.UI.Page
    {
        UsersBLL bll = new UsersBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Value) || string.IsNullOrEmpty(Password.Value) || string.IsNullOrEmpty(Code.Value))
                ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('输入不能为空')</script>");
            else
            {
                if (Code.Value.ToLower() == Session["checkCode"].ToString())
                {
                    List<UserModel>  list = bll.UserLogin(UserName.Value, Password.Value);                    
                    if (list.Count == 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名或密码错误')</script>");
                    }
                    else
                    {
                        Session["User"] = list[0];
                        if (list[0].UserType == 1)
                            Response.Redirect("Admin/Default.aspx");
                        else
                            Response.Redirect("MyHouse/Default.aspx");
                    }
                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('验证码错误')</script>");
            }
        }

        protected void Cancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
