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
    public partial class RoomInfo : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
        RoomBLL roombll = new RoomBLL();
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
                    DDLPa.DataSource = pabll.GetAllPavilion();                    
                    DDLCell.DataSource = bll.GetType("Cell");
                    DDLSunny.DataSource = bll.GetType("Sunny");
                    DDLUse.DataSource = bll.GetType("RoomUse");
                    DDLFormat.DataSource = bll.GetType("RoomFormat");
                    DDLIndoor.DataSource = bll.GetType("Indoor");
                    RoomGridView.DataSource = roombll.GetAllRoom();
                    DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkPaName.Checked)
            {
                htabel.Add("PaID", Convert.ToInt32(DDLPa.SelectedValue));
            }
            if (chkCell.Checked)
            {
                htabel.Add("CellID", Convert.ToInt32(DDLCell.SelectedValue));
            }
            if (chkSunny.Checked)
            {
                htabel.Add("SunnyID", Convert.ToInt32(DDLSunny.SelectedValue));
            }
            if (chkLayer.Checked)
            {
                htabel.Add("Substring(Code,6,2)", txtLayer.Text.Trim());
            }
            if (chkUse.Checked)
            {
                htabel.Add("RoomUseID", Convert.ToInt32(DDLUse.SelectedValue));
            }
            if (chkFormat.Checked)
            {
                htabel.Add("RoomFormatID", Convert.ToInt32(DDLFormat.SelectedValue));
            }
            if (chkIndoor.Checked)
            {
                htabel.Add("IndoorID", Convert.ToInt32(DDLIndoor.SelectedValue));
            }
            if (rbSale.Checked)
            {
                htabel.Add("State", 1);
            }
            else
            {
                htabel.Add("State", 0);
            }
            RoomGridView.DataSource = roombll.GetRoomByCondition(Ultility.GetConditionClause(htabel, false));
            RoomGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            RoomGridView.DataSource = roombll.GetAllRoom();
            RoomGridView.DataBind();
        }

        protected void RoomView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text == "1")
                {
                    e.Row.Cells[8].Text = "<font color='red'>已有住户</font>";
                    e.Row.Cells[9].Enabled = false;
                    e.Row.Cells[10].Enabled = false;
                }
                else
                {
                    e.Row.Cells[8].Text = "<font color='blue'>空</font>";
                    e.Row.Cells[9].Enabled = true;
                    e.Row.Cells[10].Enabled = true;
                }
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void RoomView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditRoom.aspx?ID=" + RoomGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }        

        protected void RoomGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RoomGridView.PageIndex = e.NewPageIndex;
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RoomGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)RoomGridView.Rows[i].FindControl("chkSelect");
                if (RoomGridView.Rows[i].Cells[10].Enabled)
                {
                    chkSelect.Checked = true;
                }
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RoomGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)RoomGridView.Rows[i].FindControl("chkSelect");
                if (RoomGridView.Rows[i].Cells[10].Enabled)
                {
                    chkSelect.Checked = !chkSelect.Checked;
                }                
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RoomGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)RoomGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RoomGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)RoomGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    roombll.DeleteRoom(RoomGridView.DataKeys[i].Value.ToString());
            }
            RoomGridView.DataSource = roombll.GetAllRoom();
            RoomGridView.DataBind();
        }
    }
}
