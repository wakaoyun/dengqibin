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
    public partial class NewsInfo : System.Web.UI.Page
    {
        NewsBLL bll = new NewsBLL();
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
                    NewsGridView.DataSource = bll.ShowAllNews();
                    NewsGridView.DataBind();
                }
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Hashtable htabel = new Hashtable();
            if (chkNoticeTitle.Checked)
                htabel.Add("Title", txtNoticeTitle.Text.Trim());
            if(chkPerson.Checked)
                htabel.Add("NoticePerson",txtPerson.Text.Trim());
            string strsql = Ultility.GetConditionClause(htabel, chkExact.Checked);
            if (chkNoticeDate.Checked)
                strsql += " and NoticeDate between '" + txtBeginDate.Value.Trim() + "' and '" + txtEndDate.Value.Trim() + "'";
            NewsGridView.DataSource = bll.GetNewsByCondition(strsql);
            NewsGridView.DataBind();
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            NewsGridView.DataSource = bll.ShowAllNews();
            NewsGridView.DataBind();
        }

        protected void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NewsGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)NewsGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ConvertSelected_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NewsGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)NewsGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = !chkSelect.Checked;
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NewsGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)NewsGridView.Rows[i].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NewsGridView.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)NewsGridView.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked)
                    bll.DeleteNews(int.Parse(NewsGridView.DataKeys[i].Value.ToString()));
            }
            NewsGridView.DataSource = bll.ShowAllNews();
            NewsGridView.DataBind();
        }

        protected void NewsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void NewsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditNews.aspx?ID=" + NewsGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void NewsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewsGridView.PageIndex = e.NewPageIndex;
        }        
    }
}
