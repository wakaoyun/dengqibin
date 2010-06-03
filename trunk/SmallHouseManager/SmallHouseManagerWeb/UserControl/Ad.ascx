<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ad.ascx.cs" Inherits="SmallHouseManagerWeb.UserControl.Ad" %>
<div id="headimg"><div id="headtext">图片展示</div></div>
<div id="flash"><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"  width="524" height="145">  
        <param name="allowScriptAccess" value="sameDomain"/>                  
        <param name="movie" value="Flash\focus.swf" />
        <param name="quality" value="high"/>
        <param name="bgcolor" value="#FFFFFF"/>
        <param name="menu" value="false"/>
        <param name="wmode" value="opaque"/>
        <param name="FlashVars" value="pics=<%#pic %>&links=<%#url %>&borderwidth=524&borderheight=145"/>
        <embed src="Flash/focus.swf"  wmode="opaque" flashvars="pics=<%#pic %>&links=<%#url %>&borderwidth=524&borderheight=145" quality="high" bgcolor="#FFFFFF" width="524" height="145" allowscriptaccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
     </object>
</div>