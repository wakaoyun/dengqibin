<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddComplaints.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.AddComplaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加投诉</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">投诉编号：</div>
        <div class="info_text"><asp:Label ID="lbID" runat="server" ForeColor="Red"></asp:Label></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">投诉内容：</div>
        <div class="info_text">
            <asp:TextBox ID="txtTitle" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
        </div>
    </div>    
    <div class="info_lines grid reporttext">
        <div class="info_title reporttext">详细说明：（详细说明投诉的原因便于处理）</div>
        <div class="info_text">
            <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                TextMode="MultiLine" Width="364px" ForeColor="#1A438E"></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />
            <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                ForeColor="#1A438E" />
        </div>
    </div>
</asp:Content>
