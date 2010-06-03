<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddPavilion.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddPavilion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区楼宇</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">楼宇名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPavilionName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPavilionName"></asp:RequiredFieldValidator>
        </div>
    </div>     
     <div class="info_lines grid">
        <div class="info_title">楼宇层数：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPavilionLayer" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPavilionLayer" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtPavilionLayer" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">大楼高度（米）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPavilionHeight" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPavilionHeight" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtPavilionHeight" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">大楼面积（亩）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPavilionArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtPavilionArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtPavilionArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">建成日期：</div>
        <div class="info_text">
            <input id="txtBuildDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtBuildDate"></asp:RequiredFieldValidator>
        </div>
    </div>    
    <div class="info_lines grid">
        <div class="info_title">楼宇类型：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLPavilionType" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
            选择一种楼宇类型，如果没有选项，请单击此处
            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Red" NavigateUrl="~/admin/SystemCode.aspx">
                添加
            </asp:HyperLink>
        </div>
    </div>    
    <div class="info_lines grid reporttext">
        <div class="info_title reporttext">备注说明：</div>
        <div class="info_text">
            <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                TextMode="MultiLine" Width="364px" ForeColor="#1A438E" ></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />            
        </div>
    </div>  
</asp:Content>
