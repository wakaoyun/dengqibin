using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallHouseManagerBLL;
using SmallHouseManagerModel;
using System.Data;

namespace SmallHouseManagerWeb.Admin
{
    public partial class AddRoom : System.Web.UI.Page
    {
        TypeBLL bll = new TypeBLL();
        PavilionBLL pabll = new PavilionBLL();
        RoomBLL roombll = new RoomBLL();
        UserModel user = new UserModel();
        static DataTable dttable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (UserModel)Session["User"];
            if (Session["User"] == null || Session["User"].ToString() == "" || user.UserType != 1)
                Response.Redirect("../Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DDLPa.DataSource = pabll.GetAllPavilion();
                    DDLPa.DataBind();
                    DDLCell.DataSource = bll.GetType("Cell");
                    DDLCell.DataBind();
                    DDLLayerBegin.Items.Clear();
                    DDLLayerEnd.Items.Clear();
                    PavilionModel pavilion = pabll.GetPavilionByID(DDLPa.SelectedValue);
                    for (int i = 1; i <= pavilion.Layer; i++)
                    {
                        ListItem items = new ListItem();
                        items.Text = i.ToString();
                        if (i < 10)
                        {
                            items.Value = "0" + i.ToString();
                        }
                        else
                        {
                            items.Value = i.ToString();
                        }
                        DDLLayerBegin.Items.Add(items);
                        DDLLayerEnd.Items.Add(items);
                    }
                    DDLLayerBegin.DataBind();
                    DDLLayerEnd.DataBind();
                    if (dttable.Columns.Count == 0)
                    {
                        dttable.Columns.Add("ID", typeof(int));
                        dttable.Columns.Add("SunnyID", typeof(int));
                        dttable.Columns.Add("SunnyName", typeof(string));
                        dttable.Columns.Add("IndoorID", typeof(int));
                        dttable.Columns.Add("IndoorName", typeof(string));
                        dttable.Columns.Add("RoomUseID", typeof(int));
                        dttable.Columns.Add("RoomUseName", typeof(string));
                        dttable.Columns.Add("RoomFormatID", typeof(int));
                        dttable.Columns.Add("RoomFormatName", typeof(string));
                        dttable.Columns.Add("BuildArea", typeof(double));
                        dttable.Columns.Add("UseArea", typeof(double));
                        DataColumn[] key = { dttable.Columns["ID"] };
                        dttable.PrimaryKey = key;
                    }
                }                
            }
        }

        protected void Build_Click(object sender, EventArgs e)
        {
            int end=Convert.ToInt32(DDLLayerEnd.SelectedValue);
            int begin=Convert.ToInt32(DDLLayerBegin.SelectedValue);
            if (begin > end)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('开始楼层不应大于结束楼层');</script>");
            }
            else
            {
                DDLSunny.DataSource = bll.GetType("Sunny");
                DDLSunny.DataBind();
                DDLFormat.DataSource = bll.GetType("RoomFormat");
                DDLFormat.DataBind();
                DDLRoomUse.DataSource = bll.GetType("RoomUse");
                DDLRoomUse.DataBind();
                DDLIndoor.DataSource = bll.GetType("Indoor");
                DDLIndoor.DataBind();
                for (int i = 0; i <= Convert.ToInt32(txtCount.Text.Trim()); i++)
                {
                    DDLRow.Items.Add(i.ToString());
                }
                ModifyPanel.Visible = true;
                dttable.Rows.Clear();
                int count = Convert.ToInt32(txtCount.Text.Trim()) * (end - begin+1);
                for (int i = 1; i <= count; i++)
                {
                    DataRow row = dttable.NewRow();
                    row[0] = i;
                    row[1] = Convert.ToInt32(DDLSunny.SelectedValue);
                    row[2] = DDLSunny.SelectedItem.Text;
                    row[3] = Convert.ToInt32(DDLIndoor.SelectedValue);
                    row[4] = DDLIndoor.SelectedItem.Text;
                    row[5] = Convert.ToInt32(DDLRoomUse.SelectedValue);
                    row[6] = DDLRoomUse.SelectedItem.Text;
                    row[7] = Convert.ToInt32(DDLFormat.SelectedValue);
                    row[8] = DDLFormat.SelectedItem.Text;
                    row[9] = Convert.ToDouble(txtArea.Text.Trim());
                    row[10] = Convert.ToDouble(txtUseArea.Text.Trim());
                    dttable.Rows.Add(row);
                }
                RoomGridView.DataSource = dttable;
                RoomGridView.DataBind();
                DDLPa.Enabled = false;
                DDLCell.Enabled = false;
                DDLLayerBegin.Enabled = false;
                DDLLayerEnd.Enabled = false;
                txtCount.Enabled = false;
                txtPrefix.Enabled = false;
                Build.Enabled = false;
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            DataRowCollection drowc;
            DataRow row;
            drowc = dttable.Rows;
            if (DDLRow.SelectedValue != "0")
            {
                if (drowc.Contains(DDLRow.SelectedValue))
                {
                    row = drowc.Find(DDLRow.SelectedValue);
                    row[1] = Convert.ToInt32(DDLSunny.SelectedValue);
                    row[2] = DDLSunny.SelectedItem.Text;
                    row[3] = Convert.ToInt32(DDLIndoor.SelectedValue);
                    row[4] = DDLIndoor.SelectedItem.Text;
                    row[5] = Convert.ToInt32(DDLRoomUse.SelectedValue);
                    row[6] = DDLRoomUse.SelectedItem.Text;
                    row[7] = Convert.ToInt32(DDLFormat.SelectedValue);
                    row[8] = DDLFormat.SelectedItem.Text;
                    row[9] = Convert.ToDouble(txtArea.Text.Trim());
                    row[10] = Convert.ToDouble(txtUseArea.Text.Trim());
                    if (row.HasErrors)
                    {
                        dttable.RejectChanges();
                    }
                    else
                    {
                        dttable.AcceptChanges();
                    }
                }
            }
            else 
            {

                for (int i = 1; i <= drowc.Count; i++)
                {

                    if (drowc.Contains(i))
                    {
                        row = drowc.Find(i);
                        row[1] = Convert.ToInt32(DDLSunny.SelectedValue);
                        row[2] = DDLSunny.SelectedItem.Text;
                        row[3] = Convert.ToInt32(DDLIndoor.SelectedValue);
                        row[4] = DDLIndoor.SelectedItem.Text;
                        row[5] = Convert.ToInt32(DDLRoomUse.SelectedValue);
                        row[6] = DDLRoomUse.SelectedItem.Text;
                        row[7] = Convert.ToInt32(DDLFormat.SelectedValue);
                        row[8] = DDLFormat.SelectedItem.Text;
                        row[9] = Convert.ToDouble(txtArea.Text.Trim());
                        row[10] = Convert.ToDouble(txtUseArea.Text.Trim());
                        if (row.HasErrors)
                        {
                            dttable.RejectChanges();
                        }
                        else
                        {
                            dttable.AcceptChanges();
                        }
                    }
                }
            }
            RoomGridView.DataSource = dttable;
            RoomGridView.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string code=DDLPa.SelectedValue;
            int cellID = Convert.ToInt32(DDLCell.SelectedValue);
            if(cellID<10)
                code+="0"+cellID.ToString();
            else
                code+=cellID.ToString();
            if (roombll.CheckRooms(code))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('单元重复，插入失败');</script>");
            }
            else
            {
                int end = Convert.ToInt32(DDLLayerEnd.SelectedValue);
                int begin = Convert.ToInt32(DDLLayerBegin.SelectedValue);
                RoomModel[] room = new RoomModel[dttable.Rows.Count];
                for (int i = begin; i <= end; i++)
                {
                    int count = 0;
                    for (int j = 1; j <= Convert.ToInt32(txtCount.Text.Trim()); j++)
                    {
                        RoomModel roomitems = new RoomModel();
                        room[count] = roomitems;
                        room[count].PaID = DDLPa.SelectedValue;
                        room[count].CellID = cellID;
                        room[count].SunnyID = Convert.ToInt32(dttable.Rows[count]["SunnyID"]);
                        room[count].IndoorID = Convert.ToInt32(dttable.Rows[count]["IndoorID"]);
                        room[count].RoomUseID = Convert.ToInt32(dttable.Rows[count]["RoomUseID"]);
                        room[count].RoomFormatID = Convert.ToInt32(dttable.Rows[count]["RoomFormatID"]);
                        room[count].BuildArea = Convert.ToDouble(dttable.Rows[count]["BuildArea"]);
                        room[count].UseArea = Convert.ToDouble(dttable.Rows[count]["UseArea"]);
                        string str = DDLPa.SelectedValue;
                        string strRoomID = txtPrefix.Text.Trim();
                        if (cellID < 10)
                            str += "0" + cellID.ToString();
                        else
                            str += cellID.ToString();
                        if (i < 10)
                        {
                            str += "0" + i.ToString();
                            strRoomID += "0" + i.ToString();
                        }
                        else
                        {
                            str += i.ToString();
                            strRoomID += i.ToString();
                        }
                        if (j < 10)
                        {
                            str += "0" + j.ToString();
                            strRoomID += "0" + j.ToString();
                        }
                        else
                        {
                            str += j.ToString();
                            strRoomID += j.ToString();
                        }
                        room[count].RoomID = strRoomID;///房间号＝前缀+层号+房间序号
                        room[count].Code = str;//房间编号＝楼宇号+单元号+层号+房间序号
                        count++;
                    }
                }
                bool flag = roombll.InsertRoom(room);
                if (flag)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加成功');location.href='RoomInfo.aspx';</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "OnSubmit", "<script>alert('添加失败');</script>");
                }
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            dttable.Rows.Clear();
            RoomGridView.DataSource = dttable;
            RoomGridView.DataBind();
            ModifyPanel.Visible = false;
            DDLPa.Enabled = true;
            DDLCell.Enabled = true;
            DDLLayerBegin.Enabled = true;
            DDLLayerEnd.Enabled = true;
            txtCount.Enabled = true;
            txtPrefix.Enabled = true;
            Build.Enabled = true;
        }

        protected void RoomGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#99ccff';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
            }
        }

        protected void DDLPa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLLayerBegin.Items.Clear();
            DDLLayerEnd.Items.Clear();
            PavilionModel pavilion = pabll.GetPavilionByID(DDLPa.SelectedValue);
            for (int i = 1; i <= pavilion.Layer; i++)
            {
                ListItem items = new ListItem();
                items.Text = i.ToString();
                if (i < 10)
                {
                    items.Value = "0" + i.ToString();
                }
                else
                {
                    items.Value = i.ToString();
                }
                DDLLayerBegin.Items.Add(items);
                DDLLayerEnd.Items.Add(items);
            }
            DDLLayerBegin.DataBind();
            DDLLayerEnd.DataBind();
        }
    }
}
