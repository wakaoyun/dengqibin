using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;

namespace SmallHouseManagerWeb.MyHouse
{
    public partial class MyInfo : System.Web.UI.Page
    {
        HomeHoldBLL bll = new HomeHoldBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserModel user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == ""||user.UserType!=0)
                Response.Redirect("../Login.aspx");
            else
            {              
                HomeHoldModel homeHold = bll.ShowHomeHoldByID(user.Code);               
                OwnerName.Text = homeHold.UserName;
                Telephone.Text = homeHold.Tel;
                Address.Text = homeHold.Contact;
                Email.Text = homeHold.Email;
                CardID.Text = homeHold.OwnerID;
                WorkUnit.Text = homeHold.Unit;
                Mobile.Text = homeHold.Mobile;
                Identity.Text = homeHold.CardID;
                Memo.Text = homeHold.Memo;
                UserName.Text = user.UID;
            }
        }
    }
}
