<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdsInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AdsInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑广告</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="grid">
            <asp:DataList ID="AdsDataList" runat="server" ForeColor="#1A438E" Font-Size="12px" 
                ondeletecommand="AdsDataList_DeleteCommand">
                <ItemTemplate>
                    <div class="adsborder">
                        <div class="employeecontent">
                            <asp:Image ID="AdsPhoto" runat="server" ImageUrl='<%#Eval("PhotoPath") %>' Width="524px" Height="145px" />
                        </div>                                             
                        <div class="employeecontent">
                            <div class="adsitem">广告名：<%#Eval("AdName")%></div>
                            <div class="adsitem">广告地址：<%#Eval("Url")%></div>
                            <div class="adsitem">
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
