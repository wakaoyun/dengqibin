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
    public partial class AdminInfo : System.Web.UI.UserControl
    {
        EmployeeBLL bll = new EmployeeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {           
            EmployeeDataList.DataSource = bll.ShowEmployee();
            EmployeeDataList.DataBind();           
        }         
    }
}