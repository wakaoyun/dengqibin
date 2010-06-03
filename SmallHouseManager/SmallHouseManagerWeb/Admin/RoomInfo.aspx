<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="RoomInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.RoomInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">小区房产管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkPaName" runat="server" Text="楼宇：" Width="80px" />
        <asp:DropDownList ID="DDLPa" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="PaID"></asp:DropDownList> 
        <asp:CheckBox ID="chkCell" runat="server" Text="单元：" Width="80px" />
        <asp:DropDownList ID="DDLCell" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="ID"></asp:DropDownList> 
        <asp:CheckBox ID="chkSunny" runat="server" Text="房间朝向：" Width="80px" />
        <asp:DropDownList ID="DDLSunny" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="ID"></asp:DropDownList> 
        <asp:CheckBox ID="chkLayer" runat="server" Text="楼层：" Width="80px" />
        <asp:TextBox ID="txtLayer" runat="server" ForeColor="#1A438E" Width="105px"></asp:TextBox>        
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkUse" runat="server" Text="房间功能：" Width="80px" />
        <asp:DropDownList ID="DDLUse" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="ID"></asp:DropDownList> 
        <asp:CheckBox ID="chkFormat" runat="server" Text="房间规格：" Width="80px" />
        <asp:DropDownList ID="DDLFormat" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="ID"></asp:DropDownList> 
        <asp:CheckBox ID="chkIndoor" runat="server" Text="装修标准：" Width="80px" />
        <asp:DropDownList ID="DDLIndoor" runat="server" ForeColor="#1A438E" Width="80px" 
            DataTextField="Name" DataValueField="ID"></asp:DropDownList> 
        <asp:RadioButton ID="rbSale" runat="server" Text="已出售" Checked="True" 
            GroupName="state" />
        <asp:RadioButton ID="rbUnSale" runat="server" Text="未出售" GroupName="state" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="RoomGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="RoomView_RowEditing"
            onrowdatabound="RoomView_RowDataBound"
            DataKeyNames="Code" Font-Size="12px" 
            onpageindexchanging="RoomGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="房间号" DataField="RoomID" >
                <ItemStyle Width="65px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="楼宇" DataField="PaName" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房间朝向" DataField="SunnyName" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房间功能" DataField="RoomUseName" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房间规格" DataField="RoomFormatName" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="装修标准" DataField="IndoorName" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房间面积" DataField="BuildArea" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="可用面积" DataField="UseArea" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否出售" DataField="State" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="选择删除">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
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
