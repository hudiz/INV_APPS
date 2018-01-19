<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Stock_Item_Dtls.aspx.vb" Inherits="Reports_Stock_Item_Dtls" %>

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
        <h1>ITEM STOCK DETAILS
        </h1>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset"> 
                   <table>
                        <tr>
 
                            <td>
                    <asp:Label ID="itmgrplbl" runat="server" AssociatedControlID="dditemgrp">Item&nbsp;Group:</asp:Label>
                 
                            </td>
                            <td>
                                <asp:DropDownList ID="dditemgrp" runat="server" CssClass="form-control"
                                    DataTextField="grp_desc" DataValueField="grp_cd" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                         
                    <asp:Label ID="itmcdLabel" runat="server" AssociatedControlID="dditmcd">Item&nbsp;Code:</asp:Label>
                    </td>
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
                            <td>
                    <asp:Label ID="itmnamelbl" runat="server" AssociatedControlID="itmname">Item&nbsp;Name:</asp:Label>
                    </td>
                            <td>
                                <asp:TextBox ID="itmname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 20px;"> 
                            </td>
                            <td style="width: 20px;"></td> 
                            <td style="width: 20px;">
                    <asp:Button ID="btn_proceed" runat="server" Text="Proceed" CssClass="btn btn-primary"
                        ValidationGroup="RegisterUserValidationGroup" />
                            </td> 
                            <td>                                
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="RegisterUserValidationGroup" />
                            </td>
                        </tr>
                    </table>  
            </div>
        </div>
    </div>

    <div class="col-lg-4 col-md-4 col-sm-12">
        
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmstkadj" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."  Caption="Stock Details"  >
                    <Columns> 
                        <asp:BoundField DataField="createon" HeaderText="Date" ItemStyle-Width="80px"
                            SortExpression="createon" DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_rmk" HeaderText="Remarks" ItemStyle-Width="100px"
                            SortExpression="itm_pr_info">
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_qty" HeaderText="Quantity"  DataFormatString="{0:N0}"  ItemStyle-Width="80px"
                            SortExpression="stk_itm_qty">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="stk_itm_price" HeaderText="Price" ItemStyle-Width="80px"
                            SortExpression="stk_itm_price">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>  
                    </Columns>
                </asp:GridView>
            </div>
            <%-- <asp:SqlDataSource ID="SqlDataSource1"
    runat="server" ConnectionString="<%$ ConnectionStrings:invappConnectionString2 %>"
    ProviderName="<%$ ConnectionStrings:invappConnectionString2.ProviderName %>" SelectCommand="SELECT
    [cust_sup_auto_id], [cust_sup_id], [cust_sup_name], [cust_sup_addr], [cust_sup_ph],
    [cust_sup_ph1], [cust_sup_email], [createby], [createon], [updateby], [updateon],
    [comp_id], [cust_sup_sts] FROM [cust_supp_dtls]"> </asp:SqlDataSource>--%>
        </div>
    </div>
    
    <div class="col-lg-4 col-md-4 col-sm-12">
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itms" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."  Caption="Sales Item Details"  >
                    <Columns>
                        <asp:BoundField DataField="sr_itm_date" HeaderText="Date" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="sr_itm_date"  DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_itm_inv" HeaderText="Invoice" ItemStyle-Width="80px"
                            SortExpression="sr_itm_inv">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_itm_qty" HeaderText="Quantity"    ItemStyle-Width="250px"
                            SortExpression="stk_itm_qty">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_itm_custname" HeaderText="Customer" ItemStyle-Width="80px"
                            SortExpression="sr_itm_custid">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField> 
                    </Columns>
                </asp:GridView>
            </div>
        </div>
      
    </div>
    
    <div class="col-lg-4 col-md-4 col-sm-12">
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmp" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."   Caption="Purchase Item Details"  >
                    <Columns>
                        <asp:BoundField DataField="pr_itm_date" HeaderText="Date" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="pr_itm_date"  DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="pr_itm_inv" HeaderText="Invoice" ItemStyle-Width="80px"
                            SortExpression="pr_itm_inv">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="pr_itm_qty" HeaderText="Quantity"    ItemStyle-Width="250px"
                            SortExpression="ptk_itm_qty">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="pr_itm_custname" HeaderText="Customer" ItemStyle-Width="80px"
                            SortExpression="pr_itm_custid">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField> 
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>
            <%-- <asp:SqlDataSource ID="SqlDataSource1"
    runat="server" ConnectionString="<%$ ConnectionStrings:invappConnectionString2 %>"
    ProviderName="<%$ ConnectionStrings:invappConnectionString2.ProviderName %>" SelectCommand="SELECT
    [cust_sup_auto_id], [cust_sup_id], [cust_sup_name], [cust_sup_addr], [cust_sup_ph],
    [cust_sup_ph1], [cust_sup_email], [createby], [createon], [updateby], [updateon],
    [comp_id], [cust_sup_sts] FROM [cust_supp_dtls]"> </asp:SqlDataSource>--%>
        </div>
    </div>
        
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="expensedtls">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmgenvw" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."   Caption="Item Stock Details ()"  >
                    <Columns>
                        <asp:BoundField DataField="IDTE" HeaderText="Date" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="IDTE"  DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="REFID" HeaderText="Remarks" ItemStyle-Width="80px"
                            SortExpression="REFID">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="INQTY" HeaderText="Quantity(+)"    ItemStyle-Width="250px"
                            SortExpression="INQTY">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="OUTQTY" HeaderText="Quantity(-)"    ItemStyle-Width="250px"
                            SortExpression="OUTQTY">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TRXTYP" HeaderText="Trx Type" ItemStyle-Width="80px"
                            SortExpression="TRXTYP">
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:BoundField>  
                    </Columns>
                </asp:GridView>
            </div> 
        </div>
    </div>
        
    <div class="col-lg-12 col-md-12 col-sm-12">
                    <asp:Label ID="lbl_currstock" runat="server" AssociatedControlID="dditmcd"></asp:Label>
    </div>
</asp:Content>

