
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class packprint

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim crystalReport As New ReportDocument()
        Call dbMAIN()
        con.Open()
        Dim sql1 As String = "Select * from subgrnpacking"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then

            CrystalReportViewer1.Visible = True
            CrystalReportViewer1.ReportSource = dt1
            crystalReport.Load(Server.MapPath("~/subgrnpackingslipaqty.rpt"))
        End If
        con.Close()
    End Sub




End Class