<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Restor.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.Restor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">恢复数据库</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">选择备份文件：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLFileName" runat="server" ForeColor="#1A438E" 
                    Width="145px">
            </asp:DropDownList> 
        </div>
    </div> 
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="RestorData" runat="server" Text="恢复数据库" onclick="RestorData_Click" ForeColor="#1A438E" />             
        </div>
    </div>       
</asp:Content>
