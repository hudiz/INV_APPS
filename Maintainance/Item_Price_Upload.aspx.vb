Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports Excel
Imports System.IO

Partial Class Mntnc_Item_Price_Upload
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then

            Call clear_control_values()
        End If
    End Sub

    Public Sub load_item_price_list_grid()
        Dim qry As String = "SELECT * FROM [itm_price_dtls]" & " "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmprclst.DataSource = dt
        gv_itmprclst.DataBind()

    End Sub
    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        FileUploadControl.Dispose()
        Session("Uplditem_dt") = Nothing
        gv_itmprclst.DataSource = Nothing
        gv_itmprclst.DataBind()
        UpdateUserButton.Visible = False
    End Sub

    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = Session("login_compid")
        Dim STAT As Boolean = False
        Dim CONN As SqlConnection = con
        Dim TRAN As SqlTransaction = Nothing
        Try
            If Not CONN.State = ConnectionState.Open Then
                CONN.Open()
            End If
            TRAN = CONN.BeginTransaction

            Dim dt As DataTable = TryCast(Session("Uplditem_dt"), DataTable)
            For Each dr As DataRow In dt.Rows
                If dr("itm_pr_id").ToString.Trim = "" Then
                    Exit For
                End If
                Dim qry As String = ""
                Dim rwqry As String = "SELECT * from item_dtls where itm_pr_id='" & dr("itm_pr_id").ToString.Trim & "'"

                Dim cmd As SqlCommand = New SqlCommand("update item_dtls set itm_pr_info=@_itm_pr_info, itm_pr_grp=@_itm_pr_grp, " &
                            "  itm_pr_price = @_itm_pr_price, itm_pr_cost = @_itm_pr_cost,  itm_pr_unt = @_itm_pr_unt, itm_pr_refid = @_itm_pr_refid, " &
                            "itm_pr_taxpct = @_itm_pr_taxpct ,itm_pr_taxamt =@_itm_pr_taxamt,  itm_pr_rmk =  @_itm_pr_rmk,itm_pr_sts = @_itm_pr_sts , " &
                                               " updateby=@_updateby, updateon = @_updateon  where itm_pr_id=@_itm_pr_id ", ConfigData.con)

                If ConfigData.SelTbl_Trnscn(rwqry, CONN, TRAN).Rows.Count = 0 Then
                    cmd = New SqlCommand("insert into item_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_exp,itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " null,@_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)
                End If

                cmd.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dr("itm_pr_id").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = dr("itm_pr_info").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dr("itm_pr_grp").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = dr("itm_pr_cost").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = dr("itm_pr_price").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = dr("itm_pr_taxpct").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = dr("itm_pr_taxamt").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dr("itm_pr_unt").ToString.Trim
                cmd.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = ""
                cmd.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = "S"
                cmd.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = dr("itm_pr_rmk").ToString.Trim
                cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = upby
                cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp




                Dim cmd2 As SqlCommand = New SqlCommand("insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_exp,itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " null,@_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

                cmd2.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dr("itm_pr_id").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = dr("itm_pr_info").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dr("itm_pr_grp").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd2.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = dr("itm_pr_cost").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = dr("itm_pr_price").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = dr("itm_pr_taxpct").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = dr("itm_pr_taxamt").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dr("itm_pr_unt").ToString.Trim
                cmd2.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = ""
                cmd2.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = "S"
                cmd2.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = dr("itm_pr_rmk").ToString.Trim
                cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = upby
                cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
                cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
                cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp


                cmd.Connection = CONN
                cmd.Transaction = TRAN
                cmd.ExecuteNonQuery()

                cmd2.Connection = CONN
                cmd2.Transaction = TRAN
                cmd2.ExecuteNonQuery()

                ' Dim inssts As Boolean = ConfigData.Transact(qry, qry2, "", "", "")


            Next

            STAT = True
            TRAN.Commit()

        Catch Err As Exception
            STAT = False
            TRAN.Rollback()
            msg_box(Me, Err.Message.ToString)
            ' mbox("Error : " & err.Message.ToString)
        Finally

            con.Close()
        End Try
        If STAT Then
            Call clear_control_values()
        End If
    End Sub
    Protected Sub UploadButton_Click(sender As Object, e As System.EventArgs)

        If (FileUploadControl.HasFile) Then

            Try

                Dim f1 As String = FileUploadControl.FileName
                Dim filename As String = Server.MapPath("~/Uploaded/") + f1

                FileUploadControl.SaveAs(filename)
                Dim ext As String() = FileUploadControl.FileName.Split(".")

                Dim dt As New DataTable

                Dim stream As FileStream = File.Open(filename, FileMode.Open, FileAccess.Read)
                Dim excelReader As IExcelDataReader

                If ext(1).Trim = "xlsx" Then
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                Else
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
                End If

                excelReader.IsFirstRowAsColumnNames = True
                Dim ds As DataSet = excelReader.AsDataSet()
                dt = ds.Tables(0)

                Session("Uplditem_dt") = dt

                If dt.Rows.Count > 0 Then
                    gv_itmprclst.DataSource = dt
                    gv_itmprclst.DataBind()
                    UpdateUserButton.Visible = True
                Else
                    gv_itmprclst.DataSource = dt
                    gv_itmprclst.DataBind()
                End If


                'msg_box(Me, "Upload status: File uploaded!")

            Catch ex As Exception

                msg_box(Me, "Upload status: The file could not be uploaded. The following error occured: " + ex.Message)
            End Try
        End If


    End Sub
End Class
