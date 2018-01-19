<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master"   EnableEventValidation="false" AutoEventWireup="false" CodeFile="Receipts_trx.aspx.vb" Inherits="Trans_Receipts_trx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../Scripts/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/quicksearch.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.search_textbox').each(function (i) {
                $(this).quicksearch("[id*=gv_rcpt_list] tr:not(:has(th))", {
                    'testQuery': function (query, txt, row) {
                        return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
                    }
                });
            });
        });
    </script> 
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page_header">
        <h1>RECEIPTS DETAILS
        </h1>
    </div>
    
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
                        <asp:Label ID="lbl_rcptno" runat="server" AssociatedControlID="txt_rcptno">RECEIPT NO.</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_rcptno" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td> 
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_rcpttype" runat="server" AssociatedControlID="ddl_rcpttype">TYPE</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td> <asp:DropDownList ID="ddl_rcpttype" runat="server" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem Text="MISCELLANEOUS" Value="M"></asp:ListItem> 
                                    <asp:ListItem Text="EXPENSES" Value="E"></asp:ListItem> 
                                    <asp:ListItem Text="TRANSFERS" Value="T"></asp:ListItem> 
                                    <asp:ListItem Text="SALES" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="PURCHASE" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="SALES RETURN" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="PURCHASE RETURN" Value="R"></asp:ListItem>
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_refid" runat="server" AssociatedControlID="txt_refid">REFERENCE ID</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_refid" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_sourcetype" runat="server" AssociatedControlID="ddl_sourcetype">SOURCE</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_sourcetype" runat="server" CssClass="form-control"  AutoPostBack="true">
                                    <asp:ListItem Text="CUSTOMER" Value="C"></asp:ListItem> 
                                    <asp:ListItem Text="SUPPLIER" Value="S"></asp:ListItem> 
                                    <asp:ListItem Text="ACCOUNT" Value="A"></asp:ListItem> 
                                    <asp:ListItem Text="EXPENSE" Value="E"></asp:ListItem> 
                                    <asp:ListItem Text="STAFF" Value="T"></asp:ListItem> 
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                   
                    <td>
                        <asp:Label ID="lbl_sourceinfo" runat="server" AssociatedControlID="ddl_sourceinfo">SOURCE INFO</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_sourceinfo" runat="server" CssClass="form-control" 
                                    DataTextField="NAME" DataValueField="ID"  AutoPostBack="true"> 
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td colspan="3">                       
                        <asp:TextBox ID="txt_sourceinfo" runat="server" CssClass="form-control"></asp:TextBox>    
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_destintype" runat="server" AssociatedControlID="ddl_destintype">DESTINATION</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td> <asp:DropDownList ID="ddl_destintype" runat="server" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Text="ACCOUNT" Value="A"></asp:ListItem> 
                                    <asp:ListItem Text="CUSTOMER" Value="C"></asp:ListItem> 
                                    <asp:ListItem Text="SUPPLIER" Value="S"></asp:ListItem> 
                                    <asp:ListItem Text="EXPENSE" Value="E"></asp:ListItem> 
                                    <asp:ListItem Text="STAFF" Value="T"></asp:ListItem> 
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_destininfo" runat="server" AssociatedControlID="ddl_destininfo">DESTINATION INFO</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td> <asp:DropDownList ID="ddl_destininfo" runat="server" CssClass="form-control" 
                                    DataTextField="NAME" DataValueField="ID"   AutoPostBack="true"> 
                                </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    
                    <td colspan="3">                       
                        <asp:TextBox ID="txt_destininfo" runat="server" CssClass="form-control"></asp:TextBox>    
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr> <td>
                        <asp:Label ID="lbl_remarks" runat="server" AssociatedControlID="txt_remarks">REMARKS</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td colspan="5" rowspan="2">
                        <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                    </td> 
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_paidamt" runat="server" AssociatedControlID="txt_paidamt">PAID AMOUNT</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_paidamt" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr> <td>
                         
                    </td>
                    <td>
                       
                    </td>  
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_rcvdamt" runat="server" AssociatedControlID="txt_rcvdamt">RECEIVED AMOUNT</asp:Label>
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="txt_rcvdamt" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                    </td>
                    <td colspan="8">
                <div class="submitButton">
                    <asp:Button ID="CreateUserButton" runat="server" Text="Add" CssClass="btn btn-primary"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="UpdateUserButton" runat="server" Text="Update" CssClass="btn btn-warning"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="DeleteUserButton" runat="server" Text="Delete" CssClass="btn btn-danger"
                        ValidationGroup="RegisterUserValidationGroup" />
                    <asp:Button ID="CancelUserButton" runat="server" Text="Cancel" CssClass="btn btn-success" />
                </div>  
                    </td>
                </tr>
            </table>
            </div> 

    <div class="col-lg-12 col-md-12 col-sm-12">
        
        <div class="expensedtls">
            <div class="table-responsive">
                
                <asp:Label ID="txt_rcptgid" runat="server" Text="" Visible="false"
                    ForeColor="White" BackColor="Black"
                    Style="float: right; padding: 2px; border-radius: 5px; margin-top: -10px;"
                    Font-Bold="true"></asp:Label>

                <asp:GridView CssClass="table table-bordered table-hover table-condensed table-striped"
                    ID="gv_rcpt_list" runat="server" AutoGenerateColumns="False"
                    EmptyDataText="There are no data records to display." 
                    ShowFooter="true" FooterStyle-BackColor="DarkGray" FooterStyle-ForeColor="Black" FooterStyle-Font-Bold="true"
                      OnSelectedIndexChanged="OnSelectedIndexChanged"  OnDataBound="OnDataBound">
                    <Columns>
                        <asp:BoundField DataField="rcpt_gid" HeaderText="GID" ItemStyle-Width="80px"
                            ReadOnly="True" SortExpression="rcpt_gid">
                            <ItemStyle Width="40px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="createon" HeaderText="DATE" ItemStyle-Width="80px"
                            SortExpression="createon" DataFormatString="{0:d}">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_no" HeaderText="RECEIPT NO" ItemStyle-Width="100px"
                            SortExpression="rcpt_no">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_typ" HeaderText="RECEIPT TYPE" ItemStyle-Width="100px"
                            SortExpression="rcpt_typ">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_ref_id" HeaderText="REF ID" ItemStyle-Width="80px"
                            SortExpression="rcpt_ref_id">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_frm_typ" HeaderText="FROM TYPE" ItemStyle-Width="80px"
                            SortExpression="rcpt_frm_typ">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_frm_acc" HeaderText="FROM ACC" ItemStyle-Width="80px"
                            SortExpression="rcpt_frm_acc">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_frm_name" HeaderText="NAME" ItemStyle-Width="80px"
                            SortExpression="rcpt_frm_name">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_to_typ" HeaderText="TO TYPE" ItemStyle-Width="80px"
                            SortExpression="rcpt_to_typ">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_to_acc" HeaderText="TO ACC" ItemStyle-Width="80px"
                            SortExpression="rcpt_to_acc">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_to_name" HeaderText="NAME" ItemStyle-Width="80px"
                            SortExpression="rcpt_to_name">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_paid_amt" HeaderText="PAID" ItemStyle-Width="80px"
                            SortExpression="rcpt_paid_amt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_rcvd_amt" HeaderText="RECEIVED" ItemStyle-Width="80px"
                            SortExpression="rcpt_rcvd_amt">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="rcpt_rmk" HeaderText="REMARKS" ItemStyle-Width="80px"
                            SortExpression="rcpt_rmk">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Change" ID="lnkSelect" runat="server" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
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

