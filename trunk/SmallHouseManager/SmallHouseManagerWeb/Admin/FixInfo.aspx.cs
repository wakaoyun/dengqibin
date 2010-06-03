using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;
using System.Collections;

namespace SmallHouseManagerWeb
{
    public partial class FixInfo : System.Web.UI.Page
    {
        FixBLL bll = new FixBLL();
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
                    FixGridView.DataSource = bll.GetAllFix();
                    FixGridView.DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkFixID.Checked)
            {
                htabel.Add("FixID", txtFixID.Text.Trim());
            }
            if (chkFixName.Checked)
            {
                htabel.Add("Name", txtFixName.Text.Trim());
            }
            if (chkFixFactory.Checked)
            {
                htabel.Add("Factory", txtFixFactory.Text.Trim());
            }            
            string strsql = Ultility.GetConditionClause(htabel, chkExact.Checked);
            if (chkFixFactoryDate.Checked)
                strsql += " and FactoryDate between '" + txtBeginDate.Value.Trim() + "' and '" + txtEndDate.Value.Trim() + "'";
            FixGridView.DataSource = bll.GetFixByCondition(strsql);
            FixGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            FixGridView.DataSource = bll.GetAllFix();
            FixGridView.DataBind();
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    bll.DeleteFix(FixGridView.DataKeys[i].Value.ToString());
            }
            FixGridView.DataSource = bll.GetAllFix();
            FixGridView.DataBind();
        }

        protected void FixGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void FixGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditFix.aspx?ID=" + FixGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void FixGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FixGridView.PageIndex = e.NewPageIndex;
        }
    }
}
