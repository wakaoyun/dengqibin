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
    public partial class AddFixRepair : System.Web.UI.Page
    {
        FixBLL fixbll = new FixBLL();
        FixRepairBLL bll = new FixRepairBLL();
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
                    DDLFixName.DataSource = fixbll.GetAllFix();
                    DDLFixName.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            FixRepairModel fixRepair = new FixRepairModel();
            fixRepair.FixID = DDLFixName.SelectedValue;
            fixRepair.RepairDate = Convert.ToDateTime(txtRepairDate.Value.Trim());
            fixRepair.EndDate = Convert.ToDateTime(txtEndDate.Value.Trim());
            fixRepair.MainHead = txtMainHead.Text.Trim();
            fixRepair.ServiceFee = Convert.ToDouble(txtServiceFee.Text.Trim());
            fixRepair.MaterielFee = Convert.ToDouble(txtMaterielFee.Text.Trim());
            fixRepair.RepairSum = Convert.ToDouble(txtSum.Text);
            fixRepair.RepairMemo = ftbMemo.Text.Trim();
            fixRepair.Sign = Convert.ToInt32(rblSign.SelectedValue.Trim());
            fixRepair.RepairUnit = txtRepairUnit.Text.Trim();
            bool flag = bll.InsertFixRepair(fixRepair);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='FixRepairInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
