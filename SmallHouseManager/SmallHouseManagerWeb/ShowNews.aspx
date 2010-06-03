<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShowNews.aspx.cs" Inherits="SmallHouseManagerWeb.ShowNews" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区物业公告</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="headimg"><div id="headtext">物业公告</div></div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <asp:Repeater ID="NewsRepeater" runat="server">
                <ItemTemplate>
                    <div id="newstitle">●  <a href="NewsDetail.aspx?ID=<%#Eval("ID") %>"><%#Eval("Title") %></a></div>
                    <div id="newsdate"><%#Eval("NoticeDate","{0:(yy-MM-dd)}") %></div>            
                </ItemTemplate>                      
            </asp:Repeater>
            <div id="navigatecount">
                共<asp:Label ID="lbPage" runat="server" ForeColor="Red"></asp:Label>页 当前第
                <asp:Label ID="lbCurrentPage" runat="server" ForeColor="Red"></asp:Label>页
            </div>
            <div id="navigate">
                <asp:LinkButton ID="FirstPage" runat="server" CausesValidation="False" OnClick="FirstPage_Click">首页</asp:LinkButton>
                <asp:LinkButton ID="PriorPage" runat="server" CausesValidation="False" OnClick="PriorPage_Click">上一页</asp:LinkButton>
                <asp:LinkButton ID="NextPage" runat="server" CausesValidation="False" OnClick="NextPage_Click">下一页</asp:LinkButton>
                <asp:LinkButton ID="LastPage" runat="server" CausesValidation="False" OnClick="LastPage_Click">尾页</asp:LinkButton>
            </div>
       </ContentTemplate>
        
    </asp:UpdatePanel>
</asp:Content>
