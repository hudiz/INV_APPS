<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" EnableEventValidation="false"
     CodeFile="Item_grp_Details.aspx.vb" Inherits="Maintaince_Item_grp_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="../Scripts/quicksearch.js"></script>
<script type="text/javascript">
    $(function () {
        $('.search_textbox').each(function (i) {
            $(this).quicksearch("[id*=gv_itm] tr:not(:has(th))", {
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
            ITEM GROUP DETAILS
        </h1>
    </div>
    <div class="col-lg-5 col-md-5 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">
            <asp:Label ID="itmgid" runat="server" Text="" Visible="false" 
            ForeColor="White" BackColor="Black" 
            style="float:right; padding: 2px;border-radius: 5px;margin-top: -10px;"
             Font-Bold="true"></asp:Label>
                <div>
                    <asp:Label ID="itmcdLabel" runat="server" AssociatedControlID="itmcd">Item Group Code:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmcd" runat="server" CssClass="form-control"  AutoPostBack="true" ></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmcdRequired" runat="server" ControlToValidate="itmcd"
                                    CssClass="failureNotification" ErrorMessage="Item Group Code is required." ToolTip="Item Group Code is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmnamelbl" runat="server" AssociatedControlID="itmname">Item Group Description:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmname" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="ItmnameRequired" runat="server" ControlToValidate="itmname"
                                    CssClass="failureNotification" ErrorMessage="Item Group Description is required." ToolTip="Item Group Description is required."
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
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itm" runat="server" AutoGenerateColumns="False"
                     EmptyDataText="There are no data records to display." 
                     OnSelectedIndexChanged = "OnSelectedIndexChanged" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="itm_grp_id" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="itm_grp_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_grp_code" HeaderText="ITEM GROUP CODE" ItemStyle-Width="120px"
                            SortExpression="itm_grp_code">
                           
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_grp_desc" HeaderText="ITEM GROUP DESC." ItemStyle-Width="300px"
                            SortExpression="itm_grp_desc">
                            
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="50px">
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

