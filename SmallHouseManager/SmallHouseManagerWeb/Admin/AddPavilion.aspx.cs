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
    public partial class AddPavilion : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
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
                    DDLPavilionType.DataSource = bll.GetType("Pavilion");
                    DDLPavilionType.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            PavilionModel pavilion = new PavilionModel();
            int i = pabll.GetMaxID();
            if (i == -1)
            {
                pavilion.PaID = "100";
            }
            else
            {
                pavilion.PaID = (i + 1).ToString();
            }
            pavilion.Name = txtPavilionName.Text.Trim();
            pavilion.Layer = Convert.ToInt32(txtPavilionLayer.Text.Trim());
            pavilion.Height = Convert.ToDouble(txtPavilionHeight.Text.Trim());
            pavilion.Area = Convert.ToDouble(txtPavilionArea.Text.Trim());
            pavilion.BuildDate = Convert.ToDateTime(txtBuildDate.Value.Trim());
            pavilion.TypeID = Convert.ToInt32(DDLPavilionType.SelectedValue);
            pavilion.Memo = txtMemo.Text.Trim();
            bool flag = pabll.InsertPavilion(pavilion);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='PavilionInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
