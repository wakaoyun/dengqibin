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
    public partial class AddChargeFee : System.Web.UI.Page
    {
        ChargeFreeTypeBLL freetypebll = new ChargeFreeTypeBLL();
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
                    DDLType.DataSource = freetypebll.GetChargeFreeType();
                    DDLType.DataBind();
                    DDLPa.DataSource = pabll.GetAllPavilion();
                    DDLPa.DataBind();
                    DDLCell.DataSource = typebll.GetType("Cell");
                    DDLCell.DataBind();
                    string str=DDLPa.SelectedValue;
                    int i=Convert.ToInt32(DDLCell.SelectedValue);
                    if(i<10)
                        str+="0"+i.ToString();
                    else
                        str+=i.ToString();
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
            HomeFreeBLL bll = new HomeFreeBLL();
            HomeFreeModel homeFree = new HomeFreeModel();
            homeFree.Code = DDLRoomID.SelectedValue;
            homeFree.TypeID = Convert.ToInt32(DDLType.SelectedValue);
            homeFree.Number = Convert.ToDouble(txtNumber.Text.Trim());
            homeFree.StartDate = Convert.ToDateTime(txtBuildDate.Value);
            homeFree.PayDate = DateTime.Now;
            homeFree.AddName = user.Name;
            bool flag = bll.InsertHomeFree(homeFree);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='ChargeFeeInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChargeFeeInfo.aspx");
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
