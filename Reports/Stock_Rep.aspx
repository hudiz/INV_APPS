<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Stock_Rep.aspx.vb" Inherits="Report_Stock_Rep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/quicksearch.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=gv_Sales_rep] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            });
        }); 
    </script> 
     <style type="text/css">
        .sales_tab tr
        {
            padding: 2px;
        }
        .sales_tab tr td
        {
            padding: 3px;
        }
    </style>
    <script type="text/javascript" src="../Scripts/md5.js"></script>
    <script src="../Scripts/aes.js"></script>
    <link rel="stylesheet" href="../Scripts/jquery/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="../Scripts/jquery/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery/ui/1.12.1/jquery-ui.js"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page_header">
        <h1>STOCK SUMMARY REPORTS
        </h1>
    </div>

    <div class="col-lg-12 col-md-12 col-sm-12">
        
        <div class="expensedtls">
            <div class="table-responsive">
                
                <asp:Label ID="txt_reportgid" runat="server" Text="" Visible="false"
                    ForeColor="White" BackColor="Black"
                    Style="float: right; padding: 2px; border-radius: 5px; margin-top: -10px;"
                    Font-Bold="true"></asp:Label>
                
            <div class="divfieldset">  
            <table class="sales_tab">
                <tr>
                    
                    <%--<td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_asondte">AS ON DATE :</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_asondte" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                    </td>
                    <td>
                    </td>--%>
                    <td>
                        <asp:Label ID="lbl_itemgrp" runat="server" AssociatedControlID="dd_itemgrp">ITEM GROUP :</asp:Label>
                    </td> 
                    <td>
                       
                                <asp:DropDownList ID="dd_itemgrp" runat="server" CssClass="form-control"
                                    DataTextField="grp_desc" DataValueField="grp_cd" AutoPostBack="true">
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_itmcd" runat="server" AssociatedControlID="txt_srch">SEARCH :</asp:Label>
                    </td> 
                    <td>
                        <asp:TextBox ID="txt_srch" runat="server" CssClass="form-control" placeholder="Check Item code name, Remarks, etc." Width="200px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td> 
                            <asp:Button ID="btn_refresh" runat="server" Text="PROCEED" 
                            CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
             </div>

                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_itmprclst" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display."   OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="itm_grp_desc" HeaderText="Item Group" ItemStyle-Width="180px"
                            ReadOnly="True" SortExpression="itm_grp_desc">
                            <ItemStyle Width="180px"></ItemStyle>
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="itm_pr_id" HeaderText="Item Code" ItemStyle-Width="80px"
                            SortExpression="itm_pr_id"  >
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="itm_pr_info" HeaderText="Item Name" ItemStyle-Width="100px"
                            SortExpression="item_name">
                            <ItemStyle Width="250px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="QTY"   HeaderText="Quantity" ItemStyle-Width="80px"
                            SortExpression="QTY">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_cost" HeaderText="Cost" ItemStyle-Width="80px"
                            SortExpression="itm_pr_cost">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_price" HeaderText="Sell Price" ItemStyle-Width="80px"
                            SortExpression="itm_pr_price">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_unt" HeaderText="Unit" ItemStyle-Width="80px"
                            SortExpression="itm_pr_unt">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_taxpct" HeaderText="Tax perc" ItemStyle-Width="80px"
                            SortExpression="itm_pr_taxpct">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_taxamt" HeaderText="Tax perc" ItemStyle-Width="80px"
                            SortExpression="itm_pr_taxamt">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_value" HeaderText="Cost Value" ItemStyle-Width="100px"
                            SortExpression="itm_pr_value">
                            <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                      
                <asp:HyperLinkField DataNavigateUrlFields="itm_pr_id" HeaderText="Details"
                    ControlStyle-Width="80px" DataNavigateUrlFormatString="Stock_Item_Dtls.aspx?item={0}"
                    Target="_blank" DataTextField="itm_pr_id" />
                    </Columns>
                </asp:GridView>
<asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-success"  OnClick = "ExportToExcel" />
            </div>
           
        </div>
    </div>
     

</asp:Content>

