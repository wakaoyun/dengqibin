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
    public partial class News : System.Web.UI.UserControl
    {
        NewsBLL bll = new NewsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {         
            List<NewsModel> list = bll.ShowNews();            
            NewsRepeater.DataSource = list;
            NewsRepeater.DataBind();
        }
    }
}