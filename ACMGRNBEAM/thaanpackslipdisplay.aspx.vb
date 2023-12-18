Imports System.Net
Imports System.Data.SqlClient
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text.RegularExpressions
Public Class thaanpackslipdisplay
    Inherits Page
    Dim str As String
    Dim docnumber, rowcount As Integer
    Dim grndate, autonumdate, dcdate As DateTime
    Dim autonumber As String
    Dim dt3 As DataTable
    Dim machinenum, DOCNO As String
    Dim totalAquantity, totalBquantity, totalCQuantity, totalDquantity, AQTY, BQTY, AMTRS, BMTRS As Decimal
    Dim qrcode() As String
    Dim LineOfText, arraystring, batch, itemcode, totalmtrs, totalqty, thaanno, thaanbatchno, thaannumber, stickercategory, Weight As String
    Dim setmachine As Boolean = False
    Dim seqnumber, selectedautonumber As String
    Dim grnquantity, grnmeters, grnlength, grnwidth, totalquantity, totalmeters, reedspace, warpcons, weftcons, pick As Decimal
    Dim grndocentry As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Call dbMAIN()
        txtgrn.Focus()
        lblusername.Text = Session("UserName")
    End Sub

    Public Sub generateslipno()
        con.Open()
        Dim sql As String = "select isnull(max(docnum), 0) as docnum from subgrnpacking"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            lbldocnum.Text = dt.Rows(0)("docnum").ToString() + 1
        End If
        con.Close()
    End Sub


    Public Sub CLEAR()
        lbltotthaans.Text = ""
        lbltotqty.Text = ""
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        lbltotmtrs.Text = ""
        lbltotweight.Text = ""
    End Sub

    Public Sub loadlength()
        Dim sql As String = "select max(docentry) from  [@INT_OPDN] where docnum ='" & txtgrn.Text & "' and U_Process ='W2'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            grndocentry = dt.Rows(0)(0).ToString()
        End If
        Dim sql1 As String = "select  A.CreateDate,B.U_Length,B.U_Width,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            grnlength = dt1.Rows(0)("U_Length").ToString()
            grnwidth = dt1.Rows(0)("U_Width").ToString()
            dcdate = dt1.Rows(0)("u_SUPPDCdt").ToString()
            grndate = dt1.Rows(0)("createdate").ToString()
        End If
    End Sub



    Private Sub btnget_Click(sender As Object, e As EventArgs) Handles btnget.Click
        con.Open()
        Dim sql As String = "select distinct DOCNUM from SUBGRNPACKING where subgrnno = '" & txtgrn.Text & "'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            cmbdocnum.DataSource = dt
            cmbdocnum.DataTextField = "DOCNUM"
            cmbdocnum.DataValueField = "DOCNUM"
            cmbdocnum.DataBind()
            lbldocnum.Text = cmbdocnum.Text
            cmbdocnum.Items.Insert(0, New ListItem("-Select Slip No", "-Select Slip No-"))
            cmbdocnum.SelectedIndex = 0
        End If

        con.Close()
    End Sub

    Private Sub cmbdocnum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbdocnum.SelectedIndexChanged
        Dim reportdocument As New ReportDocument()
        con.Open()
        Dim sql1 As String = "Select * from subgrnpacking where subgrnno ='" & txtgrn.Text & "' and docnum = '" & cmbdocnum.Text & "'  ORDER BY DOCENTRY"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            CrystalReportViewer1.Visible = True
            reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipaqty.rpt"))
            reportdocument.SetDataSource(dt1)
            CrystalReportViewer1.ReportSource = reportdocument
            'reportdocument.PrintToPrinter(1, False, 0, 0)
        End If
        con.Close()
    End Sub
End Class