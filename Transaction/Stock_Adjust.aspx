<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false"
     CodeFile="Stock_Adjust.aspx.vb" Inherits="Trans_Stock_Adjust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
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
    <script type="text/javascript">
        $(function () {
            $('.search_textbox_sitm').each(function (i) {
                $(this).quicksearch("[id*=gv_sitm] tr:not(:has(th))", {
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
        <h1>STOCK ADJUSTMENT DETAILS
        </h1>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">
                <asp:Label ID="itmgid" runat="server" Text="" Visible="false"
                    ForeColor="White" BackColor="Black"
                    Style="float: right; padding: 2px; border-radius: 5px; margin-top: -10px;"
                    Font-Bold="true"></asp:Label>


                <div>
                    <asp:Label ID="itmgrplbl" runat="server" AssociatedControlID="dditemgrp">Item Group:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dditemgrp" runat="server" CssClass="form-control"
                                    DataTextField="grp_desc" DataValueField="grp_cd" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmcdLabel" runat="server" AssociatedControlID="dditmcd">Item Code:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dditmcd" runat="server" CssClass="form-control"
                                    DataTextField="item_desc" DataValueField="itm_pr_id" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmcdRequired" runat="server" ControlToValidate="dditmcd"
                                    CssClass="failureNotification" ErrorMessage="Item Code is required." ToolTip="Item Code is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div> 

                <div>
                    <asp:Label ID="itmUnit" runat="server" AssociatedControlID="dditemUnit">Unit:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dditemUnit" runat="server" CssClass="form-control"
                                    DataTextField="unit_desc" DataValueField="unit_cd" AutoPostBack="false">
                                    <%--<asp:ListItem Text="Dozen" Value="DZN"></asp:ListItem>
                                    <asp:ListItem Text="Packet" Value="PKT"></asp:ListItem>
                                    <asp:ListItem Text="Piece" Value="PCS"></asp:ListItem>
                                    <asp:ListItem Text="Box" Value="BOX"></asp:ListItem>
                                    <asp:ListItem Text="Carton" Value="CRT"></asp:ListItem>
                                    <asp:ListItem Text="Pouch" Value="PCH"></asp:ListItem>
                                    <asp:ListItem Text="Box" Value="BOX"></asp:ListItem>
                                    <asp:ListItem Text="Case" Value="CAS"></asp:ListItem>
                                    <asp:ListItem Text="Container" Value="CNT"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                        </tr>
                    </table>
                </div>

                <div>
                    <asp:Label ID="itmQtyLabel" runat="server" AssociatedControlID="itmQty">Item Quantity:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmQty" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmQtyRequired" runat="server" ControlToValidate="itmQty"
                                    CssClass="failureNotification" ErrorMessage="Item Cost Price is required." ToolTip="Item Cost Price is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmpriceLabel" runat="server" AssociatedControlID="itmprice">Item Price:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmprice" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmpriceRequired" runat="server" ControlToValidate="itmprice"
                                    CssClass="failureNotification" ErrorMessage="Item Sell Price is required." ToolTip="Item Sell Price is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                
                
                <div>
                    <asp:Label ID="dditemstatusLabel" runat="server" AssociatedControlID="dditemstatus">Status:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dditemstatus" runat="server" CssClass="form-control" AutoPostBack="false">

                                    <asp:ListItem Text="Valid" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                        </tr>
                    </table>
                </div>
                 
                <div>
                    <asp:Label ID="itmremarkLabel" runat="server" AssociatedControlID="itmremark">Remarks:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmremark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
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

    <div class="col-lg-8 col-md-8 col-sm-12">
        
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmprclst" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."  OnSelectedIndexChanged="OnSelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="stk_gid" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="item_id">
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="updateon" HeaderText="DATE" ItemStyle-Width="80px"
                            SortExpression="updateon" DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_grp" HeaderText="GROUP" ItemStyle-Width="100px"
                            SortExpression="stk_itm_grp">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_id" HeaderText="ITEM" ItemStyle-Width="100px"
                            SortExpression="stk_itm_id">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_qty" HeaderText="QUANTITY" ItemStyle-Width="80px"
                            SortExpression="stk_itm_qty">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_price" HeaderText="PRICE" ItemStyle-Width="80px"
                            SortExpression="stk_itm_price">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_unit" HeaderText="UNIT" ItemStyle-Width="80px"
                            SortExpression="stk_itm_unit">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_rmk" HeaderText="REMARKS" ItemStyle-Width="80px"
                            SortExpression="stk_rmk">
                            <ItemStyle Width="80px"></ItemStyle>
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

