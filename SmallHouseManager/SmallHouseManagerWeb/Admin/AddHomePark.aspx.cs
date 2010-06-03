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
    public partial class AddHomePark : System.Web.UI.Page
    {
        BaseParkBLL parkbll = new BaseParkBLL();
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
                    DDLType.DataSource = parkbll.GetBasePark();
                    DDLType.DataBind();
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
                    List<RoomModel> list = roombll.GetAllRoomUsed(str);
                    if (list.Count != 0)
                    {
                        DDLRoomID.DataSource = list;
                        DDLRoomID.DataBind();
                        Panel1.Visible = true;
                        Panel2.Visible = false;
                    }
                    else
                    {
                        Panel1.Visible = false;
                        Panel2.Visible = true;
                    }
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            HomeParkBLL bll = new HomeParkBLL();
            HomeParkModel homePark = new HomeParkModel();
            homePark.ParkID = Convert.ToInt32(DDLType.SelectedValue);
            homePark.Code = DDLRoomID.SelectedValue;
            homePark.CarID = txtCarID.Text.Trim();
            homePark.Type = txtType.Text.Trim();
            homePark.BuyDate = Convert.ToDateTime(txtBuyDate.Value);
            homePark.Color = txtColor.Text.Trim();
            bool flag = bll.InsertHomePark(homePark);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='HomeParkInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomeParkInfo.aspx");
        }

        protected void DDLPa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = DDLPa.SelectedValue;
            int i = Convert.ToInt32(DDLCell.SelectedValue);
            if (i < 10)
                str += "0" + i.ToString();
            else
                str += i.ToString();
            List<RoomModel> list = roombll.GetAllRoomUsed(str);
            if (list.Count != 0)
            {
                DDLRoomID.DataSource = list;
                DDLRoomID.DataBind();
                Panel1.Visible = true;
                Panel2.Visible = false;
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
        }
    }
}
