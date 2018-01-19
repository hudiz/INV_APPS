Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO

Partial Class Trans_Stock_Adjust
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            session_datatable_store("STKDTLS_stock_adj_dtls", Nothing)
            Call clear_control_values()
            Call load_unit_details()
            Call load_itemGroup_details()
            Call load_item_details()
        End If
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
    Private Sub dditemgrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dditemgrp.SelectedIndexChanged
        Call load_item_details()
    End Sub
    Public Sub load_item_details()
        Dim qry As String = "SELECT [itm_pr_gid], [itm_pr_id], [itm_pr_info], [itm_pr_grp] , itm_pr_id + ' - ' + itm_pr_info as item_desc FROM [item_dtls] " &
            " where itm_pr_grp ='" & dditemgrp.SelectedValue.Trim & "' order by itm_pr_id "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)

        dditmcd.DataSource = dt
        dditmcd.DataBind()

        If dditmcd.Items.Count > 0 Then
            Call load_item_price_list_grid()
        End If

    End Sub

    Private Sub dditmcd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dditmcd.SelectedIndexChanged
        Call load_item_price_list_grid()
    End Sub


    Public Sub load_item_price_list_grid()
        Dim qry As String = "SELECT * FROM [stock_adj_dtls]" &
            " where stk_itm_id ='" & dditmcd.SelectedValue.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmprclst.DataSource = dt
        gv_itmprclst.DataBind()
        session_datatable_store("STKDTLS_stock_adj_dtls", dt)

        btnExport.Visible = dt.Rows.Count
    End Sub
    Private Sub gv_itmprclst_Init(sender As Object, e As EventArgs) Handles gv_itmprclst.Init

        gv_itmprclst.DataSource = session_datatable_fill("STKDTLS_stock_adj_dtls")
        gv_itmprclst.DataBind()

    End Sub
    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        itmgid.Text = ""
        'dditemgrp.SelectedValue = ""
        'dditmcd.SelectedValue = "" 

        itmQty.Text = "0"
        itmprice.Text = "0.000"
        dditemUnit.SelectedValue = "DZN"
        itmremark.Text = ""
        dditemstatus.SelectedValue = "S"
        itmgid.Visible = False
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_itmprclst.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT * " & " FROM stock_adj_dtls " & " where stk_gid ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            itmgid.Text = dt.Rows(0)("stk_gid").ToString.Trim
            dditemgrp.SelectedValue = dt.Rows(0)("stk_itm_grp").ToString.Trim
            dditmcd.SelectedValue = dt.Rows(0)("stk_itm_id").ToString.Trim
            itmQty.Text = dt.Rows(0)("stk_itm_qty").ToString.Trim
            itmprice.Text = dt.Rows(0)("stk_itm_price").ToString.Trim
            dditemUnit.SelectedValue = dt.Rows(0)("stk_itm_unit").ToString.Trim
            itmremark.Text = dt.Rows(0)("stk_rmk").ToString.Trim
            dditemstatus.SelectedValue = dt.Rows(0)("stk_sts").ToString.Trim
            itmgid.Visible = True
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
        Dim comp As String = Session("login_compid")
        Dim qry As String = "insert into stock_adj_dtls (stk_itm_id,   stk_itm_grp, " &
                            " stk_itm_qty,stk_itm_price,stk_itm_unit, stk_rmk,stk_sts, " &
                            " createby, createon, updateby, updateon, comp_id) " &
                            " values('" & dditmcd.SelectedValue.Trim & "','" & dditemgrp.SelectedValue.Trim & "'," &
                                   "  '" & itmQty.Text.Trim & "','" & itmprice.Text.Trim & "','" & dditemUnit.SelectedValue.Trim & "'," &
                                   "    '" & itmremark.Text.Trim & "','" & dditemstatus.SelectedValue.Trim & "'," &
                                   "'" & crby & "'," & "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"
        Dim inssts As Integer = ConfigData.postDataToTable(qry)
        If inssts > 0 Then
            Call load_item_price_list_grid()
            Call clear_control_values()
        End If
    End Sub

    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")

            Dim qry As String = "Update stock_adj_dtls   SET stk_itm_price = '" & itmprice.Text.Trim & "', stk_itm_qty = '" & itmQty.Text.Trim & "', " &
                            " stk_itm_unit ='" & dditemUnit.SelectedValue.Trim & "'," &
                            "stk_rmk =  '" & itmremark.Text.Trim & "',stk_sts ='" & dditemstatus.SelectedValue.Trim & "' , " &
                            " updateby='" & upby & "', updateon = '" & upon & "'" &
                            " where stk_gid='" & gid & "' "
            'update item_dtls Set item_code='" & itmcd.Text.Trim & "',item_name='" & itmname.Text.Trim & "', item_grp ='" & dditemgrp.SelectedValue.Trim & "'," &
            Dim inssts As Integer = ConfigData.postDataToTable(qry)
            If inssts > 0 Then
                Call load_item_price_list_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "update is unsuccessful")
            End If
        Else
            msg_box(Me, "GID is null")
        End If

    End Sub

    Protected Sub DeleteUserButton_Click(sender As Object, e As System.EventArgs) Handles DeleteUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim qry As String = "delete from stock_adj_dtls where stk_gid='" & gid & "' and stk_sts = 'S' "
            Dim inssts As Integer = ConfigData.postDataToTable(qry)
            If inssts > 0 Then
                Call load_item_price_list_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "Delete is unsuccessful")
            End If
        Else
            msg_box(Me, "GID is null")
        End If
    End Sub


    'Protected Sub OnDataBound(sender As Object, e As EventArgs)
    '    If gv_itm.Rows.Count > 0 Then
    '        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
    '        For i As Integer = 0 To gv_itm.Columns.Count - 2
    '            Dim cell As New TableHeaderCell()
    '            Dim txtSearch As New TextBox()
    '            txtSearch.Width = gv_itm.Columns(i).ItemStyle.Width
    '            txtSearch.CssClass = "search_textbox form-control"
    '            txtSearch.Height = "25"
    '            cell.Controls.Add(txtSearch)
    '            row.Controls.Add(cell)
    '        Next
    '        gv_itm.HeaderRow.Parent.Controls.AddAt(1, row)
    '    End If
    'End Sub

    'Protected Sub OnDataBound_sitm(sender As Object, e As EventArgs)
    '    If gv_itm.Rows.Count > 0 Then
    '        Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
    '        For i As Integer = 0 To gv_itm.Columns.Count - 2
    '            Dim cell As New TableHeaderCell()
    '            Dim txtSearch As New TextBox()
    '            txtSearch.Width = gv_itm.Columns(i).ItemStyle.Width
    '            txtSearch.CssClass = "search_textbox_sitm form-control"
    '            txtSearch.Height = "25"
    '            cell.Controls.Add(txtSearch)
    '            row.Controls.Add(cell)
    '        Next
    '        gv_sitm.HeaderRow.Parent.Controls.AddAt(1, row)
    '    End If
    'End Sub 
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Stock_Adjust_Rep.xls")
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

            ' gv_itmprclst.HeaderRow.Parent.Controls.RemoveAt(1)

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
