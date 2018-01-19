Imports System.Data
Partial Class Account_Register
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack And Not IsCallback Then
            Session("login_dtls") = Nothing
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
            Call load_company_dtls()
        End If
    End Sub

    Protected Sub CreateUserButton_Click(sender As Object, e As System.EventArgs) Handles CreateUserButton.Click
        Dim crby As String = User.Identity.Name.ToString.Trim()
        Dim upby As String = User.Identity.Name.ToString.Trim()
        Dim comp As String = logincomp.SelectedValue.Trim
        Dim qry As String = "insert into login_dtls (login_name, login_pwd, login_email, login_ph,login_expry,login_user_name," & _
                                  "createby, createon, updateby, updateon, comp_id) " & _
                                  " values('" & UserName.Text.Trim & "','" & Password.Text.Trim & "','" & Email.Text.Trim & "','" & Phone.Text.Trim & "','" & loginexp.Text.Trim & "'," & _
                                  "'" & loginname.Text.Trim & "','" & crby & "','" & Date.Now.ToString("MM/dd/yyyy") & "','" & upby & "'," & _
                                  "'" & Date.Now.ToString("MM/dd/yyyy") & "','" & comp & "')"
        Dim inssts As Integer = ConfigData.postDataToTable(qry)
        If inssts > 0 Then
            Dim dt_login As New DataTable
            dt_login.Columns.Add("l_name")
            dt_login.Columns.Add("comp")
            dt_login.Rows.Add(loginname.Text.Trim, comp)
            Session("login_dtls") = dt_login
            Response.Redirect("../Default.aspx")
        Else
            MsgBox("Sorry Try again!!!")
        End If
    End Sub

    Private Sub load_company_dtls()
        Dim qry As String = "select comp_id ,comp_name from comp_dtls"
        Dim dt_comp As DataTable = ConfigData.getDataToDatatable(qry)
        Dim dr As DataRow = dt_comp.NewRow()
        dr("comp_id") = ""
        dr("comp_name") = "-Select-"
        dt_comp.Rows.InsertAt(dr, 0)
        logincomp.DataSource = dt_comp
        logincomp.DataBind()
        Session("login_company") = dt_comp
    End Sub

End Class
