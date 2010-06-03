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
    public partial class HomeChargeFree : System.Web.UI.Page
    {
        HomeFreeBLL bll = new HomeFreeBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    List<HomeFreeModel> list = bll.GetHomeFreeByCode(user.Code);
                    HomeFreeGridView.DataSource = list;
                    HomeFreeGridView.DataBind();
                }
            }
        }

        protected void HomeFreeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "0")
                {
                    e.Row.Cells[6].Text = "<font color='Red'>已结算</font>";
                }
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }
    }
}
