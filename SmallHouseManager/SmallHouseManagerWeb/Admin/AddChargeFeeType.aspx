<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddChargeFeeType.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddChargeFeeType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加收费类型</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">    
    <div class="info_lines grid">
        <div class="info_title">费用名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFeeName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFeeName"></asp:RequiredFieldValidator>
        </div>
    </div>    
     <div class="info_lines grid">
        <div class="info_title">单价：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPrice" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPrice" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtPrice" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">计量单位：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFormat" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFormat" Display="Dynamic"></asp:RequiredFieldValidator>            
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
