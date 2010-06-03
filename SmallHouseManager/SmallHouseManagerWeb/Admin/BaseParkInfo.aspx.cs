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
    public partial class BaseParkInfo : System.Web.UI.Page
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
                    ParkGridView.DataSource = bll.GetBasePark();
                    ParkGridView.DataBind();
                }
            }
        }

        protected void NewsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void NewsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditBasePark.aspx?ID=" + ParkGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void NewsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ParkGridView.PageIndex = e.NewPageIndex;
            ParkGridView.DataSource = bll.GetBasePark();
            ParkGridView.DataBind();
        }

        protected void ParkGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (bll.CheckHomePark(Convert.ToInt32( ParkGridView.DataKeys[e.RowIndex].Value)))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('场内有用户停车位，请先删除用户停车位！');location.href='HomeParkInfo.aspx';</script>");
            }
            else
            {
                bll.DeleteBasePark(Convert.ToInt32(ParkGridView.DataKeys[e.RowIndex].Value));
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('删除成功');</script>");
                ParkGridView.DataSource = bll.GetBasePark();
                ParkGridView.DataBind();
            }
        }
    }
}
