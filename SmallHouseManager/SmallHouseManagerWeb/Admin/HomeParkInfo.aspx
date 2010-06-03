<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HomeParkInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.HomeParkInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">查看住户停车位</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">    
    <div class="grid">
        <asp:GridView ID="HomeParkGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"
            onrowdatabound="HomeParkGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="HomeParkGridView_PageIndexChanging" 
            onrowediting="HomeParkGridView_RowEditing">
            <Columns>
                <asp:BoundField HeaderText="房间号" DataField="RoomID" >
                <ItemStyle Width="65px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="停车场名称" DataField="ParkName" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>                
                <asp:BoundField HeaderText="车类型" DataField="Type" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="车牌号码" DataField="CarID" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="颜色" DataField="Color" >
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="购买日期" DataField="BuyDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>             
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="85px" />
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
