Imports System.Data.SqlClient
Public Class login
    Inherits System.Web.UI.Page
    Dim wages, labels, hr, specialuser, company As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call dbMAIN()
        txtuser.Focus()
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        con.Open()
        Dim sql1 As String = "SELECT username,password FROM usermaster WHERE username ='" & txtuser.Text & "' and password = '" & txtpassword.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            Session("UserName") = txtuser.Text
            Response.Redirect("beamwise.aspx")
        Else
            lblmsg.Text = "Invalid Username and Password"
            txtuser.Text = ""
            txtpassword.Text = ""
            txtuser.Focus()
        End If

        con.Close()
    End Sub


End Class