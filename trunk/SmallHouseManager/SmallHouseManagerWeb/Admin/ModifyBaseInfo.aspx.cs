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
    public partial class ModifyBaseInfo : System.Web.UI.Page
    {
        BaseInfoBLL bll = new BaseInfoBLL();
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
                    BaseInfoBLL bll = new BaseInfoBLL();
                    List<BaseInfoModel> list = bll.ShowBaseInfo();
                    if (list.Count != 0)
                    {
                        txtBaseName.Text = list[0].HomeName;
                        txtAmount.Text = list[0].Amount.ToString(); 
                        txtBuildArea.Text = list[0].BuildArea.ToString();
                        txtGreenArea.Text = list[0].GreenArea.ToString();
                        txtRoadArea.Text = list[0].RoadArea.ToString();
                        txtParkArea.Text = list[0].ParkingArea.ToString(); 
                        txtBuildDate.Value = list[0].BuildDate.ToString("yyy-MM-dd");
                        txtMainHead.Text = list[0].MainHead;
                        txtTelephone.Text = list[0].Tel;
                        txtAddress.Text = list[0].Address;
                        txtMemo.Text = list[0].Memo;
                    }
                }
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {
            BaseInfoModel baseinfo = new BaseInfoModel();
            baseinfo.HomeName = txtBaseName.Text.Trim();
            baseinfo.MainHead = txtMainHead.Text.Trim();
            baseinfo.BuildDate = DateTime.Parse(txtBuildDate.Value.Trim().ToString());
            baseinfo.BuildArea = Double.Parse(txtBuildArea.Text.Trim());
            baseinfo.RoadArea = Double.Parse(txtRoadArea.Text.Trim());
            baseinfo.GreenArea = Double.Parse(txtGreenArea.Text.Trim());
            baseinfo.ParkingArea = Double.Parse(txtParkArea.Text.Trim());
            baseinfo.Amount = int.Parse(txtAmount.Text.Trim());
            baseinfo.Tel = txtTelephone.Text.Trim();
            baseinfo.Address = txtAddress.Text.Trim();
            baseinfo.Memo = txtMemo.Text.Trim();
            baseinfo.ID = (int)Session["BaseInfoID"];
            bool flag = bll.UpdateBaseInfo(baseinfo);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='BaseInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BaseInfo.aspx");
        }
    }
}
