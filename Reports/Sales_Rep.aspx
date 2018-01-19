<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"  EnableEventValidation="false"
     CodeFile="Sales_Rep.aspx.vb" Inherits="Report_Sales_Rep" %>

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
  <script>
      $(function () {
          $("#<%= txt_fdte.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
          $("#<%= txt_tdte.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
      });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page_header">
        <h1>SALES REPORTS
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
                    <td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_fdte">DATE RANGE :</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_fdte" runat="server" CssClass="form-control" Width="110px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_tdte" runat="server" AssociatedControlID="txt_tdte">TO :</asp:Label>
                    </td> 
                    <td>
                        <asp:TextBox ID="txt_tdte" runat="server" CssClass="form-control"  Width="110px"></asp:TextBox>
                    </td>
                    <td>
                    </td> 
                    <td>
                        <asp:Label ID="lbl_custid" runat="server" AssociatedControlID="dd_custid">CUSTOMER ID :</asp:Label>
                    </td> 
                    <td>
                        <asp:DropDownList ID="dd_custid" runat="server" CssClass="form-control"
                                    DataTextField="NAME" DataValueField="ID" AutoPostBack="true">
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_srch" runat="server" AssociatedControlID="txt_srch">SEARCH :</asp:Label>
                    </td> 
                    <td>
                        <asp:TextBox ID="txt_srch" runat="server" CssClass="form-control" placeholder="Check Invoice, Remarks, etc." Width="200px"></asp:TextBox>
                    </td>
                    <td>
                            <asp:Button ID="btn_refresh" runat="server" Text="PROCEED" 
                            CssClass="btn btn-primary" />
                    </td>
                    <td>  
                    </td>
                </tr>
            </table>
             </div>
                <br />
                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_Sales_rep" runat="server" AutoGenerateColumns="False" 
                    ShowFooter="true"   FooterStyle-ForeColor="Black" FooterStyle-Font-Bold="true"
                    EmptyDataText="There are no data records to display."   OnDataBound="OnDataBound" >                 
                    <Columns>
                        <asp:BoundField DataField="sr_guid" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="sr_guid" Visible="false">
                            <ItemStyle Width="40px"></ItemStyle>                           
                        </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="sr_inv_id" HeaderText="INVOICE NO" ItemStyle-Width="80px"
                    ControlStyle-Width="80px" DataNavigateUrlFormatString="Sales_Details_Rep.aspx?invno={0}"
                    Target="_blank" DataTextField="sr_inv_id" />
                        <%--<asp:BoundField DataField="sr_inv_id" HeaderText="INVOICE NO" ItemStyle-Width="100px"
                            SortExpression="sr_inv_id">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="sr_date_time" HeaderText="DATE" ItemStyle-Width="80px"
                            SortExpression="sr_date_time" DataFormatString="{0:d}" FooterText="Count:">
                            <ItemStyle Width="80px"></ItemStyle>
                            <FooterStyle  Width="100px" HorizontalAlign="Right"></FooterStyle> 
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_cust_id" HeaderText="CUSTOMER ID" ItemStyle-Width="80px"
                            SortExpression="rcpt_typ">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_cust_name" HeaderText="CUSTOMER NAME" ItemStyle-Width="200px"
                            SortExpression="rcpt_ref_id" FooterText="Total:">
                            <ItemStyle Width="200px"></ItemStyle>
                            <FooterStyle   HorizontalAlign="Right"></FooterStyle>                            
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_tot_amt" HeaderText="AMOUNT" ItemStyle-Width="80px"
                            SortExpression="rcpt_frm_typ">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle  Width="80px" HorizontalAlign="Right"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_disc" HeaderText="DISCOUNT" ItemStyle-Width="80px"
                            SortExpression="rcpt_frm_acc">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle  Width="80px" HorizontalAlign="Right"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_tax_amt" HeaderText="TAX" ItemStyle-Width="80px"
                            SortExpression="sr_tax_amt">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle  Width="80px" HorizontalAlign="Right"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_oth_amt" HeaderText="OTHER" ItemStyle-Width="80px"
                            SortExpression="sr_oth_amt">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle  Width="80px" HorizontalAlign="Right"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_net_amt" HeaderText="NETT" ItemStyle-Width="80px"
                            SortExpression="sr_net_amt">
                            <ItemStyle Width="80px" HorizontalAlign="Right"></ItemStyle>
                            <FooterStyle  Width="80px" HorizontalAlign="Right"></FooterStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_remark" HeaderText="REMARK" ItemStyle-Width="200px"
                            SortExpression="sr_remark">
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>
                       <%-- <asp:BoundField DataField="sr_paid_by1" HeaderText="PAID BY[1]" ItemStyle-Width="80px"
                            SortExpression="sr_paid_by1">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_amt1" HeaderText="AMT[1]" ItemStyle-Width="80px"
                            SortExpression="sr_amt1">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_rmk1" HeaderText="RMK[1]" ItemStyle-Width="80px"
                            SortExpression="sr_rmk1">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_paid_by2" HeaderText="PAID BY[2]" ItemStyle-Width="80px"
                            SortExpression="sr_paid_by2">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_amt2" HeaderText="AMT[2]" ItemStyle-Width="80px"
                            SortExpression="sr_amt2">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_rmk2" HeaderText="RMK[2]" ItemStyle-Width="80px"
                            SortExpression="sr_rmk2">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_crd_flg" HeaderText="CREDIT" ItemStyle-Width="80px"
                            SortExpression="sr_crd_flg">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="sr_cr_amt" HeaderText="CR AMT" ItemStyle-Width="80px"
                            SortExpression="sr_cr_amt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>--%>
                       <%-- <asp:TemplateField>
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

