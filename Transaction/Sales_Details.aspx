<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false"
    CodeFile="Sales_Details.aspx.vb" Inherits="Transaction_Sales_Details" %>

<%@ Register Assembly="ServerControl1" Namespace="ServerControl1" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .sales_tab tr {
            padding: 2px;
        }

            .sales_tab tr td {
                padding: 3px;
            }

        .test_css {
            display: none;
        }
    </style>
    <script type="text/javascript" src="../Scripts/md5.js"></script>
    <script src="../Scripts/aes.js"></script>
    <%--<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">--%>
    <link rel="stylesheet" href="../Scripts/jquery/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="../Scripts/jquery/jquery-1.12.4.js"></script>
    <script src="../Scripts/jquery/ui/1.12.1/jquery-ui.js"></script>



   <%-- <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
    <%--<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.css" />--%>
    <link rel="stylesheet" type="text/css" href="../Scripts/datatables/v/dt/dt-1.10.15/datatables.min.css" /> 
    <script type="text/javascript" src="../Scripts/datatables/v/dt/dt-1.10.15/datatables.min.js"></script>



    <script>
        $(function () {
            $("#<%= txt_dte.ClientID %>").datepicker({ dateFormat: 'dd/mm/yy' });
        });
        function LoadCurrentReport() {
            var jsnstr = document.getElementById("<%=jsonsrv.ClientID %>");
             var oResults = JSON.parse(jsnstr.attributes.jsonstring.nodeValue);
             var aDemoItems = oResults; //
             var jsonString = JSON.stringify(aDemoItems)
             var oTblReport = $("#example");

             oTblReport.DataTable({
                 "data": aDemoItems,
                 destroy: true,
                 scrollY: '50vh',
                 scrollCollapse: true,
                 paging: false,
                 info:false,
                 "columns": [
                     { "data": "itmval" },
                     { "data": "itmtxt" },
                     { "data": "itm_pr_unt" },
                     { "data": "itm_pr_price" },
                     { "data": "itm_pr_taxpct" },
                     { "data": "itm_pr_cost" },
                     { "data": "qty" }
                 ]
             });
         }
         $(document).ready(function () {

             $("#example").on('click', 'tr', function () {
                 var data = $("#example").DataTable().row(this).data();
                 fill_data_to_controls(data);
                 $("#dialog").dialog('close');
             });
         });
         function fill_data_to_controls(data) {
             var qty1 = data["qty"];
             if (parseFloat(qty1) > 0) {
                 document.getElementById("<%=txt_qty.ClientID%>").value = "1";
             }
             else {
                 document.getElementById("<%=txt_qty.ClientID%>").value = "";
             }
             document.getElementById("<%=txt_unt.ClientID%>").value = data["itm_pr_unt"];
             document.getElementById("<%=txt_amt.ClientID%>").value = data["itm_pr_price"];
             document.getElementById("<%=txt_taxpct.ClientID%>").value = data["itm_pr_taxpct"];
             document.getElementById("<%=hid_costamt.ClientID%>").value = data["itm_pr_cost"];

             $("#<%=dd_itm.ClientID%>").val(data["itmval"].trim());

             var x_qty = document.getElementById("<%=txt_qty.ClientID%>");
             x_qty.title = qty1;

             var qty = document.getElementById("<%=txt_qty.ClientID%>").value;
             var amount = document.getElementById("<%=txt_amt.ClientID%>").value;
             var disc = document.getElementById("<%=txt_disc.ClientID%>").value;
             var taxpct = document.getElementById("<%=txt_taxpct.ClientID%>").value;
             var cost = document.getElementById("<%=hid_costamt.ClientID%>").value;

             qty = (qty.trim() == "") ? "0.000" : qty;
             amount = (amount.trim() == "") ? "0.000" : amount;
             disc = (disc.trim() == "") ? "0.000" : disc;
             taxpct = (taxpct.trim() == "") ? "0.000" : taxpct;
             cost = (cost.trim() == "") ? "0.000" : cost;

             var netamtded = (parseFloat(qty) * parseFloat(amount)) - parseFloat(disc);
             var netcostded = (parseFloat(qty) * parseFloat(cost));
             var nettax = (netcostded * parseFloat(taxpct)) / 100;
             var nettot = (parseFloat(qty) * parseFloat(amount));

             document.getElementById("<%=txt_taxamt.ClientID%>").value = nettax;
             document.getElementById("<%=txt_netamt.ClientID%>").value = nettot;
             document.getElementById("<%=hid_costamt.ClientID%>").value = cost;
             
         }

        function fill_data_Updated_qty(act_qty) {
            var qty1 = act_qty.value;
            var x_qty = document.getElementById("<%=txt_qty.ClientID%>");
            // alert(act_qty.title + " -  qty = " + qty1 + " - " + parseFloat(act_qty.title) >= parseFloat(qty1));

            //document.getElementById("<%=txt_qty.ClientID%>").value = 0;
            if (parseFloat(act_qty.title) >= parseFloat(qty1)) {
                document.getElementById("<%=txt_qty.ClientID%>").value = parseFloat(qty1);
            } else {
                if (parseFloat(act_qty.title) < parseFloat(qty1)) {
                    document.getElementById("<%=txt_qty.ClientID%>").value = parseFloat(act_qty.title);
                }
            }

            var qty = document.getElementById("<%=txt_qty.ClientID%>").value;
            var amount = document.getElementById("<%=txt_amt.ClientID%>").value;
            var disc = document.getElementById("<%=txt_disc.ClientID%>").value;
            var taxpct = document.getElementById("<%=txt_taxpct.ClientID%>").value;
            var cost = document.getElementById("<%=hid_costamt.ClientID%>").value;

            qty = (qty.trim() == "") ? "0.000" : qty;
            amount = (amount.trim() == "") ? "0.000" : amount;
            disc = (disc.trim() == "") ? "0.000" : disc;
            taxpct = (taxpct.trim() == "") ? "0.000" : taxpct;
            cost = (cost.trim() == "") ? "0.000" : cost;

            var netamtded = (parseFloat(qty) * parseFloat(amount)) - parseFloat(disc);
            var netcostded = (parseFloat(qty) * parseFloat(cost)) ;
            var nettax = (netcostded * parseFloat(taxpct)) / 100;
            var nettot = (parseFloat(qty) * parseFloat(amount));

            document.getElementById("<%=txt_taxamt.ClientID%>").value = nettax;
            document.getElementById("<%=txt_netamt.ClientID%>").value = nettot;
            document.getElementById("<%=hid_costamt.ClientID%>").value = cost;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <asp:HiddenField ID="hid_chg_sts" runat="server" Value="" />
        <cc1:ServerControl1 ID="jsonsrv" runat="server" />
        <div id="dialog" title="Basic dialog" style="display: none;">
            <table id="example" width="100%" cellspacing="0" class="table table-bordered table-hover table-condensed table-striped">
                <thead>
                    <tr>
                        <th>ITEM CD
                        </th>
                        <th>DESCRIPTION
                        </th>
                        <th>UNIT
                        </th>
                        <th>PRICE
                        </th>
                        <th>TAX%
                        </th>
                        <th>COST
                        </th>
                        <th>QTY
                        </th>
                    </tr>
                </thead>
              <%--  <tfoot>
                    <tr>
                        <th>ITEM CD
                        </th>
                        <th>DESCRIPTION
                        </th>
                        <th>UNIT
                        </th>
                        <th>PRICE
                        </th>
                        <th>TAX%
                        </th>
                        <th>COST
                        </th>
                        <th>QTY
                        </th>
                    </tr>
                </tfoot>--%>
            </table>
        </div>
        <div class="page_header">
            <h1>SALES DETAILS
            </h1>
        </div>
        <div class="table-responsive">
            <table class="sales_tab">
                <tr>
                    <td>
                        <asp:Label ID="lbl_invno" runat="server" AssociatedControlID="txt_invno">INVOICE NO.</asp:Label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_invno" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true" Width="110px"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lbl_dte" runat="server" AssociatedControlID="txt_dte">INVOICE DATE</asp:Label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_dte" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_custid" runat="server" AssociatedControlID="dd_custid">CUSTOMER ID</asp:Label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="dd_custid" runat="server" CssClass="form-control" 
                            DataTextField="cname" DataValueField="cid"><%--AutoPostBack="true"--%>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lbl_custname" runat="server" AssociatedControlID="txt_custname">CUSTOMER NAME.</asp:Label>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_custname" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:HyperLink ID="hpl_addnwcust" runat="server" onclick="return add_new_customer();" CssClass="btn btn-success">
                            <asp:Label ID="Label1" runat="server">Add New Customer</asp:Label>

                        </asp:HyperLink>
                        <script type="text/javascript">
                            function add_new_customer() {
                                var iele = document.getElementById("<%=txt_invno.ClientID%>");
                                var qrystr = "../Maintainance/Customer_details.aspx?invno=" + iele.value.trim();
                                //alert(qrystr);
                                window.open(qrystr, "_self");
                            }
                        </script>
                    </td>
                </tr>
            </table>
            <table class="sales_tab">
                <tr>
                    <td>
                        <%--<asp:Label ID="lbl_itm" runat="server" AssociatedControlID="dd_itm" >ITEM</asp:Label>--%>
                        <asp:HyperLink ID="hpl_history" runat="server" onclick="return history_details_of_customer();">
                            <asp:Label ID="lbl_itm" runat="server" AssociatedControlID="dd_itm" ToolTip="Check item history for the Customer.">ITEM</asp:Label>
                        </asp:HyperLink>
                        <script type="text/javascript">
                            function history_details_of_customer() {
                                var cele = document.getElementById("<%=dd_custid.ClientID%>");
                                var iele = document.getElementById("<%=dd_itm.ClientID%>");
                                //var ddval = ele.options[ele.selectedIndex].value;
                                //alert(cele.value + " - " + iele.value);
                                var qrystr = "../Transaction/history_Details.aspx?custid=" + cele.value.trim() + "&itemid=" + iele.value.trim();
                                //alert(qrystr);
                                window.open(qrystr);
                            }
                        </script>
                         <asp:TextBox ID="dd_itm" runat="server" Width="150px" CssClass="form-control" AutoPostBack="false" onclick="change_amt_process1();"></asp:TextBox>
                       
                       <%-- <asp:DropDownList ID="dd_itm" runat="server" DataTextField="itmtxt" DataValueField="itmval"
                            Width="100px" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>--%>
                        <%--<asp:TextBox ID="txt_itm" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:Label ID="lbl_qty" runat="server" AssociatedControlID="txt_qty">QTY.</asp:Label>
                        <asp:TextBox ID="txt_qty" runat="server" Width="100px" CssClass="form-control" onchange="fill_data_Updated_qty(this);" ></asp:TextBox>
                        <script type="text/javascript">
                            function change_amt_process() {

                                var qty = document.getElementById("<%=txt_qty.ClientID %>").value.trim();
                                var amt = document.getElementById("<%=txt_amt.ClientID %>").value.trim();
                                var tax = document.getElementById("<%=txt_taxamt.ClientID %>").value.trim();

                                qty = (qty == "") ? "0.000" : qty;
                                amt = (amt == "") ? "0.000" : amt;
                                tax = (tax == "") ? "0.000" : tax;

                                var nettot = (parseFloat(qty) * parseFloat(amt));
                                //+parseFloat(tax);
                                document.getElementById("<%=txt_netamt.ClientID %>").value = nettot;
                                change_tax_pct();
                            }
                            function change_amt_process1() {
                                
                                var custid = document.getElementById("<%=dd_custid.ClientID %>").value;
                                if(custid.trim().length > 0) {
                                    $("#dialog").dialog({
                                        width: 1000,
                                        title: "ITEMS LIST"
                                    });
                                    LoadCurrentReport();
                                } else {
                                    alert('Please select Customer !! ');
                                }
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
                        <script type="text/javascript">
                            function change_tax_pct() {
                                var qty = document.getElementById("<%=txt_qty.ClientID %>").value.trim();
                                var amt = document.getElementById("<%=txt_amt.ClientID %>").value.trim();
                                var taxpct = document.getElementById("<%=txt_taxpct.ClientID %>").value.trim();
                                var disc = document.getElementById("<%=txt_disc.ClientID %>").value.trim();
                                var cost = document.getElementById("<%=hid_costamt.ClientID%>").value;

                                qty = (qty == "") ? "0.000" : qty;
                                amt = (amt == "") ? "0.000" : amt;
                                taxpct = (taxpct == "") ? "0.000" : taxpct;
                                disc = (disc == "") ? "0.000" : disc;

                                var netamtded = (parseFloat(qty) * parseFloat(amt)) - parseFloat(disc);
                                var netcostded = (parseFloat(qty) * parseFloat(cost)) ;
                                var nettax = (netcostded * parseFloat(taxpct)) / 100;
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
                        <div style="height: 15px;"></div>
                        <div class="submitButton">
                            <asp:Button ID="addButton" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_add_Changed" />
                            <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning" />
                            <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger" />
                            <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                        <div style="width: 100%; max-height: 250px; overflow: auto; border: 1px solid #ddd;">
                            <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                                ID="gv_sales" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display."
                                OnSelectedIndexChanged="OnSelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="item" HeaderText="ITEM" ReadOnly="True" SortExpression="item">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="itemname" HeaderText="DESCRIPTION" ReadOnly="True" SortExpression="itemname">
                                        <ItemStyle Width="200px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="hsnno" HeaderText="HSN/SAC" ReadOnly="True" SortExpression="hsnno">
                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="qty" HeaderText="QTY."  DataFormatString="{0:N0}"  ReadOnly="True" SortExpression="qty">
                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="unit" HeaderText="UNIT" ReadOnly="True" SortExpression="unit">
                                        <ItemStyle Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="amount" HeaderText="PRICE" ReadOnly="True" SortExpression="amount">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="netamt" HeaderText="AMOUNT" ReadOnly="True" SortExpression="netamt">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="disc" HeaderText="DISC." ReadOnly="True" SortExpression="disc">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="taxpct" HeaderText="TAX%" ReadOnly="True" SortExpression="taxpct">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="taxamt" HeaderText="TAX AMT." ReadOnly="True" SortExpression="taxamt">
                                        <ItemStyle Width="70px"></ItemStyle>
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
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td rowspan="8">
                        <div style="width:100px;"></div>
                    </td>
                    <td>
                        <asp:Label ID="lbl_tot" runat="server" AssociatedControlID="txt_tot" Text="TOTAL AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_tot" runat="server" Width="100px" CssClass="form-control" Text="0.000"
                            onchange="return change_tot_amt_process();"></asp:TextBox>
                        <script type="text/javascript">
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

                                change_credit_amt_process();
                            }
                        </script>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_credit" runat="server" AssociatedControlID="rbl_credit" Text="CREDIT"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:RadioButtonList ID="rbl_credit" runat="server" RepeatDirection="Horizontal"
                            Width="150px" onchange="return change_credit_amt_process();" >
                            <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                            <asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                        <script type="text/javascript">
                            function change_credit_amt_process() {
                                //alert('test');
                                var radioButtonList = document.getElementById("<%=rbl_credit.ClientID%>");
                                var answer;
                                for (var i = 0; i < radioButtonList.rows.length; ++i) {
                                    if (radioButtonList.rows[i].cells[0].firstChild.checked) {
                                        answer = "Y";
                                    }
                                    if (radioButtonList.rows[i].cells[1].firstChild.checked) {
                                        answer = "N";
                                    }
                                }
                                //alert(answer);
                                var crdt = answer;

                                var tot = document.getElementById("<%=txt_nettot.ClientID %>").value.trim(); 
                                var nettot = document.getElementById("<%=txt_nettot.ClientID %>").value.trim(); 

                                tot = (tot == "") ? "0.000" : tot;
                                tot = (crdt == "Y") ? tot : "0.000";
                                nettot = (crdt == "N") ? nettot : "0.000";
                                 
                                document.getElementById("<%=txt_credit_amt.ClientID %>").value = tot;
                                document.getElementById("<%=txt_pdbyamt1.ClientID %>").value = nettot;
                                document.getElementById("<%=txt_pdbyamt2.ClientID %>").value = 0; 
                                if (crdt == "Y") {
                                    document.getElementById("<%=txt_credit_amt.ClientID %>").disabled = false;
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").disabled = true;
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").disabled = true;
                                }
                                else {
                                    document.getElementById("<%=txt_credit_amt.ClientID %>").disabled = true;
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").disabled = false;
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").disabled = false;
                                }
                            }
                        </script>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_credit_amt" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb1');"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Label ID="lbl_netdisc" runat="server" AssociatedControlID="txt_netdisc" Text="DISC."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_netdisc" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby1" AssociatedControlID="dd_pdbyname1" runat="server" Text="PAID BY-1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="dd_pdbyname1" runat="server" Width="230px" CssClass="form-control"
                            AutoPostBack="true" DataValueField="acc_id" DataTextField="acc_name">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txt_pdbyname1" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt1" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb1');"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk1" runat="server" Width="230px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax1" runat="server" AssociatedControlID="txt_CGSTtax" Text="CGST TAX"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGSTtax" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_paidby2" runat="server" AssociatedControlID="dd_pdbyname2" Text="PAID BY-2"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="dd_pdbyname2" runat="server" Width="230px" CssClass="form-control"
                            AutoPostBack="true" DataValueField="acc_id" DataTextField="acc_name">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txt_pdbyname2" runat="server" Width="230px" 
                            CssClass="form-control"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_pdbyamt2" runat="server" Width="100px" Text="0.000" CssClass="form-control"
                            onchange="return paid_amount_adjustment('pb2');"></asp:TextBox>
                        <script type="text/javascript">
                            function paid_amount_adjustment(adj_type) {
                                 var radioButtonList = document.getElementById("<%=rbl_credit.ClientID%>");
                                var answer;
                                for (var i = 0; i < radioButtonList.rows.length; ++i) {
                                    if (radioButtonList.rows[i].cells[0].firstChild.checked) {
                                        answer = "Y";
                                    }
                                    if (radioButtonList.rows[i].cells[1].firstChild.checked) {
                                        answer = "N";
                                    }
                                }
                                //alert(answer);
                                var crdt = answer;

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

                                if (crdt == "Y") {
                                    document.getElementById("<%=txt_credit_amt.ClientID %>").disabled = false;
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").disabled = true;
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").disabled = true;

                                    document.getElementById("<%=txt_credit_amt.ClientID %>").value = netamt;
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").value = 0;
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").value = 0;
                                }
                                else {
                                    document.getElementById("<%=txt_credit_amt.ClientID %>").disabled = true;
                                    document.getElementById("<%=txt_pdbyamt1.ClientID %>").disabled = false;
                                    document.getElementById("<%=txt_pdbyamt2.ClientID %>").disabled = false;

                                    document.getElementById("<%=txt_credit_amt.ClientID %>").value = 0;
                                }
                        }
                        </script>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txt_pdbyrmk2" runat="server" Width="230px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_nettax" runat="server" AssociatedControlID="txt_SGSTtax" Text="SGST TAX"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTtax" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_rmk" runat="server" AssociatedControlID="txt_rmk" Text="REMARKS"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_rmk" runat="server" Width="630px" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbl_netoth" runat="server" AssociatedControlID="txt_netoth" Text="OTH. AMT."></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_netoth" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000" onchange="return change_tot_amt_process();"></asp:TextBox>
                    </td>
                    <td></td>
                    
                </tr>
                <tr>
                    <td> 
                    </td>
                    <td colspan="5"> 
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:Label ID="lbl_nettot" runat="server" AssociatedControlID="txt_nettot" Text="NET AMT."></asp:Label>
                    </td>
                    <td style="border-top: solid 2px #ccc; border-bottom: solid 2px #ccc;">
                        <asp:TextBox ID="txt_nettot" runat="server" Width="100px" CssClass="form-control"
                            Text="0.000"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_add_to_table" runat="server" Text="PROCEED" CssClass="btn btn-primary" />
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
                    <td>&nbsp;
                    </td>
                    <td colspan="5">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btn_print" runat="server" Text="PRINT" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
