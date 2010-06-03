<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChargeFreeType.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.ChargeFreeType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">物业费用类型</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <asp:GridView ID="ChargeFreeGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="20" ForeColor="#1A438E"  
            onrowdatabound="HomeFreeGridView_RowDataBound" Font-Size="12px">
            <Columns>
                <asp:BoundField HeaderText="费用名称" DataField="TypeName" >
                <ItemStyle HorizontalAlign="Center" Width="256px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单价" DataField="Price" >
                <ItemStyle HorizontalAlign="Center" Width="256px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="计量单位" DataField="Format" >
                <ItemStyle HorizontalAlign="Center" Width="256px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
