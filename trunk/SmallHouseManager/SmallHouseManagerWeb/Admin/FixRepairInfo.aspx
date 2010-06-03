<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FixRepairInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.FixRepairInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑维修设备</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkFixName" runat="server" Text="设备名称" Width="80px" />
        <asp:TextBox ID="txtFixName" runat="server" ForeColor="#1A438E" Width="130px"></asp:TextBox>
        <asp:CheckBox ID="chkMainHead" runat="server" Text="负责人" Width="70px" />
        <asp:TextBox ID="txtMainHead" runat="server" ForeColor="#1A438E" Width="130px"></asp:TextBox>
        <asp:CheckBox ID="chkFixID" runat="server" Text="设备编号" Width="70px" />
        <asp:TextBox ID="txtFixID" runat="server" ForeColor="#1A438E" Width="130px"></asp:TextBox>
        <asp:CheckBox ID="chkSign" runat="server" Text="是否付款" Width="80px" />
        <asp:DropDownList ID="DDLSign" runat="server" Height="16px" Width="62px">
            <asp:ListItem Selected="True" Value="0">已付</asp:ListItem>
            <asp:ListItem Value="1">未付</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkUnit" runat="server" Text="维修单位" Width="80px" />
        <asp:TextBox ID="txtUnit" runat="server" ForeColor="#1A438E" Width="130px"></asp:TextBox>
        <asp:CheckBox ID="chkFee" runat="server" Text="维修费用" Width="70px" />
        <asp:TextBox ID="txtFee" runat="server" ForeColor="#1A438E" Width="130px"></asp:TextBox>
        <asp:CheckBox ID="chkExact" runat="server" Text="精确查找" Width="80px" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="FixRepairGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="FixRepairGridView_RowEditing"
            onrowdatabound="FixRepairGridView_RowDataBound"
            DataKeyNames="ID" Font-Size="12px" 
            onselectedindexchanging="FixRepairGridView_SelectedIndexChanging" 
            onpageindexchanging="FixRepairGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="设备编号" DataField="FixID" >
                <ItemStyle Width="125px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="设备名称" DataField="FixName" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="维修日期" DataField="RepairDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="负责人" DataField="MainHead" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="维修费用" DataField="RepairSum" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否付款" DataField="Sign" >
                <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="维修单位" DataField="RepairUnit" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Button" HeaderText="付款" SelectText="付款" 
                    ShowSelectButton="True">
                <ControlStyle ForeColor="#1A438E" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:CommandField>
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
