<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="SmallHouseManagerWeb.UserControl.Login" %>
<asp:Panel ID="Login_Pannel" runat="server">
    <div id="login" class="loginning">
        <div class="row">
            <div class="name">用户名：</div>
            <div class="text">
                <asp:TextBox ID="UserName" runat="server" MaxLength="20" 
                    Width="88px"></asp:TextBox></div>
            <div class="check"><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="不能为空" ControlToValidate="UserName" 
                    Height="23px"></asp:RequiredFieldValidator></div>
        </div>
        <div class="row">
            <div class="name">密&nbsp;&nbsp;码：</div>
            <div class="text">
                <asp:TextBox ID="Password" runat="server" MaxLength="20" 
                    TextMode="Password" Width="88px"></asp:TextBox></div>
            <div class="check">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="Password" ErrorMessage="不能为空" Height="23px"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="name">验证码：</div>
            <div class="text checkcode">
                <asp:TextBox ID="Code" runat="server" MaxLength="4" 
                    Width="36px"></asp:TextBox></div>
            <div id="code" class="checkcode"><img alt="验证码" src="CheckCode.aspx" /></div>
            <div class="check">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="Code" ErrorMessage="不能为空" Height="23px"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div id="confirm"><asp:ImageButton ID="Submit" runat="server" 
                ImageUrl="~/Images/confirm_buttom.gif" onclick="Submit_Click"/></div>                        
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="Loginned_Pannel" runat="server" Visible="False">
    <div id="loginned" class="loginning">
        <div id="login_line">
            <div class="row welcome">欢迎您：<asp:Label ID="MyUserName" runat="server" ForeColor="Red"></asp:Label></div>
            <div class="row welcome"><asp:LinkButton ID="Url" runat="server">进入管理页面</asp:LinkButton></div>
            <div class="row welcome">
                <asp:ImageButton ID="QuitButton" runat="server" ImageUrl="~/Images/logout_buttom.gif" PostBackUrl="~/Exit.aspx"/>
            </div>
        </div>
    </div>
</asp:Panel>