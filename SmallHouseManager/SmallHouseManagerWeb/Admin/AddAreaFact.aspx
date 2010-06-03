<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddAreaFact.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddAreaFact"  ValidateRequest="false"%>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区公用设施</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">公用设施名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtFactName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtFactName" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">主要负责人：</div>
        <div class="info_text">
            <asp:TextBox ID="txtMainHead" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtMainHead" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">设施类型：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLAreaType" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
            选择一种设施类型，如果没有选项，请单击此处
            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Red" NavigateUrl="~/admin/SystemCode.aspx">
                添加
            </asp:HyperLink>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">联系电话：</div>
        <div class="info_text">
            <asp:TextBox ID="txtTelphone" runat="server" ForeColor="#1A438E"></asp:TextBox>如：0591－8323384
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtTelphone" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                ErrorMessage="格式不正确" ControlToValidate="txtTelphone" Display="Dynamic" 
                ValidationExpression="\d{3,4}-\d{7,8}"></asp:RegularExpressionValidator>
        </div>
    </div>    
    <div class="info_lines grid Areafacttext">
        <div class="info_title Areafacttext">详细说明：（详细说明新建设施的用途等）</div>
        <div class="info_text"> 
            <FTB:FreeTextBox ID="ftbMemo" runat="server" 
                ImageGalleryPath="~/AdsImages/" 
                SupportFolder="~/Admin/aspnet_client/FreeTextBox/" 
                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Cut,Copy,Paste;Undo,Redo|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImageFromGallery,InsertRule" 
                Width="505px" Language="zh-cn">
            </FTB:FreeTextBox>           
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />      
        </div>
    </div>       
</asp:Content>
