Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO

Partial Class Report_Purchase_Rep
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Session("PURCHASEREP_gv_Sales_rep") = Nothing
            txt_fdte.Text = Today.AddDays(-7).ToShortDateString
            txt_tdte.Text = Today.ToShortDateString
            Call load_Customer_details()
            Call load_report_grid()
        End If
    End Sub

    Public Sub load_Customer_details()
        Dim qry As String = "SELECT  cust_sup_id ID, cust_sup_id + ' - ' + cust_sup_name as NAME FROM cust_supp_dtls where cust_sup_sts = 'S' "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dt.Rows.Add("%", " * - ALL CUSTOMER ")
        dd_custid.DataSource = dt
        dd_custid.DataBind()
        dd_custid.SelectedIndex = dt.Rows.Count - 1
    End Sub

    Public Sub load_report_grid()
        Dim srch As String = txt_srch.Text
        'If srch.Trim.Length < 1 Then
        srch = srch.Trim & "%"
        'Else
        '    srch = srch.Trim
        'End If
        Dim fromdt As Date = txt_fdte.Text
        Dim todt As Date = txt_tdte.Text

        Dim qry As String = "SELECT * FROM [pr_dtls] Where pr_cust_id like '" + dd_custid.SelectedValue + "' " +
                            " and  createon between '" + fromdt.ToString("MM/dd/yyyy HH:mm:ss") + "' and  '" + todt.AddDays(1).AddSeconds(-1).ToString("MM/dd/yyyy HH:mm:ss") + "'   " +
                            " and ( pr_remark like '" + srch + "'  or pr_inv_id like '" + srch + "' " +
                            "  or pr_rmk1 like '" + srch + "'  or pr_rmk2 like '" + srch + "'   )"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Session("PURCHASEREP_gv_Sales_rep") = dt
        gv_Sales_rep.DataSource = dt
        gv_Sales_rep.DataBind()
        btnExport.Visible = dt.Rows.Count
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        load_report_grid()
    End Sub
    Private Sub gv_Sales_rep_Load(sender As Object, e As EventArgs) Handles gv_Sales_rep.Load
        Dim dt As DataTable = TryCast(Session("PURCHASEREP_gv_Sales_rep"), DataTable)
        gv_Sales_rep.DataSource = dt
        gv_Sales_rep.DataBind()
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_Sales_rep.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_Sales_rep.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_Sales_rep.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_Sales_rep.HeaderRow.Parent.Controls.AddAt(1, row)


            For j As Integer = 0 To gv_Sales_rep.Columns.Count - 2
                Dim txtfooter As New TextBox()
                Dim footerval As Double = 0

                If j = 1 Then
                    txtfooter.Text = "Count: " & gv_Sales_rep.Rows.Count

                ElseIf j = 4 Then
                    txtfooter.Text = "Total:"
                Else

                    If j > 4 And j < 10 Then
                        For k As Integer = 0 To gv_Sales_rep.Rows.Count - 1
                            footerval += CDec(gv_Sales_rep.Rows(k).Cells(j).Text)
                        Next
                        txtfooter.Text = footerval.ToString("N3")
                    End If

                End If
                gv_Sales_rep.FooterRow.Cells(j).Text = txtfooter.Text
                gv_Sales_rep.FooterRow.Cells(j).BackColor = Color.DarkGray
            Next
        End If

    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=PURCHASE_REP.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_Sales_rep.AllowPaging = False
            'Me.BindGrid()

            gv_Sales_rep.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_Sales_rep.HeaderRow.Cells
                'gv_Sales_rep.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_Sales_rep.HeaderStyle.BackColor
            Next

            gv_Sales_rep.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_Sales_rep.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_Sales_rep.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_Sales_rep.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_Sales_rep.RenderControl(hw)
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
