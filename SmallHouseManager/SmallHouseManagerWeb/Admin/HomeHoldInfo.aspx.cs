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
    public partial class HomeHoldInfo : System.Web.UI.Page
    {
        HomeHoldBLL bll = new HomeHoldBLL();
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
                    HomeHoldGridView.DataSource = bll.GetAllHomeHold();
                    HomeHoldGridView.DataBind();
                }
            }
        }        

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeHoldGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeHoldGridView.Rows[i].FindControl("chkSelect");
                if (HomeHoldGridView.Rows[i].Cells[8].Enabled)
                {
                    chkSelect.Checked = true;
                }
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeHoldGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeHoldGridView.Rows[i].FindControl("chkSelect");
                if (HomeHoldGridView.Rows[i].Cells[8].Enabled)
                {
                    chkSelect.Checked = !chkSelect.Checked;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeHoldGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeHoldGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeHoldGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeHoldGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    int id=Convert.ToInt32(HomeHoldGridView.DataKeys[i].Value.ToString());
                    string code=HomeHoldGridView.Rows[i].Cells[0].Text;
                    bll.DeleteHomeHold(id,code,0);
                }
            }
            HomeHoldGridView.DataSource = bll.GetAllHomeHold();
            HomeHoldGridView.DataBind();
        }

        protected void HomeHoldGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void HomeHoldGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HomeHoldGridView.PageIndex = e.NewPageIndex;
            HomeHoldGridView.DataSource = bll.GetAllHomeHold();
            HomeHoldGridView.DataBind();
        }
    }
}
