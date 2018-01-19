Imports System.Data
Partial Class Account_Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Session("login_dtls") = Nothing
            'HttpContext.Current.Session.Clear()
            'HttpContext.Current.Session.Abandon()
        End If
    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As System.EventArgs)

        Dim uname As String = LoginUser.UserName.Trim
        Dim pwd As String = LoginUser.Password.Trim
        Dim qry As String = "select login_user_name as l_name,comp_id as comp from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
        '"select count(*) as cnt from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
        Dim dt_login As DataTable = ConfigData.getDataToDatatable(qry)
        '  Dim cnt As Integer = CInt(dt_itm.Rows(0)("cnt"))
        If dt_login.Rows.Count = 1 Then
            lblmatch.Text = ""
            'qry = "select login_user_name as l_name,comp_id as comp from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
            'Dim dt_login As DataTable = ConfigData.getDataToDatatable(qry)
            Session("login_dtls") = dt_login
            Session("login_usrname") = dt_login.Rows(0)("l_name")
            Session("login_compid") = dt_login.Rows(0)("comp")
            Response.Redirect("../Reports/Home.aspx")
        Else
            lblmatch.Text = "UserName or Password does not match... Try Again !!!"
        End If
    End Sub

    Protected Sub LoginUser_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles LoginUser.Authenticate
        Dim uname As String = LoginUser.UserName.Trim
        Dim pwd As String = LoginUser.Password.Trim
        Dim qry As String = "select login_user_name as l_name,comp_id as comp from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
        '"select count(*) as cnt from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
        Dim dt_login As DataTable = ConfigData.getDataToDatatable(qry)
        '  Dim cnt As Integer = CInt(dt_itm.Rows(0)("cnt"))
        If dt_login.Rows.Count = 1 Then
            e.Authenticated = True
            lblmatch.Text = ""
            'qry = "select login_user_name as l_name,comp_id as comp from login_dtls where login_name='" & uname & "' and login_pwd='" & pwd & "'"
            'Dim dt_login As DataTable = ConfigData.getDataToDatatable(qry)
            Session("login_dtls") = dt_login
            Session("login_usrname") = dt_login.Rows(0)("l_name")
            Session("login_compid") = dt_login.Rows(0)("comp")
            ' Response.Redirect("../Reports/Home.aspx")
        Else
            e.Authenticated = False
            lblmatch.Text = "UserName or Password does not match... Try Again !!!"
        End If
    End Sub

    Private Sub LoginUser_LoggedIn(sender As Object, e As EventArgs) Handles LoginUser.LoggedIn

        Response.Redirect("../Reports/Home.aspx")
    End Sub
End Class