Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports ConfigData

Partial Class Reports_Stock_Item_Dtls
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then

            Call clear_control_values()
            Call load_itemGroup_details()
            Call load_item_details()
            If Not IsNothing(Request.QueryString("item")) Then
                Dim itm_id As String = Request.QueryString("item").Trim
                Call Item_Selected(itm_id)
            Else
                Call load_all_item_details()
            End If
        End If
    End Sub
    Public Sub load_itemGroup_details()
        Dim qry As String = "SELECT [itm_grp_code] as grp_cd, itm_grp_code + ' - ' + itm_grp_desc as grp_desc FROM [itm_grp_dtls]"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        dditemgrp.DataSource = dt
        dditemgrp.DataBind()
    End Sub
    Private Sub dditemgrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dditemgrp.SelectedIndexChanged
        Call load_item_details()
        Call load_all_item_details()
    End Sub
    Public Sub load_item_details()
        Dim qry As String = "SELECT [itm_pr_gid], [itm_pr_id], [itm_pr_info], [itm_pr_grp] , itm_pr_id + ' - ' + itm_pr_info as item_desc FROM [item_dtls] " &
            " where itm_pr_grp ='" & dditemgrp.SelectedValue.Trim & "' order by itm_pr_id "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)

        dditmcd.DataSource = dt
        dditmcd.DataBind()

        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
            ' Call Item_Selected(item.Split("-")(0))
        End If

    End Sub

    Public Sub load_all_item_details()
        If dditmcd.SelectedIndex > -1 Then
            Call load_Supplier_Purchase_grid()
            Call load_customer_Sales_grid()
            Call load_item_price_list_grid()
            Call load_item_qty_view_grid()
        End If
    End Sub

    Private Sub dditmcd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dditmcd.SelectedIndexChanged
        Call load_all_item_details()
        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
            ' Call Item_Selected(item.Split("-")(0))
        End If

    End Sub

    Public Sub load_Supplier_Purchase_grid()
        Dim qry As String = "SELECT pr_item_dtls.*, cust_sup_name  pr_itm_custname FROM pr_item_dtls left outer join cust_supp_dtls on pr_itm_custid = cust_sup_id " &
            " where pr_itm_id ='" & dditmcd.SelectedValue.Trim & "'"

        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmp.DataSource = dt
        gv_itmp.DataBind()

    End Sub
    Public Sub load_customer_Sales_grid()
        Dim qry As String = "SELECT  sr_item_dtls.*, cust_sup_name sr_itm_custname FROM sr_item_dtls left outer join cust_supp_dtls on sr_itm_custid = cust_sup_id " &
            " where sr_itm_id ='" & dditmcd.SelectedValue.Trim & "'"

        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)

        gv_itms.DataSource = dt
        gv_itms.DataBind()

    End Sub

    Public Sub load_item_price_list_grid()
        Dim qry As String = "SELECT *  FROM stock_adj_dtls " &
            " where stk_itm_id ='" & dditmcd.SelectedValue.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmstkadj.DataSource = dt
        gv_itmstkadj.DataBind()

    End Sub

    Public Sub load_item_qty_view_grid()
        Dim qry As String = "SELECT V_Item_Stock_Details.*, " &
            "  CASE WHEN TYP = 'S' THEN 'SALES' WHEN TYP = 'SR' THEN 'SALES RETURN' " &
            "   WHEN TYP = 'P' THEN 'PURCHASE' WHEN TYP = 'PR' THEN 'PURCHASE RETURN' " &
            "   WHEN TYP = 'SA' THEN 'STOCK ADJUSTMENT'  END AS  TRXTYP " &
            " FROM V_Item_Stock_Details " &
            " where ITEMCD ='" & dditmcd.SelectedValue.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_itmgenvw.DataSource = dt
        gv_itmgenvw.DataBind()

        lbl_currstock.Text = "Current Stock : 0"
        qry = "select QTY from V_Item_Stock_Summ where ITEMCD = '" & dditmcd.SelectedValue.Trim & "' and QTY is not null"
        Dim dtqty As DataTable = ConfigData.getDataToDatatable(qry)
        If dtqty.Rows.Count > 0 Then
            lbl_currstock.Text = "Current Stock : " & CDec(dtqty.Rows(0)("QTY")).ToString("#0.00") & " " & dtqty.Rows(0)("QTY")
        End If

    End Sub

    Public Sub clear_control_values()
        'dditemgrp.SelectedValue = ""
        'dditmcd.SelectedValue = ""
        itmname.Text = ""
        If dditmcd.SelectedIndex > -1 Then
            Dim item As String = dditmcd.SelectedItem.Text
            itmname.Text = item.Split("-")(1)
        End If
        Call load_all_item_details()
    End Sub

    Protected Sub Item_Selected(itm_id As String)
        ' Dim row As GridViewRow = gv_itmprclst.SelectedRow
        ''Dim gid As String = row.Cells(0).Text
        ''Dim cid As String = row.Cells(1).Text
        Dim qry As String = "SELECT * " & " FROM [item_dtls]" & " where itm_pr_id ='" & itm_id.Trim & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        If dt.Rows.Count > 0 Then
            '   itmgid.Text = dt.Rows(0)("itm_pr_gid").ToString.Trim
            dditemgrp.SelectedValue = dt.Rows(0)("itm_pr_grp").ToString.Trim
            Call load_item_details()

            dditmcd.SelectedValue = dt.Rows(0)("itm_pr_id").ToString.Trim
            itmname.Text = dt.Rows(0)("itm_pr_info").ToString.Trim
            Call load_all_item_details()
        Else
            clear_control_values()
        End If
    End Sub

    Protected Sub OnDataBound_itmp(sender As Object, e As EventArgs)
        If gv_itmp.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_itmp.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_itmp.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_itmp.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

    Protected Sub OnDataBound_itms(sender As Object, e As EventArgs)
        If gv_itms.Rows.Count > 0 Then
            Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
            For i As Integer = 0 To gv_itms.Columns.Count - 2
                Dim cell As New TableHeaderCell()
                Dim txtSearch As New TextBox()
                txtSearch.Width = gv_itms.Columns(i).ItemStyle.Width
                txtSearch.CssClass = "search_textbox_sitm form-control"
                txtSearch.Height = "25"
                cell.Controls.Add(txtSearch)
                row.Controls.Add(cell)
            Next
            gv_itms.HeaderRow.Parent.Controls.AddAt(1, row)
        End If
    End Sub

End Class
