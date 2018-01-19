<%@ Page Title="Register" Language="VB" MasterPageFile="~/Site_login.Master" AutoEventWireup="false"
    CodeFile="Register.aspx.vb" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <h2 style="text-align: center; padding-bottom: 5px;">CREATE A NEW ACCOUNT
    </h2>
    <div style="width: 500px; margin: 0 auto;">
        <div class="divfieldset">
            <p>
                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                <asp:TextBox ID="UserName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                    ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server"
                    ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
            </p>
            <p>
                <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="Phone">Phone:</asp:Label>
                <asp:TextBox ID="Phone" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PhoneRequired" runat="server" ControlToValidate="Phone"
                    CssClass="failureNotification" ErrorMessage="Phone is required." ToolTip="Phone is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="loginnameLabel" runat="server" AssociatedControlID="loginname" Enabled="false">Login User Name:</asp:Label>
                <asp:TextBox ID="loginname" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="loginnameRequired" runat="server" ControlToValidate="loginname"
                    CssClass="failureNotification" ErrorMessage="Login Name is required." ToolTip="Login Name is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="loginexpLabel" runat="server" AssociatedControlID="loginexp">Login Expiry:</asp:Label>
                <asp:TextBox ID="loginexp" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="loginexpRequired" runat="server" ControlToValidate="loginexp"
                    CssClass="failureNotification" ErrorMessage="Login Expiry is required." ToolTip="Login Expiry is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="labelcomp" runat="server" AssociatedControlID="logincomp">Company:</asp:Label>
                <asp:DropDownList ID="logincomp" DataTextField="comp_name" DataValueField="comp_id"
                    runat="server" CssClass="form-control"  AutoPostBack="True" >
                </asp:DropDownList>
            </p>

            <p>
                <span class="failureNotification">
                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                </span>
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="RegisterUserValidationGroup" />
            </p>
        </div>
        <p class="submitButton">
            <asp:Button ID="CreateUserButton" runat="server" Text="Create User" ValidationGroup="RegisterUserValidationGroup" />
        </p>
    </div>

</asp:Content>
