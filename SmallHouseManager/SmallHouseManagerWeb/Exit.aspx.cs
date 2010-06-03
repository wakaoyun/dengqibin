using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmallHouseManagerWeb
{
    public partial class Exit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}
