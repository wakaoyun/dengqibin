<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BaseInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.BaseInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">小区基本信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="baseinfo_title">小区名称：</div>
        <div class="baseinfo_text"><asp:Label ID="BaseName" runat="server" Font-Bold="True"></asp:Label></div> 
        <div class="baseinfo_title1">联系电话：</div>
        <div class="baseinfo_text"><asp:Label ID="Telephone" runat="server"></asp:Label></div>       
    </div>
    <div class="info_lines grid">
        <div class="baseinfo_title">主要负责人：</div>
        <div class="baseinfo_text"><asp:Label ID="MainHead" runat="server"></asp:Label></div>
        <div class="baseinfo_title1">停车声面积：</div>
        <div class="baseinfo_text"><asp:Label ID="ParkArea" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="baseinfo_title">建成日期：</div>
        <div class="baseinfo_text"><asp:Label ID="BuildDate" runat="server"></asp:Label></div>
        <div class="baseinfo_title1">道路面积：</div>
        <div class="baseinfo_text"><asp:Label ID="RoadArea" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="baseinfo_title">建筑面积：</div>
        <div class="baseinfo_text"><asp:Label ID="BuildArea" runat="server"></asp:Label></div>
        <div class="baseinfo_title1">绿化面积：</div>
        <div class="baseinfo_text"><asp:Label ID="GreenArea" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="baseinfo_title">楼宇数量：</div>
        <div class="baseinfo_text"><asp:Label ID="Amount" runat="server"></asp:Label></div>
        <div class="baseinfo_title1">小区地址：</div>
        <div class="baseinfo_text"><asp:Label ID="Address" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="baseinfo_title">小区说明：</div>
        <div class="baseinfo_text1"><asp:Label ID="BaseMemo" runat="server"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="bottom_button">
            <asp:Button ID="Modify" runat="server" Text="修改" onclick="Modify_Click" />    
        </div>
         
    </div>    
</asp:Content>
