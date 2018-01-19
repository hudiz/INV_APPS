Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Partial Class Transaction_history_Details
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsCallback AndAlso Not IsPostBack Then
            Call load_item_details()
            Call Load_customer_list()
            Call load_gv_hist_details()

        End If
    End Sub

    Public Sub load_item_details()
        Dim qry As String = "SELECT [itm_pr_gid], [itm_pr_id], [itm_pr_info], [itm_pr_grp] , itm_pr_id + ' - ' + itm_pr_info as item_desc FROM [item_dtls] "
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("itm_pr_id") = "*"
        dr("item_desc") = "-select-"
        dt.Rows.InsertAt(dr, 0)

        dditmcd.DataSource = dt
        dditmcd.DataBind()
        dditmcd.SelectedIndex = 0

        If Not IsNothing(Request.QueryString("itemid")) Then
            Dim itemid As String = Request.QueryString("itemid").Trim
            If itemid.Trim <> "" Then
                dditmcd.SelectedValue = itemid
            End If
        End If

    End Sub
    Private Sub Load_customer_list()
        Dim qry As String = "SELECT [cust_sup_id] as cid, [cust_sup_id] + '-' + [cust_sup_name] as cname FROM [cust_supp_dtls] where cust_sup_sts='C'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("cid") = "*"
        dr("cname") = "-select-"
        dt.Rows.InsertAt(dr, 0)

        dd_custid.DataSource = dt
        dd_custid.DataBind()
        dd_custid.SelectedIndex = 0

        If Not IsNothing(Request.QueryString("custid")) Then
            Dim custid As String = Request.QueryString("custid").Trim
            If custid.Trim <> "" Then
                dd_custid.SelectedValue = custid
            End If
        End If
    End Sub

    Private Sub load_gv_hist_details()
        Dim custid As String = "%"
        Dim itemid As String = "%"

        'If Not IsNothing(Request.QueryString("custid")) Then
        '    custid = Request.QueryString("custid").Trim
        '    If custid.Trim <> "" Then
        '        dd_custid.SelectedValue = custid
        '    End If
        'Else
        If dditmcd.SelectedIndex > -1 Then
            custid = dd_custid.SelectedValue
        End If

        'If Not IsNothing(Request.QueryString("itemid")) Then
        '    itemid = Request.QueryString("itemid").Trim
        '    If itemid.Trim <> "" Then
        '        dditmcd.SelectedValue = itemid
        '    End If
        'Else
        If dd_custid.SelectedIndex > -1 Then
            itemid = dditmcd.SelectedValue
        End If

        Dim qry As String = "select sr_cust_id as custid,sr_itm_inv as inv,sr_itm_id,sr_itm_name as item," &
            "sr_itm_price as price,sr_itm_cost as cost,sr_itm_disc as disc,sr_itm_taxamt as taxamt," &
            "sr_itm_amt as amount,sr_itm_qty as qty from (sr_item_dtls left OUTER JOIN sr_dtls ON sr_inv_id = sr_itm_inv)" &
            " where sr_cust_id like '" & custid & "' and  sr_itm_id = '" & itemid & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        gv_hist.DataSource = dt
        gv_hist.DataBind()
        btnExport.Visible = dt.Rows.Count
    End Sub

    Private Sub CreateUserButton_Click(sender As Object, e As EventArgs) Handles CreateUserButton.Click
        Call load_gv_hist_details()
    End Sub

    Private Sub CancelUserButton_Click(sender As Object, e As EventArgs) Handles CancelUserButton.Click

        dd_custid.SelectedIndex = 0
        dditmcd.SelectedIndex = 0
        Call load_gv_hist_details()
    End Sub
    Protected Sub ExportToExcel(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=History_details.xls")
        Response.Charset = ""
        Response.ContentType = "application/ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages
            'gv_hist.AllowPaging = False
            'Me.BindGrid()

            gv_hist.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In gv_hist.HeaderRow.Cells
                'gv_hist.HeaderRow.Parent.Controls.AddAt(1, Row)
                cell.BackColor = gv_hist.HeaderStyle.BackColor
            Next

            'gv_hist.HeaderRow.Parent.Controls.RemoveAt(1)

            For Each row As GridViewRow In gv_hist.Rows
                If row.RowIndex = 0 Then
                    Continue For
                End If
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = gv_hist.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = gv_hist.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            gv_hist.RenderControl(hw)
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
