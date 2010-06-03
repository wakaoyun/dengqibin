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
    public partial class EditEmployee : System.Web.UI.Page
    {
        static string path = "";
        EmployeeBLL bll = new EmployeeBLL();
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
                    EmployeeModel employee = new EmployeeModel();
                    employee = bll.GetEmployeeByID(id);
                    txtName.Text = employee.EmlpoyeeName;
                    if (employee.Sex == 0)
                        lbSex.Text = "男";
                    else
                        lbSex.Text = "女";
                    lbID.Text = employee.CardID;
                    txtArrage.Text = employee.Arrage;
                    txtTel.Text = employee.Tel;
                    txtMobile.Text = employee.Mobile;
                    txtAddress.Text = employee.Address;
                    path = employee.PhotoPath;
                    Photo.ImageUrl = path;
                }
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {
            if (IMGFileUpload.PostedFile.FileName == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('请选择图片');</script>");
            }
            else
            {

                string exten = "." + IMGFileUpload.PostedFile.FileName.Substring(IMGFileUpload.PostedFile.FileName.LastIndexOf(".") + 1);
                string mypath = DateTime.Now.ToString("yyyyMMddHHmmss") + exten;
                try
                {
                    IMGFileUpload.SaveAs(Server.MapPath(@"UserHeadImages\" + mypath));
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('图片上传失败，请重新上传');</script>");
                }
                path = @"~\Admin\UserHeadImages\" + mypath;
                Photo.ImageUrl = path;
            }
        }

        protected void Modify_Click(object sender, EventArgs e)
        {

            EmployeeModel employee = new EmployeeModel();
            employee.ID = int.Parse(Request.QueryString["ID"]);
            employee.EmlpoyeeName = txtName.Text.Trim();
            employee.Arrage = txtArrage.Text.Trim();
            employee.Address = txtAddress.Text.Trim();
            employee.Tel = txtTel.Text.Trim();
            employee.Mobile = txtMobile.Text.Trim();
            if (path == "")
            {
                employee.PhotoPath = @"~\Admin\UserHeadImages\Default.jpg";
            }
            else
            {
                employee.PhotoPath = path;
            }
            bool flag = bll.UpdateEmployee(employee);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改成功');location.href='EmployeeInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('修改失败');</script>");
            }
        }
    }
}
