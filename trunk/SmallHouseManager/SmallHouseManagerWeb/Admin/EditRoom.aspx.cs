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
    public partial class EditRoom : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        RoomBLL roombll = new RoomBLL();
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
                    DDLRoomFormat.DataSource = bll.GetType("RoomFormat");
                    DDLRoomFormat.DataBind();
                    DDLSunny.DataSource = bll.GetType("Sunny");
                    DDLSunny.DataBind();
                    DDLRoomUse.DataSource = bll.GetType("RoomUse");
                    DDLRoomUse.DataBind();
                    DDLIndoor.DataSource = bll.GetType("Indoor");
                    DDLIndoor.DataBind();
                    string code = Request.QueryString["ID"].ToString();
                    RoomModel room = roombll.GetRoomByID(code);
                    lbCode.Text = room.Code;
                    lbRoomID.Text = room.RoomID;
                    lbPaName.Text = room.PaName;
                    txtBuildArea.Text = room.BuildArea.ToString();
                    txtUseArea.Text = room.UseArea.ToString();
                    DDLRoomFormat.SelectedValue = room.RoomFormatID.ToString();
                    DDLSunny.SelectedValue = room.SunnyID.ToString();
                    DDLRoomUse.SelectedValue = room.RoomUseID.ToString();
                    DDLIndoor.SelectedValue = room.IndoorID.ToString();
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            RoomModel room = new RoomModel();
            room.Code = Request.QueryString["ID"].ToString();
            room.RoomFormatID = Convert.ToInt32(DDLRoomFormat.SelectedValue);
            room.SunnyID = Convert.ToInt32(DDLSunny.SelectedValue);
            room.RoomUseID = Convert.ToInt32(DDLRoomUse.SelectedValue);
            room.IndoorID = Convert.ToInt32(DDLIndoor.SelectedValue);
            room.BuildArea = Convert.ToDouble(txtBuildArea.Text.Trim());
            room.UseArea = Convert.ToDouble(txtUseArea.Text.Trim());
            bool flag = roombll.UpdateRoom(room);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='RoomInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("RoomInfo.aspx");
        }
    }
}
