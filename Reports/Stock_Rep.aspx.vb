Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO


Partial Class Report_Stock_Rep
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Session("STOCKREP_gv_itmprclst") = Nothing
            Call load_itemGroup_details()
            Call load_report_grid()
        End If
    End Sub

    Public Sub load_itemGroup_details()
        Dim qry As String = "SELECT [itm_grp_code] as grp_cd, itm_grp_code + ' - ' + itm_grp_desc as grp_desc FROM [itm_grp_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dt.Rows.Add("%", " * - ALL GROUP ")
        dd_itemgrp.DataSource = dt
        dd_itemgrp.DataBind()
        dd_itemgrp.SelectedIndex = dt.Rows.Count - 1
    End Sub



    Public Sub load_report_grid()
        Dim srch As String = txt_srch.Text
        'If srch.Trim.Length < 1 Then
        srch = srch.Trim & "%"
        'Else
        '    srch = srch.Trim
        'End If 
        ' Dim asondte As Date = CDate(txt_asondte.Text)

        Dim qry As String = "SELECT ITEMCD , QTY, item_dtls.* ,itm_grp_desc, (QTY * itm_pr_cost) itm_pr_value " &
                            " FROM item_dtls left outer join  V_Item_Stock_Summ on  item_dtls.itm_pr_id = V_Item_Stock_Summ.ITEMCD  " &
                            "                left outer join  itm_grp_dtls on item_dtls.itm_pr_grp = itm_grp_dtls.itm_grp_code " &
            " where itm_pr_grp like '" & dd_itemgrp.SelectedValue.Trim & "' and ( itm_pr_id like '" & srch.Trim & "' or itm_pr_grp like '" & srch.Trim & "' or  itm_pr_info like '" & srch.Trim & "' ) order by itm_pr_id "

        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Session("STOCKREP_gv_itmprclst") = dt
        gv_itmprclst.DataSource = dt
        gv_itmprclst.DataBind()
        btnExport.Visible = dt.Rows.Count
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        load_report_grid()
    End Sub
    Private Sub gv_itmprclst_Load(sender As Object, e As EventArgs) Handles gv_itmprclst.Load
        Dim dt As DataTable = TryCast(Session("STOCKREP_gv_itmprclst"), DataTable)
        gv_itmprclst.DataSource = dt
        gv_itmprclst.DataBind()
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_itmprclst.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_itmprclst.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_itmprclst.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_itmprclst.HeaderRow.Parent.Controls.AddAt(1, row)


            For j As Integer = 0 To gv_itmprclst.Columns.Count - 2
                Dim txtfooter As New TextBox()
                Dim footerval As Double = 0

                If j = 1 Then
                    txtfooter.Text = "Count: " & gv_itmprclst.Rows.Count

                ElseIf j = 4 Then
                    txtfooter.Text = "Total:"
                Else

                    'If j > 4 And j < 10 Then
                    '    For k As Integer = 0 To gv_itmprclst.Rows.Count - 1
                    '        footerval += CDec(gv_itmprclst.Rows(k).Cells(j).Text)
                    '    Next
                    '    txtfooter.Text = footerval.ToString("N3")
                    'End If

                End If
                gv_itmprclst.FooterRow.Cells(j).Text = txtfooter.Text
            Next
        End If

    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Stock_Rep.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_itmprclst.AllowPaging = False
            'Me.BindGrid()

            gv_itmprclst.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_itmprclst.HeaderRow.Cells
                'gv_itmprclst.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_itmprclst.HeaderStyle.BackColor
            Next

            gv_itmprclst.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_itmprclst.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_itmprclst.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_itmprclst.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_itmprclst.RenderControl(hw)
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
