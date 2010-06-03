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
    public partial class AddFix : System.Web.UI.Page
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
                int i = bll.GetMaxID();
                if (i == -1)
                {
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "FM100001";
                }
                else
                {
                    lbID.Text = DateTime.Now.ToString("yyyy-MM-dd") + "FM" + (i + 1).ToString();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            FixModel fix = new FixModel();
            fix.FixID = lbID.Text;
            fix.Name = txtFixName.Text.Trim();
            fix.Amount = Convert.ToInt32(txtFixAmount.Text.Trim());
            fix.Factory = txtFixFactory.Text.Trim();
            fix.FactoryDate = Convert.ToDateTime(txtFixFactoryDate.Value);
            bool flag = bll.InsertFix(fix);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='FixInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FixInfo.aspx");
        }
    }
}
