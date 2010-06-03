<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="SmallHouseManagerWeb.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>关于我们</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
    <div id="headimg"><div id="headtext">关于我们</div></div>
    <div id="aboutustext">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果你在生活中有什么问题及意见，可以及时和我们联系，我们会在最短的时间内处理您的问题，总之您的满意就是 我们奋斗的目标。让我们一道把我们的家园建设的更加美好和舒适。</div>
    <div id="aboutuscontact">
        <div class="basetext">负责人：<asp:Label ID="MainHead" runat="server" Text=""></asp:Label></div>
        <div class="basetext">电话：<asp:Label ID="Tel" runat="server" Text=""></asp:Label></div>
        <div class="basetext">地址：<asp:Label ID="Address" runat="server" Text=""></asp:Label></div>
    </div>
</asp:Content>
