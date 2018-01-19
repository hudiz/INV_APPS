Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Maintaince_Company_details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub

    Public Sub load_customer_grid()
        Dim qry As String = "SELECT comp_id, comp_name, comp_addr, comp_ph,comp_email, comp_cont FROM comp_dtls"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_comp.DataSource = dt
        gv_comp.DataBind()
        session_datatable_store("COMPDTLS_gv_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub
    Private Sub gv_comp_Init(sender As Object, e As EventArgs) Handles gv_comp.Init
        gv_comp.DataSource = session_datatable_fill("COMPDTLS_gv_dtls")
        gv_comp.DataBind()
    End Sub

    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_comp.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT comp_id, comp_name, comp_addr, comp_ph,comp_email, comp_cont FROM comp_dtls" & _
            " where comp_id ='" & gid.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            compgid.Text = dt.Rows(0)("comp_id").ToString.Trim
            compid.Text = dt.Rows(0)("comp_id").ToString.Trim
            compname.Text = dt.Rows(0)("comp_name").ToString.Trim
            compadd.Text = dt.Rows(0)("comp_addr").ToString.Trim
            compph.Text = dt.Rows(0)("comp_ph").ToString.Trim
            compemail.Text = dt.Rows(0)("comp_email").ToString.Trim
            compcont.Text = dt.Rows(0)("comp_cont").ToString.Trim
            compgid.Visible = True
            compid.Enabled = False
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
        Dim cmd As SqlCommand = New SqlCommand("insert into  comp_dtls (comp_id, comp_name, comp_addr, comp_ph,comp_email, comp_cont,  " &
                                               " createby, createon, updateby, updateon ) " &
                                               " values(@_comp_id, @_comp_name, @_comp_addr, @_comp_ph,@_comp_email, @_comp_cont,  " &
                                               " @_createby,@_createon,@_updateby,@_updateon )", ConfigData.con)

        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = compid.Text.Trim
        cmd.Parameters.Add("@_comp_name", SqlDbType.NVarChar).Value = compname.Text.Trim
        cmd.Parameters.Add("@_comp_addr", SqlDbType.NVarChar).Value = compadd.Text.Trim
        cmd.Parameters.Add("@_comp_ph", SqlDbType.NVarChar).Value = compph.Text.Trim
        cmd.Parameters.Add("@_comp_email", SqlDbType.NVarChar).Value = compemail.Text.Trim
        cmd.Parameters.Add("@_comp_cont", SqlDbType.NVarChar).Value = compcont.Text.Trim
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")

        Dim inssts As Boolean = ConfigData.postquery(cmd)
        If inssts Then
            load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = compgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim cmd As SqlCommand = New SqlCommand("update comp_dtls Set comp_name=@_comp_name, comp_addr=@_comp_addr,  comp_ph=@_comp_ph, comp_email=@_comp_email, comp_cont=@_comp_cont, " &
                                               " updateby=@_updateby, updateon =@_updateon where comp_id=@_comp_id ", ConfigData.con)

            cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = compid.Text.Trim
            cmd.Parameters.Add("@_comp_name", SqlDbType.NVarChar).Value = compname.Text.Trim
            cmd.Parameters.Add("@_comp_addr", SqlDbType.NVarChar).Value = compadd.Text.Trim
            cmd.Parameters.Add("@_comp_ph", SqlDbType.NVarChar).Value = compph.Text.Trim
            cmd.Parameters.Add("@_comp_email", SqlDbType.NVarChar).Value = compemail.Text.Trim
            cmd.Parameters.Add("@_comp_cont", SqlDbType.NVarChar).Value = compcont.Text.Trim
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = upon

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts Then
                load_customer_grid()
                Call clear_control_values()
            Else
                msg_box(Me, "update Is unsuccessful")
            End If
        Else
            msg_box(Me, "GID Is null")
        End If

    End Sub

    Protected Sub DeleteUserButton_Click(sender As Object, e As System.EventArgs) Handles DeleteUserButton.Click
        Dim gid As String = compgid.Text.Trim
        If gid <> "" Then
            Dim cmd As SqlCommand = New SqlCommand("delete from comp_dtls  where comp_id=@_comp_id ", ConfigData.con)

            cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = compid.Text.Trim

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
        compgid.Text = ""
        compid.Text = ""
        compname.Text = ""
        compadd.Text = ""
        compph.Text = ""
        compemail.Text = ""
        compcont.Text = ""
        compgid.Visible = False
        compid.Enabled = True
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub
    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_comp.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_comp.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_comp.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_comp.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Company_Detail.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_comp.AllowPaging = False
            'Me.BindGrid()

            gv_comp.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_comp.HeaderRow.Cells
                'gv_comp.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_comp.HeaderStyle.BackColor
            Next

            gv_comp.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_comp.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_comp.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_comp.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_comp.RenderControl(hw)
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
