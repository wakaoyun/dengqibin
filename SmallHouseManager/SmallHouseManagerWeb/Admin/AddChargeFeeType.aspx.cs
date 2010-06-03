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
    public partial class AddChargeFeeType : System.Web.UI.Page
    {
        ChargeFreeTypeBLL bll = new ChargeFreeTypeBLL();
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChargeFeeTypeInfo.aspx");
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            ChargeFreeTypeModel chargefeeType = new ChargeFreeTypeModel();
            chargefeeType.TypeName = txtFeeName.Text.Trim();
            chargefeeType.Price = Convert.ToDouble(txtPrice.Text.Trim());
            chargefeeType.Format = txtFormat.Text.Trim();
            bool flag = bll.InsertChargeFeeType(chargefeeType);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='ChargeFeeTypeInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
