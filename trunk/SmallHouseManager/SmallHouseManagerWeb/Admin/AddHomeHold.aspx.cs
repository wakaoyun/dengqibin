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
    public partial class AddHomeHold : System.Web.UI.Page
    {
        RoomBLL roombll = new RoomBLL();
        PavilionBLL pabll = new PavilionBLL();
        TypeBLL typebll = new TypeBLL();
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
                    DDLPa.DataSource = pabll.GetAllPavilion();
                    DDLPa.DataBind();
                    DDLCell.DataSource = typebll.GetType("Cell");
                    DDLCell.DataBind();
                    string str = DDLPa.SelectedValue;
                    int i = Convert.ToInt32(DDLCell.SelectedValue);
                    if (i < 10)
                        str += "0" + i.ToString();
                    else
                        str += i.ToString();
                    List<RoomModel> list = roombll.GetAllRoomNotUsed(str);
                    if (list.Count != 0)
                    {
                        DDLRoomID.DataSource = list;
                        DDLRoomID.DataBind();
                        Panel1.Visible = true;
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }
                }
            }
        }

        protected void DDLPa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = DDLPa.SelectedValue;
            int i = Convert.ToInt32(DDLCell.SelectedValue);
            if (i < 10)
                str += "0" + i.ToString();
            else
                str += i.ToString();
            List<RoomModel> list = roombll.GetAllRoomNotUsed(str);
            if (list.Count != 0)
            {
                DDLRoomID.DataSource = list;
                DDLRoomID.DataBind();
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            HomeHoldBLL homebll = new HomeHoldBLL();
            HomeHoldModel homeHold = new HomeHoldModel();            
            UserModel addUser = new UserModel();
            UsersBLL userbll = new UsersBLL();
            homeHold.Code = DDLRoomID.SelectedValue;
            homeHold.UserName = txtName.Text.Trim();
            homeHold.Tel = txtTelephone.Text.Trim();
            homeHold.Contact = txtAddress.Text.Trim();
            homeHold.Mobile = txtMobile.Text.Trim();
            homeHold.Email = txtEmail.Text.Trim();
            homeHold.CardID = txtID.Text.Trim();
            homeHold.OwnerID = txtOwnerID.Text.Trim();
            homeHold.Unit = txtUnit.Text.Trim();
            homeHold.Memo = txtMemo.Text.Trim();
            if (txtUID.Text.Trim() == "")
            {
                addUser.UID = txtID.Text.Trim();
            }
            else
            {
                addUser.UID = txtUID.Text.Trim();
            }
            if (txtPassword.Text.Trim() == "")
            {
                addUser.Password = txtID.Text.Trim();
            }
            else
            {
                addUser.Password = txtPassword.Text.Trim();
            }
            addUser.UserType = 0;
            addUser.SubID = homebll.GetMaxID() + 1;
            if (userbll.CheckUserByUID(addUser.UID))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名已存在！');</script>");
                return;
            }
            else
            {
                bool flag = homebll.InsertHomeHold(homeHold, addUser, DDLRoomID.SelectedValue, 1);
                if (flag)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='HomeHoldInfo.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
                }
            }
        }
    }
}
