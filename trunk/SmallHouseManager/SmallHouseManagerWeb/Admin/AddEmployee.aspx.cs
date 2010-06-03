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
    public partial class AddEmployee : System.Web.UI.Page
    {
        static string path = "";
        UserModel user = new UserModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
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
                    IMGFileUpload.SaveAs(Server.MapPath(@"UserHeadImages\"+mypath));                    
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('图片上传失败，请重新上传');</script>");
                }
                path = @"~\Admin\UserHeadImages\" + mypath;
                Photo.ImageUrl = path;
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            EmployeeBLL bll = new EmployeeBLL();
            EmployeeModel employee = new EmployeeModel();
            UserModel addUser = new UserModel();
            UsersBLL userbll = new UsersBLL();
            employee.EmlpoyeeName = txtName.Text.Trim();
            employee.Sex=Convert.ToInt32(RBLSex.SelectedValue);
            employee.Arrage = txtArrage.Text.Trim();
            employee.Address = txtAddress.Text.Trim();
            employee.Tel = txtTel.Text.Trim();
            employee.Mobile = txtMobile.Text.Trim();
            employee.CardID = txtID.Text.Trim();
            if (path == "")
            {
                employee.PhotoPath = @"~\Admin\UserHeadImages\Default.jpg";
            }
            else
            {
                employee.PhotoPath = path;
            }
            if (txtUID.Text.Trim() == "")
            {
                addUser.UID = txtID.Text.Trim();
            }
            else
            {
                addUser.UID = txtUID.Text.Trim();
            }
            if (txtPassword.Text.Trim() == "")
            {
                addUser.Password = txtID.Text.Trim();
            }
            else
            {
                addUser.Password = txtPassword.Text.Trim();
            }
            addUser.UserType = 1;
            addUser.SubID = bll.GetMaxID() + 1;
            if (userbll.CheckUserByUID(addUser.UID))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('用户名已存在！');</script>");
                return;
            }
            else
            {
                bool flag = bll.InsertEmployee(employee, addUser);
                if (flag)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='EmployeeInfo.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
                }
            }
        }
    }
}
