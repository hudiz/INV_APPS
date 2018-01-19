<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeFile="Purchase_Details_Rep.aspx.vb" Inherits="Report_Purchase_Details_Rep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
    <div class="page_header">
        <h1>
            PURCHASE DETAILS REPORT
        </h1>
    </div>
        <div class="table-responsive">
            <table class="sales_tab">
                <tr>
                    <td>
                        <asp:Label ID="lbl_invno" runat="server" AssociatedControlID="txt_invno">INVOICE NO.</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="txt_invno" runat="server"  ></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_dte">DATE</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="txt_dte" runat="server" ></asp:Label>
                    </td>
                    <td>
                    </td>
                <%--</tr>
                <tr>--%>
                    <td>
                        <asp:Label ID="lbl_custid" runat="server" AssociatedControlID="txt_custid">SUPPLIER ID</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="txt_custid" runat="server" ></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_custname" runat="server" AssociatedControlID="txt_custname">SUPPLIER NAME</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:Label ID="txt_custname" runat="server" ></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table class="sales_tab">
                <tr>
                    <td colspan="9">
                    <div>
                        <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                            ID="gv_sales" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display.">
                            <Columns>
                                <asp:BoundField DataField="pr_itm_id" HeaderText="CODE"   ReadOnly="True" SortExpression="pr_itm_qty">
                                    <ItemStyle Width="70px" ></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_name" HeaderText="ITEM" ReadOnly="True" SortExpression="pr_itm_name">
                                    <ItemStyle Width="250px"></ItemStyle>
                                </asp:BoundField>
                                    <asp:BoundField DataField="itm_pr_rmk" HeaderText="HSN/SAC" ReadOnly="True" SortExpression="pr_itm_id">
                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_qty" HeaderText="QTY."  ReadOnly="True" SortExpression="pr_itm_qty">
                                    <ItemStyle Width="70px" ></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="unt" HeaderText="UNIT" ReadOnly="True" SortExpression="unt">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_price" HeaderText="PRICE" ReadOnly="True" SortExpression="pr_itm_price">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_amt" HeaderText="AMOUNT" ReadOnly="True" SortExpression="pr_itm_amt">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_disc" HeaderText="DISC." ReadOnly="True" SortExpression="pr_itm_disc">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_taxpct" HeaderText="TAX%" ReadOnly="True" SortExpression="pr_itm_taxpct">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="pr_itm_taxamt" HeaderText="TAX AMT." ReadOnly="True" SortExpression="pr_itm_taxamt">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_tot" runat="server" AssociatedControlID="txt_tot" Text="TOTAL AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_tot" runat="server" Width="100px"  Text="0.000"
                            onchange="return change_tot_amt_process();"></asp:Label>
                        <script>
                            function change_tot_amt_process() {

                                var amt = document.getElementById("<%=txt_tot.ClientID %>").value.trim();
                                var disc = document.getElementById("<%=txt_netdisc.ClientID %>").value.trim();
                                var tax = document.getElementById("<%=txt_nettax.ClientID %>").value.trim();
                                var oth = document.getElementById("<%=txt_netoth.ClientID %>").value.trim();

                                amt = (amt == "") ? "0.000" : amt;
                                disc = (disc == "") ? "0.000" : disc;
                                tax = (tax == "") ? "0.000" : tax;
                                oth = (oth == "") ? "0.000" : oth;

                                var nettot = parseFloat(amt) + parseFloat(tax) + parseFloat(oth) - parseFloat(disc);
                                document.getElementById("<%=txt_nettot.ClientID %>").value = nettot;
                            }
                        </script>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_credit" runat="server" AssociatedControlID="rbl_credit" Text="CREDIT"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rbl_credit" runat="server" 
                            RepeatDirection="Horizontal" Width="150px">
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netdisc" runat="server" AssociatedControlID="txt_netdisc" Text="DISC."></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_netdisc" runat="server" Width="100px" 
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby1" AssociatedControlID="txt_pdbyname1" runat="server" Text="PAID BY-1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="txt_pdbyname1" runat="server" Width="230px" 
                            ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_pdbyamt1" runat="server" Width="100px" Text="0.000" ></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="txt_pdbyrmk1" runat="server" Width="230px" 
                            ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax" runat="server" AssociatedControlID="txt_nettax" Text="TAX."></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_nettax" runat="server" Width="100px" 
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby2" runat="server" AssociatedControlID="txt_pdbyname2" Text="PAID BY-2"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="txt_pdbyname2" runat="server" Width="230px" 
                            ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_pdbyamt2" runat="server" Width="100px" Text="0.000" ></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="txt_pdbyrmk2" runat="server" Width="230px" 
                            ></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netoth" runat="server" AssociatedControlID="txt_netoth" Text="OTH. AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_netoth" runat="server" Width="100px" 
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="lbl_rmk" runat="server" AssociatedControlID="txt_rmk" Text="REMARKS"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="txt_rmk" runat="server" Width="630px" ></asp:Label>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:Label ID="lbl_nettot" runat="server" AssociatedControlID="txt_nettot" Text="NET AMT."></asp:Label>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:Label ID="txt_nettot" runat="server" Width="100px" 
                            Text="0.000"></asp:Label>
                    </td>
                    <td>
                            <asp:Button ID="btn_add_to_table" runat="server" Text="PROCEED" 
                            CssClass="btn btn-primary" />
                            <%--<input type="button" id="btn_add_to_table" value="PROCEED" class="btn btn-primary" onclick="return get_result();" />
                            <script>
                                function get_result() {
                                    var encrypted = CryptoJS.AES.encrypt("Sujith Kumar V.k", "Secret Passphrase");
                                    alert(encrypted);
                                    var decrypted = CryptoJS.AES.decrypt(encrypted, "Secret Passphrase");
                                    alert(decrypted + " : - Actual Content : - "+ decrypted.toString(CryptoJS.enc.Utf8));
                                }
                            </script>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="5">
                        &nbsp;</td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        &nbsp;</td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        &nbsp;</td>
                    <td>
                            <asp:Button ID="btn_print" runat="server" Text="PRINT" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
