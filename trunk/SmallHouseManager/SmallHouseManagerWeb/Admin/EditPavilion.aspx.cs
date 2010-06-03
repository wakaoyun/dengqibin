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
    public partial class EditPavilion : System.Web.UI.Page
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
                    string paID = Request.QueryString["ID"].ToString();
                    PavilionModel pavilion = pabll.GetPavilionByID(paID);
                    txtPavilionName.Text = pavilion.Name;
                    txtPavilionLayer.Text = pavilion.Layer.ToString();
                    txtPavilionHeight.Text = pavilion.Height.ToString();
                    txtPavilionArea.Text = pavilion.Area.ToString();
                    txtBuildDate.Value = pavilion.BuildDate.ToString("yyyy-MM-dd");
                    DDLPavilionType.SelectedValue = pavilion.TypeID.ToString();
                    txtMemo.Text = pavilion.Memo;
                }
            }
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            PavilionModel pavilion = new PavilionModel();
            pavilion.PaID = Request.QueryString["ID"].ToString();
            pavilion.Name = txtPavilionName.Text.Trim();
            pavilion.Layer = Convert.ToInt32(txtPavilionLayer.Text.Trim());
            pavilion.Height = Convert.ToDouble(txtPavilionHeight.Text.Trim());
            pavilion.Area = Convert.ToDouble(txtPavilionArea.Text.Trim());
            pavilion.BuildDate = Convert.ToDateTime(txtBuildDate.Value.Trim());
            pavilion.TypeID = Convert.ToInt32(DDLPavilionType.SelectedValue);
            pavilion.Memo = txtMemo.Text.Trim();
            bool flag = pabll.UpdatePavilion(pavilion);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='PavilionInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PavilionInfo.aspx");
        }
    }
}
