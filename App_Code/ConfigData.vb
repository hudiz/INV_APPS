Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class ConfigData

    'Shared constr As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=D:\SRC\INV_APPS\App_Data\INVAPP.mdf;Integrated Security=True;User Instance=True"
    'Shared con As New SqlConnection(constr)
    Public Shared con As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("invappConnectionString1").ConnectionString)
    Shared cmd As New SqlCommand
    Public Shared Function getDataToDatatable(qry As String) As DataTable
        Dim dt As New DataTable
        Try
            If Not con.State = ConnectionState.Open Then
                con.Open()
            End If

            cmd.CommandText = qry
            cmd.Connection = con
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        Catch ex As Exception
            ' msg_box(Me, qry & Environment.NewLine & ex.ToString)
        Finally
            con.Close()
        End Try
        Return dt
    End Function
    Public Shared Function postDataToTable(qry As String) As Integer
        cmd.CommandText = qry
        cmd.Connection = con
        Dim poststs As Integer = 0
        If Not con.State = ConnectionState.Open Then
            con.Open()
        End If
        Try
            poststs = cmd.ExecuteNonQuery()
        Catch ex As Exception
            'msg_box(Me, qry & Environment.NewLine & ex.ToString)
        Finally
            con.Close()
        End Try
        Return poststs
    End Function

    Public Shared Function postquery(cmdqry As SqlCommand) As Integer
        'cmdqry.CommandText = qry
        'cmdqry.Connection = con
        Dim poststs As Boolean = False
        If Not con.State = ConnectionState.Open Then
            con.Open()
        End If
        Try
            poststs = cmdqry.ExecuteNonQuery()
        Catch ex As Exception
            'msg_box(Me, qry & Environment.NewLine & ex.ToString)
        Finally
            con.Close()
        End Try
        Return poststs
    End Function
    Public Shared Function postTransact(ByVal cmd1 As SqlCommand, ByVal cmd2 As SqlCommand, ByVal cmd3 As SqlCommand) As Boolean
        'Use this function when dbops needs to be done within a single transaction
        'Presently supports upto 5 db operations
        'Params expected are the respective query strings for dbops
        Dim stat As Boolean = False
        Dim tran As SqlTransaction = Nothing

        cmd1.Connection = con

        Try
            If Not con.State = ConnectionState.Open Then
                con.Open()
            End If
            tran = con.BeginTransaction
            cmd1.Transaction = tran
            cmd1.ExecuteNonQuery()
            If Not IsNothing(cmd2) Then
                cmd2.Connection = con
                cmd2.Transaction = tran
                cmd2.ExecuteNonQuery()
            End If
            If Not IsNothing(cmd3) Then
                cmd3.Connection = con
                cmd3.Transaction = tran
                cmd3.ExecuteNonQuery()
            End If
            stat = True
            tran.Commit()

        Catch err As Exception
            stat = False
            tran.Rollback()
            ' msg_box(Me, err.Message.ToString)
            ' mbox("Error : " & err.Message.ToString)
        Finally
            con.Close()
        End Try

        Return stat
    End Function


    Public Shared Function Transact(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal str4 As String, ByVal str5 As String) As Boolean
        'Use this function when dbops needs to be done within a single transaction
        'Presently supports upto 5 db operations
        'Params expected are the respective query strings for dbops
        Dim stat As Boolean = False
        Dim cmd1 As New SqlCommand(str1)
        Dim cmd2 As New SqlCommand(str2)
        Dim cmd3 As New SqlCommand(str3)
        Dim cmd4 As New SqlCommand(str4)
        Dim cmd5 As New SqlCommand(str5)
        Dim tran As SqlTransaction = Nothing

        cmd1.Connection = con
        cmd2.Connection = con
        cmd3.Connection = con
        cmd4.Connection = con
        cmd5.Connection = con

        Try
            If Not con.State = ConnectionState.Open Then
                con.Open()
            End If
            tran = con.BeginTransaction
            cmd1.Transaction = tran
            cmd1.ExecuteNonQuery()
            If str2.Length > 0 Then
                cmd2.Transaction = tran
                cmd2.ExecuteNonQuery()
            End If
            If str3.Length > 0 Then
                cmd3.Transaction = tran
                cmd3.ExecuteNonQuery()
            End If
            If str4.Length > 0 Then
                cmd4.Transaction = tran
                cmd4.ExecuteNonQuery()
            End If
            If str5.Length > 0 Then
                cmd5.Transaction = tran
                cmd5.ExecuteNonQuery()
            End If
            stat = True
            tran.Commit()

        Catch err As Exception
            stat = False
            tran.Rollback()
            ' msg_box(Me, err.Message.ToString)
            ' mbox("Error : " & err.Message.ToString)
        Finally
            con.Close()
        End Try

        Return stat
    End Function


    Public Shared Function SelTbl_Trnscn(ByVal sqlstring As String, ByRef V_OdbcConnection As SqlConnection, ByRef V_OdbcTransaction As SqlTransaction) As DataTable
        Dim dt As New DataTable

        Dim cmd As New SqlCommand("")
        Try
            cmd = New SqlCommand(sqlstring)
            cmd.Connection = V_OdbcConnection
            cmd.Transaction = V_OdbcTransaction

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            'Dim reader As SqlDataReader = cmd.ExecuteReader()

            'If reader.HasRows Then
            '    Dim dt_rdr As DataTable = reader.GetSchemaTable()

            '    For Each row In dt_rdr.Rows
            '        dt.Columns.Add(row(0))

            '    Next

            '    Dim dr As DataRow = dt.NewRow

            '    For col = 0 To reader.FieldCount - 1
            '        dr(col) = dt_rdr.Rows(0)(col)
            '    Next

            '    dt.Rows.Add(dr)
            'End If

        Catch err As Exception
            '  msg_box(Me, "Error : " & err.Message.ToString)
        End Try

        Return dt
    End Function


    Public Shared Function load_next_receipt_no() As String
        Dim qry As String = "SELECT max(rcpt_no) FROM [Receipt_dtls] "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(0)) Then
                Dim srl As Double = CDec(dt.Rows(0)(0).ToString.Substring(4)) + 1
                Return "RCPT" & srl.ToString("000000")
            End If
        End If
        Return "RCPT000001"
    End Function

    Public Shared Function load_next_receipt_no(CON As SqlConnection, TRAN As SqlTransaction, adds As Integer) As String
        Dim qry As String = "SELECT max(rcpt_no) FROM [Receipt_dtls] "
        Dim dt As DataTable = ConfigData.SelTbl_Trnscn(qry, CON, TRAN)
        If dt.Rows.Count > 0 Then
            If Not IsDBNull(dt.Rows(0)(0)) Then
                Dim srl As Double = CDec(dt.Rows(0)(0).ToString.Substring(4)) + adds
                Return "RCPT" & srl.ToString("000000")
            End If
        End If
        Return "RCPT000001"
    End Function


    Public Shared Function Get_unit_list() As DataTable
        Dim dt As New DataTable

        dt.Columns.Add("unit_cd")
        dt.Columns.Add("unit_desc")
        dt.Rows.Add("DZN", "DZN - Dozen")
        dt.Rows.Add("PKT", "PKT - Packet")
        dt.Rows.Add("PCS", "PCS - Piece")
        dt.Rows.Add("BOX", "BOX - Box")
        dt.Rows.Add("CRT", "CRT - Carton")
        dt.Rows.Add("PCH", "PCH - Pouch")
        dt.Rows.Add("CAS", "CAS - Case")
        dt.Rows.Add("CNT", "CNT - Container")
        dt.Rows.Add("KGS", "KGS - KG")
        dt.Rows.Add("GMS", "GMS - Grams")
        dt.Rows.Add("MTS", "MTS - Meter")
        dt.Rows.Add("INC", "INC - Inch")
        dt.Rows.Add("FTS", "FTS - Feet")
        dt.Rows.Add("LNT", "LNT - Length")
        '<asp:ListItem Text = "DZN - Dozen" Value="DZN"></asp: ListItem>
        '<asp:ListItem Text="PKT - Packet" Value="PKT"></asp:ListItem>
        '<asp:ListItem Text = "PCS - Piece" Value="PCS"></asp: ListItem>
        '<asp:ListItem Text="BOX - Box" Value="BOX"></asp:ListItem>
        '<asp:ListItem Text = "CRT - Carton" Value="CRT"></asp: ListItem>
        '<asp:ListItem Text="PCH - Pouch" Value="PCH"></asp:ListItem>
        '<asp:ListItem Text = "CAS - Case" Value="CAS"></asp: ListItem>
        '<asp:ListItem Text="CNT - Container" Value="CNT"></asp:ListItem>
        '<asp:ListItem Text = "KGS - KG" Value="KGS"></asp: ListItem>
        '<asp:ListItem Text="MTS - METER" Value="MTS"></asp:ListItem>
        '<asp:ListItem Text = "INC - INCH" Value="INC"></asp: ListItem>
        '<asp:ListItem Text="FTS - FEET" Value="FTS"></asp:ListItem>
        Return dt
    End Function

    Public Shared Function session_datatable_fill(ByVal datatablename As String) As DataTable
        Dim session_dt As New DataTable
        If Not HttpContext.Current.Session(datatablename) Is Nothing Then
            session_dt = DirectCast(HttpContext.Current.Session(datatablename), DataTable)
        End If
        Return session_dt
    End Function

    Public Shared Sub session_datatable_store(ByVal datatablename As String, ByRef datatabletemp As DataTable)
        If Not HttpContext.Current.Session(datatablename) Is Nothing Then
            HttpContext.Current.Session.Remove(datatablename)
            HttpContext.Current.Session(datatablename) = datatabletemp
        Else
            HttpContext.Current.Session(datatablename) = datatabletemp
        End If
    End Sub
    Public Shared Sub msg_box(webPageInstance As Page, ByVal val As String)
        Dim cs As String = "c"
        Dim cst As String = "<script type=""text/javascript"">" &
            "alert('" & val & "');</" & "script>"
        ' Dim cscrip As System.Web.UI.ClientScriptManager

        webPageInstance.ClientScript.RegisterStartupScript(webPageInstance.GetType, cs, cst)

        '        msg_box(Me, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message)

        ' HttpContext.Current.Request..ClientScript.RegisterStartupScript(Me.GetType, cs, cst)
    End Sub
End Class
