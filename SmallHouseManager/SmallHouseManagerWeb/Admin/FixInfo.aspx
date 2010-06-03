<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FixInfo.aspx.cs" Inherits="SmallHouseManagerWeb.FixInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">小区设备信息查看</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkFixID" runat="server" Text="设备编号" Width="75px" />
        <asp:TextBox ID="txtFixID" runat="server" ForeColor="#1A438E" Width="105px"></asp:TextBox>
        <asp:CheckBox ID="chkFixName" runat="server" Text="设备名称" Width="75px" />
        <asp:TextBox ID="txtFixName" runat="server" ForeColor="#1A438E" Width="105px"></asp:TextBox>
        <asp:CheckBox ID="chkFixFactoryDate" runat="server" Text="生产日期从" Width="80px" />
        <input id="txtBeginDate" runat="server" onclick="calendar()" 
            onfocus="calendar()" readonly="readonly" style="color: #1A438E" type="text" />到
        <input id="txtEndDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />       
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkFixFactory" runat="server" Text="生产厂家" Width="75px" />
        <asp:TextBox ID="txtFixFactory" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkExact" runat="server" Text="精确查找" Width="80px" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="FixGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="FixGridView_RowEditing"
            onrowdatabound="FixGridView_RowDataBound"
            DataKeyNames="FixID" Font-Size="12px" 
            onpageindexchanging="FixGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="设备编号" DataField="FixID" >
                <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="设备名称" DataField="Name" >
                <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量（件）" DataField="Amount" >
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="生产厂家" DataField="Factory" >
                <ItemStyle HorizontalAlign="Center" Width="140px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="生产日期" DataField="FactoryDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="选择删除">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
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
