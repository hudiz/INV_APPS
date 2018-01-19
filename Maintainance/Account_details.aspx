<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false"
     CodeFile="Account_details.aspx.vb" Inherits="Account_Account_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/quicksearch.js"></script>
<script type="text/javascript">
    $(function () {
        $('.search_textbox').each(function (i) {
            $(this).quicksearch("[id*=gv_acc] tr:not(:has(th))", {
                'testQuery': function (query, txt, row) {
                    return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                }
            });
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="page_header">
        <h1>
            ACCOUNT DETAILS
        </h1>
    </div>
    <div class="col-lg-5 col-md-5 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">
            <asp:Label ID="accgid" runat="server" Text="" Visible="false" 
            ForeColor="White" BackColor="Black" 
            style="float:right; padding: 2px;border-radius: 5px;margin-top: -10px;"
             Font-Bold="true"></asp:Label>
                
                <div>
                    <asp:Label ID="accnameLabel" runat="server" AssociatedControlID="accname">Account Name:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="accname" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="custnameRequired" runat="server" ControlToValidate="accname"
                                    CssClass="failureNotification" ErrorMessage="Account Name is required." ToolTip="Account Name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="accdescLabel" runat="server" AssociatedControlID="accdesc">Account Description:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="accdesc" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="addrsRequired" runat="server" ControlToValidate="accdesc"
                                    CssClass="failureNotification" ErrorMessage="Description is required." ToolTip="Description is required."
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
                    ID="gv_acc" runat="server" AutoGenerateColumns="False"
                     EmptyDataText="There are no data records to display." 
                     OnSelectedIndexChanged = "OnSelectedIndexChanged" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="acc_id" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="acc_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="acc_name" HeaderText="NAME" ItemStyle-Width="250px" SortExpression="acc_name">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="acc_desc" HeaderText="DESCRIPTION" ItemStyle-Width="250px"
                            SortExpression="acc_desc">
                            <ItemStyle Width="300px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="acc_sts" HeaderText="STATUS" ItemStyle-Width="110px"
                            SortExpression="acc_sts">
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

