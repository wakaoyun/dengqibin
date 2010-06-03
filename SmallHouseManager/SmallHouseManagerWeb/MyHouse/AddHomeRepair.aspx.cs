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
    public partial class AddHomeRepair : System.Web.UI.Page
    {
        HomeRepairBLL bll = new HomeRepairBLL();
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
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "BX100001";
                }
                else
                {
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "BX" + (i + 1).ToString();
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyHomeRepair.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            HomeRepairModel homerepair = new HomeRepairModel();
            homerepair.ID = lbID.Text;
            homerepair.Code = user.Code;
            homerepair.RepairText = txtTitle.Text.Trim();
            homerepair.RepairMemo = txtMemo.Text.Trim();           
            bool flag=bll.InsertHomeRepair(homerepair);            
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='MyHomeRepair.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
