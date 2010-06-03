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
    public partial class AddPark : System.Web.UI.Page
    {
        BaseParkBLL bll = new BaseParkBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            BaseParkModel basePark = new BaseParkModel();
            basePark.Name = txtParkName.Text.Trim();
            basePark.Amount = Convert.ToInt32(txtParkAmount.Text.Trim());
            basePark.Memo = txtMemo.Text.Trim();
            bool flag = bll.InsertBasePark(basePark);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='BaseParkInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BaseParkInfo.aspx");
        }        
    }
}
