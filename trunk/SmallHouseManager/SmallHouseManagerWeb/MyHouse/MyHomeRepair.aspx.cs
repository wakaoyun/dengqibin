using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.MyHouse
{
    public partial class MyHomeRepair : System.Web.UI.Page
    {
        HomeRepairBLL bll = new HomeRepairBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {            
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 0)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {                  
                    List<HomeRepairModel> list = bll.ShowHomeRepairByCode(user.Code);                  
                    HomeRepairGridView.DataSource = list;
                    HomeRepairGridView.DataBind();
                }
            }
        }

        protected void HomeRepairGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "0")
                {
                    e.Row.Cells[3].Text = "<font color='blue'>未处理</font>";
                    e.Row.Cells[6].Enabled = false;
                }
                else
                {
                    e.Row.Cells[3].Text = "<font color='red'>已处理</font>";
                    e.Row.Cells[4].Enabled = false;
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[6].Enabled = true;
                }
                ((LinkButton)(e.Row.Cells[5].Controls[0])).Attributes.Add("onclick", "return confirm('确定删除吗？')");
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void HomeRepairGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Response.Redirect("EditHomeRepair.aspx?ID="+HomeRepairGridView.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void HomeRepairGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = HomeRepairGridView.DataKeys[e.RowIndex].Value.ToString();        
            bool flag = bll.DeleteHomeRepair(id);           
            if (flag)
            {
                Response.Redirect("MyHomeRepair.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('删除失败')</script>");
            }
        }
    }
}
