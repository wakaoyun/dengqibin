<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BaseParkInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.BaseParkInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑小区停车场</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:GridView ID="ParkGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="NewsGridView_RowEditing"
            onrowdatabound="NewsGridView_RowDataBound"
            DataKeyNames="ParkID" Font-Size="12px" 
            onpageindexchanging="NewsGridView_PageIndexChanging" 
            onrowdeleting="ParkGridView_RowDeleting" >
            <Columns>
                <asp:BoundField HeaderText="停车场名称" DataField="Name" >
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="停车位数量" DataField="Amount" >
                <ItemStyle HorizontalAlign="Center" Width="130px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="停车场说明" DataField="Memo" >
                <ItemStyle HorizontalAlign="Center" Width="310px" />
                </asp:BoundField>
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="85px" />
                </asp:CommandField>
                <asp:CommandField DeleteText="删除" HeaderText="删除" ShowDeleteButton="True">
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
