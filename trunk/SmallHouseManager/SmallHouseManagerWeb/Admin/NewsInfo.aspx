<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="NewsInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.NewsInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑小区物业公告</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkNoticeTitle" runat="server" Text="公告名称" Width="80px" />
        <asp:TextBox ID="txtNoticeTitle" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkNoticeDate" runat="server" Text="公告日期从" Width="80px" />
        <input id="txtBeginDate" runat="server" onclick="calendar()" 
            onfocus="calendar()" readonly="readonly" style="color: #1A438E" type="text" />到
        <input id="txtEndDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkPerson" runat="server" Text="发布人" Width="80px" />
        <asp:TextBox ID="txtPerson" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkExact" runat="server" Text="精确查找" Width="80px" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="NewsGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="NewsGridView_RowEditing"
            onrowdatabound="NewsGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="NewsGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="公告名称" DataField="Title" >
                <ItemStyle Width="300px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="公告日期" DataField="NoticeDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="发布人" DataField="NoticePerson" >
                <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="85px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="选择删除">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div class="grid bottom_button">
        <asp:Button ID="SelectAll" runat="server" Text="全选" ForeColor="#1A438E" 
            onclick="SelectAll_Click" />
        <asp:Button ID="ConvertSelected" runat="server" Text="反选" ForeColor="#1A438E" 
            onclick="ConvertSelected_Click" />
        <asp:Button ID="Cancel" runat="server" Text="取消" ForeColor="#1A438E" 
            onclick="Cancel_Click" />
        <asp:Button ID="Delete" runat="server" Text="删除" ForeColor="#1A438E" 
            onclick="Delete_Click" />
    </div>
</asp:Content>
