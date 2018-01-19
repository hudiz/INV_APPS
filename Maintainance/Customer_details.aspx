<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false"
    CodeFile="Customer_details.aspx.vb" Inherits="Customer_Customer_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/quicksearch.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=gv_cust] tr:not(:has(th))", {
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
            CUSTOMER DETAILS
        </h1>
    </div>
    <div class="col-lg-5 col-md-5 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">
                <asp:Label ID="custgid" runat="server" Text="" Visible="false" ForeColor="White"
                    BackColor="Black" Style="float: right; padding: 2px; border-radius: 5px; margin-top: -10px;"
                    Font-Bold="true"></asp:Label>
                <div> 
                    <asp:Label ID="custidLabel" runat="server" AssociatedControlID="custid">Customer ID:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custid" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="custidRequired" runat="server" ControlToValidate="custid"
                                    CssClass="failureNotification" ErrorMessage="Customer ID is required." ToolTip="Customer ID is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="custnameLabel" runat="server" AssociatedControlID="custname">Customer Name:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custname" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="custnameRequired" runat="server" ControlToValidate="custname"
                                    CssClass="failureNotification" ErrorMessage="Customer Name is required." ToolTip="Customer Name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="custaddrlbl" runat="server" AssociatedControlID="custadd">Address:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custadd" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <%--<asp:RequiredFieldValidator ID="addrsRequired" runat="server" ControlToValidate="custadd"
                                    CssClass="failureNotification" ErrorMessage="Address is required." ToolTip="Address is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="custphlbl" runat="server" AssociatedControlID="custph">Phone 1:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custph" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="phn1Required" runat="server" ControlToValidate="custph"
                                    CssClass="failureNotification" ErrorMessage="Phone Number is required." ToolTip="Phone Number is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="custph1lbl" runat="server" AssociatedControlID="custph1">Phone 2:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custph1" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="custemaillbl" runat="server" AssociatedControlID="custemail">E-mail:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="custemail" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                               <%-- <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="custemail"
                                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
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
                    ValidationGroup="RegisterUserValidationGroup" />
            </div>
        </div>
    </div>
    <div class="col-lg-7 col-md-7 col-sm-12">
        <div class="accountdtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_cust" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                    OnSelectedIndexChanged="OnSelectedIndexChanged" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="cust_sup_auto_id" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="cust_sup_auto_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cust_sup_id" HeaderText="ID" ItemStyle-Width="80px" SortExpression="cust_sup_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cust_sup_name" HeaderText="NAME" ItemStyle-Width="300px"
                            SortExpression="cust_sup_name">
                            <ItemStyle Width="300px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cust_sup_ph" HeaderText="PHONE1" ItemStyle-Width="110px"
                            SortExpression="cust_sup_ph">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cust_sup_ph1" HeaderText="PHONE2" ItemStyle-Width="110px"
                            SortExpression="cust_sup_ph1">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="cust_sup_email" HeaderText="EMAIL" ItemStyle-Width="200px"
                            SortExpression="cust_sup_email">
                            <ItemStyle Width="200px"></ItemStyle>
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
