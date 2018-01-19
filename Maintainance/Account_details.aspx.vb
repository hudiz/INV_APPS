Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Account_Account_details
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Public Sub load_customer_grid()
        Dim qry As String = "SELECT [acc_id], [acc_name], [acc_desc], [acc_sts] FROM [acc_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_acc.DataSource = dt
        gv_acc.DataBind()
        session_datatable_store("ACCTDTLS_acc_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub

    Private Sub gv_acc_Init(sender As Object, e As EventArgs) Handles gv_acc.Init
        gv_acc.DataSource = session_datatable_fill("ACCTDTLS_acc_dtls")
        gv_acc.DataBind()

    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_acc.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT [acc_id], [acc_name], [acc_desc], [acc_sts] " & _
            " FROM [acc_dtls]" & _
            " where acc_id ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            accgid.Text = dt.Rows(0)("acc_id").ToString.Trim
            accname.Text = dt.Rows(0)("acc_name").ToString.Trim
            accdesc.Text = dt.Rows(0)("acc_desc").ToString.Trim
            accgid.Visible = True
            accname.ReadOnly = True
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
        Dim cmd As SqlCommand = New SqlCommand("insert into acc_dtls (acc_name, acc_desc, acc_sts, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_acc_name,@_acc_desc,@_acc_sts," &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

        cmd.Parameters.Add("@_acc_name", SqlDbType.NVarChar).Value = accname.Text.Trim
        cmd.Parameters.Add("@_acc_desc", SqlDbType.NVarChar).Value = accdesc.Text.Trim
        cmd.Parameters.Add("@_acc_sts", SqlDbType.NVarChar).Value = "A"
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

        Dim inssts As Boolean = ConfigData.postquery(cmd)
        If inssts > 0 Then
            load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = accgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")

            Dim cmd As SqlCommand = New SqlCommand("update acc_dtls set acc_name=@_acc_name, acc_desc=@_acc_desc," &
                                                   "updateby=@_updateby, updateon = @_updateon  where acc_id=@_acc_id ", ConfigData.con)

            cmd.Parameters.Add("@_acc_name", SqlDbType.NVarChar).Value = accname.Text.Trim
            cmd.Parameters.Add("@_acc_desc", SqlDbType.NVarChar).Value = accdesc.Text.Trim
            cmd.Parameters.Add("@_acc_id", SqlDbType.NVarChar).Value = gid
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
        Dim gid As String = accgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim qry As String = "delete from acc_dtls where acc_id='" & gid & "'"

            Dim cmd As SqlCommand = New SqlCommand("delete from acc_dtls   where acc_id=@_acc_id  ", ConfigData.con)
            cmd.Parameters.Add("@_acc_id", SqlDbType.NVarChar).Value = gid

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts Then
                load_customer_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "Delete is unsuccessful")
            End If
        Else
            msg_box(Me, "GID is null")
        End If
    End Sub

    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        accgid.Text = ""
        accname.Text = ""
        accdesc.Text = ""
        accgid.Visible = False
        accname.ReadOnly = False
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_acc.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_acc.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_acc.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_acc.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Private Sub accname_TextChanged(sender As Object, e As EventArgs) Handles accname.TextChanged
        Dim code As String = accname.Text.Trim.ToUpper
        Dim qry As String = "SELECT * FROM [acc_dtls] where Upper([acc_name]) like '" & code & "' "
        If ConfigData.getDataToDatatable(qry).Rows.Count > 0 Then
            accname.Text = ""
        End If
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Account_Detail.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_acc.AllowPaging = False
            'Me.BindGrid()

            gv_acc.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_acc.HeaderRow.Cells
                'gv_acc.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_acc.HeaderStyle.BackColor
            Next

            gv_acc.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_acc.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_acc.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_acc.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_acc.RenderControl(hw)
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
