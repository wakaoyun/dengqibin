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
    public partial class EditChargeFeeType : System.Web.UI.Page
    {
        ChargeFreeTypeBLL bll = new ChargeFreeTypeBLL();
        UserModel user = new UserModel();
        ChargeFreeTypeModel chargeFeeType = new ChargeFreeTypeModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    int typeID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    chargeFeeType = bll.GetChargeFreeTypeByID(typeID);
                    lbName.Text = chargeFeeType.TypeName;
                    lbFormat.Text = chargeFeeType.Format;
                    txtPrice.Text = chargeFeeType.Price.ToString();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            chargeFeeType.TypeID = Convert.ToInt32(Request.QueryString["ID"].ToString());
            chargeFeeType.Price = Convert.ToDouble(txtPrice.Text.Trim());
            bool flag = bll.UpdateChargeFreeType(chargeFeeType);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='ChargeFeeTypeInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChargeFeeTypeInfo.aspx");
        }
    }
}
