using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb
{
    public partial class ShowNews : System.Web.UI.Page
    {
        NewsBLL bll = new NewsBLL();
        int currentPage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbCurrentPage.Text = "1";
                currentPage = 0;
                Bind();
            }              
        }
        private void Bind()
        {
            PagedDataSource ps = new PagedDataSource();            
            ps.DataSource = bll.ShowAllNews();           
            ps.AllowPaging = true;
            ps.PageSize = 20;
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
            NewsRepeater.DataSource = ps;
            NewsRepeater.DataBind();
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
    }
}
