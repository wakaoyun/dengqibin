﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;
using System.IO;

namespace SmallHouseManagerWeb.Admin
{
    public partial class Backup : System.Web.UI.Page
    {
        UserModel user = new UserModel();
        Ultility ultity = new Ultility();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    txtFileName.Text = DateTime.Now.ToString("yyyyMMddHHmmss") + "Backup";
                }
            }
        }

        protected void BackupData_Click(object sender, EventArgs e)
        {
            string file = Server.MapPath("~/Backup") + "\\"+txtFileName.Text.Trim()+".bak";
            if (File.Exists(file))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('文件名已存在，备份失败！');</script>");
            }
            else
            {
                bool flag = ultity.BackupDataBase(file);
                if (flag)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('备份成功');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('备份失败');</script>");
                }
            }
        }
    }
}
