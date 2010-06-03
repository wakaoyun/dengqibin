<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModifyPWD.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.ModifyPWD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">修改我的登录密码</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">原始密码：</div>
        <div class="info_text">
            <asp:TextBox ID="OriginalPassword" runat="server" 
                MaxLength="20" ForeColor="#1A438E" Width="70px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="OriginalPassword"></asp:RequiredFieldValidator>
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="info_title">新密码：</div>
        <div class="info_text">
            <asp:TextBox ID="NewPassword" runat="server" MaxLength="20" 
                ForeColor="#1A438E" Width="70px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="NewPassword"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">重复密码：</div>
        <div class="info_text">
            <asp:TextBox ID="RepeatPassword" runat="server" 
                MaxLength="20" ForeColor="#1A438E" TextMode="Password" Width="70px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" Display="Dynamic" ControlToValidate="RepeatPassword"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="两次输入不一致" ControlToCompare="NewPassword" 
                ControlToValidate="RepeatPassword" Display="Dynamic"></asp:CompareValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Submit" runat="server" Text="确认" 
                ForeColor="#1A438E" onclick="Submit_Click" />
        </div>        
    </div>
</asp:Content>
