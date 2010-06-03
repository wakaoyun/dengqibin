<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddNews"  ValidateRequest="false"%>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区物业公告</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">公告名称：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtNoticeTitle" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtNoticeTitle" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">公告内容：</div>
        <div class="repair_text">
            <FTB:FreeTextBox ID="ftbContent" runat="server" 
                ImageGalleryPath="~/AdsImages/" 
                SupportFolder="~/Admin/aspnet_client/FreeTextBox/" 
                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Cut,Copy,Paste;Undo,Redo|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImageFromGallery,InsertRule" 
                Width="685px" Language="zh-cn" Height="335px">
            </FTB:FreeTextBox>           
        </div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title"></div>
        <div class="repair_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />      
        </div>
    </div>       
</asp:Content>
