Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Maintaince_Expense_Details
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Public Sub load_customer_grid()
        Dim qry As String = "SELECT [expns_id], [expns_type], [expns_name], [expns_code], [expns_sts] FROM [expns_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_exp.DataSource = dt
        gv_exp.DataBind()
        session_datatable_store("EXPDTLS_expns_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub
    Private Sub gv_exp_Init(sender As Object, e As EventArgs) Handles gv_exp.Init
        gv_exp.DataSource = session_datatable_fill("EXPDTLS_expns_dtls")
        gv_exp.DataBind()
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_exp.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT [expns_id], [expns_type], [expns_name], [expns_code], " & _
            " [expns_sts] FROM [expns_dtls]" & _
            " where expns_id ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            expgid.Text = dt.Rows(0)("expns_id").ToString.Trim
            expname.Text = dt.Rows(0)("expns_name").ToString.Trim
            ddexptype.SelectedValue = dt.Rows(0)("expns_type").ToString.Trim
            expcd.Text = dt.Rows(0)("expns_code").ToString.Trim
            expgid.Visible = True
            expcd.ReadOnly = True
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

        Dim cmd As SqlCommand = New SqlCommand("insert into  expns_dtls (expns_type, expns_name, expns_code, expns_sts,  " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_expns_type,@_expns_name,@_expns_code,@_expns_sts," &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

        cmd.Parameters.Add("@_expns_type", SqlDbType.NVarChar).Value = ddexptype.SelectedValue.Trim
        cmd.Parameters.Add("@_expns_name", SqlDbType.NVarChar).Value = expname.Text.Trim
        cmd.Parameters.Add("@_expns_code", SqlDbType.NVarChar).Value = expcd.Text.Trim
        cmd.Parameters.Add("@_expns_sts", SqlDbType.NVarChar).Value = "A"
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
        Dim gid As String = expgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")

            Dim cmd As SqlCommand = New SqlCommand("update expns_dtls set expns_type=@_expns_type,expns_name=@_expns_name,expns_code =@_expns_code, " &
                                               " updateby=@_updateby, updateon = @_updateon where expns_id=@_expns_id   ", ConfigData.con)

            cmd.Parameters.Add("@_expns_type", SqlDbType.NVarChar).Value = ddexptype.SelectedValue.Trim
            cmd.Parameters.Add("@_expns_name", SqlDbType.NVarChar).Value = expname.Text.Trim
            cmd.Parameters.Add("@_expns_code", SqlDbType.NVarChar).Value = expcd.Text.Trim
            cmd.Parameters.Add("@_expns_id", SqlDbType.NVarChar).Value = gid
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = upon

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts Then
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
        Dim gid As String = expgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim cmd As SqlCommand = New SqlCommand(" delete from expns_dtls where expns_id=@_expns_id  )", ConfigData.con)
            cmd.Parameters.Add("@_expns_id", SqlDbType.NVarChar).Value = gid
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
        expgid.Text = ""
        expname.Text = ""
        ddexptype.SelectedIndex = 0
        expcd.Text = ""
        expgid.Visible = False
        expcd.ReadOnly = False
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_exp.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_exp.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_exp.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_exp.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Private Sub expcd_TextChanged(sender As Object, e As EventArgs) Handles expcd.TextChanged
        Dim code As String = expcd.Text.Trim.ToUpper
        Dim qry As String = "SELECT * FROM [expns_dtls] where Upper([expns_code]) like '" & code & "' "
        If ConfigData.getDataToDatatable(qry).Rows.Count > 0 Then
            expcd.Text = ""
        End If
    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Expense_Detail.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_exp.AllowPaging = False
            'Me.BindGrid()

            gv_exp.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_exp.HeaderRow.Cells
                'gv_exp.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_exp.HeaderStyle.BackColor
            Next

            gv_exp.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_exp.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_exp.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_exp.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_exp.RenderControl(hw)
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
