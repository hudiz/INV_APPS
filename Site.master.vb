Imports System.Data
Partial Class Site
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If IsNothing(Session("login_dtls")) Then
                HttpContext.Current.Session.Clear()
                HttpContext.Current.Session.Abandon()
                Response.Redirect("../Account/Login.aspx")
            Else
                Call load_init_details()
            End If
        End If
    End Sub

    Private Sub load_init_details()
        Dim dt_login As DataTable = TryCast(Session("login_dtls"), DataTable)
        Dim l_name As String = dt_login.Rows(0)("l_name").ToString.Trim
        Dim comp_id As String = dt_login.Rows(0)("comp").ToString.Trim

        Dim qry As String = "select comp_name as c_name from comp_dtls where comp_id='" & comp_id & "'"
        Dim dt_comp As DataTable = ConfigData.getDataToDatatable(qry)
        Dim cname As String = "INV-APP"
        If dt_comp.Rows.Count > 0 Then
            cname = dt_comp.Rows(0)("c_name").ToString.Trim
        End If
        HeadLoginName.Text = "Welcome " & l_name.ToUpper.Trim()
        comp_title.Text = cname
    End Sub

    Protected Sub HeadLoginStatus_ServerClick(sender As Object, e As System.EventArgs) Handles HeadLoginStatus.ServerClick
        HttpContext.Current.Session.Clear()
        HttpContext.Current.Session.Abandon()
        Response.Redirect("../Account/Login.aspx")
    End Sub
End Class

