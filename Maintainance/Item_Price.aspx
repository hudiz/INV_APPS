<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false"
     CodeFile="Item_Price.aspx.vb" Inherits="Maintaince_Item_Price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
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
        <h1>ITEM PRICE DETAILS
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
                    <asp:Label ID="itmnamelbl" runat="server" AssociatedControlID="itmname">Item Name:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmname" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="ItmnameRequired" runat="server" ControlToValidate="itmname"
                                    CssClass="failureNotification" ErrorMessage="Item Name is required." ToolTip="Item Name is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div>
                    <asp:Label ID="itmremarkLabel" runat="server" AssociatedControlID="itmremark">HSN/SAC:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmremark" runat="server" CssClass="form-control"  ></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
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
                                   <%-- <asp:ListItem Text="DZN - Dozen" Value="DZN"></asp:ListItem>
                                    <asp:ListItem Text="PKT - Packet" Value="PKT"></asp:ListItem>
                                    <asp:ListItem Text="PCS - Piece" Value="PCS"></asp:ListItem>
                                    <asp:ListItem Text="BOX - Box" Value="BOX"></asp:ListItem>
                                    <asp:ListItem Text="CRT - Carton" Value="CRT"></asp:ListItem>
                                    <asp:ListItem Text="PCH - Pouch" Value="PCH"></asp:ListItem>
                                    <asp:ListItem Text="CAS - Case" Value="CAS"></asp:ListItem>
                                    <asp:ListItem Text="CNT - Container" Value="CNT"></asp:ListItem>
                                    <asp:ListItem Text="KGS - KG" Value="KGS"></asp:ListItem>
                                    <asp:ListItem Text="MTS - METER" Value="MTS"></asp:ListItem>
                                    <asp:ListItem Text="INC - INCH" Value="INC"></asp:ListItem>
                                    <asp:ListItem Text="FTS - FEET" Value="FTS"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                        </tr>
                    </table>
                </div>

                <div>
                    <asp:Label ID="itmcostpriceLabel" runat="server" AssociatedControlID="itmcostprice">Item Cost Price:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmcostprice" runat="server" CssClass="form-control"  AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmcostpriceRequired" runat="server" ControlToValidate="itmcostprice"
                                    CssClass="failureNotification" ErrorMessage="Item Cost Price is required." ToolTip="Item Cost Price is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmsellpriceLabel" runat="server" AssociatedControlID="itmsellprice">Item Sell Price:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmsellprice" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmsellpriceRequired" runat="server" ControlToValidate="itmsellprice"
                                    CssClass="failureNotification" ErrorMessage="Item Sell Price is required." ToolTip="Item Sell Price is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmtaxpercLabel" runat="server" AssociatedControlID="itmtaxperc">Item Tax %:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmtaxperc" runat="server" CssClass="form-control"   AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmtaxpercRequired" runat="server" ControlToValidate="itmtaxperc"
                                    CssClass="failureNotification" ErrorMessage="Item Tax % is required." ToolTip="Item Tax % is required."
                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmtaxamtLabel" runat="server" AssociatedControlID="itmtaxamt">Item Tax Amount:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmtaxamt" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td style="width: 20px;">
                                <asp:RequiredFieldValidator ID="itmtaxamtRequired" runat="server" ControlToValidate="itmtaxamt"
                                    CssClass="failureNotification" ErrorMessage="Item Tax amount is required." ToolTip="Item Tax amount is required."
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
                                    <asp:ListItem Text="Old Price" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>

                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Label ID="itmprrefidLabel" runat="server" AssociatedControlID="itmprrefid">Purchase Ref ID:</asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="itmprrefid" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="submitButton">
                    <div style=" float:right">
                    <asp:Button ID="CreateUserButton" runat="server" Text="Add" CssClass="btn btn-primary"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                    </div>
                    
                </div>
                
                     <div style=" float:left">                          
                        <asp:HyperLink ID="hpl_addnwitem" runat="server" onclick="return add_new_customer();" CssClass="btn btn-success">
                            <asp:Label ID="Label1" runat="server" >Add New Item</asp:Label>

                        </asp:HyperLink>
                        <script type="text/javascript">
                            function add_new_customer() { 
                                var iele = document.getElementById("<%=dditemgrp.ClientID%>");  
                                var qrystr = "../Maintainance/Item_Details.aspx?grpid=" + iele.value.trim();
                                //alert(qrystr);
                                window.open(qrystr,"_self");
                            }
                        </script>
                    </div>
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="RegisterUserValidationGroup" />
              <div>  
                  &nbsp;
              </div>
                
            </div>
        </div>
    </div>

    <div class="col-lg-8 col-md-8 col-sm-12">
        
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmprclst" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."   >
                    <Columns>
                        <asp:BoundField DataField="itm_pr_gid" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="itm_pr_gid">
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_dos" HeaderText="Date" ItemStyle-Width="80px"
                            SortExpression="itm_pr_id" DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_info" HeaderText="ITEM NAME" ItemStyle-Width="100px"
                            SortExpression="itm_pr_info">
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_rmk" HeaderText="HSN/SAC" ItemStyle-Width="100px"
                            SortExpression="itm_pr_rmk">
                            <ItemStyle Width="70px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_cost" HeaderText="Cost" ItemStyle-Width="80px"
                            SortExpression="itm_pr_cost">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_price" HeaderText="Sell Price" ItemStyle-Width="80px"
                            SortExpression="itm_pr_price">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_unt" HeaderText="Unit" ItemStyle-Width="80px"
                            SortExpression="itm_pr_unt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_taxpct" HeaderText="Tax perc" ItemStyle-Width="80px"
                            SortExpression="itm_pr_taxpct">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                      <%--  <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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

