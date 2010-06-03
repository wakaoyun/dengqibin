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
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BaseInfoBLL bll = new BaseInfoBLL();         
            List<BaseInfoModel> list = bll.ShowBaseInfo();            
            if (list.Count != 0)
            {                
                MainHead.Text = list[0].MainHead;
                Tel.Text = list[0].Tel;
                Address.Text = list[0].Address;
            }
        }
    }
}
