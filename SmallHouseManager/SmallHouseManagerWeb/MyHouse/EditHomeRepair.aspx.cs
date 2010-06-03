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
    public partial class EditHomeRepair : System.Web.UI.Page
    {
        HomeRepairBLL bll = new HomeRepairBLL();
        HomeRepairModel homerepair = new HomeRepairModel();
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
                    homerepair = bll.ShowHomeRepairByID(id);                   
                    lbID.Text = homerepair.ID;
                    txtTitle.Text = homerepair.RepairText;
                    txtMemo.Text = homerepair.RepairMemo;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyHomeRepair.aspx");
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            homerepair.ID = lbID.Text;
            homerepair.RepairText = txtTitle.Text.Trim();
            homerepair.RepairMemo = txtMemo.Text.Trim();
            homerepair.RepairDate = DateTime.Now;            
            bool flag = bll.UpdateHomeRepair(homerepair);           
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='MyHomeRepair.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }
    }
}
