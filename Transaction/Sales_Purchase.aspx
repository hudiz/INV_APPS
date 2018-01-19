<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeFile="Sales_Purchase.aspx.vb" Inherits="Transaction_Sales_Purchase" %>

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
      <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
      $(function () {
          $("#<%= txt_dte.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
      });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="table-responsive">
            <table class="sales_tab">
                <tr>
                    <td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_dte">DATE</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_dte" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_invno" runat="server" AssociatedControlID="txt_invno">INVOICE NO.</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_invno" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_custid" runat="server" AssociatedControlID="txt_custid">CUSTOMER ID</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_custid" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_custname" runat="server" AssociatedControlID="txt_custname">CUSTOMER NAME.</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_custname" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table class="sales_tab">
                <tr>
                    <td>
                        <asp:Label ID="lbl_itm" runat="server" AssociatedControlID="dd_itm">ITEM</asp:Label>
                        <asp:DropDownList ID="dd_itm" runat="server" DataTextField="itmtxt" DataValueField="itmval"
                            Width="100px" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txt_itm" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:Label ID="lbl_qty" runat="server" AssociatedControlID="txt_qty">QTY.</asp:Label>
                        <asp:TextBox ID="txt_qty" runat="server" Width="100px" CssClass="form-control" onchange="return change_amt_process();"></asp:TextBox>
                        <script>
                            function change_amt_process() {
                                var qty = document.getElementById("<%=txt_qty.ClientID %>").value.trim();
                                var amt = document.getElementById("<%=txt_amt.ClientID %>").value.trim();
                                var tax = document.getElementById("<%=txt_taxamt.ClientID %>").value.trim();

                                qty = (qty == "") ? "0.000" : qty;
                                amt = (amt == "") ? "0.000" : amt;
                                tax = (tax == "") ? "0.000" : tax;

                                var nettot = (parseFloat(qty) * parseFloat(amt)) + parseFloat(tax);
                                document.getElementById("<%=txt_netamt.ClientID %>").value = nettot;
                                change_tax_pct();
                            }
                        </script>
                    </td>
                    <td>
                        <asp:Label ID="lbl_unt" runat="server" AssociatedControlID="txt_unt">UNIT</asp:Label>
                        <asp:TextBox ID="txt_unt" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_amt" runat="server" AssociatedControlID="txt_amt">PRICE</asp:Label>
                        <asp:TextBox ID="txt_amt" runat="server" Width="100px" CssClass="form-control" onchange="return change_amt_process();"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netamt" runat="server" AssociatedControlID="txt_netamt">AMOUNT</asp:Label>
                        <asp:TextBox ID="txt_netamt" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_disc" runat="server" AssociatedControlID="txt_disc">DISC.</asp:Label>
                        <asp:TextBox ID="txt_disc" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_taxpct" runat="server" AssociatedControlID="txt_taxpct">TAX%</asp:Label>
                        <asp:TextBox ID="txt_taxpct" runat="server" Width="100px" CssClass="form-control"
                            onchange="return change_tax_pct();"></asp:TextBox>
                        <script>
                            function change_tax_pct() {
                                var qty = document.getElementById("<%=txt_qty.ClientID %>").value.trim();
                                var amt = document.getElementById("<%=txt_amt.ClientID %>").value.trim();
                                var taxpct = document.getElementById("<%=txt_taxpct.ClientID %>").value.trim();
                                var disc = document.getElementById("<%=txt_disc.ClientID %>").value.trim();

                                qty = (qty == "") ? "0.000" : qty;
                                amt = (amt == "") ? "0.000" : amt;
                                taxpct = (taxpct == "") ? "0.000" : taxpct;
                                disc = (disc == "") ? "0.000" : disc;

                                var netamtded = (parseFloat(qty) * parseFloat(amt)) - parseFloat(disc);
                                var nettax = (netamtded * parseFloat(taxpct)) / 100;
                                document.getElementById("<%=txt_taxamt.ClientID %>").value = nettax;
                                change_amt_process();
                            }
                        </script>
                    </td>
                    <td>
                        <asp:Label ID="lbl_taxamt" runat="server" AssociatedControlID="txt_taxamt">TAX AMT.</asp:Label>
                        <asp:TextBox ID="txt_taxamt" runat="server" Width="100px" CssClass="form-control"
                            onchange="return change_amt_process();"></asp:TextBox>
                            <asp:HiddenField ID="hid_costamt" runat="server" />
                    </td>
                    <td>
                        <div class="submitButton">
                            <asp:Button ID="addButton" runat="server" Text="Add" CssClass="btn btn-primary" />
                            <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning" />
                            <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger" />
                            <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                    <div style="width: 100%; max-height: 250px; overflow: auto; border:1px solid #ddd;">
                        <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                            ID="gv_sales" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                            OnSelectedIndexChanged="OnSelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="item" HeaderText="ITEM" ReadOnly="True" SortExpression="item">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="qty" HeaderText="QTY." ReadOnly="True" SortExpression="qty">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="unit" HeaderText="UNIT" ReadOnly="True" SortExpression="unit">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="amount" HeaderText="PRICE" ReadOnly="True" SortExpression="amount">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="netamt" HeaderText="AMOUNT" ReadOnly="True" SortExpression="netamt">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="disc" HeaderText="DISC." ReadOnly="True" SortExpression="disc">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="taxpct" HeaderText="TAX%" ReadOnly="True" SortExpression="taxpct">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="taxamt" HeaderText="TAX AMT." ReadOnly="True" SortExpression="taxamt">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <asp:TextBox ID="txt_tot" runat="server" Width="100px" CssClass="form-control" Text="0.000"
                            onchange="return change_tot_amt_process();"></asp:TextBox>
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
                        <asp:TextBox ID="txt_netdisc" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby1" AssociatedControlID="txt_pdbyname1" runat="server" Text="PAID BY-1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyname1" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk1" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt1" runat="server" Width="100px" Text="0.000" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax" runat="server" AssociatedControlID="txt_nettax" Text="TAX."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_nettax" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby2" runat="server" AssociatedControlID="txt_pdbyname2" Text="PAID BY-2"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyname2" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk2" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt2" runat="server" Width="100px" Text="0.000" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netoth" runat="server" AssociatedControlID="txt_netoth" Text="OTH. AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_netoth" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="lbl_rmk" runat="server" AssociatedControlID="txt_rmk" Text="REMARKS"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_rmk" runat="server" Width="630px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:Label ID="lbl_nettot" runat="server" AssociatedControlID="txt_nettot" Text="NET AMT."></asp:Label>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:TextBox ID="txt_nettot" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000"></asp:TextBox>
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
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                            <asp:Button ID="btn_print" runat="server" Text="PRINT" CssClass="btn btn-primary" />
                    </td>
            </table>
        </div>
    </div>
</asp:Content>
