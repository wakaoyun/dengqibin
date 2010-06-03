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
    public partial class EditHomePark : System.Web.UI.Page
    {
        BaseParkBLL parkbll = new BaseParkBLL();
        HomeParkBLL bll = new HomeParkBLL();
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
                    DDLType.DataSource = parkbll.GetBasePark();
                    DDLType.DataBind();
                    int id = int.Parse(Request.QueryString["ID"]);
                    HomeParkModel homePark = bll.GetHomeParkByID(id);
                    lbCell.Text = homePark.RoomID;
                    DDLType.SelectedValue = homePark.ParkID.ToString();
                    txtCarID.Text = homePark.CarID;
                    txtType.Text = homePark.Type;
                    txtColor.Text = homePark.Color;
                    txtBuyDate.Value = homePark.BuyDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            HomeParkModel homePark = new HomeParkModel();
            homePark.ID = int.Parse(Request.QueryString["ID"]);
            homePark.ParkID = Convert.ToInt32(DDLType.SelectedValue);
            homePark.CarID = txtCarID.Text.Trim();
            homePark.Type = txtType.Text.Trim();
            homePark.Color = txtColor.Text.Trim();
            homePark.BuyDate = Convert.ToDateTime(txtBuyDate.Value);
            bool flag = bll.UpdateHomePark(homePark);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='HomeParkInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeParkInfo.aspx");
        }
    }
}
