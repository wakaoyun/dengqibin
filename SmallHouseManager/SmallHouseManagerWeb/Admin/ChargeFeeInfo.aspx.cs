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
    public partial class ChargeFeeInfo : System.Web.UI.Page
    {
        HomeFreeBLL bll = new HomeFreeBLL();
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
                    HomeFreeGridView.DataSource = bll.GetHomeFree();
                    HomeFreeGridView.DataBind();
                }
            }
        }        

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeFreeGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeFreeGridView.Rows[i].FindControl("chkSelect");
                if (HomeFreeGridView.Rows[i].Cells[8].Enabled)
                {
                    chkSelect.Checked = true;
                }
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeFreeGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeFreeGridView.Rows[i].FindControl("chkSelect");
                if (HomeFreeGridView.Rows[i].Cells[8].Enabled)
                {
                    chkSelect.Checked = !chkSelect.Checked;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeFreeGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeFreeGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HomeFreeGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)HomeFreeGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    bll.DeleteHomeFree(Convert.ToInt32(HomeFreeGridView.DataKeys[i].Value.ToString()));
            }
            HomeFreeGridView.DataSource = bll.GetHomeFree();
            HomeFreeGridView.DataBind();
        }

        protected void HomeFreeGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    e.Row.Cells[4].Text = "<font color='Red'>已结算</font>";
                    e.Row.Cells[7].Enabled = false;
                }
                else
                {
                    e.Row.Cells[8].Enabled = false;
                }
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }
                
        protected void HomeFreeGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            HomeFreeGridView.PageIndex = e.NewPageIndex;
            HomeFreeGridView.DataSource = bll.GetHomeFree();
            HomeFreeGridView.DataBind();
        }

        protected void HomeFreeGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            HomeFreeModel homeFree = new HomeFreeModel();
            homeFree.ID = Convert.ToInt32(HomeFreeGridView.DataKeys[e.NewSelectedIndex].Value);
            homeFree.FactPayment = Convert.ToDouble(HomeFreeGridView.Rows[e.NewSelectedIndex].Cells[3].Text);
            homeFree.HandleName = user.Name;
            bool flag = bll.UpdateHomeFreeForBargian(homeFree);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
            HomeFreeGridView.DataSource = bll.GetHomeFree();
            HomeFreeGridView.DataBind();
        }
    }
}
