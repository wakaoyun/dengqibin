<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HomeHoldInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.HomeHoldInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">小区住户管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">    
    <div class="grid">
        <asp:GridView ID="HomeHoldGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"
            onrowdatabound="HomeHoldGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="HomeHoldGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="编号">
                <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房间号" DataField="RoomID" >
                <ItemStyle HorizontalAlign="Center" Width="45px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="UserName" >
                <ItemStyle HorizontalAlign="Center" Width="55px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="电话" DataField="Tel" >
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="联系地址" DataField="Contact" >
                <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="身份证" DataField="CardID" >
                <ItemStyle HorizontalAlign="Center" Width="115px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="房产证号" DataField="OwnerID" >
                <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="登录名" DataField="UID" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>                
                <asp:TemplateField HeaderText="选择删除">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
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
