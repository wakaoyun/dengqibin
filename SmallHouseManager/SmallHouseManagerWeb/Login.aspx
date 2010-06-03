<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="Css/Login.css" rel="stylesheet" type="text/css" />    

    <script language="javascript" type="text/javascript">
<!--

        function Password_onclick() {

        }

// -->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="login">
        <div id="content">
            <div class="row">
                <div class="name">用户名：</div>
                <div class="text">
                    <input id="UserName" type="text" class="text" maxlength="20"  runat="server"/></div>
                <div class="check">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="UserName" CssClass="check" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="name">密&nbsp;&nbsp;码：</div>                
                <div class="text">
                    <input id="Password" type="password" class="text" maxlength="20"  
                        runat="server" onclick="return Password_onclick()" /></div>
                <div class="check">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="Password" CssClass="check" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="name">验证码：</div>
                <div class="text checkcode">
                    <input id="Code" type="text" class="checkcode" maxlength="4"  runat="server"/></div>
                <div id="code" class="checkcode"><img alt="验证码" src="CheckCode.aspx" /></div>
                <div class="check">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="Code" CssClass="check" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div id="confirm">
                    <asp:ImageButton ID="Submit" runat="server" 
                        ImageUrl="~/Images/confirm_buttom.gif" onclick="Submit_Click"/></div>
                <div id="cancel">
                    <asp:ImageButton ID="Cancel" runat="server" 
                        ImageUrl="~/Images/cancel_buttom.gif" onclick="Cancel_Click" 
                        CausesValidation="False" /></div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
