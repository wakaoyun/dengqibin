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
    public partial class AddAds : System.Web.UI.Page
    {
        static string path = "";
        AdBLL bll = new AdBLL();
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
                    IMGFileUpload.SaveAs(Server.MapPath(@"..\AdsImages\" + mypath));
                }
                catch
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('图片上传失败，请重新上传');</script>");
                }
                path = @"AdsImages\" + mypath;
                Photo.ImageUrl = @"~\" + path;
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            AdModel ads = new AdModel();
            ads.AdName = txtName.Text.Trim();
            ads.Url = txtUrl.Text.Trim();
            ads.PhotoPath = path;
            bool flag = bll.InsertAd(ads);
            if (flag)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='AdsInfo.aspx';</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
            }
        }
    }
}
