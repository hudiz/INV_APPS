Imports System.Data
Imports System.Data.SqlClient
Imports ConfigData
Partial Class Transaction_Purchase_Details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsCallback AndAlso Not IsPostBack Then
            Call init_details()
        End If
        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        gv_sales.DataSource = dt
        gv_sales.DataBind()
    End Sub
    Private Sub init_details()
        Call Generate_item_Sales()
        Call load_unit_details()
        Call load_itemGroup_details()
        Call Generate_item_paidby_list()
        Call Generate_item_supplier_list()
        Call clear_Main_control_values()
        Call gv_fill_init_details()
    End Sub

    Public Sub load_unit_details()
        Dim dt As DataTable = Get_unit_list()
        dditemUnit.DataSource = dt
        dditemUnit.DataBind()
    End Sub
    Public Sub load_itemGroup_details()
        Dim qry As String = "SELECT [itm_grp_code] as grp_cd, itm_grp_code + ' - ' + itm_grp_desc as grp_desc FROM [itm_grp_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dditemgrp.DataSource = dt
        dditemgrp.DataBind()
    End Sub
    Private Sub gv_fill_init_details()
        Dim itm_pr_id As String = dd_itm.Text.Trim
        Dim qry As String = "select [itm_pr_id] as itmval, [itm_pr_info] as itmtxt, " &
                            " itm_pr_unt,itm_pr_price,itm_pr_taxpct,itm_pr_cost ,itm_pr_rmk ,itm_pr_grp, QTY as qty " &
                            "from item_dtls left outer join V_Item_Stock_Summ on itm_pr_id = ITEMCD  order by 1"
        'Dim qry As String = "select [itm_pr_id] as itmval, [itm_pr_info] as itmtxt, itm_pr_unt,itm_pr_price,itm_pr_taxpct,itm_pr_cost, 1 as qty from item_dtls"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        'dt.Columns.Add("qty")
        'If dt.Rows.Count > 0 Then
        '    For Each dr In dt.Rows
        '        Dim qty1 As Decimal = 1 ' get_qty_check_details_flag(1, dr("itmval").ToString.Trim)
        '        dr("qty") = qty1
        '    Next
        'End If
        Session("TRN_purchase_PriceList") = dt
        jsonsrv.Source = dt
    End Sub

    Private Sub Generate_item_Sales()
        Dim qry As String = "SELECT [itm_pr_id] as itmval, [itm_pr_id] + ' - ' + [itm_pr_info] as itmtxt FROM [item_dtls]"
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
        'dd_itm.DataSource = dt
        'dd_itm.DataBind()

    End Sub

    Public Sub clear_Main_control_values()
        txt_dte.Text = Now.ToShortDateString
        txt_invno.Text = Purchase_next_invoce_values()
        txt_custname.Text = ""
        dd_Suppid.SelectedIndex = 0
        If Not IsNothing(Session("new_custid")) Then
            dd_Suppid.SelectedValue = Session("new_custid")
            txt_custname.Text = dd_Suppid.SelectedItem.Text.Trim.Split("-")(1).Trim
            Session("new_custid") = Nothing
        End If
        txt_tot.Text = "0.000"
        txt_netdisc.Text = "0.000"
        txt_nettax.Text = "0.000"
        txt_netoth.Text = "0.000"
        txt_nettot.Text = "0.000"
        txt_pdbyamt1.Text = "0.000"

        btn_add_to_table.Visible = True
        btn_print.Visible = False

        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        addButton.Visible = True
        Call Generate_DataTable_Sales()

        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        gv_sales.DataSource = dt
        gv_sales.DataBind()
        Call clear_control_values()
    End Sub
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_sales.SelectedRow

        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        Dim item As String = row.Cells(0).Text.Trim

        Dim dr() As DataRow = dt.Select("item = '" & item & "'")
        If dr.Length > 0 Then

            Dim qty As String = dr(0)("qty").ToString.Trim
            Dim unit As String = dr(0)("unit").ToString.Trim
            Dim amount As String = dr(0)("amount").ToString.Trim
            Dim netamt As String = dr(0)("netamt").ToString.Trim
            Dim disc As String = dr(0)("disc").ToString.Trim
            Dim taxpct As String = dr(0)("taxpct").ToString.Trim
            Dim taxamt As String = dr(0)("taxamt").ToString.Trim
            dd_itm.Text = item.Trim
            txt_qty.Text = qty.Trim
            txt_qty.ToolTip = qty.Trim ' get_qty_check_details_flag(qty, item)
            dditemUnit.SelectedValue = unit.Trim
            txt_amt.Text = amount.Trim
            txt_disc.Text = disc.Trim
            txt_taxpct.Text = taxpct.Trim
            txt_netamt.Text = netamt.Trim
            txt_taxamt.Text = taxamt.Trim
            hid_chg_sts.Value = dd_itm.Text.Trim
            txt_itmdesc.Text = dr(0)("itemname").ToString.Trim
            txt_hsnno.Text = dr(0)("hsnno").ToString.Trim
            dditemgrp.SelectedValue = dr(0)("itmgrp").ToString.Trim

            UpdateUserButton.Visible = True
            DeleteUserButton.Visible = True
            CancelUserButton.Visible = True
            addButton.Visible = False
        End If

    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        'Dim item As String = hid_chg_sts.Value.Trim
        'Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        'Dim dr() As DataRow = dt.Select("item = '" & item & "'")
        'dt.Rows.Remove(dr(0))
        'dt.AcceptChanges()
        'Session("TRN_purchase_details") = dt
        'gv_sales.DataSource = dt
        'gv_sales.DataBind()
        Call add_row_to_datatable()
        'summation_values(dt)
        'clear_control_values()
    End Sub

    Protected Sub DeleteUserButton_Click(sender As Object, e As System.EventArgs) Handles DeleteUserButton.Click
        Dim item As String = dd_itm.Text.Trim
        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        Dim dr() As DataRow = dt.Select("item = '" & item & "'")
        dt.Rows.Remove(dr(0))
        dt.AcceptChanges()
        Session("TRN_purchase_details") = dt
        gv_sales.DataSource = dt
        gv_sales.DataBind()
        summation_values(dt)
        clear_control_values()
    End Sub
    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub add_row_to_datatable()
        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        Dim item As String = dd_itm.Text.Trim
        If item.Trim.Length > 0 Then

            Dim dr() As DataRow = dt.Select("item = '" & item & "'")
            If dr.Length > 0 Then
                dt.Rows.Remove(dr(0))
                dt.AcceptChanges()
            End If

            Dim qty As String = txt_qty.Text.Trim
            Dim unit As String = dditemUnit.SelectedValue
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
            Dim itemname As String = txt_itmdesc.Text.Trim
            Dim hsnno As String = txt_hsnno.Text.Trim
            Dim itmgrp As String = dditemgrp.SelectedValue
            'Dim dtitm As DataTable = TryCast(Session("TRN_purchase_PriceList"), DataTable)
            'Dim dritm() As DataRow = dtitm.Select("itmval = '" & item & "'")
            'If dritm.Length > 0 Then
            '    itemname = dritm(0)("itmtxt").ToString
            '    hsnno = dritm(0)("itm_pr_rmk").ToString
            'End If
            dt.Rows.Add(item, qty, unit, amount, disc, taxpct, netamt, taxamt, cost, itemname, hsnno, itmgrp)
            gv_sales.DataSource = dt
            gv_sales.DataBind()
            summation_values(dt)
            clear_control_values()
        End If
    End Sub

    Protected Sub addButton_Click(sender As Object, e As System.EventArgs) Handles addButton.Click
        Call add_row_to_datatable()
    End Sub

    Private Sub Generate_DataTable_Sales()
        Session("TRN_purchase_details") = Nothing
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
        dt_sales.Columns.Add("itemname")
        dt_sales.Columns.Add("hsnno")
        dt_sales.Columns.Add("itmgrp")
        Session("TRN_purchase_details") = dt_sales
    End Sub
    Public Sub clear_control_values()
        dd_itm.Text = ""
        txt_qty.Text = ""
        txt_qty.ToolTip = ""
        txt_amt.Text = ""
        txt_disc.Text = ""
        txt_taxpct.Text = ""
        txt_netamt.Text = ""
        txt_taxamt.Text = ""
        txt_itmdesc.Text = ""
        txt_hsnno.Text = ""
        dditemgrp.SelectedIndex = -1
        dditemUnit.SelectedIndex = -1
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        addButton.Visible = True
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
            Dim netamttot As Decimal = netamt + nettax + CDec(txt_netoth.Text) - netdisc
            txt_nettot.Text = netamttot
            txt_pdbyamt1.Text = netamttot
        Else
            txt_tot.Text = "0.000"
            txt_netdisc.Text = "0.000"
            txt_nettax.Text = "0.000"
            txt_netoth.Text = "0.000"
            txt_nettot.Text = "0.000"
            txt_pdbyamt1.Text = "0.000"
        End If
    End Sub

    Protected Sub btn_add_to_table_Click(sender As Object, e As System.EventArgs) Handles btn_add_to_table.Click
        Dim tot_cnt As Integer = gv_sales.Rows.Count
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = Session("login_compid")
        Dim retid As String = "C"
        Dim pr_flg As String = "P" 'sales(S)/sales return(R)/purchase(P) flag
        txt_invno.Text = Purchase_next_invoce_values()
        Dim STAT As Boolean = False
        Dim CONN As SqlConnection = con
        Dim TRAN As SqlTransaction = Nothing
        Try
            If Not CONN.State = ConnectionState.Open Then
                CONN.Open()
            End If
            TRAN = CONN.BeginTransaction



            Dim stsins As Integer = Insert_item_to_table(CONN, TRAN, crby, upby, comp)
            If stsins > 0 Then

                Dim cmd As SqlCommand = New SqlCommand("insert into pr_dtls  (pr_inv_id, pr_cust_id, pr_cust_name, pr_tot_itm, pr_tot_amt," &
                                                       " pr_disc,pr_date_time,pr_net_amt,pr_paid_by1,pr_rmk1, " &
                                                       " pr_amt1,pr_paid_by2,pr_rmk2,pr_amt2,pr_remark,pr_sts," &
                                                       " pr_crd_flg,pr_flg,pr_cr_amt,pr_ret_id,pr_tax_amt,pr_oth_amt, " &
                                                       " createby, createon, updateby, updateon, comp_id ) " &
                                                       " values(@_pr_inv_id, @_pr_cust_id, @_pr_cust_name, @_pr_tot_itm, @_pr_tot_amt," &
                                                       " @_pr_disc,@_pr_date_time,@_pr_net_amt,@_pr_paid_by1,@_pr_rmk1, " &
                                                       " @_pr_amt1,@_pr_paid_by2,@_pr_rmk2,@_pr_amt2,@_pr_remark,@_pr_sts," &
                                                       " @_pr_crd_flg,@_pr_flg,@_pr_cr_amt,@_pr_ret_id,@_pr_tax_amt,@_pr_oth_amt,  " &
                                                       " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", ConfigData.con)

                cmd.Parameters.Add("@_pr_inv_id", SqlDbType.NVarChar).Value = txt_invno.Text.Trim
                cmd.Parameters.Add("@_pr_cust_id", SqlDbType.NVarChar).Value = dd_Suppid.SelectedValue.Trim
                cmd.Parameters.Add("@_pr_cust_name", SqlDbType.NVarChar).Value = txt_custname.Text.Trim
                cmd.Parameters.Add("@_pr_tot_itm", SqlDbType.Int).Value = tot_cnt
                cmd.Parameters.Add("@_pr_tot_amt", SqlDbType.Decimal).Value = txt_tot.Text.Trim
                cmd.Parameters.Add("@_pr_disc", SqlDbType.Decimal).Value = txt_netdisc.Text.Trim
                cmd.Parameters.Add("@_pr_date_time", SqlDbType.NVarChar).Value = CDate(txt_dte.Text.Trim).ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_pr_net_amt", SqlDbType.Decimal).Value = txt_nettot.Text.Trim
                cmd.Parameters.Add("@_pr_paid_by1", SqlDbType.NVarChar).Value = dd_pdbyname1.SelectedValue.Trim
                cmd.Parameters.Add("@_pr_rmk1", SqlDbType.NVarChar).Value = txt_pdbyrmk1.Text.Trim
                cmd.Parameters.Add("@_pr_amt1", SqlDbType.Decimal).Value = txt_pdbyamt1.Text.Trim
                cmd.Parameters.Add("@_pr_paid_by2", SqlDbType.NVarChar).Value = dd_pdbyname2.SelectedValue.Trim
                cmd.Parameters.Add("@_pr_rmk2", SqlDbType.NVarChar).Value = txt_pdbyrmk2.Text.Trim
                cmd.Parameters.Add("@_pr_amt2", SqlDbType.Decimal).Value = txt_pdbyamt2.Text.Trim
                cmd.Parameters.Add("@_pr_remark", SqlDbType.NVarChar).Value = txt_rmk.Text.Trim
                cmd.Parameters.Add("@_pr_sts", SqlDbType.NVarChar).Value = "S"
                cmd.Parameters.Add("@_pr_crd_flg", SqlDbType.NVarChar).Value = rbl_credit.SelectedValue.Trim
                cmd.Parameters.Add("@_pr_flg", SqlDbType.NVarChar).Value = pr_flg
                cmd.Parameters.Add("@_pr_cr_amt", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@_pr_tax_amt", SqlDbType.Decimal).Value = txt_nettax.Text.Trim
                cmd.Parameters.Add("@_pr_oth_amt", SqlDbType.Decimal).Value = txt_netoth.Text.Trim
                cmd.Parameters.Add("@_pr_ret_id", SqlDbType.NVarChar).Value = retid
                cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
                cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp



                'Dim RCPTqry As String = "insert into Receipt_dtls (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                '                " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                '                " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                '                "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                '                " createby, createon, updateby, updateon, comp_id) " &
                '                " values('" & load_next_receipt_no(CONN, TRAN, 1) & "','S','" & txt_invno.Text.Trim & "', " &
                '                        " 'C',   '" & dd_Suppid.SelectedValue.Trim & "',  '" & txt_custname.Text.Trim & "', " &
                '                        " 'A',   '" & dd_pdbyname1.SelectedValue.Trim & "',  '" & dd_pdbyname1.Text.Trim & "', " &
                '                        " '0','0', '0', '" & txt_pdbyamt1.Text.Trim & "', '0','0', '" & txt_pdbyrmk1.Text.Trim & "', 'V', " &
                '                       " '" & crby & "'," & "'" & CDate(txt_dte.Text).ToString("MM/dd/yyyy") & "','" & upby & "','" & Now.ToString("MM/dd/yyyy") & "','" & comp & "')"

                Dim cmd2 As SqlCommand = New SqlCommand("insert into Receipt_dtls  (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                                                        " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                                                        " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                                                        "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                                                        " createby, createon, updateby, updateon, comp_id ) " &
                                                        " values(@_rcpt_no ,  @_rcpt_typ ,  @_rcpt_ref_id , " &
                                                        " @_rcpt_frm_typ,  @_rcpt_frm_acc,  @_rcpt_frm_name,  @_rcpt_to_typ,  @_rcpt_to_acc,  @_rcpt_to_name, " &
                                                        " @_rcpt_actual_amt,  @_rcpt_balance_amt,  @_rcpt_paid_amt,  @_rcpt_rcvd_amt, " &
                                                        " @_rcpt_credit_limit,  @_rcpt_credit_balance,  @_rcpt_rmk,  @_rcpt_sts,  " &
                                                        " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", ConfigData.con)

                cmd2.Parameters.Add("@_rcpt_no", SqlDbType.NVarChar).Value = load_next_receipt_no(CONN, TRAN, 1)
                cmd2.Parameters.Add("@_rcpt_typ", SqlDbType.NVarChar).Value = pr_flg
                cmd2.Parameters.Add("@_rcpt_ref_id", SqlDbType.NVarChar).Value = txt_invno.Text.Trim
                cmd2.Parameters.Add("@_rcpt_frm_typ", SqlDbType.NVarChar).Value = "S"
                cmd2.Parameters.Add("@_rcpt_frm_acc", SqlDbType.NVarChar).Value = dd_Suppid.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_frm_name", SqlDbType.NVarChar).Value = txt_custname.Text.Trim
                cmd2.Parameters.Add("@_rcpt_to_typ", SqlDbType.NVarChar).Value = "A"
                cmd2.Parameters.Add("@_rcpt_to_acc", SqlDbType.NVarChar).Value = dd_pdbyname1.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_to_name", SqlDbType.NVarChar).Value = dd_pdbyname1.Text.Trim
                cmd2.Parameters.Add("@_rcpt_actual_amt", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_balance_amt", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_paid_amt", SqlDbType.Decimal).Value = txt_pdbyamt1.Text.Trim
                cmd2.Parameters.Add("@_rcpt_rcvd_amt", SqlDbType.Decimal).Value = 0 'txt_pdbyamt1.Text.Trim
                cmd2.Parameters.Add("@_rcpt_credit_limit", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_credit_balance", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_rmk", SqlDbType.NVarChar).Value = txt_pdbyrmk1.Text.Trim
                cmd2.Parameters.Add("@_rcpt_sts", SqlDbType.NVarChar).Value = "V"
                cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
                cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp



                'Dim RCPTqry2 As String = ""
                Dim cmd3 As New SqlCommand
                If CDec(txt_pdbyamt2.Text) > 0 Then

                    'RCPTqry2 = "insert into Receipt_dtls (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                    '            " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                    '            " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                    '            "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                    '            " createby, createon, updateby, updateon, comp_id) " &
                    '            " values('" & load_next_receipt_no(CONN, TRAN, 2) & "','s','" & txt_invno.Text.Trim & "', " &
                    '                    " 'C',   '" & dd_Suppid.SelectedValue.Trim & "',  '" & txt_custname.Text.Trim & "', " &
                    '                    " 'A',   '" & dd_pdbyname2.SelectedValue.Trim & "',  '" & dd_pdbyname2.Text.Trim & "', " &
                    '                    " '0','0', '0', '" & txt_pdbyamt2.Text.Trim & "', '0','0', '" & txt_pdbyrmk2.Text.Trim & "', 'V', " &
                    '                   " '" & crby & "'," & "'" & CDate(txt_dte.Text).ToString("MM/dd/yyyy") & "','" & upby & "','" & Now.ToString("MM/dd/yyyy") & "','" & comp & "')"
                    cmd3 = New SqlCommand("insert into Receipt_dtls  (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                                                        " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                                                        " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                                                        "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                                                        " createby, createon, updateby, updateon, comp_id ) " &
                                                        " values(@_rcpt_no ,  @_rcpt_typ ,  @_rcpt_ref_id , " &
                                                        " @_rcpt_frm_typ,  @_rcpt_frm_acc,  @_rcpt_frm_name,  @_rcpt_to_typ,  @_rcpt_to_acc,  @_rcpt_to_name, " &
                                                        " @_rcpt_actual_amt,  @_rcpt_balance_amt,  @_rcpt_paid_amt,  @_rcpt_rcvd_amt, " &
                                                        " @_rcpt_credit_limit,  @_rcpt_credit_balance,  @_rcpt_rmk,  @_rcpt_sts,  " &
                                                        " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", ConfigData.con)

                    cmd3.Parameters.Add("@_rcpt_no", SqlDbType.NVarChar).Value = load_next_receipt_no(CONN, TRAN, 2)
                    cmd3.Parameters.Add("@_rcpt_typ", SqlDbType.NVarChar).Value = pr_flg
                    cmd3.Parameters.Add("@_rcpt_ref_id", SqlDbType.NVarChar).Value = txt_invno.Text.Trim
                    cmd3.Parameters.Add("@_rcpt_frm_typ", SqlDbType.NVarChar).Value = "S"
                    cmd3.Parameters.Add("@_rcpt_frm_acc", SqlDbType.NVarChar).Value = dd_Suppid.SelectedValue.Trim
                    cmd3.Parameters.Add("@_rcpt_frm_name", SqlDbType.NVarChar).Value = txt_custname.Text.Trim
                    cmd3.Parameters.Add("@_rcpt_to_typ", SqlDbType.NVarChar).Value = "A"
                    cmd3.Parameters.Add("@_rcpt_to_acc", SqlDbType.NVarChar).Value = dd_pdbyname2.SelectedValue.Trim
                    cmd3.Parameters.Add("@_rcpt_to_name", SqlDbType.NVarChar).Value = dd_pdbyname2.Text.Trim
                    cmd3.Parameters.Add("@_rcpt_actual_amt", SqlDbType.Decimal).Value = 0
                    cmd3.Parameters.Add("@_rcpt_balance_amt", SqlDbType.Decimal).Value = 0
                    cmd3.Parameters.Add("@_rcpt_paid_amt", SqlDbType.Decimal).Value = txt_pdbyamt2.Text.Trim
                    cmd3.Parameters.Add("@_rcpt_rcvd_amt", SqlDbType.Decimal).Value = 0
                    cmd3.Parameters.Add("@_rcpt_credit_limit", SqlDbType.Decimal).Value = 0
                    cmd3.Parameters.Add("@_rcpt_credit_balance", SqlDbType.Decimal).Value = 0
                    cmd3.Parameters.Add("@_rcpt_rmk", SqlDbType.NVarChar).Value = txt_pdbyrmk2.Text.Trim
                    cmd3.Parameters.Add("@_rcpt_sts", SqlDbType.NVarChar).Value = "V"
                    cmd3.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
                    cmd3.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd3.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                    cmd3.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd3.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

                End If
                ' Dim inssts As Integer = ConfigData.Transact(qry, RCPTqry, RCPTqry2, "", "")

                cmd.Connection = CONN
                cmd.Transaction = TRAN
                cmd.ExecuteNonQuery()

                ' Dim cmd2 As New SqlCommand(RCPTqry)
                cmd2.Connection = CONN
                cmd2.Transaction = TRAN
                cmd2.ExecuteNonQuery()

                If CDec(txt_pdbyamt2.Text) > 0 Then
                    ' Dim cmd3 As New SqlCommand(RCPTqry2)
                    cmd3.Connection = CONN
                    cmd3.Transaction = TRAN
                    cmd3.ExecuteNonQuery()
                End If

                msg_box(Me, "Info Proceeded...")

                'Else
                '    msg_box(Me, "Info Not Proceeded...")
                'End If
                STAT = True
            Else
                STAT = False
                msg_box(Me, "Info Not Proceeded...")
            End If


            TRAN.Commit()

        Catch err As Exception
            STAT = False
            TRAN.Rollback()
            msg_box(Me, err.Message.ToString)
            ' mbox("Error : " & err.Message.ToString)
        Finally

            con.Close()
        End Try

        If STAT Then
            Response.Redirect("../Reports/Purchase_Details_Rep.aspx?invno=" & txt_invno.Text.Trim)

        Else
            msg_box(Me, "Info Not Proceeded...")

            ' Call clear_Main_control_values()
        End If
    End Sub

    Private Function Insert_item_to_table(CON As SqlConnection, TRAN As SqlTransaction, crby As String, upby As String, comp As String) As Integer
        Dim dt As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
        Dim inssts As Integer = 0
        Dim qry As String = ""
        Dim dt_itm As New DataTable
        If dt.Rows.Count > 0 Then
            For Each dr In dt.Rows
                qry = "SELECT [itm_pr_info] as itmtxt,[itm_pr_grp] as itmgrp FROM [item_dtls] where itm_pr_id='" & dr("item").ToString.Trim & "'"
                dt_itm = ConfigData.SelTbl_Trnscn(qry, CON, TRAN)

                Dim pr_itm_flg As String = "P" 'sales(S)/return(R)/purchase(P) flag

                Dim cmd1 As SqlCommand = New SqlCommand()
                If dt_itm.Rows.Count = 0 Then
                    cmd1 = New SqlCommand("insert into item_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_exp,itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " null,@_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

                    cmd1.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dr("item").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = dr("itemname").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dr("itmgrp").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd1.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = dr("cost").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = dr("cost").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = dr("taxpct").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = dr("taxamt").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dr("unit").ToString.Trim
                    cmd1.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = ""
                    cmd1.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = "S"
                    cmd1.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = dr("hsnno").ToString.Trim
                    cmd1.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = upby
                    cmd1.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd1.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                    cmd1.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd1.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp




                    Dim cmd2 As SqlCommand = New SqlCommand("insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_exp,itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " null,@_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

                    cmd2.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dr("item").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = dr("itemname").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dr("itmgrp").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd2.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = dr("cost").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = dr("cost").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = dr("taxpct").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = dr("taxamt").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dr("unit").ToString.Trim
                    cmd2.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = ""
                    cmd2.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = "S"
                    cmd2.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = dr("hsnno").ToString.Trim
                    cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = upby
                    cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                    cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                    cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp


                    cmd1.Connection = CON
                    cmd1.Transaction = TRAN
                    cmd1.ExecuteNonQuery()

                    cmd2.Connection = CON
                    cmd2.Transaction = TRAN
                    cmd2.ExecuteNonQuery()

                End If
                'Dim itmname As String = dt_itm.Rows(0)("itmtxt").ToString.Trim
                'Dim itmgrp As String = dt_itm.Rows(0)("itmgrp").ToString.Trim
                Dim cmd As SqlCommand = New SqlCommand("insert into pr_item_dtls  (pr_itm_inv, pr_itm_flg, pr_itm_id, pr_itm_grp, pr_itm_name," &
                                                       "pr_itm_price,pr_itm_cost,pr_itm_disc,pr_itm_taxamt,pr_itm_amt, " &
                                                       "pr_itm_qty,pr_itm_custid,pr_itm_date,pr_itm_taxpct, " &
                                                       "createby, createon, updateby, updateon, comp_id ) " &
                                                       " values(@_pr_itm_inv, @_pr_itm_flg, @_pr_itm_id, @_pr_itm_grp, @_pr_itm_name," &
                                                       " @_pr_itm_price, @_pr_itm_cost, @_pr_itm_disc, @_pr_itm_taxamt, @_pr_itm_amt, " &
                                                       " @_pr_itm_qty, @_pr_itm_custid, @_pr_itm_date, @_pr_itm_taxpct, " &
                                                       " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", ConfigData.con)

                cmd.Parameters.Add("@_pr_itm_inv", SqlDbType.NVarChar).Value = txt_invno.Text.Trim
                cmd.Parameters.Add("@_pr_itm_flg", SqlDbType.NVarChar).Value = pr_itm_flg
                cmd.Parameters.Add("@_pr_itm_id", SqlDbType.NVarChar).Value = dr("item").ToString
                cmd.Parameters.Add("@_pr_itm_grp", SqlDbType.NVarChar).Value = dr("itmgrp").ToString
                cmd.Parameters.Add("@_pr_itm_name", SqlDbType.NVarChar).Value = dr("itemname").ToString
                cmd.Parameters.Add("@_pr_itm_price", SqlDbType.Decimal).Value = dr("amount").ToString
                cmd.Parameters.Add("@_pr_itm_cost", SqlDbType.Decimal).Value = dr("cost").ToString
                cmd.Parameters.Add("@_pr_itm_disc", SqlDbType.Decimal).Value = dr("disc").ToString
                cmd.Parameters.Add("@_pr_itm_taxamt", SqlDbType.Decimal).Value = dr("taxamt").ToString
                cmd.Parameters.Add("@_pr_itm_amt", SqlDbType.Decimal).Value = dr("netamt").ToString
                cmd.Parameters.Add("@_pr_itm_qty", SqlDbType.Decimal).Value = dr("qty").ToString
                cmd.Parameters.Add("@_pr_itm_custid", SqlDbType.NVarChar).Value = dd_Suppid.SelectedValue.Trim
                cmd.Parameters.Add("@_pr_itm_date", SqlDbType.NVarChar).Value = CDate(txt_dte.Text.Trim).ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_pr_itm_taxpct", SqlDbType.Decimal).Value = dr("taxpct").ToString
                cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
                cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp


                cmd.Connection = CON
                cmd.Transaction = TRAN
                cmd.ExecuteNonQuery()
                inssts += 1
            Next
        End If
        Return inssts
    End Function

    'Protected Sub dd_itm_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dd_itm.SelectedIndexChanged
    '    Dim itm_pr_id As String = dd_itm.SelectedValue.Trim
    '    Dim qry As String = "select itm_pr_unt,itm_pr_price,itm_pr_taxpct,itm_pr_cost from item_dtls where itm_pr_id='" & itm_pr_id & "'"
    '    Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
    '    If dt.Rows.Count > 0 Then
    '        Dim dr As DataRow = dt.Rows(0)
    '        Dim qty1 As Decimal = get_qty_check_details_flag(1, itm_pr_id)
    '        If qty1 > 0 Then
    '            txt_qty.Text = "1"
    '        End If
    '        txt_qty.ToolTip = "Total Quantity:" & qty1
    '        txt_disc.Text = "0"

    '        txt_unt.Text = dr("itm_pr_unt").ToString.Trim
    '        txt_amt.Text = dr("itm_pr_price").ToString.Trim
    '        txt_taxpct.Text = dr("itm_pr_taxpct").ToString.Trim
    '        hid_costamt.Value = dr("itm_pr_cost").ToString.Trim

    '        Dim qty As String = txt_qty.Text.Trim
    '        Dim amount As String = txt_amt.Text.Trim
    '        Dim disc As String = txt_disc.Text.Trim
    '        Dim taxpct As String = txt_taxpct.Text.Trim
    '        Dim cost As String = hid_costamt.Value.Trim
    '        qty = If(qty = "", "0.000", qty)
    '        amount = If(amount = "", "0.000", amount)
    '        disc = If(disc = "", "0.000", disc)
    '        taxpct = If(taxpct = "", "0.00", taxpct)
    '        cost = If(cost = "", "0.00", cost)

    '        Dim netamtded As Decimal = (CDec(qty) * CDec(amount)) - CDec(disc)
    '        Dim nettax As Decimal = (netamtded * CDec(taxpct)) / 100
    '        Dim nettot As Decimal = (CDec(qty) * CDec(amount)) ' + nettax
    '        txt_taxamt.Text = nettax
    '        txt_netamt.Text = nettot
    '        hid_costamt.Value = cost
    '    Else
    '        txt_unt.Text = ""
    '        txt_amt.Text = ""
    '        txt_taxpct.Text = ""
    '        txt_taxamt.Text = ""
    '        txt_netamt.Text = ""
    '        hid_costamt.Value = ""
    '    End If
    'End Sub

    Protected Sub btn_print_Click(sender As Object, e As System.EventArgs) Handles btn_print.Click
        Response.Redirect("~/Reports/Sales_Details_Rep.aspx?invno=" & txt_invno.Text.Trim & "")
    End Sub

    Private Sub Generate_item_paidby_list()
        Dim qry As String = "SELECT [acc_name] FROM [acc_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dd_pdbyname1.DataSource = dt
        dd_pdbyname1.DataBind()
        dd_pdbyname1.SelectedIndex = 0
        dd_pdbyname2.DataSource = dt
        dd_pdbyname2.DataBind()
        dd_pdbyname2.SelectedIndex = 1
    End Sub

    Private Sub Generate_item_supplier_list()
        Dim qry As String = "SELECT [cust_sup_id] as cid, [cust_sup_id] + '-' + [cust_sup_name] as cname FROM [cust_supp_dtls] where cust_sup_sts='S'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("cid") = ""
        dr("cname") = "-select-"
        dt.Rows.InsertAt(dr, 0)
        dd_Suppid.DataSource = dt
        dd_Suppid.DataBind()
        dd_Suppid.SelectedIndex = 0
    End Sub

    Protected Sub dd_Suppid_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dd_Suppid.SelectedIndexChanged
        txt_custname.Text = dd_Suppid.SelectedItem.Text.Trim.Split("-")(1).Trim
    End Sub

    Protected Sub btn_add_Changed(sender As Object, e As System.EventArgs) Handles addButton.Click

        Dim qty As Decimal = 0.0
        Dim amt As Decimal = 0.0
        Dim tax As Decimal = 0.0
        Dim taxpct As Decimal = 0.0
        Dim disc As Decimal = 0.0
        qty = If(txt_qty.Text.Trim = "", 0.0, CDec(txt_qty.Text.Trim))
        amt = If(txt_amt.Text.Trim = "", 0.0, CDec(txt_amt.Text.Trim))
        tax = If(txt_taxamt.Text.Trim = "", 0.0, CDec(txt_taxamt.Text.Trim))
        taxpct = If(txt_taxpct.Text.Trim = "", 0.0, CDec(txt_taxpct.Text.Trim))
        disc = If(txt_disc.Text.Trim = "", 0.0, CDec(txt_disc.Text.Trim))

        ' Dim chk_bool As Decimal = get_qty_check_details_flag(qty, dd_itm.Text.Trim)

        If qty > 0 Then
            Dim netamtded As Decimal = (qty * amt) - disc
            Dim nettax As Decimal = (netamtded * taxpct) / 100
            txt_taxamt.Text = nettax.ToString("0.000") ' String.Format("{0:N3}", nettax)
            Dim nettot As Decimal = (qty * amt) '+ nettax
            txt_netamt.Text = nettot.ToString("0.000") 'String.Format("{0:N3}", nettot)
        Else
            txt_taxamt.Text = String.Format("{0:N3}", 0)
            txt_netamt.Text = String.Format("{0:N3}", 0)
            clear_control_values()
        End If

    End Sub

    'Private Function get_qty_check_details_flag(iqty As Decimal, itmval As String) As Decimal
    '    Dim qty As Decimal = 0
    '    Dim alrdyqty As Decimal = 0
    '    Dim itm_dec As Decimal = 0.0
    '    'Dim itmval As String = dd_itm.SelectedValue.Trim
    '    Dim qry As String = "select QTY from V_Item_Stock_Summ where ITEMCD = '" & itmval & "'"
    '    Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
    '    If dt.Rows.Count > 0 Then

    '        Dim item As String = dd_itm.Text.Trim
    '        Dim dts As DataTable = TryCast(Session("TRN_purchase_details"), DataTable)
    '        Dim dr() As DataRow = dts.Select("item = '" & item & "'")
    '        'For Each drw As DataRow In dr
    '        '    alrdyqty += drw("qty")
    '        'Next

    '        itm_dec = CDec(dt.Rows(0)("QTY"))
    '        If itm_dec >= iqty + alrdyqty Then
    '            qty = itm_dec
    '        Else
    '            qty = 0
    '        End If
    '    End If
    '    Return qty
    'End Function

    Private Sub txt_invno_TextChanged(sender As Object, e As EventArgs) Handles txt_invno.TextChanged
        Dim qry As String = " select * from pr_dtls where pr_inv_id likes '" & txt_invno.Text & "' "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            msg_box(Me, " This Invoice number already exist !!")
            txt_invno.Text = ""
        End If
    End Sub
    Public Function Purchase_next_invoce_values() As String
        Dim dte As Date = CDate(txt_dte.Text)
        Dim qry As String = " select max(pr_inv_id) from pr_dtls where pr_inv_id like '" & "PR" & dte.ToString("yyMMdd") & "%' "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(0)) Then
                If dt.Rows(0)(0).ToString.Length > 8 Then
                    Dim srl As Int16 = dt.Rows(0)(0).ToString.Substring(8)
                    Return "PR" & dte.ToString("yyMMdd") & (srl + 1).ToString("00")
                End If
            End If
        End If
        Return "PR" & dte.ToString("yyMMdd") & "01"
    End Function
End Class
