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
    public partial class HomeReportInfo : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
        HomeReportBLL reportbll = new HomeReportBLL();
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
                    HomeReportGridView.DataSource = reportbll.GetHomeReport();
                    HomeReportGridView.DataBind();
                }
            }
        }               

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeReportGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeReportGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeReportGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeReportGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeReportGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeReportGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeReportGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeReportGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    reportbll.DeleteHomeReport(HomeReportGridView.DataKeys[i].Value.ToString());
            }
            HomeReportGridView.DataSource = reportbll.GetHomeReport();
            HomeReportGridView.DataBind();
        }

        protected void HomeReportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void HomeReportGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("ProcessReport.aspx?ID=" + HomeReportGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void HomeReportGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HomeReportGridView.PageIndex = e.NewPageIndex;
        }
    }
}
