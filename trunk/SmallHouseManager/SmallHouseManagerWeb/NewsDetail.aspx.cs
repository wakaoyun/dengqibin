using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.UserControl
{
    public partial class ShowNews : System.Web.UI.Page
    {
        NewsBLL bll = new NewsBLL();
        public NewsModel news = new NewsModel();
        protected void Page_Load(object sender, EventArgs e)
        {           
            int id = Convert.ToInt32(Request.QueryString["ID"]);            
            news = bll.ShowNewsByID(id);
            Page.DataBind();
        }
    }
}
