Imports System.Data
Imports System.Data.SqlClient
Partial Class Report_Purchase_Details_Rep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsCallback AndAlso Not IsPostBack Then
            Dim invno As String = Request.QueryString("invno").Trim
            Call Generate_DataTable_Sales(invno)
        End If
    End Sub


    Private Sub Generate_DataTable_Sales(invno As String)
        Dim qry As String = "select * from pr_dtls where pr_inv_id='" & invno & "'"
        Dim dt As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dt_itm As New DataTable
        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            txt_dte.Text = dr("pr_date_time").ToString.Trim.Replace(" 00:00:00", "")
            txt_invno.Text = dr("pr_inv_id").ToString.Trim
            txt_custid.Text = dr("pr_cust_id").ToString.Trim
            txt_custname.Text = dr("pr_cust_name").ToString.Trim

            txt_tot.Text = dr("pr_tot_amt").ToString.Trim
            txt_netdisc.Text = dr("pr_disc").ToString.Trim
            txt_nettax.Text = dr("pr_tax_amt").ToString.Trim
            txt_netoth.Text = dr("pr_oth_amt").ToString.Trim
            txt_nettot.Text = dr("pr_net_amt").ToString.Trim

            txt_pdbyname1.Text = dr("pr_paid_by1").ToString.Trim
            txt_pdbyamt1.Text = dr("pr_amt1").ToString.Trim
            txt_pdbyrmk1.Text = dr("pr_rmk1").ToString.Trim
            txt_pdbyname2.Text = dr("pr_paid_by2").ToString.Trim
            txt_pdbyamt2.Text = dr("pr_amt2").ToString.Trim
            txt_pdbyrmk2.Text = dr("pr_rmk2").ToString.Trim
            txt_rmk.Text = dr("pr_remark").ToString.Trim


            qry = "select pr_item_dtls.*,item_dtls.itm_pr_unt as unt, item_dtls.itm_pr_rmk  from " &
                " (pr_item_dtls left outer join item_dtls on pr_itm_id=itm_pr_id) where pr_itm_inv='" & invno & "'"
            dt_itm = ConfigData.getDataToDatatable(qry)
            gv_sales.DataSource = dt_itm
            gv_sales.DataBind()
        End If
    End Sub

End Class
