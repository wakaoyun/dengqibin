<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区物业管理员</div>
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
            <asp:RadioButtonList ID="RBLSex" runat="server" 
                RepeatDirection="Horizontal" Font-Size="12px" ForeColor="#1A438E" >
                <asp:ListItem Selected="True" Value="0">男</asp:ListItem>
                <asp:ListItem Value="1">女</asp:ListItem>
            </asp:RadioButtonList>    
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">身份证号码：</div>
        <div class="info_text">
            <asp:TextBox ID="txtID" runat="server" ForeColor="#1A438E" MaxLength="18"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtID" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ErrorMessage="请输入正确的身份证号码" ControlToValidate="txtID" Display="Dynamic" 
                ValidationExpression="\d{17}[\d|X]|\d{15}"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">工作安排：</div>
        <div class="info_text">
            <asp:TextBox ID="txtArrage" runat="server" ForeColor="#1A438E" MaxLength="18"></asp:TextBox>
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
    <div class="info_lines grid titles">以下是住户用户名和密码（<asp:Label ID="Label1" 
         runat="server" Text="默认都为身份证号" ForeColor="Red"></asp:Label>）
    </div> 
    <div class="info_lines grid">
        <div class="info_title">用户名：</div>
        <div class="info_text">
            <asp:TextBox ID="txtUID" runat="server" ForeColor="#1A438E" MaxLength="20"></asp:TextBox>            
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">密码：</div>
        <div class="info_text">
            <asp:TextBox ID="txtPassword" runat="server" ForeColor="#1A438E" 
                TextMode="Password" MaxLength="20"></asp:TextBox>            
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
