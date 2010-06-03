<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditChargeFeeType.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditChargeFeeType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">修改收费类型</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">    
    <div class="info_lines grid">
        <div class="info_title">费用名称：</div>
        <div class="info_text"><asp:Label ID="lbName" runat="server" ></asp:Label></div>
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
        <div class="info_text"><asp:Label ID="lbFormat" runat="server" ></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="修改" onclick="Add_Click" ForeColor="#1A438E" /> 
            <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                ForeColor="#1A438E" CausesValidation="False" />
        </div>
    </div>      
</asp:Content>
