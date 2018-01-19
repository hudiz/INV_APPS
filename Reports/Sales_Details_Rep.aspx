<%@ Page Title="" Language="VB" MasterPageFile="~/Site_login.master" AutoEventWireup="false"
    CodeFile="Sales_Details_Rep.aspx.vb" Inherits="Report_Sales_Details_Rep" %>

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
                        <script type="text/javascript">
                            function returnback() { 
                                var qrystr = "../Transaction/Sales_Details.aspx"; 
                                window.open(qrystr, '');
                                window.close();
                            }
                        </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="page_header">
            <h1>
                SALES DETAILS
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
                        <asp:TextBox ID="txt_invno" runat="server" CssClass="form-control" AutoPostBack="true"  ReadOnly="true" Width="110px"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_dte">INVOICE DATE</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_dte" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_custid" runat="server" AssociatedControlID="dd_custid">CUSTOMER ID</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="dd_custid" runat="server" CssClass="form-control" AutoPostBack="true"
                            DataTextField="cname" DataValueField="cid" ReadOnly="true">
                        </asp:DropDownList>
                        <%-- <asp:TextBox ID="txt_custid" runat="server" CssClass="form-control"></asp:TextBox>--%>
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
                        <asp:TextBox ID="txt_custname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <%--<asp:HyperLink ID="hpl_history" runat="server" onclick="return history_details_of_customer();"
                            Text="History"></asp:HyperLink>
                        <script>
                            function history_details_of_customer() {
                                var ele = document.getElementById("<%=hpl_history.ClientID%>");
                                var ddval = ele.options[ele.selectedIndex].value;
                                var qrystr = "../Transaction/history_Details.aspx?Custid=" + ddval.trim() + "";
                                window.open(qrystr);
                            }
                        </script>--%>
                    </td>
                </tr>
            </table>
            <table class="sales_tab">
                <tr>
                    <td colspan="9">
                        <div style="width: 100%;">
                           <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                            ID="gv_sales" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display." 
                               ShowFooter="true"  FooterStyle-BackColor="DarkGray"  FooterStyle-ForeColor="Black" FooterStyle-Font-Bold="true"  OnDataBound="OnDataBound" >    
                            <Columns>
                                <asp:BoundField DataField="sr_itm_id" HeaderText="CODE"   ReadOnly="True" SortExpression="pr_itm_qty">
                                    <ItemStyle Width="70px" ></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_name" HeaderText="ITEM" ReadOnly="True" SortExpression="sr_itm_name">
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:BoundField>
                                    <asp:BoundField DataField="itm_pr_rmk" HeaderText="HSN/SAC" ReadOnly="True" SortExpression="sr_itm_id">
                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_qty" HeaderText="QTY."   ReadOnly="True" SortExpression="sr_itm_qty">
                                    <ItemStyle Width="60px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="unt" HeaderText="UNIT" ReadOnly="True" SortExpression="unt">
                                    <ItemStyle Width="60px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_price" HeaderText="PRICE" ReadOnly="True" SortExpression="sr_itm_price">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_amt" HeaderText="AMOUNT" ReadOnly="True" SortExpression="sr_itm_amt">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_disc" HeaderText="DISC." ReadOnly="True" SortExpression="sr_itm_disc">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_taxpct" HeaderText="TAX%" ReadOnly="True" SortExpression="sr_itm_taxpct">
                                    <ItemStyle Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="sr_itm_taxamt" HeaderText="TAX AMT." ReadOnly="True" SortExpression="sr_itm_taxamt">
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
                    <td rowspan="8">
                        <div style="width:100px;"></div>
                    </td>
                    <td>
                        <asp:Label ID="lbl_tot" runat="server" AssociatedControlID="txt_tot" Text="TOTAL AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_tot" runat="server" Width="100px" CssClass="form-control" Text="0.000"
                            onchange="return change_tot_amt_process();" ReadOnly="true"></asp:TextBox>
                        <script>
                            function change_tot_amt_process() {

                                var amt = document.getElementById("<%=txt_tot.ClientID %>").value.trim();
                                var disc = document.getElementById("<%=txt_netdisc.ClientID %>").value.trim();
                                var tax1 = document.getElementById("<%=txt_CGSTtax.ClientID %>").value.trim();
                                var tax2 = document.getElementById("<%=txt_SGSTtax.ClientID %>").value.trim();
                                var oth = document.getElementById("<%=txt_netoth.ClientID %>").value.trim();

                                amt = (amt == "") ? "0.000" : amt;
                                disc = (disc == "") ? "0.000" : disc;
                                tax1 = (tax1 == "") ? "0.000" : tax1;
                                tax2 = (tax2 == "") ? "0.000" : tax2;
                                oth = (oth == "") ? "0.000" : oth;

                                var nettot = parseFloat(amt) + parseFloat(tax1) + parseFloat(tax2) + parseFloat(oth) - parseFloat(disc);
                                document.getElementById("<%=txt_nettot.ClientID %>").value = nettot;
                                document.getElementById("<%=txt_pdbyamt1.ClientID %>").value = nettot;
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
                        <asp:RadioButtonList ID="rbl_credit" runat="server" RepeatDirection="Horizontal"
                            Width="150px" ReadOnly="true">
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_credit_amt" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb1');" ReadOnly="true"></asp:TextBox>
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
                            Text="0.000" onchange="return change_tot_amt_process();" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby1" AssociatedControlID="dd_pdbyname1" runat="server" Text="PAID BY-1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="dd_pdbyname1" runat="server" Width="230px" CssClass="form-control"
                            AutoPostBack="true" DataValueField="acc_id" DataTextField="acc_name" ReadOnly="true">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txt_pdbyname1" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt1" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb1');" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk1" runat="server" Width="230px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax1" runat="server" AssociatedControlID="txt_CGSTtax" Text="CGST TAX"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGSTtax" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby2" runat="server" AssociatedControlID="dd_pdbyname2" Text="PAID BY-2"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="dd_pdbyname2" runat="server" Width="230px" CssClass="form-control"
                            AutoPostBack="true" DataValueField="acc_id" DataTextField="acc_name" ReadOnly="true">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txt_pdbyname2" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt2" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb2');" ReadOnly="true"></asp:TextBox>
                        <script>
                            function paid_amount_adjustment(adj_type) {
                                var netamt = document.getElementById("<%=txt_nettot.ClientID %>").value.trim();
                                var pdpayamt2 = document.getElementById("<%=txt_pdbyamt2.ClientID %>").value.trim();
                                var pdpayamt1 = document.getElementById("<%=txt_pdbyamt1.ClientID %>").value.trim();
                                netamt = (netamt == "") ? "0.000" : netamt;
                                pdpayamt2 = (pdpayamt2 == "") ? "0.000" : pdpayamt2;
                                pdpayamt1 = (pdpayamt1 == "") ? "0.000" : pdpayamt1;
                                if ((parseFloat(netamt) > 0) && (parseFloat(netamt) >= parseFloat(pdpayamt2)) && (adj_type == "pb2")) {
                                    var l_pdpayamt1 = parseFloat(netamt) - parseFloat(pdpayamt2);
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").value = l_pdpayamt1;
                                }
                                else if ((parseFloat(netamt) > 0) && (parseFloat(netamt) >= parseFloat(pdpayamt1)) && (adj_type == "pb1")) {
                                    var l_pdpayamt2 = parseFloat(netamt) - parseFloat(pdpayamt1);
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").value = l_pdpayamt2;
                                }
                                else {
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").value = "0.000";
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").value = "0.000";
                                }

                            }
                        </script>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk2" runat="server" Width="230px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax" runat="server" AssociatedControlID="txt_SGSTtax" Text="SGST TAX"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTtax" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_rmk" runat="server" AssociatedControlID="txt_rmk" Text="REMARKS"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_rmk" runat="server" Width="630px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netoth" runat="server" AssociatedControlID="txt_netoth" Text="OTH. AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_netoth" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="5">
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettot" runat="server" AssociatedControlID="txt_nettot" Text="NET AMT."></asp:Label>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:TextBox ID="txt_nettot" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                      <asp:Button ID="btn_print" runat="server" Text="PRINT" CssClass="btn btn-primary" />
                   <%--  OnClientClick="javascript:window.print()" </td>
                    <td>--%>
                      <asp:Button ID="Button1" runat="server" Text="BACK" CssClass="btn btn-success" OnClientClick="returnback();" />   
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="5">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>