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
    public partial class EditFix : System.Web.UI.Page
    {
        FixBLL bll = new FixBLL();
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
                    string fixID = Request.QueryString["ID"].ToString();
                    FixModel fix = bll.GetFixByID(fixID);
                    lbID.Text = fix.FixID;
                    txtFixName.Text = fix.Name;
                    txtFixAmount.Text = fix.Amount.ToString();
                    txtFixFactory.Text = fix.Factory;
                    txtFixFactoryDate.Value = fix.FactoryDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            FixModel fix = new FixModel();
            fix.FixID = lbID.Text;
            fix.Name = txtFixName.Text.Trim();
            fix.Amount = Convert.ToInt32(txtFixAmount.Text.Trim());
            fix.Factory = txtFixFactory.Text.Trim();
            fix.FactoryDate = Convert.ToDateTime(txtFixFactoryDate.Value);
            bool flag = bll.UpdateFix(fix);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='FixInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("FixInfo.aspx");
        }
    }
}
