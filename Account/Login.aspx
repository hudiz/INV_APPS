<%@ Page Title="Log In" Language="VB" MasterPageFile="~/Site_login.Master" AutoEventWireup="false"
    CodeFile="Login.aspx.vb" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<div style="width:500px;margin: 0 auto;">
    <h2 style="text-align: center; padding-bottom:5px;">
        LOG IN
    </h2>
    <asp:Login ID="LoginUser" runat="server" >
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
           <%-- <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>--%>
            <div style="width:335px;margin: 0 auto;">
                <div class="divfieldset">
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUser">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUser">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>
                    </p>
                </div>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" Text="Log In"  CommandName="Login" ValidationGroup="LoginUser"    />
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <asp:Label ID="lblmatch" runat="server" Text="" ForeColor="red"></asp:Label>
    <p style="text-align: center;">
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" NavigateUrl="~/Account/Register.aspx" EnableViewState="false">Register</asp:HyperLink> if you don't have an account.
    </p>
    </div>
</asp:Content>