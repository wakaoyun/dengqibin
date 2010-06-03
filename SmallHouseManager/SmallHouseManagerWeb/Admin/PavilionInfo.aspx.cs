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
    public partial class PavilionInfo : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
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
                    DDLPaType.DataSource = bll.GetType("Pavilion");
                    DDLPaType.DataBind();
                    PavilionGridView.DataSource = pabll.GetAllPavilion();
                    PavilionGridView.DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkPaName.Checked)
            {
                htabel.Add("Name", txtPaName.Text.Trim());
            }
            if (chkPaLayer.Checked)
            {
                htabel.Add("Layer", txtPavLayer.Text.Trim());
            }
            if (chkType.Checked)
            {
                htabel.Add("TypeID", int.Parse(DDLPaType.SelectedValue));
            }
            if (chkPaHeight.Checked)
            {
                htabel.Add("Height", txtPaHeight.Text.Trim());
            }
            if (chkPaArea.Checked)
            {
                htabel.Add("Area", txtPaArea.Text.Trim());
            }
            string strsql = Ultility.GetConditionClause(htabel, chkExact.Checked);
            if (chkPaBuildDate.Checked)
                strsql += " and BuildDate between '" + txtBeginDate.Value.Trim() + "' and '" + txtEndDate.Value.Trim() + "'";
            PavilionGridView.DataSource = pabll.GetPavilionByCondition(strsql);
            PavilionGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            PavilionGridView.DataSource = pabll.GetAllPavilion();
            PavilionGridView.DataBind();
        }
        
        protected void PavilionView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void PavilionView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditPavilion.aspx?ID=" + PavilionGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void PavilionGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (pabll.CheckRoom(PavilionGridView.DataKeys[e.RowIndex].Value.ToString()))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('楼内有住户 ，请先删除住户！');location.href='RoomInfo.aspx';</script>");
            }
            else
            {
                pabll.DeletePavilion(PavilionGridView.DataKeys[e.RowIndex].Value.ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('删除成功');</script>");
                PavilionGridView.DataSource = pabll.GetAllPavilion();
                PavilionGridView.DataBind();
            }
        }

        protected void PavilionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PavilionGridView.PageIndex = e.NewPageIndex;
        }
    }
}
