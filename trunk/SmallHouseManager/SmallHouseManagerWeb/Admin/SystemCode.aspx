<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="SystemCode.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.SystemCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">系统相关参数维护</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid bottom_button">
        请选择参数类别：
        <asp:DropDownList ID="DDLType" runat="server" ForeColor="#1A438E" 
            Width="145px" DataTextField="TypeName" DataValueField="ID" 
            AutoPostBack="True" onselectedindexchanged="DDLPa_SelectedIndexChanged">
        </asp:DropDownList>         
    </div>
    <div class="grid bottom_button">
        <asp:GridView ID="CodeGridView" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#1A438E"  
                onrowdatabound="RoomGridView_RowDataBound"
                Font-Size="12px">
                <Columns>
                    <asp:BoundField HeaderText="编号" DataField="ID" >
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="参数名称" DataField="Name" >
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="参数类别" DataField="TypeName" >
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>                             
                </Columns>
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
    </div>    
    <div class="grid bottom_button">
        参数名称：
        <asp:TextBox ID="txtName" runat="server" ForeColor="#1A438E"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ErrorMessage="不能为空" ControlToValidate="txtName"></asp:RequiredFieldValidator>        
    </div>
    <div class="grid bottom_button"><asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" ForeColor="#1A438E" /></div>
</asp:Content>
