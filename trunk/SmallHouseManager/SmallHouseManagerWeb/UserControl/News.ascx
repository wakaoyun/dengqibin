<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="News.ascx.cs" Inherits="SmallHouseManagerWeb.UserControl.News" %>
<div id="newsimg"></div>
<div>    
    <asp:Repeater ID="NewsRepeater" runat="server">
        <ItemTemplate>
            <div id="newstitle">●  <a href="NewsDetail.aspx?ID=<%#Eval("ID") %>"><%#Eval("Title") %></a></div>
            <div id="newsdate"><%#Eval("NoticeDate","{0:(yy-MM-dd)}") %></div>            
        </ItemTemplate>        
    </asp:Repeater>
</div>

