<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HouseIntroduce.aspx.cs" Inherits="SmallHouseManagerWeb.HouseIntroduce" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>小区介绍</title> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" runat="server">
            <div id="headimg"><div id="headtext">小区介绍</div></div>
            <div id="introduce_top"></div>
            <div id="fact_middle_left">
                <div id="fact_middle_right">
                    <div id="fact_content"> 
                        <div class="fact_lines">
                            <div class="fact_title">小区名称：</div>
                            <div class="fact_text"><asp:Label ID="BaseName" runat="server"></asp:Label></div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">负 责 人：</div>
                            <div class="fact_text"><asp:Label ID="MainHead" runat="server"></asp:Label></div>
                        </div>                        
                        <div class="fact_lines">
                            <div class="fact_title">建造日期：</div>
                            <div class="fact_text"><asp:Label ID="BuildDate" runat="server"></asp:Label></div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">建筑面积：</div>
                            <div class="fact_text"><asp:Label ID="BuildArea" runat="server"></asp:Label>亩</div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">楼宇数量：</div>
                            <div class="fact_text"><asp:Label ID="Amount" runat="server"></asp:Label>栋</div>
                        </div>                                               
                        <div class="fact_lines">
                            <div class="fact_title">停车面积：</div>
                            <div class="fact_text"><asp:Label ID="ParkingArea" runat="server"></asp:Label>亩</div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">道路面积：</div>
                            <div class="fact_text"><asp:Label ID="RoadArea" runat="server"></asp:Label>亩</div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">绿化面积：</div>
                            <div class="fact_text"><asp:Label ID="GreenArea" runat="server"></asp:Label>亩</div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">联系电话：</div>
                            <div class="fact_text"><asp:Label ID="Tel" runat="server"></asp:Label></div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">小区地址：</div>
                            <div class="fact_text"><asp:Label ID="Address" runat="server"></asp:Label></div>
                        </div>
                        <div class="fact_lines">
                            <div class="fact_title">小区说明：</div>
                            <div class="fact_text"><asp:Label ID="Introduce" runat="server"></asp:Label></div>
                        </div>
                    </div>            
                </div>
            </div>            
            <div id="fact_bottom"></div>
</asp:Content>
