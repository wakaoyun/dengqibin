<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditBasePark.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditBasePark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区停车场</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">停车场名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtParkName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtParkName" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">停车位数量：</div>
        <div class="info_text">
            <asp:TextBox ID="txtParkAmount" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtParkAmount" Display="Dynamic"></asp:RequiredFieldValidator> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                    ErrorMessage="只能是数字" ControlToValidate="txtParkAmount" Display="Dynamic" 
                    ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>           
        </div>
    </div>    
    <div class="info_lines grid reporttext">
        <div class="info_title reporttext">详细说明</div>
        <div class="info_text">
            <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                TextMode="MultiLine" Width="364px" ForeColor="#1A438E"></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Modify" runat="server" Text="修改" onclick="Modify_Click" 
                ForeColor="#1A438E" />
            <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                ForeColor="#1A438E" CausesValidation="False" />
        </div>
    </div>    
</asp:Content>
