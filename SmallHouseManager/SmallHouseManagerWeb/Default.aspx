<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmallHouseManagerWeb.Default" Title="Untitled Page" %>
<%@ Register src="UserControl/Ad.ascx" tagname="Ad" tagprefix="uc1" %>
<%@ Register src="UserControl/News.ascx" tagname="News" tagprefix="uc2" %>
<%@ Register src="UserControl/AdminInfo.ascx" tagname="AdminInfo" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区物业管理首页</title> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">    
    <uc1:Ad ID="Ad1" runat="server" />     
    <uc2:News ID="News1" runat="server" />
    <uc3:AdminInfo ID="AdminInfo1" runat="server" />   
</asp:Content>
