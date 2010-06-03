<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">修改物业管理员信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">姓名：</div>
        <div class="info_text">
            <asp:TextBox ID="txtName" runat="server" ForeColor="#1A438E" MaxLength="20"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtName"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">性别：</div>
        <div class="info_text">
            <asp:Label ID="lbSex" runat="server"></asp:Label>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">身份证号码：</div>
        <div class="info_text">
            <asp:Label ID="lbID" runat="server"></asp:Label>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">工作安排：</div>
        <div class="info_text">
            <asp:TextBox ID="txtArrage" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtArrage"></asp:RequiredFieldValidator>            
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">联系电话：</div>
        <div class="info_text">
            <asp:TextBox ID="txtTel" runat="server" ForeColor="#1A438E"  MaxLength="13"></asp:TextBox>例如：0591-8298396
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="格式不正确" ControlToValidate="txtTel" Display="Dynamic" 
                ValidationExpression="(\d{3,4})-(\d{7,8})"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">手机：</div>
        <div class="info_text">
            <asp:TextBox ID="txtMobile" runat="server" ForeColor="#1A438E" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="请输入正确的手机号码" ControlToValidate="txtMobile" Display="Dynamic" 
                ValidationExpression="\d{11,12}"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">居住地址：</div>
        <div class="info_text">
            <asp:TextBox ID="txtAddress" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="employee_title">照片路径：</div>
        <div class="employee_text">
            <asp:Image ID="Photo" runat="server" Height="130px" Width="110px" 
                ImageUrl="~/Admin/UserHeadImages/Default.jpg" />
            <br />
            <asp:FileUpload ID="IMGFileUpload" runat="server" ForeColor="#1A438E" /><asp:Button ID="Upload" 
                runat="server" Text="上传" onclick="Upload_Click" CausesValidation="False" 
                ForeColor="#1A438E" />
        </div>
    </div>    
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Modify" runat="server" Text="修改" onclick="Modify_Click" 
                ForeColor="#1A438E" />            
        </div>
    </div>  
</asp:Content>
