<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModifyUser.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.ModifyUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">修改我的登录账号名</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">原用户名：</div>
        <div class="info_text"><asp:Label ID="UserName" runat="server" ForeColor="Red"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="info_title">新用户名：</div>
        <div class="info_text"><asp:TextBox ID="NewUserName" runat="server" 
                ForeColor="#1A438E" MaxLength="20" Width="89px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="不能为空" 
                ControlToValidate="NewUserName"></asp:RequiredFieldValidator></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="CheckUser" runat="server" Text="检测用户名" ForeColor="#1A438E" 
                onclick="CheckUser_Click" />
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Submit" runat="server" Text="修改" ForeColor="#1A438E" 
                onclick="Submit_Click" />
            <asp:Button ID="Button2" runat="server" Text="取消" ForeColor="#1A438E" 
                onclick="Button2_Click" CausesValidation="False" />
        </div>
        
    </div>
</asp:Content>
