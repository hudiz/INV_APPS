Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class Report_Sales_Details_Rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsCallback AndAlso Not IsPostBack Then
            Call init_details()
            If Not IsNothing(Request.QueryString("invno")) Then
                Dim invno As String = Request.QueryString("invno").Trim
                Call Generate_DataTable_Sales(invno)
            End If
        End If
    End Sub

    Private Sub init_details()
        ' Call Generate_item_Sales()
        Call Generate_item_paidby_list()
        Call Generate_item_customer_list()
    End Sub

    Private Sub Generate_item_paidby_list()
        Dim qry As String = "SELECT  [acc_id],[acc_name] FROM [acc_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dd_pdbyname1.DataSource = dt
        dd_pdbyname1.DataBind()
        dd_pdbyname2.DataSource = dt
        dd_pdbyname2.DataBind()
    End Sub

    Private Sub Generate_item_customer_list()
        Dim qry As String = "SELECT [cust_sup_id] as cid, [cust_sup_id] + '-' + [cust_sup_name] as cname FROM [cust_supp_dtls] where cust_sup_sts='C'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("cid") = ""
        dr("cname") = "-select-"
        dt.Rows.InsertAt(dr, 0)
        dd_custid.DataSource = dt
        dd_custid.DataBind()
        dd_custid.SelectedIndex = 0
    End Sub


    'Private Sub Generate_item_Sales()
    '    Dim qry As String = "SELECT [itm_pr_id] as itmval, [itm_pr_id] + ' - ' + [itm_pr_info] as itmtxt FROM [item_dtls]"
    '    Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
    '    Dim dr As DataRow = dt.NewRow
    '    dr("itmval") = ""
    '    dr("itmtxt") = "-select-"
    '    If dt.Rows.Count > 0 Then
    '        For Each dr1 In dt.Rows
    '            dr1("itmval") = dr1("itmval").ToString.Trim
    '            dr1("itmtxt") = dr1("itmtxt").ToString.Trim
    '        Next
    '    End If
    '    dt.Rows.InsertAt(dr, 0)
    '    dd_itm.DataSource = dt
    '    dd_itm.DataBind()
    'End Sub
    Private Sub Generate_DataTable_Sales(invno As String)
        Dim qry As String = "select * from sr_dtls where sr_inv_id='" & invno & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dt_itm As New DataTable
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            txt_dte.Text = dr("sr_date_time").ToString.Trim.Replace(" 00:00:00", "")
            txt_invno.Text = dr("sr_inv_id").ToString.Trim
            dd_custid.SelectedValue = dr("sr_cust_id").ToString.Trim
            txt_custname.Text = dr("sr_cust_name").ToString.Trim

            txt_tot.Text = dr("sr_tot_amt").ToString.Trim
            txt_netdisc.Text = dr("sr_disc").ToString.Trim
            txt_CGSTtax.Text = dr("sr_tax_amt").ToString.Trim
            txt_SGSTtax.Text = dr("sr_tax_amt2").ToString.Trim
            txt_netoth.Text = dr("sr_oth_amt").ToString.Trim
            txt_nettot.Text = dr("sr_net_amt").ToString.Trim

            rbl_credit.SelectedValue = dr("sr_crd_flg").ToString.Trim
            txt_credit_amt.Text = dr("sr_cr_amt").ToString.Trim

            dd_pdbyname1.SelectedValue = dr("sr_paid_by1").ToString.Trim
            txt_pdbyamt1.Text = dr("sr_amt1").ToString.Trim
            txt_pdbyrmk1.Text = dr("sr_rmk1").ToString.Trim
            dd_pdbyname2.SelectedValue = dr("sr_paid_by2").ToString.Trim
            txt_pdbyamt2.Text = dr("sr_amt2").ToString.Trim
            txt_pdbyrmk2.Text = dr("sr_rmk2").ToString.Trim
            txt_rmk.Text = dr("sr_remark").ToString.Trim

            qry = "select sr_item_dtls.*,item_dtls.itm_pr_unt as unt, item_dtls.itm_pr_rmk  from " &
                " (sr_item_dtls left outer join item_dtls on sr_itm_id=itm_pr_id) where sr_itm_inv='" & invno & "'"
            dt_itm = ConfigData.getDataToDatatable(qry)
            gv_sales.DataSource = dt_itm
            gv_sales.DataBind()
        End If
    End Sub

    Private Sub Generate_Sales_invoice_slip(invno As String)
        Dim qry As String = " "
        Dim dt_itm As New DataTable
        Session("Sales_invoice_dt_itm") = Nothing
        'qry = "select sr_dtls.*,sr_item_dtls.*,item_dtls.itm_pr_unt, item_dtls.itm_pr_cost as unt from " &
        '        " (sr_item_dtls left outer join sr_dtls on sr_inv_id=sr_itm_inv  " &
        '        "   left outer join item_dtls on sr_itm_id=itm_pr_id) where sr_itm_inv='" & invno & "'"
        qry = "select  sr_guid , sr_inv_id  , sr_cust_id  , sr_cust_name  , sr_tot_itm  , sr_tot_amt  , sr_disc  , " &
              " sr_date_time   , sr_net_amt  , sr_paid_by1  , sr_rmk1  , sr_amt1  , sr_paid_by2   , sr_rmk2  , " &
              " sr_amt2  , sr_remark  , sr_sts  , sr_crd_flg , sr_flg  , sr_cr_amt   , sr_item_dtls.*, item_dtls.itm_pr_unt , item_dtls.itm_pr_cost  , item_dtls.itm_pr_rmk    " &
              "from sr_item_dtls left outer join sr_dtls on sr_inv_id=sr_itm_inv  " &
              "                  left outer join item_dtls on sr_itm_id=itm_pr_id  where sr_itm_inv='" & invno & "'"
        dt_itm = ConfigData.getDataToDatatable(qry)

        If dt_itm.Rows.Count > 0 Then
            Session("Sales_invoice_dt_itm") = dt_itm
        End If
    End Sub
    Protected Sub btn_print_Click(sender As Object, e As System.EventArgs) Handles btn_print.Click
        Dim rpt As New ReportDocument
        Dim invno As String = txt_invno.Text.Trim
        Generate_Sales_invoice_slip(invno)
        Dim rname As String = Server.MapPath("~/Reports/SalesInvoiceSlip.rpt")
        Dim dwnldfname As String = Server.MapPath("~/ReportsGenerate/SalesInvoice_" & invno & ".pdf")
        Dim dt As DataTable = TryCast(Session("Sales_invoice_dt_itm"), DataTable)

        If dt.Rows.Count > 0 Then
            rpt.Load(rname)
            rpt.SetDataSource(dt)

            'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
            'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            'rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname)
            'rpt.Close()
            'rpt.Dispose()


            'Response.ClearContent()
            'Response.ClearHeaders()
            'Response.AddHeader("content-disposition", "attachment;filename =" & "NAVREPORT.xls")
            'Response.ContentType = "application/ms-excel"
            'Response.TransmitFile(dwnldfname)
            ''If dt.Rows.Count > 0 Then
            '    rpt.Load(rname)
            '    rpt.SetDataSource(dt)

            rpt.PrintOptions.PaperSize = PaperSize.PaperA4
            rpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, invno)
            rpt.Close()
            rpt.Dispose()


            'Response.ClearContent()
            'Response.ClearHeaders()
            'Response.AddHeader("content-disposition", "attachment;filename =" & "SalesInvoice_" & invno & ".pdf")
            'Response.ContentType = "application/pdf"
            'Response.TransmitFile(dwnldfname)

            'End If
        End If
    End Sub
    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_sales.Rows.Count > 0 Then

            For j As Integer = 0 To gv_sales.Columns.Count - 1
                Dim txtfooter As New TextBox()
                Dim footerval As Double = 0

                If j = 2 Then
                    txtfooter.Text = "Count: " & gv_sales.Rows.Count

                ElseIf j = 5 Then
                    txtfooter.Text = "Total:"
                Else
                    If j = 6 Or j = 7 Or j = 9 Then
                        For k As Integer = 0 To gv_sales.Rows.Count - 1
                            footerval += CDec(gv_sales.Rows(k).Cells(j).Text)
                        Next
                        txtfooter.Text = footerval.ToString("N3")
                    End If

                End If
                gv_sales.FooterRow.Cells(j).Text = txtfooter.Text
            Next
        End If

    End Sub
End Class
