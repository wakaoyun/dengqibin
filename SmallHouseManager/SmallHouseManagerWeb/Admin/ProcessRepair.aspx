<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProcessRepair.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.ProcessRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">您的报修内容</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">投诉内容：</div>
        <div class="repair_text"><asp:Label ID="lbRepairText" runat="server"></asp:Label></div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">投诉时间：</div>
        <div class="repair_text"><asp:Label ID="lbRepairDate" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title">投诉说明：</div>
        <div class="repair_text"><asp:Label ID="lbRepairMemo" runat="server"></asp:Label></div>
    </div>
    <div class="info_lines grid titles">投诉处理</div>
    <div class="info_lines grid">
        <div class="repair_title">维修单位：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtRepairUnit" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtRepairUnit" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
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