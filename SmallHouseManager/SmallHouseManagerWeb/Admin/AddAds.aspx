<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddAds.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddAds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加广告</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="ads_title">广告名：</div>
        <div class="ads_text">
            <asp:TextBox ID="txtName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </div>
    </div>    
     <div class="info_lines grid">
        <div class="ads_title">广告地址：</div>
        <div class="ads_text">
            <asp:TextBox ID="txtUrl" runat="server" ForeColor="#1A438E">http://</asp:TextBox>如：http://www.baidu.com
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtUrl" Display="Dynamic"></asp:RequiredFieldValidator>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="ads_title">广告图片路径（最好是524 × 145）：</div>
        <div class="ads_text">
            <asp:Image ID="Photo" runat="server" Height="145px" Width="524px" 
                ImageUrl="~/Admin/UserHeadImages/Default.jpg" />
            <asp:FileUpload ID="IMGFileUpload" runat="server" ForeColor="#1A438E" /><asp:Button ID="Upload" 
                runat="server" Text="上传" onclick="Upload_Click" CausesValidation="False" 
                ForeColor="#1A438E" />
        </div>
    </div>
    <div class="info_lines grid">
        <div class="ads_title"></div>
        <div class="ads_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />            
        </div>
    </div>  
</asp:Content>
