<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditHomePark.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditHomePark" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑住户停车位</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div class="info_lines grid">
            <div class="info_title">单元名称：</div>
            <div class="info_text">
                <asp:Label ID="lbCell" runat="server" ForeColor="Red"></asp:Label>            
            </div>
        </div>     
    <div class="info_lines grid">
        <div class="info_title">停车场名称：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLType" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ParkID" >
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
                <asp:Button ID="Modify" runat="server" Text="修改" onclick="Modify_Click" ForeColor="#1A438E" /> 
                <asp:Button ID="Cancel" runat="server" Text="取消" onclick="Cancel_Click" 
                    ForeColor="#1A438E" CausesValidation="False" />
            </div>
        </div>  
</asp:Content>
