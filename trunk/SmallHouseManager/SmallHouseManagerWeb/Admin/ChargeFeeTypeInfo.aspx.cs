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
    public partial class ChargeFeeTypeInfo : System.Web.UI.Page
    {
        ChargeFreeTypeBLL bll = new ChargeFreeTypeBLL();
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
                    ChargeFeeTypeGridView.DataSource = bll.GetChargeFreeType();
                    ChargeFeeTypeGridView.DataBind();
                }
            }
        }

        protected void HomeFreeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void ChargeFreeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ChargeFeeTypeGridView.PageIndex = e.NewPageIndex;
            ChargeFeeTypeGridView.DataSource = bll.GetChargeFreeType();
            ChargeFeeTypeGridView.DataBind();
        }

        protected void ChargeFeeTypeGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditChargeFeeType.aspx?ID=" + ChargeFeeTypeGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }
    }
}
