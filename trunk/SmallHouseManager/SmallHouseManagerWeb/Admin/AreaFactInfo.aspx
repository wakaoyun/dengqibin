<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AreaFactInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AreaFactInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">公共设施管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkAreaName" runat="server" Text="设施名称" Width="80px" />
        <asp:TextBox ID="txtAreaName" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkMainHead" runat="server" Text="负责人" Width="60px" />
        <asp:TextBox ID="txtMainHead" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkType" runat="server" Text="设施类型" Width="80px" />
        <asp:DropDownList ID="DDLAreaType" runat="server" ForeColor="#1A438E" 
            Width="145px" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkTelephone" runat="server" Text="联系电话" Width="80px" />
        <asp:TextBox ID="txtTelehpone" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:CheckBox ID="chkExact" runat="server" Text="精确查找" Width="80px" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="AreaFactGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="AreaFactGridView_RowEditing"
            onrowdatabound="AreaFactGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="AreaFactGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="设施名称" DataField="FactName" >
                <ItemStyle Width="160px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="设施类型" DataField="TypeName" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="负责人" DataField="MainHead" >
                <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="联系电话" DataField="Tel" >
                <ItemStyle HorizontalAlign="Center" Width="140px" />
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
