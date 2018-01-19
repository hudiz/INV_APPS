<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false" 
    CodeFile="Item_Details.aspx.vb" Inherits="Maintaince_Item_Details" %>

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
            ITEM DETAILS
        </h1>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12"> 
            
            <div class="divfieldset"> 
            <asp:Label ID="itmgid" runat="server" Text="" Visible="false" 
            ForeColor="White" BackColor="Black" 
            style="float:right; padding: 2px;border-radius: 5px;margin-top: -10px;"
             Font-Bold="true"></asp:Label>
            <table class="sales_tab">
                <tr>
                    <td> 
                    <asp:Label ID="itmcdLabel" runat="server" AssociatedControlID="itmcd">Item Code:</asp:Label>
                    </td>
                    <td>
                                <asp:TextBox ID="itmcd" runat="server" CssClass="form-control"  AutoPostBack="true" ></asp:TextBox>
                    </td>
                    <td>
                                <asp:RequiredFieldValidator ID="itmcdRequired" runat="server" ControlToValidate="itmcd"
                                    CssClass="failureNotification" ErrorMessage="Item Code is required." ToolTip="Item Code is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                      <asp:Label ID="itmnamelbl" runat="server" AssociatedControlID="itmname">Item Name:</asp:Label>
                    </td> 
                    <td>
                       
                                <asp:TextBox ID="itmname" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="ItmnameRequired" runat="server" ControlToValidate="itmname"
                                    CssClass="failureNotification" ErrorMessage="Item Name is required." ToolTip="Item Name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                    </td> 
                    
                    <td>
                    <asp:Label ID="itmgrplbl" runat="server" AssociatedControlID="dditemgrp">Item Group:</asp:Label>
                    </td> 
                    <td>
                                <asp:DropDownList ID="dditemgrp" runat="server" CssClass="form-control"
                                 DataTextField="grp_desc" DataValueField="grp_cd" AutoPostBack="false"  ></asp:DropDownList>
                    </td>
                    <td>
                    </td>
                            <td style="width: 20px;"></td>
                    <td valign="top"> 
                            
                <%--<div class="submitButton">--%>
                    <asp:Button ID="CreateUserButton" runat="server" Text="Add" CssClass="btn btn-primary"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger"
                        ValidationGroup="RegisterUserValidationGroup" />
                        <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                <%--</div>--%>
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="RegisterUserValidationGroup"  />
                    </td>
                </tr>
            </table>
                 


           
            </div> 
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itm" runat="server" AutoGenerateColumns="False"
                     EmptyDataText="There are no data records to display." 
                     OnSelectedIndexChanged = "OnSelectedIndexChanged" OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="itm_pr_gid" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="item_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_id" HeaderText="ITEM CODE" ItemStyle-Width="80px"
                            SortExpression="item_code">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_info" HeaderText="ITEM NAME" ItemStyle-Width="250px"
                            SortExpression="item_name">
                            <ItemStyle Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_grp" HeaderText="ITEM GROUP" ItemStyle-Width="80px"
                            SortExpression="item_grp">
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField >
                            <ItemStyle Width="110px"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" Width="70px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="itm_pr_id"  
                    ControlStyle-Width="80px" DataNavigateUrlFormatString="../Maintainance/Item_Price.aspx?itm_id={0}"
                    Target="_blank"  Text="Pricing" />
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

