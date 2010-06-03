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
    public partial class EditBasePark : System.Web.UI.Page
    {
        BaseParkBLL bll = new BaseParkBLL();
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
                    int parkID = int.Parse(Request.QueryString["ID"]);
                    BaseParkModel basePark = bll.GetBaseParkByID(parkID);
                    txtParkName.Text = basePark.Name;
                    txtParkAmount.Text = basePark.Amount.ToString();
                    txtMemo.Text = basePark.Memo;
                }
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            BaseParkModel basePark = new BaseParkModel();
            basePark.ParkID = int.Parse(Request.QueryString["ID"]);
            basePark.Name = txtParkName.Text.Trim();
            basePark.Amount = Convert.ToInt32(txtParkAmount.Text.Trim());
            basePark.Memo = txtMemo.Text.Trim();
            bool flag = bll.UpdateBasePark(basePark);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='BaseParkInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BaseParkInfo.aspx");
        }
    }
}
