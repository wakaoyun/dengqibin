<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProcessReport.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.ProcessReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">您的投诉内容</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">报修内容：</div>
        <div class="repair_text"><asp:Label ID="lbReportText" runat="server"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">报修时间：</div>
        <div class="repair_text"><asp:Label ID="lbReportDate" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">报修说明：</div>
        <div class="repair_text"><asp:Label ID="lbReportMemo" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">报修处理</div>
    <div class="info_lines grid reporttext">
        <div class="repair_title">处理说明：</div>
        <div class="repair_text reporttext"><asp:TextBox ID="txtProcessResult" runat="server" 
                Height="131px" TextMode="MultiLine" Width="364px" ForeColor="#1A438E"></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title"></div>
        <div class="repair_text">
            <asp:Button ID="Submit" runat="server" Text="提交" onclick="Submit_Click" 
                ForeColor="#1A438E" />
            <asp:Button ID="Cancel" runat="server" Text="返回" onclick="Cancel_Click" 
                ForeColor="#1A438E" />
        </div>
    </div>
</asp:Content>
