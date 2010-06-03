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
    public partial class Ad : System.Web.UI.UserControl
    {
        AdBLL bll = new AdBLL();
        public string pic,url;
        protected void Page_Load(object sender, EventArgs e)
        {        
            List<AdModel> list = bll.ShowAd();
            if (list.Count != 0)
            {
                foreach (AdModel ad in list)
                {
                    pic += ad.PhotoPath + "|";
                    url += ad.Url + "|";
                }
                pic = pic.Remove(pic.Length - 1);
                url = url.Remove(url.Length - 1);
                Page.DataBind();
            }
        }
    }
}