Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Maintaince_Item_Details
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            session_datatable_store("ITEMDTLS_itm_grp_dtls", Nothing)
            Call load_customer_grid()
            Call clear_control_values()
            Call load_itemGroup_details()
        End If
    End Sub
    Public Sub load_itemGroup_details()
        Dim qry As String = "SELECT [itm_grp_code] as grp_cd, [itm_grp_desc] as grp_desc FROM [itm_grp_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dditemgrp.DataSource = dt
        dditemgrp.DataBind()
        session_datatable_store("ITEMDTLS_itm_grp_dtls", dt)
    End Sub
    Public Sub load_customer_grid()
        Dim qry As String = "SELECT [itm_pr_gid], [itm_pr_id], [itm_pr_info], [itm_pr_grp] FROM [item_dtls] order by itm_pr_id "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itm.DataSource = dt
        gv_itm.DataBind()
        session_datatable_store("ITEMDTLS_Item_dtls_gv", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_itm.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT [itm_pr_gid], [itm_pr_id], [itm_pr_info], [itm_pr_grp] " &
            " FROM [item_dtls]" &
            " where itm_pr_gid ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            itmgid.Text = dt.Rows(0)("itm_pr_gid").ToString.Trim
            itmcd.Text = dt.Rows(0)("itm_pr_id").ToString.Trim
            If itmcd.Text.Trim <> "" Then
                itmcd.ReadOnly = True
            End If

            itmname.Text = dt.Rows(0)("itm_pr_info").ToString.Trim
            dditemgrp.SelectedValue = dt.Rows(0)("itm_pr_grp").ToString.Trim

            itmgid.Visible = True
            UpdateUserButton.Visible = True
            DeleteUserButton.Visible = True
            CancelUserButton.Visible = True
            CreateUserButton.Visible = False
        Else
            Call clear_control_values()
        End If
    End Sub

    Protected Sub CreateUserButton_Click(sender As Object, e As System.EventArgs) Handles CreateUserButton.Click
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = Session("login_compid")
        Dim cmd As SqlCommand = New SqlCommand("insert into  item_dtls (itm_pr_id, itm_pr_info, itm_pr_grp, " &
                                               " itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, " &
                                               " @_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

        cmd.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = itmcd.Text.Trim
        cmd.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
        cmd.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
        cmd.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_itm_pr_cost", SqlDbType.NVarChar).Value = 0
        cmd.Parameters.Add("@_itm_pr_price", SqlDbType.NVarChar).Value = 0
        cmd.Parameters.Add("@_itm_pr_taxpct", SqlDbType.NVarChar).Value = 0
        cmd.Parameters.Add("@_itm_pr_taxamt", SqlDbType.NVarChar).Value = 0
        cmd.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = "S"
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

        Dim inssts As Boolean = ConfigData.postquery(cmd)
        If inssts Then
            If Not IsNothing(Request.QueryString("grpid")) Then
                'Session("new_itemid") = itmcd.Text.Trim
                'Session("new_grpid") = dditemgrp.SelectedValue.Trim
                Response.Redirect("../Maintainance/Item_Price.aspx?itm_id=" & itmcd.Text.Trim)
            End If
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim cmd As SqlCommand = New SqlCommand("update item_dtls set itm_pr_id=@_itm_pr_id, itm_pr_info=@_itm_pr_info, itm_pr_grp=@_itm_pr_grp, " &
                                               " updateby=@_updateby, updateon = @_updateon  where itm_pr_gid=@_itm_pr_gid ", ConfigData.con)

            cmd.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = itmcd.Text.Trim
            cmd.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
            cmd.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
            cmd.Parameters.Add("@_itm_pr_gid", SqlDbType.NVarChar).Value = gid
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = upon

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts > 0 Then
                load_customer_grid()
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
            Dim qry As String = "delete from item_dtls where itm_pr_gid='" & gid & "'"

            Dim cmd As SqlCommand = New SqlCommand("delete from item_dtls where itm_pr_gid=@_itm_pr_gid ", ConfigData.con)

            cmd.Parameters.Add("@_itm_pr_gid", SqlDbType.NVarChar).Value = gid

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts > 0 Then
                load_customer_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "Delete Is unsuccessful")
            End If
        Else
            msg_box(Me, "GID Is null")
        End If
    End Sub

    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        itmgid.Text = ""
        itmname.Text = ""
        itmcd.Text = ""
        dditemgrp.SelectedIndex = 0
        itmgid.Visible = False
        itmcd.ReadOnly = False
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub
    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_itm.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_itm.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_itm.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_itm.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Protected Sub dditemgrp_Load(sender As Object, e As System.EventArgs) Handles dditemgrp.Init
        dditemgrp.DataSource = session_datatable_fill("ITEMDTLS_itm_grp_dtls")
        dditemgrp.DataBind()
    End Sub
    'Protected Sub UploadButton_Click(sender As Object, e As System.EventArgs)

    '    If (FileUploadControl.HasFile) Then

    '        Try

    '            Dim filename As String = FileUploadControl.FileName
    '            FileUploadControl.SaveAs(Server.MapPath("~/Uploaded/") + filename)
    '            msg_box(Me, "Upload status: File uploaded!")

    '        Catch ex As Exception

    '            msg_box(Me, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message)
    '        End Try
    '    End If
    'End Sub

    Private Sub itmcd_TextChanged(sender As Object, e As EventArgs) Handles itmcd.TextChanged
        Dim code As String = itmcd.Text.Trim.ToUpper
        Dim qry As String = "SELECT * FROM [item_dtls] where Upper([itm_pr_id]) like '" & code & "' "
        If ConfigData.getDataToDatatable(qry).Rows.Count > 0 Then
            itmcd.Text = ""
        End If
    End Sub

    Private Sub gv_itm_Init(sender As Object, e As EventArgs) Handles gv_itm.Init
        gv_itm.DataSource = session_datatable_fill("ITEMDTLS_Item_dtls_gv")
        gv_itm.DataBind()
    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Item_Detail.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_itmprclst.AllowPaging = False
            'Me.BindGrid()

            gv_itm.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_itm.HeaderRow.Cells
                'gv_itmprclst.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_itm.HeaderStyle.BackColor
            Next

            gv_itm.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_itm.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_itm.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_itm.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_itm.RenderControl(hw)
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
