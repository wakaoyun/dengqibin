using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;
using System.IO;

namespace SmallHouseManagerWeb.Admin
{
    public partial class Restor : System.Web.UI.Page
    {
        UserModel user = new UserModel();
        Ultility ultity = new Ultility();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    string mydir = Server.MapPath("~/Backup") + "\\";
                    string extname = "*.bak"; 
                    foreach (string myfile in Directory.GetFiles(mydir, extname, SearchOption.AllDirectories))
                    {
                        DDLFileName.Items.Add(Path.GetFileNameWithoutExtension(myfile));
                    }
                }
            }
        }

        protected void RestorData_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Backup") + "\\" + DDLFileName.SelectedItem.Text.Trim() + ".bak";
            ultity.Killspid("MyHouse");
            if (ultity.RestorDataBase(filePath))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('恢复成功');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('恢复失败');</script>");
            }
        }
    }
}
