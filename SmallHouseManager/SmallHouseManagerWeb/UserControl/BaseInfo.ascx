<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BaseInfo.ascx.cs" Inherits="SmallHouseManagerWeb.UserControl.BaseInfo" %>
<div>
    <div id="information"></div>
    <div class="basetext">小区名称：<asp:Label ID="BaseName" runat="server" Text=""></asp:Label></div>
    <div class="basetext">楼宇数量：<asp:Label ID="Amount" runat="server" Text=""></asp:Label>栋</div>
    <div class="basetext">建筑面积：<asp:Label ID="BuildArea" runat="server" Text=""></asp:Label>亩</div>
    <div class="basetext">绿化面积：<asp:Label ID="GreenArea" runat="server" Text=""></asp:Label>亩</div>
    <div class="basetext">道路面积：<asp:Label ID="RoadArea" runat="server" Text=""></asp:Label>亩</div>
</div>
<div>
    <div id="contact"></div>
    <div class="basetext">负责人：<asp:Label ID="MainHead" runat="server" Text=""></asp:Label></div>
    <div class="basetext">电话：<asp:Label ID="Tel" runat="server" Text=""></asp:Label></div>
    <div class="basetext">地址：<asp:Label ID="Address" runat="server" Text=""></asp:Label></div>
</div>