<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ChargeFeeInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.ChargeFeeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">查看住户费用</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">   
    <div class="grid">
        <asp:GridView ID="HomeFreeGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"
            onrowdatabound="HomeFreeGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onpageindexchanging="HomeFreeGridView_PageIndexChanging" 
            onselectedindexchanging="HomeFreeGridView_SelectedIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="房间号" DataField="RoomID" >
                <ItemStyle Width="65px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="费用名称" DataField="TypeName" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="计费日期" DataField="StartDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="应付金额（元）" DataField="PayMent" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="未付金额（元）" DataField="NotPayMent" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="收费人" DataField="HandleName" NullDisplayText="未交费" >
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="登记人" DataField="AddName" >
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:BoundField>                
                <asp:CommandField ButtonType="Button" HeaderText="交费" SelectText="交费" 
                    ShowSelectButton="True">
                <ControlStyle ForeColor="#1A438E" />
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
