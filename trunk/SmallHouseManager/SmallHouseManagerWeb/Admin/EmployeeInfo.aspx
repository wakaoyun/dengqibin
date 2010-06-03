<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EmployeeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑物业管理人员</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
            <asp:DataList ID="EmployeeDataList" runat="server" RepeatDirection="Horizontal" 
                RepeatColumns="3" ForeColor="#1A438E" Font-Size="12px" 
                ondeletecommand="EmployeeDataList_DeleteCommand">
                <ItemTemplate>
                    <div class="employeeborder">
                        <div class="employeecontent">
                            <asp:Image ID="EmployeePhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' CssClass="employeeimg1"/>
                        </div>                                             
                        <div class="employeecontent">
                            <div class="employeeitem">姓名：<%#Eval("EmlpoyeeName")%></div>
                            <div class="employeeitem">性别：<%#Convert.ToInt32(Eval("Sex")) == 0 ? "男" : "女"%></div>
                            <div class="employeeitem">职务：<%#Eval("Arrage")%></div>
                            <div class="employeeitem">联系电话：<%#Eval("Tel")%></div>
                            <div class="employeeitem">手机：<%#Eval("Mobile")%></div>
                            <div class="employeeitem">
                                <a href="EditEmployee.aspx?ID=<%#Eval("ID")%>">编辑</a>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Delete" ForeColor="Blue" >删除</asp:LinkButton>
                            </div>
                        </div>                                              
                    </div>                    
                </ItemTemplate>               
            </asp:DataList>
    </div>
            <div class="grid">
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
</asp:Content>
