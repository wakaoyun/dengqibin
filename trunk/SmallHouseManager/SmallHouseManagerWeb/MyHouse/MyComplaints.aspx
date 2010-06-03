<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MyComplaints.aspx.cs" Inherits="SmallHouseManagerWeb.MyHouse.MyComplaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">投诉记录</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div>
        <asp:GridView ID="HomeReportGridView" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" CellPadding="4" PageSize="20" ForeColor="#1A438E"  
            onrowdatabound="HomeReportGridView_RowDataBound" 
            onrowdeleting="HomeReportGridView_RowDeleting" 
            onrowediting="HomeReportGridView_RowEditing"
            DataKeyNames="ID" Font-Size="12px">
            <Columns>
                <asp:BoundField HeaderText="投诉编号" DataField="ID" >
                <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="投诉日期" DataField="ReportDate" 
                    DataFormatString="{0:yyyy年MM月dd日}" >
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="投诉内容" DataField="ReportText" >
                <ItemStyle HorizontalAlign="Center" Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否处理" DataField="Sign" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:CommandField EditText="编辑" HeaderText="编辑" ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:CommandField>
                <asp:CommandField HeaderText="删除" DeleteText="删除" ShowDeleteButton="True" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:CommandField>
                <asp:HyperLinkField HeaderText="详细信息" Text="详细信息" DataNavigateUrlFields="ID" 
                    DataNavigateUrlFormatString="ComplaintsInfo.aspx?ID={0}" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:HyperLinkField>
            </Columns>
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
    </div>
</asp:Content>
