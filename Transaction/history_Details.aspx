<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.Master"  EnableEventValidation="false"  CodeFile="history_Details.aspx.vb" Inherits="Transaction_history_Details" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">--%>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
        <div class="col-lg-12 col-md-12 col-sm-12">
            <div class="accountInfo">
                <div class="divfieldset">
                    <div>

                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="itmnamelbl" runat="server" AssociatedControlID="dditmcd">Item Name:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="dditmcd" runat="server" CssClass="form-control" Width="200px"
                                        DataTextField="item_desc" DataValueField="itm_pr_id" >
                                    </asp:DropDownList>
                                </td>

                                <td>

                                    <asp:Label ID="itmgrplbl" runat="server" AssociatedControlID="dd_custid">Customer:</asp:Label>
                                </td>
                                <td>

                                    <asp:DropDownList ID="dd_custid" runat="server" CssClass="form-control"   Width="200px"
                                        DataTextField="cname" DataValueField="cid">
                                    </asp:DropDownList>
                                </td>

                                <td>

                                    <div class="submitButton">
                                        <asp:Button ID="CreateUserButton" runat="server" Text="Search" CssClass="btn btn-primary" />
                                        <asp:Button ID="CancelUserButton" runat="server" Text="Refresh" CssClass="btn btn-success" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    
        <div class="col-lg-12 col-md-12 col-sm-12">
        <div style="width: 100%; max-height: 250px; overflow: auto; border: 1px solid #ddd;">
            <asp:GridView ID="gv_hist" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no data records to display.">
                <Columns>
                    <asp:BoundField DataField="inv" HeaderText="INVOICE" ReadOnly="True" SortExpression="inv">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="custid" HeaderText="CUST ID" ReadOnly="True" SortExpression="custid">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="item" HeaderText="ITEM" ReadOnly="True" SortExpression="item">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="qty" HeaderText="QTY." ReadOnly="True" SortExpression="qty">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="amount" HeaderText="AMOUNT" ReadOnly="True" SortExpression="amount">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="price" HeaderText="PRICE" ReadOnly="True" SortExpression="price">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="disc" HeaderText="DISC." ReadOnly="True" SortExpression="disc">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="cost" HeaderText="COST" ReadOnly="True" SortExpression="cost">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="taxamt" HeaderText="TAX AMT." ReadOnly="True" SortExpression="taxamt">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
<asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-success"  OnClick = "ExportToExcel" />
        </div>
        </div>
   
    
</asp:Content>
<%-- </form>
    </body>
</html>--%>
