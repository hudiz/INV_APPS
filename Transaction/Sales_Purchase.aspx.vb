Imports System.Data
Imports System.Data.SqlClient
Imports ConfigData
Partial Class Transaction_Sales_Purchase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsCallback AndAlso Not IsPostBack Then
            Call Generate_DataTable_Sales()
            Call Generate_item_Sales()
            UpdateUserButton.Visible = False
            DeleteUserButton.Visible = False
            CancelUserButton.Visible = False
            addButton.Visible = True
        End If
        Dim dt As DataTable = TryCast(Session("TRN_sales_details"), DataTable)
        gv_sales.DataSource = dt
        gv_sales.DataBind()
    End Sub
    Protected Sub addButton_Click(sender As Object, e As System.EventArgs) Handles addButton.Click
        Call add_row_to_datatable()
    End Sub

    Private Sub Generate_DataTable_Sales()
        Session("TRN_sales_details") = Nothing
        Dim dt_sales As New DataTable
        dt_sales.Columns.Add("item")
        dt_sales.Columns.Add("qty")
        dt_sales.Columns.Add("unit")
        dt_sales.Columns.Add("amount")
        dt_sales.Columns.Add("disc")
        dt_sales.Columns.Add("taxpct")
        dt_sales.Columns.Add("netamt")
        dt_sales.Columns.Add("taxamt")
        dt_sales.Columns.Add("cost")
        Session("TRN_sales_details") = dt_sales
    End Sub
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_sales.SelectedRow
        Dim item As String = row.Cells(0).Text.Trim
        Dim qty As String = row.Cells(1).Text.Trim
        Dim unit As String = row.Cells(2).Text.Trim
        Dim amount As String = row.Cells(3).Text.Trim
        Dim netamt As String = row.Cells(4).Text.Trim
        Dim disc As String = row.Cells(5).Text.Trim
        Dim taxpct As String = row.Cells(6).Text.Trim
        Dim taxamt As String = row.Cells(7).Text.Trim
        dd_itm.SelectedValue = item.Trim
        txt_qty.Text = qty.Trim
        txt_unt.Text = unit.Trim
        txt_amt.Text = amount.Trim
        txt_disc.Text = disc.Trim
        txt_taxpct.Text = taxpct.Trim
        txt_netamt.Text = netamt.Trim
        txt_taxamt.Text = taxamt.Trim

        UpdateUserButton.Visible = True
        DeleteUserButton.Visible = True
        CancelUserButton.Visible = True
        addButton.Visible = False

    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim item As String = dd_itm.SelectedValue.Trim
        Dim dt As DataTable = TryCast(Session("TRN_sales_details"), DataTable)
        Dim dr() As DataRow = dt.Select("item = '" & item & "'")
        dt.Rows.Remove(dr(0))
        dt.AcceptChanges()
        Session("TRN_sales_details") = dt
        gv_sales.DataSource = dt
        gv_sales.DataBind()
        Call add_row_to_datatable()
        summation_values(dt)
        clear_control_values()
    End Sub

    Protected Sub DeleteUserButton_Click(sender As Object, e As System.EventArgs) Handles DeleteUserButton.Click
        Dim item As String = dd_itm.SelectedValue.Trim
        Dim dt As DataTable = TryCast(Session("TRN_sales_details"), DataTable)
        Dim dr() As DataRow = dt.Select("item = '" & item & "'")
        dt.Rows.Remove(dr(0))
        dt.AcceptChanges()
        Session("TRN_sales_details") = dt
        gv_sales.DataSource = dt
        gv_sales.DataBind()
        summation_values(dt)
        clear_control_values()
    End Sub
    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub add_row_to_datatable()
        Dim dt As DataTable = TryCast(Session("TRN_sales_details"), DataTable)
        Dim item As String = dd_itm.SelectedValue.Trim
        Dim qty As String = txt_qty.Text.Trim
        Dim unit As String = txt_unt.Text.Trim
        Dim amount As String = txt_amt.Text.Trim
        Dim disc As String = txt_disc.Text.Trim
        Dim taxpct As String = txt_taxpct.Text.Trim
        Dim netamt As String = txt_netamt.Text.Trim
        Dim taxamt As String = txt_taxamt.Text.Trim
        Dim cost As String = hid_costamt.Value.Trim
        qty = If(qty = "", "0.000", qty)
        unit = If(unit = "", "-", unit)
        amount = If(amount = "", "0.000", amount)
        disc = If(disc = "", "0.000", disc)
        taxpct = If(taxpct = "", "0.00", taxpct)
        netamt = If(netamt = "", "0.000", netamt)
        taxamt = If(taxamt = "", "0.000", taxamt)
        cost = If(taxamt = "", "0.000", cost)
        dt.Rows.Add(item, qty, unit, amount, disc, taxpct, netamt, taxamt, cost)
        gv_sales.DataSource = dt
        gv_sales.DataBind()
        summation_values(dt)
        clear_control_values()
    End Sub
    Public Sub clear_control_values()
        dd_itm.SelectedValue = ""
        txt_qty.Text = ""
        txt_unt.Text = ""
        txt_amt.Text = ""
        txt_disc.Text = ""
        txt_taxpct.Text = ""
        txt_netamt.Text = ""
        txt_taxamt.Text = ""
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        addButton.Visible = True
    End Sub

    Private Sub Generate_item_Sales()
        Dim qry As String = "SELECT [item_code] as itmval, [item_code] + ' - ' + [item_name] as itmtxt FROM [item_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("itmval") = ""
        dr("itmtxt") = "-select-"
        If dt.Rows.Count > 0 Then
            For Each dr1 In dt.Rows
                dr1("itmval") = dr1("itmval").ToString.Trim
                dr1("itmtxt") = dr1("itmtxt").ToString.Trim
            Next
        End If
        dt.Rows.InsertAt(dr, 0)
        dd_itm.DataSource = dt
        dd_itm.DataBind()
    End Sub

    Private Sub summation_values(ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            Dim netamt As Decimal = 0.0
            Dim netdisc As Decimal = 0.0
            Dim nettax As Decimal = 0.0
            For Each dr In dt.Rows
                netamt = netamt + CDec(dr("netamt"))
                netdisc = netdisc + CDec(dr("disc"))
                nettax = nettax + CDec(dr("taxamt"))
            Next

            txt_tot.Text = netamt
            txt_netdisc.Text = netdisc
            txt_nettax.Text = nettax
            txt_netoth.Text = "0.000"
            txt_nettot.Text = netamt + nettax + CDec(txt_netoth.Text) - netdisc
        Else
            txt_tot.Text = "0.000"
            txt_netdisc.Text = "0.000"
            txt_nettax.Text = "0.000"
            txt_netoth.Text = "0.000"
            txt_nettot.Text = "0.000"
        End If
    End Sub

    Protected Sub btn_add_to_table_Click(sender As Object, e As System.EventArgs) Handles btn_add_to_table.Click
        Dim tot_cnt As Integer = gv_sales.Rows.Count
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = "001"
        Dim retid As String = "C"
        Dim sr_flg As String = "P" 'sales(S)/return(R)/purchase(P) flag

        Dim stsins As Integer = Insert_item_to_table(crby, upby, comp)
        If stsins > 0 Then
            Dim qry As String = "insert into sr_dtls (sr_inv_id, sr_cust_id, sr_cust_name, sr_tot_itm, sr_tot_amt,sr_disc,sr_date_time,sr_net_amt,sr_paid_by1,sr_rmk1, " & _
                            "sr_amt1,sr_paid_by2,sr_rmk2,sr_amt2,sr_remark,sr_sts,sr_crd_flg,sr_flg,sr_cr_amt, createby, createon, updateby, updateon, comp_id, sr_ret_id,sr_tax_amt,sr_oth_amt) " & _
                                   " values('" & txt_invno.Text.Trim & "','" & txt_custid.Text.Trim & "','" & txt_custname.Text.Trim & "','" & tot_cnt & "'," & _
                                   "'" & txt_tot.Text.Trim & "','" & txt_netdisc.Text.Trim & "','" & txt_dte.Text.Trim & "','" & txt_nettot.Text.Trim & "'," & _
                                   "'" & txt_pdbyname1.Text.Trim & "','" & txt_pdbyrmk1.Text.Trim & "','" & txt_pdbyamt1.Text.Trim & "'," & _
                                   "'" & txt_pdbyname2.Text.Trim & "','" & txt_pdbyrmk2.Text.Trim & "','" & txt_pdbyamt2.Text.Trim & "','" & txt_rmk.Text.Trim & "'," & _
                                   "'S','" & rbl_credit.SelectedValue.Trim & "','" & sr_flg & "','0.000','" & crby & "'," & _
                                   "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "','" & retid & "'," & _
                                   "'" & txt_nettax.Text.Trim & "','" & txt_netoth.Text.Trim & "')"
            Dim inssts As Integer = ConfigData.postDataToTable(qry)
            If inssts > 0 Then
                msg_box(Me, "Info Proceeded...")
            Else
                msg_box(Me, "Info Not Proceeded...")
            End If
        Else
            msg_box(Me, "Info Not Proceeded...")
        End If
    End Sub

    Private Function Insert_item_to_table(crby As String, upby As String, comp As String) As Integer
        Dim dt As DataTable = TryCast(Session("TRN_sales_details"), DataTable)
        Dim inssts As Integer = 0
        Dim qry As String = ""
        Dim dt_itm As New DataTable
        If dt.Rows.Count > 0 Then
            For Each dr In dt.Rows
                qry = "SELECT [item_name] as itmtxt,[item_grp] as itmgrp FROM [item_dtls] where item_code='" & dr("item").ToString.Trim & "'"
                dt_itm = ConfigData.getDataToDatatable(qry)
                Dim itmname As String = dt_itm.Rows(0)("itmtxt").ToString.Trim
                Dim itmgrp As String = dt_itm.Rows(0)("itmgrp").ToString.Trim
                Dim sr_itm_flg As String = "P" 'sales(S)/return(R)/purchase(P) flag

                qry = "insert into sr_item_dtls (sr_itm_inv, sr_itm_flg, sr_itm_id, sr_itm_grp, sr_itm_name,sr_itm_price,sr_itm_cost,sr_itm_disc,sr_itm_taxamt,sr_itm_amt, " & _
                                  "sr_itm_qty,sr_itm_custid,sr_itm_date,createby, createon, updateby, updateon, comp_id, sr_itm_taxpct) " & _
                                  " values('" & txt_invno.Text.Trim & "','" & sr_itm_flg & "','" & dr("item") & "','" & itmgrp & "','" & itmname & "'," & _
                                  "'" & dr("amount") & "','" & dr("cost") & "','" & dr("disc") & "','" & dr("taxamt") & "'," & _
                                  "'" & dr("netamt") & "','" & dr("qty") & "','" & txt_custid.Text.Trim & "'," & _
                                  "'" & txt_dte.Text.Trim & "','" & crby & "'," & _
                                  "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "','" & dr("taxpct") & "')"
                inssts = ConfigData.postDataToTable(qry)
            Next
        End If
        Return inssts
    End Function

    Protected Sub dd_itm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dd_itm.SelectedIndexChanged
        Dim item_code As String = dd_itm.SelectedValue.Trim
        Dim qry As String = "select itm_pr_unt,itm_pr_price,itm_pr_taxpct,itm_pr_cost from itm_price_dtls where itm_pr_id='" & item_code & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            txt_unt.Text = dr("itm_pr_unt").ToString.Trim
            txt_amt.Text = dr("itm_pr_price").ToString.Trim
            txt_taxpct.Text = dr("itm_pr_taxpct").ToString.Trim
            hid_costamt.Value = dr("itm_pr_cost").ToString.Trim

            Dim qty As String = txt_qty.Text.Trim
            Dim amount As String = txt_amt.Text.Trim
            Dim disc As String = txt_disc.Text.Trim
            Dim taxpct As String = txt_taxpct.Text.Trim
            Dim cost As String = hid_costamt.Value.Trim
            qty = If(qty = "", "0.000", qty)
            amount = If(amount = "", "0.000", amount)
            disc = If(disc = "", "0.000", disc)
            taxpct = If(taxpct = "", "0.00", taxpct)
            cost = If(cost = "", "0.00", cost)

            Dim netamtded As Decimal = (CDec(qty) * CDec(amount)) - CDec(disc)
            Dim nettax As Decimal = (netamtded * CDec(taxpct)) / 100
            Dim nettot As Decimal = (CDec(qty) * CDec(amount)) + nettax
            txt_taxamt.Text = nettax
            txt_netamt.Text = nettot
            hid_costamt.Value = cost
        Else
            txt_unt.Text = ""
            txt_amt.Text = ""
            txt_taxpct.Text = ""
            txt_taxamt.Text = ""
            txt_netamt.Text = ""
            hid_costamt.Value = ""
        End If
    End Sub

    Protected Sub btn_print_Click(sender As Object, e As System.EventArgs) Handles btn_print.Click
        Response.Redirect("~/Reports/Sales_Details_Rep.aspx?invno=" & txt_invno.Text.Trim & "")
    End Sub
End Class
