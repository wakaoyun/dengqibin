<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AreaAfact.aspx.cs" Inherits="SmallHouseManagerWeb.AreaAfact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区公共设施</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div id="headimg"><div id="headtext">小区设施</div></div>
            <div id="fact_top"></div>
            <div id="fact_middle_left">
                <div id="fact_middle_right">
                    <div id="fact_content">
                        <asp:Repeater ID="AreafactRepeater" runat="server">
                            <ItemTemplate>
                                <div class="fact_lines">
                                    <div class="fact_title">设施名称：</div>
                                    <div class="fact_text"><%#Eval("FactName") %></div>
                                </div> 
                                <div class="fact_lines">
                                    <div class="fact_title">设施类型：</div>
                                    <div class="fact_text"><%#Eval("TypeName")%></div>
                                </div>       
                                <div class="fact_lines">
                                    <div class="fact_title">负 责 人：</div>
                                    <div class="fact_text"><%#Eval("MainHead")%></div>
                                </div>       
                                <div class="fact_lines">
                                    <div class="fact_title">联系电话：</div>
                                    <div class="fact_text"><%#Eval("Tel")%></div>
                                </div>       
                                <div class="fact_lines">
                                    <div class="fact_title">设施说明：</div>
                                    <div class="fact_text"><%#Eval("Memo") %></div>
                                </div>
                                <div class="dashline"></div>                          
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>            
                </div>
            </div>
            <div id="fact_bottom"></div>
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
<%--        </ContentTemplate>
    </asp:UpdatePanel>  --%> 
</asp:Content>
