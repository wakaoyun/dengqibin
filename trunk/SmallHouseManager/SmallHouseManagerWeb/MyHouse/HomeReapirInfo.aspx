<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HomeReapirInfo.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.HomeReapirInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">您的报修内容</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">报修内容：</div>
        <div class="repair_text"><asp:Label ID="lbRepairText" runat="server"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">报修时间：</div>
        <div class="repair_text"><asp:Label ID="lbRepairDate" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">报修说明：</div>
        <div class="repair_text"><asp:Label ID="lbRepairMemo" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">小区物业的处理</div>
    <div class="info_lines grid">
        <div class="repair_title">处理人：</div>
        <div class="repair_text"><asp:Label ID="lbProcessName" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">维修单位：</div>
        <div class="repair_text"><asp:Label ID="lbRepairUnit" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">处理结果说明：</div>
        <div class="repair_text"><asp:Label ID="lbProcessResult" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">以下是您的验收意见</div>
    <div class="info_lines grid reporttext">
        <div class="repair_title">验收意见：</div>
        <div class="repair_text reporttext"><asp:TextBox ID="txtCheckResult" runat="server" 
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
