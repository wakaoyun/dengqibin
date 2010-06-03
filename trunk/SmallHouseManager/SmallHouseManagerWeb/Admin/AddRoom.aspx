<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">添加房间管理</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="room_left">
            <div class="room_title">楼宇：</div>
            <div class="room_text">
                <asp:DropDownList ID="DDLPa" runat="server" ForeColor="#1A438E" 
                    Width="145px" DataTextField="Name" DataValueField="PaID" 
                    AutoPostBack="True" onselectedindexchanged="DDLPa_SelectedIndexChanged">
                </asp:DropDownList> 
            </div>
        </div>
        <div class="room_right">
            <div class="room_title">单元： </div>
            <div class="room_text">
                <asp:DropDownList ID="DDLCell" runat="server" ForeColor="#1A438E" 
                    Width="145px" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList> 
            </div>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="room_left">
            <div class="room_title">楼层：</div>
            <div class="room_text">
                从
                <asp:DropDownList ID="DDLLayerBegin" runat="server" ForeColor="#1A438E" Width="51px"></asp:DropDownList>                 
                到
                <asp:DropDownList ID="DDLLayerEnd" runat="server" ForeColor="#1A438E" Width="51px"></asp:DropDownList>                 
            </div>
        </div>
        <div class="room_right">
            <div class="room_title">每层房间数：</div>
            <div class="room_text">
                <asp:TextBox ID="txtCount" runat="server" ForeColor="#1A438E">10</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtCount" Display="Dynamic"></asp:RequiredFieldValidator> 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="只能是数字" ControlToValidate="txtCount" Display="Dynamic" 
                    ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator> 
            </div>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="room_left">
            <div class="room_title">前缀：</div>
            <div class="room_text">
                <asp:TextBox ID="txtPrefix" runat="server" ForeColor="#1A438E"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="不能为空" ControlToValidate="txtPrefix"></asp:RequiredFieldValidator> 
            </div>
        </div>
        <div class="room_right room_button"><asp:Button ID="Build" runat="server" Text="创建" onclick="Build_Click" ForeColor="#1A438E" /></div>
    </div>
    <asp:Panel ID="ModifyPanel" runat="server" Visible="False">
        <div class="info_lines grid"></div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">房间朝向：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLSunny" runat="server" ForeColor="#1A438E" 
                        Width="145px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList> 
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">房间规格：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLFormat" runat="server" ForeColor="#1A438E" 
                        Width="145px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList> 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">房间功能：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLRoomUse" runat="server" ForeColor="#1A438E" 
                        Width="145px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList> 
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">装修标准：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLIndoor" runat="server" ForeColor="#1A438E" 
                        Width="145px" DataTextField="Name" DataValueField="ID">
                    </asp:DropDownList> 
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">房间面积：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtArea" runat="server" ForeColor="#1A438E">125</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtArea" Display="Dynamic"></asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ErrorMessage="只能是数字" ControlToValidate="txtArea" Display="Dynamic" 
                        ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>   
                </div>
            </div>
            <div class="room_right">
                <div class="room_title">可用面积：</div>
                <div class="room_text">
                    <asp:TextBox ID="txtUseArea" runat="server" ForeColor="#1A438E">120</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="不能为空" ControlToValidate="txtUseArea"></asp:RequiredFieldValidator> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ErrorMessage="只能是数字" ControlToValidate="txtUseArea" Display="Dynamic" 
                        ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>   
                </div>
            </div>
        </div>
        <div class="info_lines grid">
            <div class="room_left">
                <div class="room_title">更新行：</div>
                <div class="room_text">
                    <asp:DropDownList ID="DDLRow" runat="server" ForeColor="#1A438E" 
                        Width="145px">
                    </asp:DropDownList>  
                    <asp:Label ID="Label1" runat="server" Text="0表示所有行" ForeColor="Red"></asp:Label>  
                </div>
            </div>
            <div class="room_right room_button"><asp:Button ID="Update" runat="server" Text="更新" onclick="Update_Click" ForeColor="#1A438E" /></div>
        </div>
        <div class="grid">
            <asp:GridView ID="RoomGridView" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#1A438E"  
                onrowdatabound="RoomGridView_RowDataBound"
                Font-Size="12px">
                <Columns>
                    <asp:BoundField HeaderText="编号" DataField="ID" >
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="房间朝向" DataField="SunnyName" >
                    <ItemStyle HorizontalAlign="Center" Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="房间规格" DataField="RoomFormatName" >
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="装修标准" DataField="IndoorName" >
                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="房间功能" DataField="RoomUseName" >
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>  
                    <asp:BoundField HeaderText="房间面积" DataField="BuildArea" >
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="可用面积" DataField="UseArea" >
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>                  
                </Columns>
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </div>
        <div class="grid bottom_button">
            <asp:Button ID="Save" runat="server" Text="保存到数据库" ForeColor="#1A438E" 
                onclick="Save_Click" />
            <asp:Button ID="Clear" runat="server" Text="清空" ForeColor="#1A438E" 
                onclick="Clear_Click" />
        </div>
    </asp:Panel>    
</asp:Content>
