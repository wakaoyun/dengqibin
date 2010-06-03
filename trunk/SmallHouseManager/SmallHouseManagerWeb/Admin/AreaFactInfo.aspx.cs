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
    public partial class AreaFactInfo : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        AreafactBLL afbll = new AreafactBLL();
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
                    DDLAreaType.DataSource = bll.GetType("AreaFact");
                    DDLAreaType.DataBind();
                    AreaFactGridView.DataSource = afbll.ShowAllAreafact();
                    AreaFactGridView.DataBind();
                }
            }
        }

        protected void AreaFactGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditAreaFact.aspx?ID=" + AreaFactGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void AreaFactGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < AreaFactGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)AreaFactGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < AreaFactGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)AreaFactGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < AreaFactGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)AreaFactGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < AreaFactGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)AreaFactGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    afbll.DeleteAreaFact(int.Parse(AreaFactGridView.DataKeys[i].Value.ToString()));
            }
            AreaFactGridView.DataSource = afbll.ShowAllAreafact();
            AreaFactGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            AreaFactGridView.DataSource = afbll.ShowAllAreafact();
            AreaFactGridView.DataBind();
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkAreaName.Checked)
            {
                htabel.Add("FactName", txtAreaName.Text.Trim());
            }
            if (chkMainHead.Checked)
            {
                htabel.Add("MainHead", txtMainHead.Text.Trim());
            }
            if (chkType.Checked)
            {
                htabel.Add("TypeID", int.Parse(DDLAreaType.SelectedValue));
            }
            if (chkTelephone.Checked)
            {
                htabel.Add("Tel", txtTelehpone.Text.Trim());
            }
            AreaFactGridView.DataSource = afbll.GetAreaFactByCondition(Ultility.GetConditionClause(htabel, chkExact.Checked));
            AreaFactGridView.DataBind();
        }

        protected void AreaFactGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AreaFactGridView.PageIndex = e.NewPageIndex;
        }
    }
}
