using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.Admin
{
    public partial class SystemCode : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
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
                    DDLType.DataSource = bll.GetCodeTable();
                    DDLType.DataBind();
                    CodeGridView.DataSource = bll.GetTypeByID(Convert.ToInt32(DDLType.SelectedValue));
                    CodeGridView.DataBind();
                }
            }
        }

        protected void RoomGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void DDLPa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeGridView.DataSource = bll.GetTypeByID(Convert.ToInt32(DDLType.SelectedValue));
            CodeGridView.DataBind();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            TypeModel type = new TypeModel();
            type.TypeID = Convert.ToInt32(DDLType.SelectedValue);
            type.Name = txtName.Text.Trim();
            bool flag = bll.InsertType(type);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');</script>");
                CodeGridView.DataSource = bll.GetTypeByID(Convert.ToInt32(DDLType.SelectedValue));
                CodeGridView.DataBind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
            txtName.Text = "";
        }
    }
}
