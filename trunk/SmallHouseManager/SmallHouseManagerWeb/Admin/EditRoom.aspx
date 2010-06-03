<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditRoom.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑小区房产信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        当前单元ID：
        <asp:Label ID="lbCode" runat="server" ForeColor="Red"></asp:Label>
        单元名称：
        <asp:Label ID="lbRoomID" runat="server" ForeColor="Red"></asp:Label>
        楼宇号：
        <asp:Label ID="lbPaName" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div class="info_lines grid">
        <div class="info_title">房间规格：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLRoomFormat" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
        </div>
    </div>  
    <div class="info_lines grid">
        <div class="info_title">房间朝向：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLSunny" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
        </div>
    </div>     
    <div class="info_lines grid">
        <div class="info_title">房间功能：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLRoomUse" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
        </div>
    </div>     
    <div class="info_lines grid">
        <div class="info_title">装修标准：</div>
        <div class="info_text">
            <asp:DropDownList ID="DDLIndoor" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
        </div>
    </div>        
     <div class="info_lines grid">
        <div class="info_title">房间面积：</div>
        <div class="info_text">
            <asp:TextBox ID="txtBuildArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtBuildArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtBuildArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">可用面积：</div>
        <div class="info_text">
            <asp:TextBox ID="txtUseArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtUseArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtUseArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Edit" runat="server" Text="修改" onclick="Edit_Click" ForeColor="#1A438E" /> 
            <asp:Button ID="Back" runat="server" Text="反回" onclick="Back_Click" ForeColor="#1A438E" />        
        </div>
    </div>  
</asp:Content>
