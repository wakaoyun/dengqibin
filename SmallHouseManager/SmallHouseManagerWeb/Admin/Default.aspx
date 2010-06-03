<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理首页</title>  
    <frameset border="0" frameSpacing="0" rows="61,*" frameBorder="no" cols="*">
            <frame id="topFrame" title="topFrame" name="topFrame" src="top.htm" scrolling="no">
            <frameset border="0" frameSpacing="0" rows="*" frameBorder="no" cols="177,*">
                <frame name="left" src="menu.htm" noResize scrolling="yes">
                <frame name="right" src="BaseInfo.aspx" noResize scrolling="yes">
            </frameset>
    </frameset>
    <noframes></noframes>     
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    </div>
    </form>
</body>
</html>
