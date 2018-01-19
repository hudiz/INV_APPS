<%@ Page Title="" Language="VB" MasterPageFile="~/Site_login.Master" AutoEventWireup="false"  EnableEventValidation="false"
    CodeFile="Company_details.aspx.vb" Inherits="Maintaince_Company_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/quicksearch.js"></script>
<script type="text/javascript">
    $(function () {
        $('.search_textbox').each(function (i) {
            $(this).quicksearch("[id*=gv_comp] tr:not(:has(th))", {
                'testQuery': function (query, txt, row) {
                    return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                }
            });
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page_header">
        <h1>
            COMPANY DETAILS
        </h1>
    </div>
    <div class="col-lg-5 col-md-5 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">
            <asp:Label ID="compgid" runat="server" Text="" Visible="false" 
            ForeColor="White" BackColor="Black" 
            style="float:right; padding: 2px;border-radius: 5px;margin-top: -10px;"
             Font-Bold="true"></asp:Label>
                <div>
                <%--<asp:HiddenField ID="custgid" runat="server" Visible="true" />--%>
                    <asp:Label ID="compidLabel" runat="server" AssociatedControlID="compid">Company ID:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compid" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="compidRequired" runat="server" ControlToValidate="compid"
                                    CssClass="failureNotification" ErrorMessage="Company ID is required." ToolTip="Company ID is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="compnameLabel" runat="server" AssociatedControlID="compname">Company Name:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compname" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="compnameRequired" runat="server" ControlToValidate="compname"
                                    CssClass="failureNotification" ErrorMessage="Company Name is required." ToolTip="Company Name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="compaddrlbl" runat="server" AssociatedControlID="compadd">Address:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compadd" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="addrsRequired" runat="server" ControlToValidate="compadd"
                                    CssClass="failureNotification" ErrorMessage="Address is required." ToolTip="Address is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="compphlbl" runat="server" AssociatedControlID="compph">Phone:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compph" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="phn1Required" runat="server" ControlToValidate="compph"
                                    CssClass="failureNotification" ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div>
                    <asp:Label ID="compemaillbl" runat="server" AssociatedControlID="compemail">E-mail:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compemail" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="compemail"
                                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="compcontlbl" runat="server" AssociatedControlID="compcont">Contact Person:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="compcont" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="contRequired" runat="server" ControlToValidate="compcont"
                                    CssClass="failureNotification" ErrorMessage="Contact Person is required." ToolTip="Contact Person is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="submitButton">
                    <asp:Button ID="CreateUserButton" runat="server" Text="Add" CssClass="btn btn-primary"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                </div>
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="RegisterUserValidationGroup"  />
            </div>
        </div>
    </div>
    <div class="col-lg-7 col-md-7 col-sm-12">
        <div class="accountdtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_comp" runat="server" AutoGenerateColumns="False"
                     EmptyDataText="There are no data records to display." 
                     OnSelectedIndexChanged = "OnSelectedIndexChanged" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="comp_id" HeaderText="ID" ItemStyle-Width="80px" SortExpression="comp_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="comp_name" HeaderText="NAME" ItemStyle-Width="300px"
                            SortExpression="comp_name">
                            <ItemStyle Width="300px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="comp_ph" HeaderText="PHONE" ItemStyle-Width="110px"
                            SortExpression="comp_ph">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="comp_cont" HeaderText="CONT. PERSON" ItemStyle-Width="110px"
                            SortExpression="comp_cont">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="comp_email" HeaderText="EMAIL" ItemStyle-Width="110px"
                            SortExpression="comp_email">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
<asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-success"  OnClick = "ExportToExcel" />
            </div>
            <%-- <asp:SqlDataSource ID="SqlDataSource1"
    runat="server" ConnectionString="<%$ ConnectionStrings:invappConnectionString2 %>"
    ProviderName="<%$ ConnectionStrings:invappConnectionString2.ProviderName %>" SelectCommand="SELECT
    [cust_sup_auto_id], [cust_sup_id], [cust_sup_name], [cust_sup_addr], [cust_sup_ph],
    [cust_sup_ph1], [cust_sup_email], [createby], [createon], [updateby], [updateon],
    [comp_id], [cust_sup_sts] FROM [cust_supp_dtls]"> </asp:SqlDataSource>--%>
        </div>
    </div>
</asp:Content>
