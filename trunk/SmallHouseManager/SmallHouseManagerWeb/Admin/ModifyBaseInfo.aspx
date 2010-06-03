<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ModifyBaseInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.ModifyBaseInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑小区基本信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">小区名称：</div>
        <div class="info_text">
            <asp:TextBox ID="txtBaseName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtBaseName"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">主要负责人：</div>
        <div class="info_text">
            <asp:TextBox ID="txtMainHead" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtMainHead"></asp:RequiredFieldValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">建成日期：</div>
        <div class="info_text">
            <input id="txtBuildDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtBuildDate"></asp:RequiredFieldValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">建筑面积（亩）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtBuildArea" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtBuildArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtBuildArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">道路面积（亩）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtRoadArea" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtRoadArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtRoadArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">绿化面积（亩）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtGreenArea" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtGreenArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtGreenArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">停车场面积（亩）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtParkArea" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtParkArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtParkArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">楼宇数量（栋）：</div>
        <div class="info_text">
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtAmount" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtAmount" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">联系电话：</div>
        <div class="info_text">
            <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>如：0591－8323384
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtTelephone" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                ErrorMessage="格式不正确" ControlToValidate="txtTelephone" Display="Dynamic" 
                ValidationExpression="\d{3,4}-\d{7,8}"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">小区地址：</div>
        <div class="info_text">
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
        </div>
    </div>    
    <div class="info_lines grid reporttext">
        <div class="info_title reporttext">小区简介：</div>
        <div class="info_text">
            <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                TextMode="MultiLine" Width="364px"></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Modify" runat="server" Text="修改" onclick="Modify_Click" 
                ForeColor="#1A438E" /> 
            <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                ForeColor="#1A438E" CausesValidation="False" />            
        </div>
    </div>  
</asp:Content>
