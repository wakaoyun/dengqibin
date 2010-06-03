<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PavilionInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.PavilionInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">小区楼宇管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
        <asp:CheckBox ID="chkPaName" runat="server" Text="楼宇名称" Width="70px" />
        <asp:TextBox ID="txtPaName" runat="server" ForeColor="#1A438E" Width="110px"></asp:TextBox>
        <asp:CheckBox ID="chkPaLayer" runat="server" Text="楼宇层数" Width="70px" />
        <asp:TextBox ID="txtPavLayer" runat="server" ForeColor="#1A438E" Width="110px"></asp:TextBox>
        <asp:CheckBox ID="chkPaBuildDate" runat="server" Text="建成日期从" Width="80px" />
        <input id="txtBeginDate" runat="server" onclick="calendar()" 
            onfocus="calendar()" readonly="readonly" style="color: #1A438E" type="text"/>到
        <input id="txtEndDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E"/>
    </div>
    <div class="grid">
        <asp:CheckBox ID="chkType" runat="server" Text="楼宇类型" Width="70px" />
        <asp:DropDownList ID="DDLPaType" runat="server" ForeColor="#1A438E" 
            Width="110px" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
        <asp:CheckBox ID="chkPaHeight" runat="server" Text="楼宇高度" Width="70px" />
        <asp:TextBox ID="txtPaHeight" runat="server" ForeColor="#1A438E" Width="110px"></asp:TextBox>
        <asp:CheckBox ID="chkPaArea" runat="server" Text="楼宇面积" Width="80px" />
        <asp:TextBox ID="txtPaArea" runat="server" ForeColor="#1A438E" Width="110px"></asp:TextBox>
        <asp:CheckBox ID="chkExact" runat="server" Text="精确查找" Width="70px" />
        <asp:Button ID="Search" runat="server" Text="查询" ForeColor="#1A438E" 
            onclick="Search_Click" />
        <asp:Button ID="Show" runat="server" Text="显示全部" ForeColor="#1A438E" 
            onclick="Show_Click" />
    </div>
    <div class="grid">
        <asp:GridView ID="PavilionGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="15" ForeColor="#1A438E"  
            onrowediting="PavilionView_RowEditing"
            onrowdatabound="PavilionView_RowDataBound"
            DataKeyNames="PaID" Font-Size="12px" 
            onrowdeleting="PavilionGridView_RowDeleting" 
            onpageindexchanging="PavilionGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="楼宇名称" DataField="Name" >
                <ItemStyle Width="112px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="楼宇类型" DataField="TypeName" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="楼宇层数（层）" DataField="Layer" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="楼宇高度（米）" DataField="Height" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="楼宇面积（亩）" DataField="Area" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="建成日期" DataField="BuildDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:CommandField EditText="详细|编辑" HeaderText="详细|编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="75px" />
                </asp:CommandField>
                <asp:CommandField DeleteText="删除" HeaderText=" 删除" ShowDeleteButton="True">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
