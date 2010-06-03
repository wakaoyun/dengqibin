using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] == null || Session["User"].ToString() == "")
                Response.Redirect("../Login.aspx");
            else
            {
                UserModel user = (UserModel)Session["User"];
                if (user.UserType != 1)
                    Response.Redirect("../MyHouse/Default.aspx");
            }
        }
    }
}
