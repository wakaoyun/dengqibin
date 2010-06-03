<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HomeRepairInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.HomeRepairInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">户主报修处理管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:GridView ID="HomeRepairGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="20" ForeColor="#1A438E"  
            onrowdatabound="HomeRepairGridView_RowDataBound" 
            onrowediting="HomeRepairGridView_RowEditing"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="HomeRepairGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="报修编号" DataField="ID" >
                <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="报修房间号" DataField="RoomID" >
                <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="报修日期" DataField="RepairDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="报修内容" DataField="RepairText" >
                <ItemStyle HorizontalAlign="Center" Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否处理" DataField="Sign" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:CommandField EditText="报修处理" HeaderText="投诉处理" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
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
