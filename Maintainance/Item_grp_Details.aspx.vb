Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Maintaince_Item_grp_Details
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Public Sub load_customer_grid()
        Dim qry As String = "SELECT [itm_grp_id], [itm_grp_code], [itm_grp_desc] FROM [itm_grp_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itm.DataSource = dt
        gv_itm.DataBind()
        session_datatable_store("IGRPDTLS_itm_grp_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub
    Private Sub gv_itm_Init(sender As Object, e As EventArgs) Handles gv_itm.Init
        gv_itm.DataSource = session_datatable_fill("IGRPDTLS_itm_grp_dtls")
        gv_itm.DataBind()
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_itm.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT [itm_grp_id], [itm_grp_code], [itm_grp_desc] " & _
            " FROM [itm_grp_dtls]" & _
            " where itm_grp_id ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            itmgid.Text = dt.Rows(0)("itm_grp_id").ToString.Trim
            itmcd.Text = dt.Rows(0)("itm_grp_code").ToString.Trim
            itmname.Text = dt.Rows(0)("itm_grp_desc").ToString.Trim
            itmgid.Visible = True
            itmcd.ReadOnly = True
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
        Dim cmd As SqlCommand = New SqlCommand("insert into  itm_grp_dtls (itm_grp_code, itm_grp_desc, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_grp_code,@_itm_grp_desc," &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

        cmd.Parameters.Add("@_itm_grp_code", SqlDbType.NVarChar).Value = itmcd.Text.Trim
        cmd.Parameters.Add("@_itm_grp_desc", SqlDbType.NVarChar).Value = itmname.Text.Trim
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

        Dim inssts As Boolean = ConfigData.postquery(cmd)
        If inssts Then
            load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim cmd As SqlCommand = New SqlCommand("update itm_grp_dtls set itm_grp_code=@_itm_grp_code,itm_grp_desc=@_itm_grp_desc," &
                                               " updateby=@_updateby, updateon =@_updateon  where itm_grp_id=@_itm_grp_id ", ConfigData.con)

            cmd.Parameters.Add("@_itm_grp_code", SqlDbType.NVarChar).Value = itmcd.Text.Trim
            cmd.Parameters.Add("@_itm_grp_desc", SqlDbType.NVarChar).Value = itmname.Text.Trim
            cmd.Parameters.Add("@_itm_grp_id", SqlDbType.NVarChar).Value = gid
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = upon

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts Then
                Call load_customer_grid()
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
            'Dim upby As String = User.Identity.Name.ToString.Trim()
            'Dim upon As String = Date.Now.ToString("MM/dd/yyyy")

            'Dim cmd As SqlCommand = New SqlCommand("delete from itm_grp_dtls   where itm_grp_id=@_itm_grp_id ", ConfigData.con)

            'cmd.Parameters.Add("@_itm_grp_id", SqlDbType.NVarChar).Value = gid

            'Dim inssts As Boolean = ConfigData.postquery(cmd)
            'If inssts > 0 Then
            '    load_customer_grid()
            '    Call clear_control_values()
            'Else
            '    msg_box(Me, "Delete is unsuccessful")
            'End If
        Else
            msg_box(Me, "GID is null")
        End If
    End Sub

    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        itmgid.Text = ""
        itmname.Text = ""
        itmcd.Text = ""
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

    Private Sub itmcd_TextChanged(sender As Object, e As EventArgs) Handles itmcd.TextChanged
        Dim code As String = itmcd.Text.Trim.ToUpper
        Dim qry As String = "SELECT * FROM [itm_grp_dtls] where Upper([itm_grp_code]) like '" & code & "' "
        If ConfigData.getDataToDatatable(qry).Rows.Count > 0 Then
            itmcd.Text = ""
        End If
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Item_Group.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_itm.AllowPaging = False
            'Me.BindGrid()

            gv_itm.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_itm.HeaderRow.Cells
                'gv_itm.HeaderRow.Parent.Controls.AddAt(1, Row)
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
