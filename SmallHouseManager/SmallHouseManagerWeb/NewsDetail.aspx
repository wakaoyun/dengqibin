<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="SmallHouseManagerWeb.UserControl.ShowNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区物业公告</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RightContent" runat="server">
    <div class="right">
        <div id="newtitle" class="newscenter"><%#news.Title%></div>
        <div><hr /></div>
        <div id="newdate" class="newscenter"><%#news.NoticeDate.ToString("yyyy年MM月dd日hh:mm")%></div>
        <div><hr /></div>
        <div id="newscontent"><%#news.NoticeContent%></div>
    </div>
</asp:Content>
