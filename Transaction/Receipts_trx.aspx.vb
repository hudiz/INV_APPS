Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO

Partial Class Trans_Receipts_trx
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Session("RCPTDTLS_Receipt_dtls") = Nothing
            Call load_receipt_list_grid()
            Call load_source_details()
            Call load_destination_details()
            Call clear_control_values()
        End If
    End Sub

    Public Sub load_source_details()
        Dim srctype As String = ddl_sourcetype.SelectedValue
        Dim dt As DataTable = load_typeinfo_details(srctype)
        ddl_sourceinfo.DataSource = dt
        ddl_sourceinfo.DataBind()
    End Sub
    Private Sub ddl_sourcetype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sourcetype.SelectedIndexChanged
        Call load_source_details()
    End Sub

    Public Sub load_destination_details()
        Dim dstntype As String = ddl_destintype.SelectedValue
        Dim dt As DataTable = load_typeinfo_details(dstntype)
        ddl_destininfo.DataSource = dt
        ddl_destininfo.DataBind()
    End Sub
    Private Sub ddl_destintype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_destintype.SelectedIndexChanged
        Call load_destination_details()
    End Sub

    Public Function load_typeinfo_details(type As String) As DataTable

        Dim qry As String = ""

        If type = "A" Then
            qry = "SELECT [acc_id] GID ,[acc_id] ID , [acc_name] NAME, [acc_desc] INFO FROM [acc_dtls] "

        ElseIf type = "C" Then
            qry = "SELECT [cust_sup_auto_id] GID , [cust_sup_id] ID , [cust_sup_name] NAME,  [cust_sup_addr] INFO FROM [cust_supp_dtls] where  [cust_sup_sts] = 'C' "

        ElseIf type = "S" Then
            qry = "SELECT [cust_sup_auto_id] GID , [cust_sup_id] ID , [cust_sup_name] NAME,  [cust_sup_addr] INFO FROM [cust_supp_dtls] where [cust_sup_sts] = 'S' "

        ElseIf type = "T" Then
            qry = "SELECT [cust_sup_auto_id] GID , [cust_sup_id] ID , [cust_sup_name] NAME,  [cust_sup_addr] INFO FROM [cust_supp_dtls] where [cust_sup_sts] = 'T' "

        ElseIf type = "E" Then
            qry = "SELECT [expns_id] GID , [expns_code] ID , [expns_name] NAME,  [expns_type] INFO FROM [expns_dtls] " 'where [cust_sup_sts] = 'T' "

        End If

        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)

        Return dt
    End Function



    Public Sub load_receipt_list_grid()
        Dim qry As String = "SELECT * FROM [Receipt_dtls] "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_rcpt_list.DataSource = dt
        gv_rcpt_list.DataBind()
        session_datatable_store("RCPTDTLS_Receipt_dtls", dt)
    End Sub
    Private Sub gv_rcpt_list_Init(sender As Object, e As EventArgs) Handles gv_rcpt_list.Init

        gv_rcpt_list.DataSource = session_datatable_fill("RCPTDTLS_Receipt_dtls")
        gv_rcpt_list.DataBind()

    End Sub

    'Public Function load_next_receipt_no() As String
    '    Dim qry As String = "SELECT max(rcpt_no) FROM [Receipt_dtls] "
    '    Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
    '    If dt.Rows.Count > 0 Then
    '        If Not IsDBNull(dt.Rows(0)(0)) Then
    '            Dim srl As Double = CDec(dt.Rows(0)(0).ToString.Substring(4)) + 1
    '            Return "RCPT" & srl.ToString("000000")
    '        End If
    '    End If
    '    Return "RCPT000001"
    'End Function

    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        txt_rcptgid.Text = ""
        txt_dte.Text = Today.ToShortDateString
        txt_rcptno.Text = load_next_receipt_no()
        ddl_rcpttype.SelectedValue = "M"
        txt_refid.Text = ""
        ddl_sourcetype.SelectedValue = "C"
        ddl_sourceinfo.SelectedIndex = 0
        txt_sourceinfo.Text = ddl_sourceinfo.SelectedItem.Text
        ddl_destintype.SelectedValue = "A"
        ddl_destininfo.SelectedIndex = 0
        txt_destininfo.Text = ddl_destininfo.SelectedItem.Text
        txt_remarks.Text = ""
        txt_paidamt.Text = "0.00"
        txt_rcvdamt.Text = "0.00"

        txt_rcptgid.Visible = False
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = True
        CreateUserButton.Visible = True
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_rcpt_list.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT * " & " FROM Receipt_dtls " & " where rcpt_gid ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            txt_rcptgid.Text = dt.Rows(0)("rcpt_gid").ToString.Trim
            txt_dte.Text = dt.Rows(0)("createon").ToString.Trim
            txt_rcptno.Text = dt.Rows(0)("rcpt_no").ToString.Trim
            ddl_rcpttype.SelectedValue = dt.Rows(0)("rcpt_typ").ToString.Trim
            txt_refid.Text = dt.Rows(0)("rcpt_ref_id").ToString.Trim

            ddl_sourcetype.SelectedValue = dt.Rows(0)("rcpt_frm_typ").ToString.Trim
            Call load_source_details()
            ddl_sourceinfo.SelectedValue = dt.Rows(0)("rcpt_frm_acc").ToString.Trim
            txt_sourceinfo.Text = dt.Rows(0)("rcpt_frm_name").ToString.Trim

            ddl_destintype.SelectedValue = dt.Rows(0)("rcpt_to_typ").ToString.Trim
            Call load_destination_details()
            ddl_destininfo.SelectedValue = dt.Rows(0)("rcpt_to_acc").ToString.Trim
            txt_destininfo.Text = dt.Rows(0)("rcpt_to_name").ToString.Trim
            txt_remarks.Text = dt.Rows(0)("rcpt_rmk").ToString.Trim
            txt_paidamt.Text = dt.Rows(0)("rcpt_paid_amt").ToString.Trim
            txt_rcvdamt.Text = dt.Rows(0)("rcpt_rcvd_amt").ToString.Trim

            txt_rcptgid.Visible = True
            UpdateUserButton.Visible = True
            DeleteUserButton.Visible = True
            CancelUserButton.Visible = True
            CreateUserButton.Visible = False
        Else
            clear_control_values()
        End If
    End Sub
    Protected Sub CreateUserButton_Click(sender As Object, e As System.EventArgs) Handles CreateUserButton.Click
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        txt_rcptno.Text = load_next_receipt_no()
        Dim comp As String = Session("login_compid")
        Dim STAT As Boolean = False
        Dim CONN As SqlConnection = con
        Dim TRAN As SqlTransaction = Nothing
        Try
            If Not CONN.State = ConnectionState.Open Then
                CONN.Open()
            End If
            TRAN = CONN.BeginTransaction
            'Dim qry As String = "insert into Receipt_dtls (rcpt_no , rcpt_typ , rcpt_ref_id , " &
            '                " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
            '                " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
            '                "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
            '                " createby, createon, updateby, updateon, comp_id) " &
            '                " values('" & txt_rcptno.Text.Trim & "','" & ddl_rcpttype.SelectedValue.Trim & "','" & txt_refid.Text.Trim & "', " &
            '                        " '" & ddl_sourcetype.SelectedValue.Trim & "',   '" & ddl_sourceinfo.SelectedValue.Trim & "',  '" & ddl_sourceinfo.Text.Trim & "', " &
            '                        " '" & ddl_destintype.SelectedValue.Trim & "',   '" & ddl_destininfo.SelectedValue.Trim & "',  '" & ddl_destininfo.Text.Trim & "', " &
            '                        " '0','0', '" & txt_paidamt.Text.Trim & "', '" & txt_rcvdamt.Text.Trim & "', '0','0', '" & txt_remarks.Text.Trim & "', 'V', " &
            '                       " '" & crby & "'," & "'" & CDate(txt_dte.Text).ToString("MM/dd/yyyy HH:mm:ss") & "','" & upby & "','" & Now.ToString("MM/dd/yyyy HH:mm:ss") & "','" & comp & "')"

            Dim cmd2 As SqlCommand = New SqlCommand("insert into Receipt_dtls  (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                                                        " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                                                        " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                                                        "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                                                        " createby, createon, updateby, updateon, comp_id ) " &
                                                        " values(@_rcpt_no ,  @_rcpt_typ ,  @_rcpt_ref_id , " &
                                                        " @_rcpt_frm_typ,  @_rcpt_frm_acc,  @_rcpt_frm_name,  @_rcpt_to_typ,  @_rcpt_to_acc,  @_rcpt_to_name, " &
                                                        " @_rcpt_actual_amt,  @_rcpt_balance_amt,  @_rcpt_paid_amt,  @_rcpt_rcvd_amt, " &
                                                        " @_rcpt_credit_limit,  @_rcpt_credit_balance,  @_rcpt_rmk,  @_rcpt_sts,  " &
                                                        " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", CONN)

            cmd2.Parameters.Add("@_rcpt_no", SqlDbType.NVarChar).Value = load_next_receipt_no(CONN, TRAN, 1)
            cmd2.Parameters.Add("@_rcpt_typ", SqlDbType.NVarChar).Value = ddl_rcpttype.SelectedValue.Trim
            cmd2.Parameters.Add("@_rcpt_ref_id", SqlDbType.NVarChar).Value = txt_refid.Text.Trim
            cmd2.Parameters.Add("@_rcpt_frm_typ", SqlDbType.NVarChar).Value = ddl_sourcetype.SelectedValue.Trim
            cmd2.Parameters.Add("@_rcpt_frm_acc", SqlDbType.NVarChar).Value = ddl_sourceinfo.SelectedValue.Trim
            cmd2.Parameters.Add("@_rcpt_frm_name", SqlDbType.NVarChar).Value = txt_sourceinfo.Text.Trim
            cmd2.Parameters.Add("@_rcpt_to_typ", SqlDbType.NVarChar).Value = ddl_destintype.SelectedValue.Trim
            cmd2.Parameters.Add("@_rcpt_to_acc", SqlDbType.NVarChar).Value = ddl_destininfo.SelectedValue.Trim
            cmd2.Parameters.Add("@_rcpt_to_name", SqlDbType.NVarChar).Value = txt_destininfo.Text.Trim
            cmd2.Parameters.Add("@_rcpt_actual_amt", SqlDbType.Decimal).Value = 0
        cmd2.Parameters.Add("@_rcpt_balance_amt", SqlDbType.Decimal).Value = 0
            cmd2.Parameters.Add("@_rcpt_paid_amt", SqlDbType.Decimal).Value = txt_paidamt.Text.Trim
            cmd2.Parameters.Add("@_rcpt_rcvd_amt", SqlDbType.Decimal).Value = txt_rcvdamt.Text.Trim
            cmd2.Parameters.Add("@_rcpt_credit_limit", SqlDbType.Decimal).Value = 0
        cmd2.Parameters.Add("@_rcpt_credit_balance", SqlDbType.Decimal).Value = 0
            cmd2.Parameters.Add("@_rcpt_rmk", SqlDbType.NVarChar).Value = txt_remarks.Text.Trim
            cmd2.Parameters.Add("@_rcpt_sts", SqlDbType.NVarChar).Value = "V"
        cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
            cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")
            cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")
            cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

            ' Dim cmd2 As New SqlCommand(RCPTqry)
            cmd2.Connection = CONN
            cmd2.Transaction = TRAN
            cmd2.ExecuteNonQuery()

            If ddl_sourcetype.SelectedValue.Trim = "A" And ddl_destintype.SelectedValue.Trim = "A" Then


                Dim cmd3 As SqlCommand = New SqlCommand("insert into Receipt_dtls  (rcpt_no , rcpt_typ , rcpt_ref_id , " &
                                                        " rcpt_frm_typ, rcpt_frm_acc, rcpt_frm_name, rcpt_to_typ, rcpt_to_acc, rcpt_to_name, " &
                                                        " rcpt_actual_amt, rcpt_balance_amt, rcpt_paid_amt, rcpt_rcvd_amt, " &
                                                        "  rcpt_credit_limit, rcpt_credit_balance, rcpt_rmk, rcpt_sts,  " &
                                                        " createby, createon, updateby, updateon, comp_id ) " &
                                                        " values(@_rcpt_no ,  @_rcpt_typ ,  @_rcpt_ref_id , " &
                                                        " @_rcpt_frm_typ,  @_rcpt_frm_acc,  @_rcpt_frm_name,  @_rcpt_to_typ,  @_rcpt_to_acc,  @_rcpt_to_name, " &
                                                        " @_rcpt_actual_amt,  @_rcpt_balance_amt,  @_rcpt_paid_amt,  @_rcpt_rcvd_amt, " &
                                                        " @_rcpt_credit_limit,  @_rcpt_credit_balance,  @_rcpt_rmk,  @_rcpt_sts,  " &
                                                        " @_createby, @_createon, @_updateby, @_updateon, @_comp_id )", CONN)

                cmd3.Parameters.Add("@_rcpt_no", SqlDbType.NVarChar).Value = load_next_receipt_no(CONN, TRAN, 1)
                cmd3.Parameters.Add("@_rcpt_typ", SqlDbType.NVarChar).Value = ddl_rcpttype.SelectedValue.Trim
                cmd3.Parameters.Add("@_rcpt_ref_id", SqlDbType.NVarChar).Value = txt_refid.Text.Trim
                cmd3.Parameters.Add("@_rcpt_frm_typ", SqlDbType.NVarChar).Value = ddl_destintype.SelectedValue.Trim
                cmd3.Parameters.Add("@_rcpt_frm_acc", SqlDbType.NVarChar).Value = ddl_destininfo.SelectedValue.Trim
                cmd3.Parameters.Add("@_rcpt_frm_name", SqlDbType.NVarChar).Value = txt_destininfo.Text.Trim
                cmd3.Parameters.Add("@_rcpt_to_typ", SqlDbType.NVarChar).Value = ddl_sourcetype.SelectedValue.Trim
                cmd3.Parameters.Add("@_rcpt_to_acc", SqlDbType.NVarChar).Value = ddl_sourceinfo.SelectedValue.Trim
                cmd3.Parameters.Add("@_rcpt_to_name", SqlDbType.NVarChar).Value = txt_sourceinfo.Text.Trim
                cmd3.Parameters.Add("@_rcpt_actual_amt", SqlDbType.Decimal).Value = 0
                cmd3.Parameters.Add("@_rcpt_balance_amt", SqlDbType.Decimal).Value = 0
                cmd3.Parameters.Add("@_rcpt_paid_amt", SqlDbType.Decimal).Value = txt_rcvdamt.Text.Trim
                cmd3.Parameters.Add("@_rcpt_rcvd_amt", SqlDbType.Decimal).Value = txt_paidamt.Text.Trim
                cmd3.Parameters.Add("@_rcpt_credit_limit", SqlDbType.Decimal).Value = 0
                cmd3.Parameters.Add("@_rcpt_credit_balance", SqlDbType.Decimal).Value = 0
                cmd3.Parameters.Add("@_rcpt_rmk", SqlDbType.NVarChar).Value = txt_remarks.Text.Trim
                cmd3.Parameters.Add("@_rcpt_sts", SqlDbType.NVarChar).Value = "V"
                cmd3.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
                cmd3.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")
                cmd3.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd3.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")
                cmd3.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

                ' Dim cmd3 As New SqlCommand(RCPTqry)
                cmd3.Connection = CONN
                cmd3.Transaction = TRAN
                cmd3.ExecuteNonQuery()

            End If


            TRAN.Commit()

            STAT = True
        Catch err As Exception
            STAT = False
            TRAN.Rollback()
            msg_box(Me, err.Message.ToString)
            ' mbox("Error : " & err.Message.ToString)
        Finally

            con.Close()
        End Try

        If STAT Then
            Call load_receipt_list_grid()
            Call clear_control_values()
        Else
            msg_box(Me, "Info Not Proceeded...")
            ' Call clear_Main_control_values()
        End If

    End Sub

    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = txt_rcptgid.Text.Trim
        If gid <> "" Then
            Dim STAT As Boolean = False
            Dim CONN As SqlConnection = con
            Dim TRAN As SqlTransaction = Nothing
            Try
                If Not CONN.State = ConnectionState.Open Then
                    CONN.Open()
                End If
                TRAN = CONN.BeginTransaction
                Dim comp As String = Session("login_compid")
                Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")



                Dim qry As String = "Update Receipt_dtls   SET   updateby='" & upby & "', updateon = '" & upon & "'" &
                                " where rcpt_gid='" & gid & "' "
                Dim cmd2 As SqlCommand = New SqlCommand("Update Receipt_dtls SET   rcpt_typ=@_rcpt_typ , rcpt_ref_id=@_rcpt_ref_id , " &
                                                        " rcpt_frm_typ=@_rcpt_frm_typ, rcpt_frm_acc=@_rcpt_frm_acc, " &
                                                        " rcpt_frm_name=@_rcpt_frm_name, rcpt_to_typ=@_rcpt_to_typ, rcpt_to_acc=@_rcpt_to_acc, rcpt_to_name=@_rcpt_to_name, " &
                                                        " rcpt_paid_amt=@_rcpt_paid_amt, rcpt_rcvd_amt=@_rcpt_rcvd_amt, " &
                                                        " rcpt_rmk=@_rcpt_rmk, rcpt_sts=@_rcpt_sts,  " &
                                                        " updateby=@_updateby, updateon=@_updateon, comp_id=@_comp_id   " &
                                                        " where rcpt_gid=@_rcpt_gid and rcpt_no=@_rcpt_no ", CONN)

                cmd2.Parameters.Add("@_rcpt_gid", SqlDbType.NVarChar).Value = gid
                cmd2.Parameters.Add("@_rcpt_no", SqlDbType.NVarChar).Value = txt_rcptno.Text 'load_next_receipt_no(CONN, TRAN, 1)
                cmd2.Parameters.Add("@_rcpt_typ", SqlDbType.NVarChar).Value = ddl_rcpttype.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_ref_id", SqlDbType.NVarChar).Value = txt_refid.Text.Trim
                cmd2.Parameters.Add("@_rcpt_frm_typ", SqlDbType.NVarChar).Value = ddl_sourcetype.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_frm_acc", SqlDbType.NVarChar).Value = ddl_sourceinfo.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_frm_name", SqlDbType.NVarChar).Value = txt_sourceinfo.Text.Trim
                cmd2.Parameters.Add("@_rcpt_to_typ", SqlDbType.NVarChar).Value = ddl_destintype.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_to_acc", SqlDbType.NVarChar).Value = ddl_destininfo.SelectedValue.Trim
                cmd2.Parameters.Add("@_rcpt_to_name", SqlDbType.NVarChar).Value = txt_destininfo.Text.Trim
                cmd2.Parameters.Add("@_rcpt_actual_amt", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_balance_amt", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_paid_amt", SqlDbType.Decimal).Value = txt_paidamt.Text.Trim
                cmd2.Parameters.Add("@_rcpt_rcvd_amt", SqlDbType.Decimal).Value = txt_rcvdamt.Text.Trim
                cmd2.Parameters.Add("@_rcpt_credit_limit", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_credit_balance", SqlDbType.Decimal).Value = 0
                cmd2.Parameters.Add("@_rcpt_rmk", SqlDbType.NVarChar).Value = txt_remarks.Text.Trim
                cmd2.Parameters.Add("@_rcpt_sts", SqlDbType.NVarChar).Value = "V"
                cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy HH:mm:ss")
                cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

                ' Dim cmd2 As New SqlCommand(RCPTqry)
                cmd2.Connection = CONN
                cmd2.Transaction = TRAN
                cmd2.ExecuteNonQuery()
                cmd2.Connection = CONN
                cmd2.Transaction = TRAN
                cmd2.ExecuteNonQuery()

                TRAN.Commit()
                STAT = True

            Catch err As Exception
                STAT = False
                TRAN.Rollback()
                msg_box(Me, err.Message.ToString)
                ' mbox("Error : " & err.Message.ToString)
            Finally

                con.Close()
            End Try

            If STAT Then
                Call load_receipt_list_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "Info Not Proceeded...")
                ' Call clear_Main_control_values()
            End If
        Else
            msg_box(Me, "GID is null")
        End If

    End Sub

    Protected Sub DeleteUserButton_Click(sender As Object, e As System.EventArgs) Handles DeleteUserButton.Click
        Dim gid As String = txt_rcptgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim qry As String = "delete from Receipt_dtls where rcpt_gid ='" & gid & "' and rcpt_sts = 'V' "
            Dim inssts As Integer = ConfigData.postDataToTable(qry)
            If inssts > 0 Then
                Call load_receipt_list_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "Delete is unsuccessful")
            End If
        Else
            msg_box(Me, "GID is null")
        End If
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_rcpt_list.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_rcpt_list.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_rcpt_list.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_rcpt_list.HeaderRow.Parent.Controls.AddAt(1, row)



            For j As Integer = 0 To gv_rcpt_list.Columns.Count - 2
                Dim txtfooter As New TextBox()
                Dim footerval As Double = 0

                If j = 1 Then
                    txtfooter.Text = "Count: " & gv_rcpt_list.Rows.Count

                ElseIf j = 4 Then
                    txtfooter.Text = "Total:"
                Else

                    If j > 10 And j < 13 Then
                        For k As Integer = 0 To gv_rcpt_list.Rows.Count - 1
                            footerval += CDec(gv_rcpt_list.Rows(k).Cells(j).Text)
                        Next
                        txtfooter.Text = footerval.ToString("N3")
                    End If

                End If
                gv_rcpt_list.FooterRow.Cells(j).Text = txtfooter.Text
            Next
        End If
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Receipt_dtls.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_rcpt_list.AllowPaging = False
            'Me.BindGrid()

            gv_rcpt_list.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_rcpt_list.HeaderRow.Cells
                'gv_rcpt_list.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_rcpt_list.HeaderStyle.BackColor
            Next

            gv_rcpt_list.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_rcpt_list.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_rcpt_list.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_rcpt_list.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_rcpt_list.RenderControl(hw)
            ''style to format numbers to string
            'Dim style As String = "<style> .textmode { } </style>"
            ' Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
End Class
