Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData
Imports System.Drawing
Imports System.IO

Partial Class Maintaince_Item_Price
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then

            Call clear_control_values()
            Call load_unit_details()
            Call load_itemGroup_details()
            If Not IsNothing(Request.QueryString("itm_id")) Then
                Dim itm_id As String = Request.QueryString("itm_id").Trim
                Call Item_Selected(itm_id)
            Else
                Call load_item_details()
            End If
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
            " where itm_pr_grp ='" & dditemgrp.SelectedValue.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)

        dditmcd.DataSource = dt
        dditmcd.DataBind()

        If dditmcd.Items.Count > 0 Then
            Call load_item_price_list_grid()
        End If
        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
            ' Call Item_Selected(item.Split("-")(0))
        End If

    End Sub

    Private Sub dditmcd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dditmcd.SelectedIndexChanged

        Call load_item_price_list_grid()
        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
            ' Call Item_Selected(item.Split("-")(0))
        End If

    End Sub


    Public Sub load_item_price_list_grid()
        Dim qry As String = "SELECT * FROM [itm_price_dtls] " &
            " where itm_pr_id ='" & dditmcd.SelectedValue.Trim & "'  ORDER BY 1 DESC"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmprclst.DataSource = dt
        gv_itmprclst.DataBind()

        session_datatable_store("ITEMPRC_itm_price_dtls", dt)
        btnExport.Visible = dt.Rows.Count
    End Sub
    Private Sub gv_itmprclst_Init(sender As Object, e As EventArgs) Handles gv_itmprclst.Init

        gv_itmprclst.DataSource = session_datatable_fill("ITEMPRC_itm_price_dtls")
        gv_itmprclst.DataBind()

    End Sub
    Protected Sub CancelUserButton_Click(sender As Object, e As System.EventArgs) Handles CancelUserButton.Click
        Call clear_control_values()
    End Sub
    Public Sub clear_control_values()
        itmgid.Text = ""
        'dditemgrp.SelectedValue = ""
        'dditmcd.SelectedValue = ""
        itmname.Text = ""
        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
        End If

        itmcostprice.Text = "0.000"
        itmsellprice.Text = "0.000"
        dditemUnit.SelectedValue = "DZN"
        itmprrefid.Text = ""
        itmtaxperc.Text = "5.00"
        itmtaxamt.Text = "0.000"
        itmremark.Text = ""
        dditemstatus.SelectedValue = "S"
        itmgid.Visible = False
        UpdateUserButton.Visible = True
        DeleteUserButton.Visible = False
        CancelUserButton.Visible = False
        CreateUserButton.Visible = False
    End Sub

    Protected Sub Item_Selected(itm_id As String)
        ' Dim row As GridViewRow = gv_itmprclst.SelectedRow
        ''Dim gid As String = row.Cells(0).Text
        ''Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT * " & " FROM [item_dtls]" & " where itm_pr_id ='" & itm_id.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            itmgid.Text = dt.Rows(0)("itm_pr_gid").ToString.Trim
            dditemgrp.SelectedValue = dt.Rows(0)("itm_pr_grp").ToString.Trim
            Call load_item_details()

            dditmcd.SelectedValue = dt.Rows(0)("itm_pr_id").ToString.Trim
            Call load_item_price_list_grid()

            itmname.Text = dt.Rows(0)("itm_pr_info").ToString.Trim
            itmcostprice.Text = dt.Rows(0)("itm_pr_cost").ToString.Trim
            itmsellprice.Text = dt.Rows(0)("itm_pr_price").ToString.Trim
            dditemUnit.SelectedValue = dt.Rows(0)("itm_pr_unt").ToString.Trim
            itmprrefid.Text = dt.Rows(0)("itm_pr_refid").ToString.Trim
            itmtaxperc.Text = dt.Rows(0)("itm_pr_taxpct").ToString.Trim
            itmtaxamt.Text = dt.Rows(0)("itm_pr_taxamt").ToString.Trim
            itmremark.Text = dt.Rows(0)("itm_pr_rmk").ToString.Trim
            dditemstatus.SelectedValue = dt.Rows(0)("itm_pr_sts").ToString.Trim
            itmgid.Visible = True
            UpdateUserButton.Visible = True
            DeleteUserButton.Visible = False
            CancelUserButton.Visible = False
            CreateUserButton.Visible = False
        Else
            clear_control_values()
        End If
    End Sub

    'Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
    '    Dim row As GridViewRow = gv_itmprclst.SelectedRow
    '    Dim gid As String = row.Cells(0).Text
    '    Dim cid As String = row.Cells(1).Text
    '    Dim qry As String = "SELECT * " & " FROM [item_dtls]" & " where itm_pr_gid ='" & gid.Trim & "'"
    '    Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
    '    If dt.Rows.Count > 0 Then
    '        itmgid.Text = dt.Rows(0)("itm_pr_gid").ToString.Trim
    '        dditemgrp.SelectedValue = dt.Rows(0)("itm_pr_grp").ToString.Trim
    '        dditmcd.SelectedValue = dt.Rows(0)("itm_pr_id").ToString.Trim
    '        itmname.Text = dt.Rows(0)("itm_pr_info").ToString.Trim
    '        itmcostprice.Text = dt.Rows(0)("itm_pr_cost").ToString.Trim
    '        itmsellprice.Text = dt.Rows(0)("itm_pr_price").ToString.Trim
    '        dditemUnit.SelectedValue = dt.Rows(0)("itm_pr_unt").ToString.Trim
    '        itmprrefid.Text = dt.Rows(0)("itm_pr_refid").ToString.Trim
    '        itmtaxperc.Text = dt.Rows(0)("itm_pr_taxpct").ToString.Trim
    '        itmtaxamt.Text = dt.Rows(0)("itm_pr_taxamt").ToString.Trim
    '        itmremark.Text = dt.Rows(0)("itm_pr_rmk").ToString.Trim
    '        dditemstatus.SelectedValue = dt.Rows(0)("itm_pr_sts").ToString.Trim
    '        itmgid.Visible = True
    '        UpdateUserButton.Visible = True
    '        DeleteUserButton.Visible = True
    '        CancelUserButton.Visible = True
    '        CreateUserButton.Visible = False
    '    Else
    '        clear_control_values()
    '    End If
    'End Sub
    Protected Sub CreateUserButton_Click(sender As Object, e As System.EventArgs) Handles CreateUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        Dim crby As String = User.Identity.Name.ToString.Trim
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = Session("login_compid")
        'Dim qry As String = "insert into item_dtls (itm_pr_id, itm_pr_info, itm_pr_grp, " &
        '                    " itm_pr_exp, itm_pr_dos, itm_pr_cost,itm_pr_price,itm_pr_unt," &
        '                    " itm_pr_refid,itm_pr_taxpct,itm_pr_taxamt,itm_pr_rmk,itm_pr_sts, " &
        '                    " createby, createon, updateby, updateon, comp_id) " &
        '                    " values('" & dditmcd.SelectedValue.Trim & "','" & itmname.Text.Trim & "','" & dditemgrp.SelectedValue.Trim & "'," &
        '                           " null,'" & Date.Now.ToString("MM/dd/yyyy") & "','" & itmcostprice.Text.Trim & "','" & itmsellprice.Text.Trim & "','" & dditemUnit.SelectedValue.Trim & "'," &
        '                           " '" & itmprrefid.Text.Trim & "', '" & itmtaxperc.Text.Trim & "','" & itmtaxamt.Text.Trim & "',  '" & itmremark.Text.Trim & "','" & dditemstatus.SelectedValue.Trim & "'," &
        '                           "'" & crby & "'," & "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"

        'Dim qry As String = "Update item_dtls SET itm_pr_info = '" & itmname.Text.Trim & "' ,  " &
        '                    "  itm_pr_price = '" & itmsellprice.Text.Trim & "', itm_pr_cost = '" & itmcostprice.Text.Trim & "', " &
        '                    " itm_pr_unt ='" & dditemUnit.SelectedValue.Trim & "'," & " itm_pr_refid = '" & itmprrefid.Text.Trim & "', " &
        '                    "itm_pr_taxpct ='" & itmtaxperc.Text.Trim & "' ,itm_pr_taxamt ='" & itmtaxamt.Text.Trim & "', " &
        '                    "itm_pr_rmk =  '" & itmremark.Text.Trim & "',itm_pr_sts ='" & dditemstatus.SelectedValue.Trim & "' , " &
        '                    " updateby='" & upby & "', updateon = '" & Date.Now.ToString("MM/dd/yyyy") & "'" &
        '                    " where itm_pr_id='" & dditmcd.SelectedValue.Trim & "' "

        Dim cmd As SqlCommand = New SqlCommand("update item_dtls set itm_pr_info=@_itm_pr_info, itm_pr_grp=@_itm_pr_grp, " &
                            "  itm_pr_price = @_itm_pr_price, itm_pr_cost = @_itm_pr_cost,  itm_pr_unt = @_itm_pr_unt, itm_pr_refid = @_itm_pr_refid, " &
                            "itm_pr_taxpct = @_itm_pr_taxpct ,itm_pr_taxamt =@_itm_pr_taxamt,  itm_pr_rmk =  @_itm_pr_rmk,itm_pr_sts = @_itm_pr_sts , " &
                                               " updateby=@_updateby, updateon = @_updateon  where itm_pr_id=@_itm_pr_id ", ConfigData.con)

        cmd.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dditmcd.SelectedValue.Trim
        cmd.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
        cmd.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
        cmd.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = itmcostprice.Text.Trim
        cmd.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = itmsellprice.Text.Trim
        cmd.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = itmtaxperc.Text.Trim
        cmd.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = itmtaxamt.Text.Trim
        cmd.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dditemUnit.SelectedValue.Trim
        cmd.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = itmprrefid.Text.Trim
        cmd.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = dditemstatus.SelectedValue.Trim
        cmd.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = itmremark.Text.Trim
        cmd.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

        'Dim qry2 As String = "insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp, " &
        '                    " itm_pr_exp, itm_pr_dos, itm_pr_cost,itm_pr_price,itm_pr_unt," &
        '                    " itm_pr_refid,itm_pr_taxpct,itm_pr_taxamt,itm_pr_rmk,itm_pr_sts, " &
        '                    " createby, createon, updateby, updateon, comp_id) " &
        '                    " values('" & dditmcd.SelectedValue.Trim & "','" & itmname.Text.Trim & "','" & dditemgrp.SelectedValue.Trim & "'," &
        '                           " null,'" & Date.Now.ToString("MM/dd/yyyy") & "','" & itmcostprice.Text.Trim & "','" & itmsellprice.Text.Trim & "','" & dditemUnit.SelectedValue.Trim & "'," &
        '                           " '" & itmprrefid.Text.Trim & "', '" & itmtaxperc.Text.Trim & "','" & itmtaxamt.Text.Trim & "',  '" & itmremark.Text.Trim & "','" & dditemstatus.SelectedValue.Trim & "'," &
        '                           "'" & crby & "'," & "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"

        Dim cmd2 As SqlCommand = New SqlCommand("insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " @_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

        cmd2.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dditmcd.SelectedValue.Trim
        cmd2.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
        cmd2.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd2.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = itmcostprice.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = itmsellprice.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = itmtaxperc.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = itmtaxamt.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dditemUnit.SelectedValue.Trim
        cmd2.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = itmprrefid.Text.Trim
        cmd2.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = dditemstatus.SelectedValue.Trim
        cmd2.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = itmremark.Text.Trim
        cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = crby
        cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
        cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
        cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

        Dim inssts As Boolean = ConfigData.postTransact(cmd, cmd2, Nothing)
        If inssts = True Then
            Call load_item_price_list_grid()
        End If
    End Sub

    Protected Sub UpdateUserButton_Click(sender As Object, e As System.EventArgs) Handles UpdateUserButton.Click
        Dim gid As String = itmgid.Text.Trim
        If gid <> "" Then
            Dim upby As String = User.Identity.Name.ToString.Trim()
            Dim upon As String = Date.Now.ToString("MM/dd/yyyy")

            Dim comp As String = Session("login_compid")

            'Dim qry As String = "Update item_dtls SET itm_pr_info = '" & itmname.Text.Trim & "' ,  " &
            '                "  itm_pr_price = '" & itmsellprice.Text.Trim & "', itm_pr_cost = '" & itmcostprice.Text.Trim & "', " &
            '                " itm_pr_unt ='" & dditemUnit.SelectedValue.Trim & "'," & " itm_pr_refid = '" & itmprrefid.Text.Trim & "', " &
            '                "itm_pr_taxpct ='" & itmtaxperc.Text.Trim & "' ,itm_pr_taxamt ='" & itmtaxamt.Text.Trim & "', " &
            '                "itm_pr_rmk =  '" & itmremark.Text.Trim & "',itm_pr_sts ='" & dditemstatus.SelectedValue.Trim & "' , " &
            '                " updateby='" & upby & "', updateon = '" & upon & "'" &
            '                " where itm_pr_id='" & dditmcd.SelectedValue.Trim & "' "
            ''update item_dtls Set item_code='" & itmcd.Text.Trim & "',item_name='" & itmname.Text.Trim & "', item_grp ='" & dditemgrp.SelectedValue.Trim & "'," &

            'Dim qry2 As String = "insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp, " &
            '                " itm_pr_exp, itm_pr_dos, itm_pr_cost,itm_pr_price,itm_pr_unt," &
            '                " itm_pr_refid,itm_pr_taxpct,itm_pr_taxamt,itm_pr_rmk,itm_pr_sts, " &
            '                " createby, createon, updateby, updateon, comp_id) " &
            '                " values('" & dditmcd.SelectedValue.Trim & "','" & itmname.Text.Trim & "','" & dditemgrp.SelectedValue.Trim & "'," &
            '                       " null,'" & Date.Now.ToString("MM/dd/yyyy") & "','" & itmcostprice.Text.Trim & "','" & itmsellprice.Text.Trim & "','" & dditemUnit.SelectedValue.Trim & "'," &
            '                       " '" & itmprrefid.Text.Trim & "', '" & itmtaxperc.Text.Trim & "','" & itmtaxamt.Text.Trim & "',  '" & itmremark.Text.Trim & "','" & dditemstatus.SelectedValue.Trim & "'," &
            '                       "'" & upby & "'," & "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"

            'Dim inssts As Boolean = ConfigData.Transact(qry, qry2, "", "", "")


            Dim cmd As SqlCommand = New SqlCommand("update item_dtls set itm_pr_info=@_itm_pr_info, itm_pr_grp=@_itm_pr_grp, " &
                            "  itm_pr_price = @_itm_pr_price, itm_pr_cost = @_itm_pr_cost,  itm_pr_unt = @_itm_pr_unt, itm_pr_refid = @_itm_pr_refid, " &
                            "itm_pr_taxpct = @_itm_pr_taxpct ,itm_pr_taxamt =@_itm_pr_taxamt,  itm_pr_rmk =  @_itm_pr_rmk,itm_pr_sts = @_itm_pr_sts , " &
                                               " updateby=@_updateby, updateon = @_updateon  where itm_pr_id=@_itm_pr_id ", ConfigData.con)

            cmd.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dditmcd.SelectedValue.Trim
            cmd.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
            cmd.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
            cmd.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = itmcostprice.Text.Trim
            cmd.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = itmsellprice.Text.Trim
            cmd.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = itmtaxperc.Text.Trim
            cmd.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = itmtaxamt.Text.Trim
            cmd.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dditemUnit.SelectedValue.Trim
            cmd.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = itmprrefid.Text.Trim
            cmd.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = dditemstatus.SelectedValue.Trim
            cmd.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = itmremark.Text.Trim
            cmd.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

            Dim cmd2 As SqlCommand = New SqlCommand("insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp,itm_pr_unt,itm_pr_refid, " &
                                               " itm_pr_dos, itm_pr_cost,itm_pr_price, itm_pr_taxpct,itm_pr_taxamt, itm_pr_sts,itm_pr_rmk, " &
                                               " createby, createon, updateby, updateon, comp_id) " &
                                               " values(@_itm_pr_id, @_itm_pr_info, @_itm_pr_grp, @_itm_pr_unt, @_itm_pr_refid, " &
                                               " @_itm_pr_dos, @_itm_pr_cost,@_itm_pr_price, @_itm_pr_taxpct,@_itm_pr_taxamt, @_itm_pr_sts,@_itm_pr_rmk, " &
                                               " @_createby,@_createon,@_updateby,@_updateon,@_comp_id)", ConfigData.con)

            cmd2.Parameters.Add("@_itm_pr_id", SqlDbType.NVarChar).Value = dditmcd.SelectedValue.Trim
            cmd2.Parameters.Add("@_itm_pr_info", SqlDbType.NVarChar).Value = itmname.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_grp", SqlDbType.NVarChar).Value = dditemgrp.SelectedValue.Trim
            cmd2.Parameters.Add("@_itm_pr_dos", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd2.Parameters.Add("@_itm_pr_cost", SqlDbType.Decimal).Value = itmcostprice.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_price", SqlDbType.Decimal).Value = itmsellprice.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_taxpct", SqlDbType.Decimal).Value = itmtaxperc.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_taxamt", SqlDbType.Decimal).Value = itmtaxamt.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_unt", SqlDbType.NVarChar).Value = dditemUnit.SelectedValue.Trim
            cmd2.Parameters.Add("@_itm_pr_refid", SqlDbType.NVarChar).Value = itmprrefid.Text.Trim
            cmd2.Parameters.Add("@_itm_pr_sts", SqlDbType.NVarChar).Value = dditemstatus.SelectedValue.Trim
            cmd2.Parameters.Add("@_itm_pr_rmk", SqlDbType.NVarChar).Value = itmremark.Text.Trim
            cmd2.Parameters.Add("@_createby", SqlDbType.NVarChar).Value = upby
            cmd2.Parameters.Add("@_createon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd2.Parameters.Add("@_updateby", SqlDbType.NVarChar).Value = upby
            cmd2.Parameters.Add("@_updateon", SqlDbType.NVarChar).Value = Date.Now.ToString("MM/dd/yyyy")
            cmd2.Parameters.Add("@_comp_id", SqlDbType.NVarChar).Value = comp

            Dim inssts As Boolean = ConfigData.postTransact(cmd, cmd2, Nothing)
            If inssts = True Then
                Call load_item_price_list_grid()
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
            'Dim comp As String = Session("login_compid")

            'Dim qry As String = "Update item_dtls SET itm_pr_info = '" & itmname.Text.Trim & "' ,  " &
            '                "  itm_pr_price = '" & itmsellprice.Text.Trim & "', itm_pr_cost = '" & itmcostprice.Text.Trim & "', " &
            '                " itm_pr_unt ='" & dditemUnit.SelectedValue.Trim & "'," & " itm_pr_refid = '" & itmprrefid.Text.Trim & "', " &
            '                "itm_pr_taxpct ='" & itmtaxperc.Text.Trim & "' ,itm_pr_taxamt ='" & itmtaxamt.Text.Trim & "', " &
            '                "itm_pr_rmk =  '" & itmremark.Text.Trim & "', itm_pr_sts = 'C' " &
            '                " updateby='" & upby & "', updateon = '" & upon & "'" &
            '                " where itm_pr_id='" & dditmcd.SelectedValue.Trim & "' "

            'Dim qry2 As String = "insert into itm_price_dtls (itm_pr_id, itm_pr_info, itm_pr_grp, " &
            '                " itm_pr_exp, itm_pr_dos, itm_pr_cost,itm_pr_price,itm_pr_unt," &
            '                " itm_pr_refid,itm_pr_taxpct,itm_pr_taxamt,itm_pr_rmk,itm_pr_sts, " &
            '                " createby, createon, updateby, updateon, comp_id) " &
            '                " values('" & dditmcd.SelectedValue.Trim & "','" & itmname.Text.Trim & "','" & dditemgrp.SelectedValue.Trim & "'," &
            '                       " null,'" & Date.Now.ToString("MM/dd/yyyy") & "','" & itmcostprice.Text.Trim & "','" & itmsellprice.Text.Trim & "','" & dditemUnit.SelectedValue.Trim & "'," &
            '                       " '" & itmprrefid.Text.Trim & "', '" & itmtaxperc.Text.Trim & "','" & itmtaxamt.Text.Trim & "',  '" & itmremark.Text.Trim & "','C'," &
            '                       "'" & upby & "'," & "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"

            'Dim inssts As Boolean = ConfigData.Transact(qry, qry2, "", "", "")
            'If inssts = True Then
            '    Call load_item_price_list_grid()
            'Else
            '    msg_box(Me, "Delete is unsuccessful")
            'End If
        Else
            msg_box(Me, "GID is null")
        End If
    End Sub


    Private Sub itmcostprice_TextChanged(sender As Object, e As EventArgs) Handles itmcostprice.TextChanged

        Dim costprice As Decimal = itmcostprice.Text
        If itmtaxperc.Text.Trim <> "" Then
            Dim taxprcnt As Decimal = itmtaxperc.Text
            itmtaxamt.Text = Math.Round((costprice * taxprcnt / 100), 2)
        End If

    End Sub

    Private Sub itmtaxperc_TextChanged(sender As Object, e As EventArgs) Handles itmtaxperc.TextChanged

        Dim costprice As Decimal = itmcostprice.Text
        If itmtaxperc.Text.Trim <> "" Then
            Dim taxprcnt As Decimal = itmtaxperc.Text
            itmtaxamt.Text = Math.Round((costprice * taxprcnt / 100), 2)
        End If
    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Item_dtl_Hist.xls")
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
