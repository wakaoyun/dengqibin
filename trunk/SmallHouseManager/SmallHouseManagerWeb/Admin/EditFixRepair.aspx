<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="EditFixRepair.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.EditFixRepair" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">编辑设备维修</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="repair_title">设备名称：</div>
        <div class="repair_text">
            <asp:DropDownList ID="DDLFixName" runat="server" ForeColor="#1A438E" 
                Width="145px" DataTextField="Name" DataValueField="FixID">
            </asp:DropDownList>            
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">维修日期：</div>
        <div class="repair_text">
            <input id="txtRepairDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtRepairDate"></asp:RequiredFieldValidator>       
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">结束日期：</div>
        <div class="repair_text">
            <input id="txtEndDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtEndDate"></asp:RequiredFieldValidator>          
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">主要负责人：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtMainHead" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtMainHead"></asp:RequiredFieldValidator>           
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">维修费用：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtServiceFee" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtServiceFee"></asp:RequiredFieldValidator> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtServiceFee" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>         
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">材料费用：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtMaterielFee" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtMaterielFee"></asp:RequiredFieldValidator>  
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtMaterielFee" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>              
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">费用总计：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtSum" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtSum"></asp:RequiredFieldValidator> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ErrorMessage="只能是数字" ControlToValidate="txtSum" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>              
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">是否付款：</div>
        <div class="repair_text">
            <asp:RadioButtonList ID="rblSign" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">已付</asp:ListItem>
                <asp:ListItem Value="1">未付</asp:ListItem>
            </asp:RadioButtonList>                
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">维修单位：</div>
        <div class="repair_text">
            <asp:TextBox ID="txtRepairUnit" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="不能为空" ControlToValidate="txtRepairUnit"></asp:RequiredFieldValidator>           
        </div>        
    </div>
    <div class="info_lines grid">
        <div class="repair_title">维修说明：</div>
        <div class="repair_text">
            <FTB:FreeTextBox ID="ftbMemo" runat="server" 
                ImageGalleryPath="~/AdsImages/" 
                SupportFolder="~/Admin/aspnet_client/FreeTextBox/" 
                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Cut,Copy,Paste;Undo,Redo|Bold,Italic,Underline;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImageFromGallery,InsertRule" 
                Width="685px" Language="zh-cn" Height="200px">
            </FTB:FreeTextBox>           
        </div>
    </div>
    <div class="info_lines grid">
        <div class="repair_title"></div>
        <div class="repair_text">
            <asp:Button ID="Add" runat="server" Text="添加" onclick="Add_Click" 
                ForeColor="#1A438E" />      
        </div>
    </div>       
</asp:Content>
