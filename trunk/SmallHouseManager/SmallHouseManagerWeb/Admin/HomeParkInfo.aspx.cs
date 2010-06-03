using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;
using System.Collections;

namespace SmallHouseManagerWeb.Admin
{
    public partial class HomeParkInfo : System.Web.UI.Page
    {
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
                    HomeParkGridView.DataSource = bll.GetAllHomePark();
                    HomeParkGridView.DataBind();
                }
            }
        }
        
        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeParkGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeParkGridView.Rows[i].FindControl("chkSelect");
                if (HomeParkGridView.Rows[i].Cells[7].Enabled)
                {
                    chkSelect.Checked = true;
                }
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeParkGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeParkGridView.Rows[i].FindControl("chkSelect");
                if (HomeParkGridView.Rows[i].Cells[7].Enabled)
                {
                    chkSelect.Checked = !chkSelect.Checked;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeParkGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeParkGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeParkGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeParkGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    bll.DeleteHomePark(Convert.ToInt32(HomeParkGridView.DataKeys[i].Value.ToString()));
            }
            HomeParkGridView.DataSource = bll.GetAllHomePark();
            HomeParkGridView.DataBind();
        }

        protected void HomeParkGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void HomeParkGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditHomePark.aspx?ID=" + HomeParkGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void HomeParkGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HomeParkGridView.PageIndex = e.NewPageIndex;
            HomeParkGridView.DataSource = bll.GetAllHomePark();
            HomeParkGridView.DataBind();
        }
    }
}
