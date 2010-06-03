<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeInfo.aspx.cs" Inherits="SmallHouseManagerWeb.EmployeeInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>С�������Ա</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div id="headimg"><div id="headtext">�����Ա</div></div>
        <div id="employee">
            <asp:DataList ID="EmployeeDataList" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                <ItemTemplate>
                    <div>
                        <div class="employeecontent">
                            <asp:Image ID="EmployeePhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' CssClass="employeeimg1"/>
                        </div>                                             
                        <div class="employeecontent">
                            <div class="employeeitem">������<%#Eval("EmlpoyeeName")%></div>
                            <div class="employeeitem">�Ա�<%#Convert.ToInt32(Eval("Sex")) == 0 ? "��" : "Ů"%></div>
                            <div class="employeeitem">ְ��<%#Eval("Arrage")%></div>
                            <div class="employeeitem">��ϵ�绰��<%#Eval("Tel")%></div>
                            <div class="employeeitem">�ֻ���<%#Eval("Mobile")%></div>
                        </div>
                        <div class="dashline"></div>                                                 
                    </div>                    
                </ItemTemplate>               
            </asp:DataList>
                <div id="navigatecount">
                    ��<asp:Label ID="lbPage" runat="server" ForeColor="Red"></asp:Label>ҳ ��ǰ��
                    <asp:Label ID="lbCurrentPage" runat="server" ForeColor="Red"></asp:Label>ҳ
                </div>
                <div id="navigate">
                    <asp:LinkButton ID="FirstPage" runat="server" CausesValidation="False" OnClick="FirstPage_Click">��ҳ</asp:LinkButton>
                    <asp:LinkButton ID="PriorPage" runat="server" CausesValidation="False" OnClick="PriorPage_Click">��һҳ</asp:LinkButton>
                    <asp:LinkButton ID="NextPage" runat="server" CausesValidation="False" OnClick="NextPage_Click">��һҳ</asp:LinkButton>
                    <asp:LinkButton ID="LastPage" runat="server" CausesValidation="False" OnClick="LastPage_Click">βҳ</asp:LinkButton>
                </div>    
        </div>    
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>    
</asp:Content>
