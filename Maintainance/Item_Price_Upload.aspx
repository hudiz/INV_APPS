<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Item_Price_Upload.aspx.vb" Inherits="Mntnc_Item_Price_Upload" %>

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
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="accountInfo">
            <div class="divfieldset">                  
                <div>
                     <asp:Label ID="itmgrplbl" runat="server" >File Upload:</asp:Label>
                     <table>
                        <tr>

                            <td>
                               <asp:FileUpload id="FileUploadControl" runat="server" Width="250px"  />
                            </td>
                            <td  >
                        <asp:Button  ID="btn_upload" runat="server"  CssClass="btn btn-primary"  text="Upload" OnClick="UploadButton_Click"  /> 
                            
                        <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning" />
                        <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                            </td>
                        </tr>
                    </table>
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
                        <asp:BoundField DataField="itm_pr_id" HeaderText="CODE" ItemStyle-Width="80px"
                            SortExpression="itm_pr_id"  >
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_info" HeaderText="ITEM NAME" ItemStyle-Width="100px"
                            SortExpression="itm_pr_info">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_grp" HeaderText="ITEM GROUP" ItemStyle-Width="100px"
                            SortExpression="itm_pr_grp">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_cost" HeaderText="COST PRICE" ItemStyle-Width="80px"
                            SortExpression="itm_pr_cost">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_price" HeaderText="SELL PRICE" ItemStyle-Width="80px"
                            SortExpression="itm_pr_price">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_unt" HeaderText="UNIT" ItemStyle-Width="80px"
                            SortExpression="itm_pr_unt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_taxpct" HeaderText="TAX %" ItemStyle-Width="80px"
                            SortExpression="itm_pr_taxpct">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="itm_pr_taxamt" HeaderText="TAX AMT" ItemStyle-Width="80px"
                            SortExpression="itm_pr_taxamt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                      <%--  <asp:TemplateField>
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
    
</asp:Content>

