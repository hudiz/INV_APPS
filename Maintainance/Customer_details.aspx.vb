Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO
Partial Class Customer_Customer_details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Public Sub load_customer_grid()
        Dim qry As String = "Select [cust_sup_auto_id], [cust_sup_id], [cust_sup_name], [cust_sup_ph], [cust_sup_ph1], [cust_sup_email] FROM [cust_supp_dtls] where cust_sup_sts='C'"
                Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_cust.DataSource = dt
        gv_cust.DataBind()
        session_datatable_store("CUSTDTLS_cust_supp_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub

    Private Sub gv_cust_Init(sender As Object, e As EventArgs) Handles gv_cust.Init
        gv_cust.DataSource = session_datatable_fill("CUSTDTLS_cust_supp_dtls")
        gv_cust.DataBind()
    End Sub
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gv_cust.SelectedRow
        Dim gid As String = row.Cells(0).Text
        Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT [cust_sup_auto_id], [cust_sup_id], [cust_sup_name], [cust_sup_addr], " &
            " [cust_sup_ph], [cust_sup_ph1], [cust_sup_email] FROM [cust_supp_dtls]" &
            " where cust_sup_auto_id ='" & gid.Trim & "' and cust_sup_id ='" & cid.Trim & "' and cust_sup_sts='C'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            custgid.Text = dt.Rows(0)("cust_sup_auto_id").ToString.Trim
            custid.Text = dt.Rows(0)("cust_sup_id").ToString.Trim
            custname.Text = dt.Rows(0)("cust_sup_name").ToString.Trim
            custadd.Text = dt.Rows(0)("cust_sup_addr").ToString.Trim
            custph.Text = dt.Rows(0)("cust_sup_ph").ToString.Trim
            custph1.Text = dt.Rows(0)("cust_sup_ph1").ToString.Trim
            custemail.Text = dt.Rows(0)("cust_sup_email").ToString.Trim
            custgid.Visible = True
            custid.ReadOnly = True
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
        Dim custsts As String = "C"
        custid.Text = Cust_next_ID_values()

        'Dim qry As String = "insert into cust_supp_dtls (cust_sup_id, cust_sup_name, cust_sup_addr, cust_sup_ph, cust_sup_ph1, " & _
        '    "cust_sup_email, createby, createon, updateby, updateon, comp_id, cust_sup_sts) " & _
        '                           " values('" & custid.Text.Trim & "','" & custname.Text.Trim & "','" & custadd.Text.Trim & "'," & _
        '                           "'" & custph.Text.Trim & "','" & custph1.Text.Trim & "','" & custemail.Text.Trim & "','" & crby & "'," & _
        '                           "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "','" & custsts & "') "
        'Dim inssts As Integer = ConfigData.postDataToTable(qry)

        Dim cmd As SqlCommand = New SqlCommand("insert into cust_supp_dtls (cust_sup_id, cust_sup_name, cust_sup_addr, cust_sup_ph, cust_sup_ph1, " &
            "cust_sup_email, createby, createon, updateby, updateon, comp_id, cust_sup_sts) " &
                                   " values(@_cust_sup_id,@_cust_sup_name,@_cust_sup_addr,@_cust_sup_ph,@_cust_sup_ph1,@_cust_sup_email,@_createby,@_createon, " &
                                   "@_updateby,@_updateon,@_comp_id,@_cust_sup_sts)", ConfigData.con)

        cmd.Parameters.Add("@_cust_sup_id", SqlDbType.NVarChar).Value = custid.Text.Trim
        cmd.Parameters.Add("@_cust_sup_name", SqlDbType.NVarChar).Value = custname.Text.Trim
        cmd.Parameters.Add("@_cust_sup_addr", SqlDbType.NVarChar).Value = custadd.Text.Trim
        cmd.Parameters.Add("@_cust_sup_ph", SqlDbType.NVarChar).Value = custph.Text.Trim
        cmd.Parameters.Add("@_cust_sup_ph1", SqlDbType.NVarChar).Value = custph1.Text.Trim
        cmd.Parameters.Add("@_cust_sup_email", SqlDbType.NVarChar).Value = custemail.Text.Trim
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp
        cmd.Parameters.Add("@_cust_sup_sts", SqlDbType.NVarChar).Value = custsts

        Dim inssts As Boolean = ConfigData.postquery(cmd)

        If inssts Then
            If Not IsNothing(Request.QueryString("invno")) Then
                Session("new_custid") = custid.Text.Trim
                Response.Redirect("../Transaction/Sales_Details.aspx")
            End If
            Call load_customer_grid()
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = custgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            Dim custsts As String = "C"

            Dim cmd As SqlCommand = New SqlCommand("update cust_supp_dtls set cust_sup_name=@_cust_sup_name ,cust_sup_addr=@_cust_sup_addr, cust_sup_ph =@_cust_sup_ph," &
                " cust_sup_ph1=@_cust_sup_ph1, cust_sup_email=@_cust_sup_email,updateby=@_updateby, updateon = @_updateon" &
                " where cust_sup_id=@_cust_sup_id and cust_sup_auto_id=@_cust_sup_auto_id and cust_sup_sts=@_cust_sup_sts", ConfigData.con)
            ' Dim inssts As Integer = ConfigData.postDataToTable(qry)

            'Dim cmd As SqlCommand = New SqlCommand("insert into cust_supp_dtls (cust_sup_id, cust_sup_name, cust_sup_addr, cust_sup_ph, cust_sup_ph1, " &
            '"cust_sup_email, createby, createon, updateby, updateon, comp_id, cust_sup_sts) " &
            '                       " values(@_cust_sup_id,@_cust_sup_name,@_cust_sup_addr,@_cust_sup_ph,@_cust_sup_ph1,@_cust_sup_email,@_createby,@_createon, " &
            '                       "@_updateby,@_updateon,@_comp_id,@_cust_sup_sts)", ConfigData.con)

            cmd.Parameters.Add("@_cust_sup_id", SqlDbType.NVarChar).Value = custid.Text.Trim
            cmd.Parameters.Add("@_cust_sup_name", SqlDbType.NVarChar).Value = custname.Text.Trim
            cmd.Parameters.Add("@_cust_sup_addr", SqlDbType.NVarChar).Value = custadd.Text.Trim
            cmd.Parameters.Add("@_cust_sup_ph", SqlDbType.NVarChar).Value = custph.Text.Trim
            cmd.Parameters.Add("@_cust_sup_ph1", SqlDbType.NVarChar).Value = custph1.Text.Trim
            cmd.Parameters.Add("@_cust_sup_email", SqlDbType.NVarChar).Value = custemail.Text.Trim
            'cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
            cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            'cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp
            cmd.Parameters.Add("@_cust_sup_sts", SqlDbType.NVarChar).Value = custsts
            cmd.Parameters.Add("@_cust_sup_auto_id", SqlDbType.NVarChar).Value = gid

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
        Dim gid As String = custgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")
            'Dim qry As String = "delete from cust_supp_dtls where cust_sup_id='" & custid.Text.Trim & "' and cust_sup_auto_id='" & gid & "' and cust_sup_sts='C'"
            'Dim inssts As Integer = ConfigData.postDataToTable(qry)

            Dim custsts As String = "C"
            Dim cmd As SqlCommand = New SqlCommand("delete from cust_supp_dtls " &
                " where cust_sup_id=@_cust_sup_id and cust_sup_auto_id=@_cust_sup_auto_id and cust_sup_sts=@_cust_sup_sts", ConfigData.con)

            cmd.Parameters.Add("@_cust_sup_id", SqlDbType.NVarChar).Value = custid.Text.Trim
            cmd.Parameters.Add("@_cust_sup_sts", SqlDbType.NVarChar).Value = custsts
            cmd.Parameters.Add("@_cust_sup_auto_id", SqlDbType.NVarChar).Value = gid

            Dim inssts As Boolean = ConfigData.postquery(cmd)
            If inssts Then
                Call load_customer_grid()
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
        custgid.Text = ""
        custid.Text = Cust_next_ID_values()
        custname.Text = ""
        custadd.Text = ""
        custph.Text = ""
        custph1.Text = ""
        custemail.Text = ""
        custgid.Visible = False
        custid.ReadOnly = True
        UpdateUserButton.Visible = False
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = True
    End Sub

    Protected Sub OnDataBound(sender As Object, e As EventArgs)
        If gv_cust.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_cust.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_cust.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_cust.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Public Function Cust_next_ID_values() As String
        Dim qry As String = " select max(cust_sup_id) from cust_supp_dtls   where cust_sup_id like '" & "CST" & "%' and cust_sup_sts='C'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(0)) Then
                If dt.Rows(0)(0).ToString.Length > 8 Then
                    Dim srl As Int16 = dt.Rows(0)(0).ToString.Substring(8)
                    Return "CST" & (srl + 1).ToString("0000000")
                End If
            End If
        End If
        Return "CST" & "0000001"
    End Function

    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Customer_Detail.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_cust.AllowPaging = False
            'Me.BindGrid()

            gv_cust.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_cust.HeaderRow.Cells
                'gv_cust.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_cust.HeaderStyle.BackColor
            Next

            gv_cust.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_cust.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_cust.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_cust.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_cust.RenderControl(hw)
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
