<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddHomePark.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddHomePark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加住户停车位</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">停车场名称：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLType" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ParkID" >
            </asp:DropDownList> 
        </div>
    </div> 
    <div class="info_lines grid">
        <div class="info_title">楼宇名称：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLPa" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="PaID" 
                AutoPostBack="True" onselectedindexchanged="DDLPa_SelectedIndexChanged" >
            </asp:DropDownList> 
        </div>
    </div>    
    <div class="info_lines grid">
        <div class="info_title">单元名称：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLCell" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID" 
                AutoPostBack="True" onselectedindexchanged="DDLPa_SelectedIndexChanged">
            </asp:DropDownList> 
        </div>
    </div> 
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <div class="info_lines grid">
            <div class="info_title">房间号：</div>
            <div class="info_text">
                <asp:DropDownList ID="DDLRoomID" runat="server" ForeColor="#1A438E" 
                    Width="145px" DataTextField="RoomID" DataValueField="Code" >
                </asp:DropDownList> 
            </div>
        </div>       
        <div class="info_lines grid">
            <div class="info_title">车牌号码：</div>
            <div class="info_text">
                <asp:TextBox ID="txtCarID" runat="server" ForeColor="#1A438E"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtCarID"></asp:RequiredFieldValidator>                
            </div>
        </div>
        <div class="info_lines grid">
            <div class="info_title">车型号：</div>
            <div class="info_text">
                <asp:TextBox ID="txtType" runat="server" ForeColor="#1A438E"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtType"></asp:RequiredFieldValidator>                
            </div>
        </div>     
        <div class="info_lines grid">
            <div class="info_title">车颜色：</div>
            <div class="info_text">
                <asp:TextBox ID="txtColor" runat="server" ForeColor="#1A438E"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtColor"></asp:RequiredFieldValidator>                
            </div>
        </div>          
        <div class="info_lines grid">
            <div class="info_title">购买日期：</div>
            <div class="info_text">
                <input id="txtBuyDate" type="text" onfocus="calendar()" onclick="calendar()" 
                    readonly="readonly" runat="server" style="color: #1A438E" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtBuyDate"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="info_title"></div>
            <div class="info_text">
                <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" ForeColor="#1A438E" /> 
                <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                    ForeColor="#1A438E" CausesValidation="False" />
            </div>
        </div>      
    </asp:Panel>   
    <asp:Panel ID="Panel2" runat="server">
        <div class="grid bottom_button"><asp:Label ID="Label1" runat="server" Text="没有住户" 
                ForeColor="Red"></asp:Label></div>
    </asp:Panel>    
</asp:Content>
