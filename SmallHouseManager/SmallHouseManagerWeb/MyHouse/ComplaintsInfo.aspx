<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ComplaintsInfo.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.UnDisposeComplaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">您的投诉内容</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">投诉内容：</div>
        <div class="repair_text"><asp:Label ID="lbReportText" runat="server"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">投诉时间：</div>
        <div class="repair_text"><asp:Label ID="lbReportDate" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">投诉说明：</div>
        <div class="repair_text"><asp:Label ID="lbReportMemo" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">以下是我们的处理结果</div>
    <div class="info_lines grid">
        <div class="repair_title">处理结果：</div>
        <div class="repair_text"><asp:Label ID="lbProcessResult" runat="server"></asp:Label></div>
    </div>
    <div id="homereport" class="info_lines grid">如果您好对以上的处理意见有何异议，请反馈给我们，以便我们能更好的为您好服务，现在就<a href="AddComplaints.aspx">提交</a></div>
</asp:Content>
