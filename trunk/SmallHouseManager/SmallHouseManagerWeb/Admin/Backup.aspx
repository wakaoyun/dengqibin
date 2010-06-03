<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">备份数据库</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">文件名：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFileName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFileName"></asp:RequiredFieldValidator>
        </div>
    </div> 
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="BackupData" runat="server" Text="备份数据库" onclick="BackupData_Click" ForeColor="#1A438E" />             
        </div>
    </div>       
</asp:Content>
