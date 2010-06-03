<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminInfo.ascx.cs" Inherits="SmallHouseManagerWeb.UserControl.AdminInfo" %>
<div id="adminimg"></div>
<div id="RollPic">
    <div id="RollContent">
        <div id="RollPic1">
            <asp:DataList ID="EmployeeDataList" runat="server" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <div>
                        <a href="EmployeeInfo.aspx"><asp:Image ID="EmployeePhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' CssClass="employeeimg"/></a> 
                    </div>
                    <div>
                        <div class="employeetext">姓名：<%#Eval("EmlpoyeeName")%></div>
                        <div class="employeetext">性别：<%#Convert.ToInt32(Eval("Sex")) == 0 ? "男" : "女"%></div>
                        <div class="employeetext">职务：<%#Eval("Arrage")%></div>
                        <div class="employeetext">电话：<%#Eval("Tel")%></div>
                    </div>
                </ItemTemplate>
            </asp:DataList> 
        </div>
        <div id="RollPic2"></div>
    </div>
</div>
<script type="text/javascript"> 
<!--
    var Dspeedt = 30//速度数值越大速度越慢
    var Ddemo2t = document.getElementById("RollPic2");
    var Ddemot = document.getElementById("RollPic");
    var Ddemo1t = document.getElementById("RollPic1");
    Ddemo2t.innerHTML = Ddemo1t.innerHTML;
    function DMarqueet() {
        if (Ddemo1t.offsetWidth - Ddemot.scrollLeft <= 0)
            Ddemot.scrollLeft -= Ddemo1t.offsetWidth;
        else {
            Ddemot.scrollLeft++;
        }
    }
    var DMyMart = setInterval(DMarqueet, Dspeedt)
    Ddemot.onmouseover = function() { clearInterval(DMyMart) }
    Ddemot.onmouseout = function() { DMyMart = setInterval(DMarqueet, Dspeedt) } 
--> 
</script> 