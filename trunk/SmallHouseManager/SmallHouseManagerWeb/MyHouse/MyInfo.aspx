<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.MyInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>个人信息</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">基本信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">业主姓名：</div>
        <div class="info_text"><asp:Label ID="OwnerName" runat="server" ForeColor="Red"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="info_title">联系电话：</div>
        <div class="info_text"><asp:Label ID="Telephone" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">联系地址：</div>
        <div class="info_text"><asp:Label ID="Address" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">电子邮件：</div>
        <div class="info_text"><asp:Label ID="Email" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">房产证号：</div>
        <div class="info_text"><asp:Label ID="CardID" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">工作单位：</div>
        <div class="info_text"><asp:Label ID="WorkUnit" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">手机：</div>
        <div class="info_text"><asp:Label ID="Mobile" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">身份证号码：</div>
        <div class="info_text"><asp:Label ID="Identity" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">备注信息：</div>
        <div class="info_text"><asp:Label ID="Memo" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">登录系统的用户名</div>
    <div class="info_lines grid">
        <div class="info_title">用户名：</div>
        <div class="info_text"><asp:Label ID="UserName" runat="server"></asp:Label></div>
    </div>
</asp:Content>
