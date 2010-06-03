<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddHomeHold.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddHomeHold" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加小区住户基本信息</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="room_left">
            <div class="room_title">选择楼宇：</div>
            <div class="room_text">
                <asp:DropDownList ID="DDLPa" runat="server" ForeColor="#1A438E" 
                    Width="145px" DataTextField="Name" DataValueField="PaID" 
                    AutoPostBack="True" onselectedindexchanged="DDLPa_SelectedIndexChanged">
                </asp:DropDownList> 
            </div>
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="room_left">
            <div class="room_title">选择单元：</div>
            <div class="room_text">
                <asp:DropDownList ID="DDLCell" runat="server" ForeColor="#1A438E" 
                    Width="145px" DataTextField="Name" DataValueField="ID" AutoPostBack="True" 
                    onselectedindexchanged="DDLPa_SelectedIndexChanged">
                </asp:DropDownList> 
            </div>
        </div>
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">选择房间：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLRoomID" runat="server" ForeColor="#1A438E" 
                        Width="145px" DataTextField="RoomID" DataValueField="Code">
                    </asp:DropDownList> 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">业主姓名：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtName" runat="server" ForeColor="#1A438E"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>                    
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">房产证号：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtOwnerID" runat="server" ForeColor="#1A438E" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtOwnerID" Display="Dynamic"></asp:RequiredFieldValidator>                 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">联系电话：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtTelephone" runat="server" ForeColor="#1A438E" 
                        MaxLength="13"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtTelephone" Display="Dynamic"></asp:RequiredFieldValidator>                    
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">手机：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtMobile" runat="server" ForeColor="#1A438E" MaxLength="12"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtMobile" Display="Dynamic"></asp:RequiredFieldValidator>                 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">电子邮箱：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtEmail" runat="server" ForeColor="#1A438E"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>                    
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">身份证：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtID" runat="server" ForeColor="#1A438E" MaxLength="18"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtID" Display="Dynamic"></asp:RequiredFieldValidator>                 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">工作单位：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtUnit" runat="server" ForeColor="#1A438E"></asp:TextBox>
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">联系地址：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtAddress" runat="server" ForeColor="#1A438E"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>                 
                </div>
            </div>
        </div>
        <div class="info_lines grid titles">以下是住户用户名和密码（<asp:Label ID="Label1" 
                runat="server" Text="默认都为身份证号" ForeColor="Red"></asp:Label>）</div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">用户名：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtUID" runat="server" ForeColor="#1A438E" MaxLength="20"></asp:TextBox>
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">密码：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtPassword" runat="server" ForeColor="#1A438E" MaxLength="20" 
                        TextMode="Password"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="reporttext">
                <div class="room_title">备注信息：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                        TextMode="MultiLine" Width="364px" ForeColor="#1A438E"></asp:TextBox>                  
                </div>
            </div>        
        </div>
        <div class="grid bottom_button">        
            <asp:Button ID="Save" runat="server" Text="保存数据" ForeColor="#1A438E" onclick="Save_Click" />
        </div>
    </asp:Panel>
   
</asp:Content>
