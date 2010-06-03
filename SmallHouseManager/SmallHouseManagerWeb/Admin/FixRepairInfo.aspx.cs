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
    public partial class FixRepairInfo : System.Web.UI.Page
    {
        FixRepairBLL bll = new FixRepairBLL();
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
                    FixRepairGridView.DataSource = bll.GetAllFixRepair();
                    FixRepairGridView.DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkFixName.Checked)
            {
                htabel.Add("Name", txtFixName.Text.Trim());
            }
            if (chkMainHead.Checked)
            {
                htabel.Add("MainHead", txtMainHead.Text.Trim());
            }
            if (chkFixID.Checked)
            {
                htabel.Add("FixID", txtFixID.Text.Trim());
            }
            if (chkSign.Checked)
            {
                htabel.Add("Sign", Convert.ToInt32(DDLSign.SelectedValue));
            }
            if (chkUnit.Checked)
            {
                htabel.Add("RepairUnit", txtUnit.Text.Trim());
            }
            if (chkFee.Checked)
            {
                htabel.Add("RepairSum", Convert.ToInt32(txtFee.Text));
            }            
            FixRepairGridView.DataSource = bll.GetFixRepairByCondition(Ultility.GetConditionClause(htabel,chkExact.Checked));
            FixRepairGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            FixRepairGridView.DataSource = bll.GetAllFixRepair();
            FixRepairGridView.DataBind();
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixRepairGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FixRepairGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)FixRepairGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    bll.DeleteFixRepair(int.Parse(FixRepairGridView.DataKeys[i].Value.ToString()));
            }
            FixRepairGridView.DataSource = bll.GetAllFixRepair();
            FixRepairGridView.DataBind();
        }

        protected void FixRepairGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "<font color='red'>是</font>";
                    e.Row.Cells[7].Enabled = false;
                    e.Row.Cells[8].Enabled = false;
                }
                else
                {
                    e.Row.Cells[5].Text = "<font color='blue'>否</font>";
                    e.Row.Cells[7].Enabled = true;
                    e.Row.Cells[7].Enabled = true;
                }
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void FixRepairGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditFixRepair.aspx?ID=" + FixRepairGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void FixRepairGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            FixRepairModel fixRepair = new FixRepairModel();
            fixRepair.ID = Convert.ToInt32(FixRepairGridView.DataKeys[e.NewSelectedIndex].Value);
            fixRepair.Sign = 0;
            bll.UpdateFixRepairForSign(fixRepair);
            FixRepairGridView.DataSource = bll.GetAllFixRepair();
            FixRepairGridView.DataBind();
        }

        protected void FixRepairGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FixRepairGridView.PageIndex = e.NewPageIndex;
        }
    }
}
