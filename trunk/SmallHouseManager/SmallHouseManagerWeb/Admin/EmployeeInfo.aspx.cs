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
    public partial class EmployeeInfo : System.Web.UI.Page
    {
        EmployeeBLL bll = new EmployeeBLL();
        int currentPage;
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
                    lbCurrentPage.Text = "1";
                    currentPage = 0;
                    Bind();
                }
            }
        }

        private void Bind()
        {
            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = bll.ShowAllEmployee();
            ps.AllowPaging = true;
            ps.PageSize = 9;
            lbPage.Text = ps.PageCount.ToString();
            ps.CurrentPageIndex = currentPage;
            FirstPage.Enabled = true;
            PriorPage.Enabled = true;
            NextPage.Enabled = true;
            LastPage.Enabled = true;
            if (lbCurrentPage.Text == "1")
            {
                FirstPage.Enabled = false;
                PriorPage.Enabled = false;
            }
            if (lbCurrentPage.Text == ps.PageCount.ToString())
            {
                NextPage.Enabled = false;
                LastPage.Enabled = false;
            }
            EmployeeDataList.DataSource = ps;
            EmployeeDataList.DataKeyField = "ID";
            EmployeeDataList.DataBind();
        }

        protected void FirstPage_Click(object sender, EventArgs e)
        {
            lbCurrentPage.Text = "1";
            currentPage = 0;
            Bind();
        }

        protected void PriorPage_Click(object sender, EventArgs e)
        {
            lbCurrentPage.Text = (Convert.ToInt32(lbCurrentPage.Text) - 1).ToString();
            currentPage = Convert.ToInt32(lbCurrentPage.Text) - 1;
            Bind();
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            lbCurrentPage.Text = (Convert.ToInt32(lbCurrentPage.Text) + 1).ToString();
            currentPage = Convert.ToInt32(lbCurrentPage.Text) - 1;
            Bind();
        }

        protected void LastPage_Click(object sender, EventArgs e)
        {
            lbCurrentPage.Text = lbPage.Text;
            currentPage = Convert.ToInt32(lbCurrentPage.Text) - 1;
            Bind();
        }

        protected void EmployeeDataList_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            bool flag = bll.DeleteEmployee(Convert.ToInt32(EmployeeDataList.DataKeys[e.Item.ItemIndex]));
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('刪除成功');</script>");
                Bind();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('刪除失败');</script>");
            }            
        }
    }
}
