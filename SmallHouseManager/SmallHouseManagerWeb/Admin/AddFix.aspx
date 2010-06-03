<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddFix.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddFix" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区设备</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">设备编号：</div>
        <div class="info_text"><asp:Label ID="lbID" runat="server" ForeColor="Red"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">设备名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFixName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFixName"></asp:RequiredFieldValidator>
        </div>
    </div>    
     <div class="info_lines grid">
        <div class="info_title">设备数量：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFixAmount" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFixAmount" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtFixAmount" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">生产厂家：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFixFactory" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFixFactory" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">出厂日期：</div>
        <div class="info_text">
            <input id="txtFixFactoryDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFixFactoryDate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" ForeColor="#1A438E" /> 
            <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" ForeColor="#1A438E" />
        </div>
    </div>       
</asp:Content>
