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
    public partial class HomeRepairInfo : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
        HomeRepairBLL repairbll = new HomeRepairBLL();
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
                    HomeRepairGridView.DataSource = repairbll.GetHomeRepair();
                    HomeRepairGridView.DataBind();
                }
            }
        }        

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeRepairGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    repairbll.DeleteHomeRepair(HomeRepairGridView.DataKeys[i].Value.ToString());
            }
            HomeRepairGridView.DataSource = repairbll.GetHomeRepair();
            HomeRepairGridView.DataBind();
        }

        protected void HomeRepairGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    e.Row.Cells[4].Text = "<font color='blue'>未处理</font>";
                }
                else
                {
                    e.Row.Cells[4].Text = "<font color='red'>已处理</font>";
                    e.Row.Cells[5].Enabled = false;
                }
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void HomeRepairGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("ProcessRepair.aspx?ID=" + HomeRepairGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void HomeRepairGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HomeRepairGridView.PageIndex = e.NewPageIndex;
        }        
    }
}
