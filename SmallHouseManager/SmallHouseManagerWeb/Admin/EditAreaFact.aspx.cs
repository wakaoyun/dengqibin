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
    public partial class EditAreaFact : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        AreafactBLL afbll = new AreafactBLL();
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
                    int id = int.Parse(Request.QueryString["ID"]);
                    DDLAreaType.DataSource = bll.GetType("AreaFact");
                    DDLAreaType.DataBind();
                    AreaFactModel areafact = afbll.GetAreaFactByID(id);
                    txtFactName.Text = areafact.FactName;
                    txtMainHead.Text = areafact.MainHead;
                    DDLAreaType.SelectedValue = areafact.TypeID.ToString();
                    txtTelphone.Text = areafact.Tel;
                    ftbMemo.Text = areafact.Memo;
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            AreaFactModel areafact = new AreaFactModel();
            areafact.FactName = txtFactName.Text.Trim();
            areafact.MainHead = txtMainHead.Text.Trim();
            areafact.TypeID = Convert.ToInt32(DDLAreaType.SelectedValue);
            areafact.Tel = txtTelphone.Text.Trim();
            areafact.Memo = ftbMemo.Text.Trim();
            areafact.ID = int.Parse(Request.QueryString["ID"]);
            bool flag = afbll.UpdateAreaFact(areafact);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='AreaFactInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("AreaFactInfo.aspx");
        }
    }
}
