Imports System.Configuration
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Public Class packingprint
    Inherits System.Web.UI.Page
    Dim DOCNO, CATEGORY As String
    Dim crystalreport As ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call dbMAIN()
        DOCNO = Session("DOCNO")
        CATEGORY = Session("CATEGORY")
        'If Not IsPostBack Then
        '    CrystalReportViewer1.Visible = True
        '    Dim crystalReport As New ReportDocument()
        '    crystalReport.Load(Server.MapPath("~\Reports\packingslipaqty.rpt"))
        '    CrystalReportViewer1.ReportSource = crystalReport
        'End If
        con.Open()
        If CATEGORY = "A Quantity" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & DOCNO & "' and CQTY is NULL and BQTY IS NULL and BITQTY is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                CrystalReportViewer1.Visible = True
                Dim reportdocument As New ReportDocument()
                reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipaqty.rpt"))
                reportdocument.SetDataSource(dt1)
                CrystalReportViewer1.ReportSource = reportdocument
                'reportdocument.PrintToPrinter(1, False, 0, 0)
            End If
        ElseIf CATEGORY = "B Quantity" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & DOCNO & "' and AQTY is NULL and CQTY is null and BITQTY is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                CrystalReportViewer1.Visible = True
                Dim reportdocument As New ReportDocument()
                reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipbqty.rpt"))
                reportdocument.SetDataSource(dt1)
                CrystalReportViewer1.ReportSource = reportdocument
                ' reportdocument.PrintToPrinter(1, False, 0, 0)
            End If
        ElseIf CATEGORY = "A+B Quantity" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & DOCNO & "' and cqty is null and BITQTY is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                CrystalReportViewer1.Visible = True
                Dim reportdocument As New ReportDocument()
                reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipaqty.rpt"))
                reportdocument.SetDataSource(dt1)
                CrystalReportViewer1.ReportSource = reportdocument
                ' reportdocument.PrintToPrinter(1, False, 0, 0)
            End If
        ElseIf CATEGORY = "A+B+C Quantity" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & DOCNO & "' and AQTY is NULL and BQTY is null and BITQTY is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                CrystalReportViewer1.Visible = True
                Dim reportdocument As New ReportDocument()
                reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipcqty.rpt"))
                reportdocument.SetDataSource(dt1)
                CrystalReportViewer1.ReportSource = reportdocument
                ' reportdocument.PrintToPrinter(1, False, 0, 0)
            End If
        ElseIf CATEGORY = "Bit Quantity" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & DOCNO & "' and AQTY is NULL and BQTY is null and CQTY  is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                CrystalReportViewer1.Visible = True
                Dim reportdocument As New ReportDocument()
                reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipdqty.rpt"))
                reportdocument.SetDataSource(dt1)
                CrystalReportViewer1.ReportSource = reportdocument
                ' reportdocument.PrintToPrinter(1, False, 0, 0)
            End If
        End If

        con.Close()
    End Sub

End Class