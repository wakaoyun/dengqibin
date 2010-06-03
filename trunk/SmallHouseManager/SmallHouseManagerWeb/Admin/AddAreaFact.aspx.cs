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
    public partial class AddAreaFact : System.Web.UI.Page
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
                    DDLAreaType.DataSource = bll.GetType("AreaFact");
                    DDLAreaType.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            AreaFactModel areafact = new AreaFactModel();
            areafact.FactName = txtFactName.Text.Trim();
            areafact.MainHead = txtMainHead.Text.Trim();
            areafact.TypeID = Convert.ToInt32(DDLAreaType.SelectedValue);
            areafact.Tel = txtTelphone.Text.Trim();
            areafact.Memo = ftbMemo.Text.Trim();
            bool flag = afbll.InsertAreaFact(areafact);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='BaseInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
