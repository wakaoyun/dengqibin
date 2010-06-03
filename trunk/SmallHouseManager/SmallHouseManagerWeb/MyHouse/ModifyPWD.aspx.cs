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
    public partial class ModifyPWD : System.Web.UI.Page
    {
        UsersBLL bll = new UsersBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");  
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(OriginalPassword.Text.Trim()==""||NewPassword.Text.Trim()==""||RepeatPassword.Text.Trim()=="")
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('输入不能为空');</script>");
            else if (NewPassword.Text.Trim() == RepeatPassword.Text.Trim())
            {                
                bool flag = bll.CheckUser(user.UID, OriginalPassword.Text.Trim());               
                if (flag)
                {                   
                    int result = bll.UpdatePassword(user.ID, NewPassword.Text.Trim());                    
                    if (result == 0)
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败！');</script>");       
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功！');location.href='MyInfo.aspx';</script>");
                    }
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('原始密码错误！');</script>");
            }
            else
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('两次输入不一致！');</script>");
        }
    }
}
