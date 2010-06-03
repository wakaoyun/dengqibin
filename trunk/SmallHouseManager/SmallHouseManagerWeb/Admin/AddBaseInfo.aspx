<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AddBaseInfo.aspx.cs" Inherits="SmallHouseManagerWeb.Admin.AddBaseInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JavaScript/setday.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <div class="grid">���С��������Ϣ</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="info_lines grid">
        <div class="info_title">С�����ƣ�</div>
        <div class="info_text">
            <asp:TextBox ID="txtBaseName" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtBaseName"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="info_lines grid">
        <div class="info_title">��Ҫ�����ˣ�</div>
        <div class="info_text">
            <asp:TextBox ID="txtMainHead" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtMainHead"></asp:RequiredFieldValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">�������ڣ�</div>
        <div class="info_text">
            <input id="txtBuildDate" type="text" onfocus="calendar()" onclick="calendar()" 
                readonly="readonly" runat="server" style="color: #1A438E" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtBuildDate"></asp:RequiredFieldValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">���������Ķ����</div>
        <div class="info_text">
            <asp:TextBox ID="txtBuildArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtBuildArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="ֻ��������" ControlToValidate="txtBuildArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">��·�����Ķ����</div>
        <div class="info_text">
            <asp:TextBox ID="txtRoadArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtRoadArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ErrorMessage="ֻ��������" ControlToValidate="txtRoadArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">�̻������Ķ����</div>
        <div class="info_text">
            <asp:TextBox ID="txtGreenArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtGreenArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ErrorMessage="ֻ��������" ControlToValidate="txtGreenArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">ͣ���������Ķ����</div>
        <div class="info_text">
            <asp:TextBox ID="txtParkArea" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtParkArea" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                ErrorMessage="ֻ��������" ControlToValidate="txtParkArea" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">¥��������������</div>
        <div class="info_text">
            <asp:TextBox ID="txtAmount" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtAmount" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                ErrorMessage="ֻ��������" ControlToValidate="txtAmount" Display="Dynamic" 
                ValidationExpression="[1-9]+[0-9]*$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">��ϵ�绰��</div>
        <div class="info_text">
            <asp:TextBox ID="txtTelphone" runat="server" ForeColor="#1A438E"></asp:TextBox>�磺0591��8323384
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtTelphone" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                ErrorMessage="��ʽ����ȷ" ControlToValidate="txtTelphone" Display="Dynamic" 
                ValidationExpression="\d{3,4}-\d{7,8}"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="info_lines grid">
        <div class="info_title">С����ַ��</div>
        <div class="info_text">
            <asp:TextBox ID="txtAddress" runat="server" ForeColor="#1A438E"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ErrorMessage="����Ϊ��" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
        </div>
    </div>    
    <div class="info_lines grid reporttext">
        <div class="info_title reporttext">С����飺</div>
        <div class="info_text">
            <asp:TextBox ID="txtMemo" runat="server" Height="131px" 
                TextMode="MultiLine" Width="364px" ForeColor="#1A438E" ></asp:TextBox></div>
    </div>
    <div class="info_lines grid">
        <div class="info_title"></div>
        <div class="info_text">
            <asp:Button ID="Add" runat="server" Text="���" onclick="Add_Click" 
                ForeColor="#1A438E" />            
        </div>
    </div>  
</asp:Content>
