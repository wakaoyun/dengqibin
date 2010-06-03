<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="SmallHouseManagerWeb.EmployeeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区物管人员</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div id="headimg"><div id="headtext">物管人员</div></div>
        <div id="employee">
            <asp:DataList ID="EmployeeDataList" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                <ItemTemplate>
                    <div>
                        <div class="employeecontent">
                            <asp:Image ID="EmployeePhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' CssClass="employeeimg1"/>
                        </div>                                             
                        <div class="employeecontent">
                            <div class="employeeitem">姓名：<%#Eval("EmlpoyeeName")%></div>
                            <div class="employeeitem">性别：<%#Convert.ToInt32(Eval("Sex")) == 0 ? "男" : "女"%></div>
                            <div class="employeeitem">职务：<%#Eval("Arrage")%></div>
                            <div class="employeeitem">联系电话：<%#Eval("Tel")%></div>
                            <div class="employeeitem">手机：<%#Eval("Mobile")%></div>
                        </div>
                        <div class="dashline"></div>                                                 
                    </div>                    
                </ItemTemplate>               
            </asp:DataList>
                <div id="navigatecount">
                    共<asp:Label ID="lbPage" runat="server" ForeColor="Red"></asp:Label>页 当前第
                    <asp:Label ID="lbCurrentPage" runat="server" ForeColor="Red"></asp:Label>页
                </div>
                <div id="navigate">
                    <asp:LinkButton ID="FirstPage" runat="server" CausesValidation="False" OnClick="FirstPage_Click">首页</asp:LinkButton>
                    <asp:LinkButton ID="PriorPage" runat="server" CausesValidation="False" OnClick="PriorPage_Click">上一页</asp:LinkButton>
                    <asp:LinkButton ID="NextPage" runat="server" CausesValidation="False" OnClick="NextPage_Click">下一页</asp:LinkButton>
                    <asp:LinkButton ID="LastPage" runat="server" CausesValidation="False" OnClick="LastPage_Click">尾页</asp:LinkButton>
                </div>    
        </div>    
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>    
</asp:Content>
