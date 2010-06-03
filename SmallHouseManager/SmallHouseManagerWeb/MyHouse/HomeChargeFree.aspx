<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="HomeChargeFree.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.HomeChargeFree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">我的物业费用</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <asp:GridView ID="HomeFreeGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="20" ForeColor="#1A438E"  
            onrowdatabound="HomeFreeGridView_RowDataBound" Font-Size="12px">
            <Columns>
                <asp:BoundField HeaderText="费用名称" DataField="TypeName" >
                <ItemStyle HorizontalAlign="Center" Width="85px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="计费日期" DataField="StartDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="登记日期" DataField="PayDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量" DataField="Number" >
                <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单价（元/单位）" DataField="Price" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="应付金额（元）" DataField="Payment" >
                <ItemStyle HorizontalAlign="Center" Width="95px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="未付金额（元）" DataField="Notpayment" >
                <ItemStyle HorizontalAlign="Center" Width="95px" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField HeaderText="登记人" DataField="AddName" >
                <ItemStyle HorizontalAlign="Center" Width="85px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
    <div id="homefree" class="info_lines grid titles">如果您最近有未缴的的费用，请到物业管理相关部门缴纳费用。谢谢您的合作。</div>
</asp:Content>
