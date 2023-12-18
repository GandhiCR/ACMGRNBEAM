Imports System.Net
Imports System.Data.SqlClient
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text.RegularExpressions
Public Class packingslip
    Inherits Page
    Dim str As String
    Dim docnumber, rowcount As Integer
    Dim grndate, dcdate, autonumdate As DateTime
    Dim autonumber As String
    Dim dt3 As DataTable
    Dim machinenum, DOCNO As String
    Dim totalAquantity, totalBquantity, totalCQuantity, totalDquantity As Decimal
    Dim qrcode() As String
    Dim LineOfText, arraystring, batch, itemcode, totalmtrs, totalqty, thaanno, thaanbatchno, thaannumber, stickercategory, CATEGORY, weight As String
    Dim setmachine As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Call dbMAIN()
        Dim lblUserName As Label = TryCast(Me.Master.FindControl("lbluserName"), Label)
        lblUserName.Text = lblUserName.Text.ToUpper
        If setmachine = False Then
            cmbdevice.Enabled = True
            machinenum = cmbdevice.SelectedItem.ToString
            Label1.Text = machinenum
        Else
            txtqrcode.Enabled = True
            cmbdevice.Enabled = False
            cmbdevice.Visible = False
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Call dbMAIN()

        If txtqrcode.Text <> String.Empty Then
            con.Open()
            Dim sql As String = "Select autonum FROM subgrnpacking WHERE autonum = '" + txttagrfid.Text + "'"
            Dim cmd As New SqlCommand(sql, con)
            Dim dt As New DataTable()
            Dim ad As New SqlDataAdapter(cmd)
            ad.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    lblerror.BackColor = System.Drawing.Color.White
            '    lblerror.Text = "Thaan was Already Added to Packing Slip"
            '    txtqrcode.Text = ""
            '    txtqrcode.Focus()
            '    getinspectiondetails()
            '    arrangegrid()
            '    getinspectiondata()
            'Else
            getinspectiondetails()
                getinspectiondata()
                savedetails()
                rfidmap.Visible = True
                arrangegrid()
                con.Close()
            End If
        'End If

        txtqrcode.Focus()
    End Sub


    Public Sub clear()
        txtqrcode.Text = ""
        txttagrfid.Text = ""

    End Sub

    Public Sub getinspectiondata()
        lastvaluepanel.Visible = True
        Dim sql1 As String = "SELECT top 1 autonum,thronecolor,aqty,bqty,bitqty,totalqty,thaanbatch FROM subgrnpacking WHERE device = '" & machinenum & "' order by docdate desc"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        Dim thaanno, thaancolor, thaanaqty, thaanbqty, thaandqty, thaanqty As String
        If dt1.Rows.Count > 0 Then
            thaanno = dt1.Rows(0)("autonum").ToString
            thaancolor = dt1.Rows(0)("thronecolor").ToString
            thaanqty = dt1.Rows(0)("totalqty").ToString
            thaanaqty = dt1.Rows(0)("aqty").ToString
            thaanbqty = dt1.Rows(0)("bqty").ToString
            thaandqty = dt1.Rows(0)("bitqty").ToString
            lbltotqty.Text = " " & ":" & " " & thaanqty & " " & "Nos."
            lbllastaqty.Text = " " & ":" & " " & thaanaqty & " " & "Nos."
            lbllastbqty.Text = " " & ":" & " " & thaanbqty & " " & "Nos."
            lbllastdqty.Text = " " & ":" & " " & thaandqty & " " & "Nos."
            lblthaanno.Text = " " & ":" & " " & thaanno
            lblthronecolor.Text = " " & ":" & " " & thaancolor
        End If
    End Sub
    Public Sub getinspectiondetails()

        Dim sql1 As String = "select subgrnno,subgrndate,suppdc,suppdate,vencode,venname,itemcode,itemname,thronesize,thronewidth,batchno,aqty,bqty,cqty,bitqty,thronecolor,weight,thaanbatch from subgrninspection where autonum = '" & txttagrfid.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblgrnno.Text = dt1.Rows(0)("subgrnno").ToString()
            grndate = dt1.Rows(0)("subgrndate").ToString()
            lblgrndate.Text = grndate.ToString("dd/MM/yyyy")
            lblitemcode.Text = dt1.Rows(0)("itemcode").ToString()
            lblitemname.Text = dt1.Rows(0)("itemname").ToString()
            lbldcno.Text = dt1.Rows(0)("suppdc").ToString()
            dcdate = dt1.Rows(0)("suppdate").ToString()
            lbldcdate.Text = dcdate.ToString("dd/MM/yyyy")
            lblvencode.Text = dt1.Rows(0)("vencode").ToString()
            lblvenname.Text = dt1.Rows(0)("venname").ToString()
            lblbatchno.Text = dt1.Rows(0)("batchno").ToString()
            lblthaanlength.Text = dt1.Rows(0)("thronesize").ToString()
            lblthaanwidth.Text = dt1.Rows(0)("thronewidth").ToString()
            lblaqty.Text = dt1.Rows(0)("aqty").ToString()
            lblbqty.Text = dt1.Rows(0)("bqty").ToString()
            lblcqty.Text = dt1.Rows(0)("cqty").ToString()
            lbldqty.Text = dt1.Rows(0)("bitqty").ToString()
            lblthaancolor.Text = dt1.Rows(0)("thronecolor").ToString()
            lblthaanweight.Text = dt1.Rows(0)("weight").ToString()
            thaanbatchno = dt1.Rows(0)("thaanbatch").ToString()
        Else
            lblerror.Text = "No Data Found. kindly check GRN"
        End If

    End Sub
    Public Sub savedetails()
        getinspectiondetails()
        If lblitem.Text = "A Qty" Then
            Session("CATEGORY") = lblitem.Text
            Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[AQTY],[TOTALQTY],[WEIGHT],[DEVICE],[THAANBATCH])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@AQTY,@TOTALQTY,@WEIGHT,@DEVICE,@THAANBATCH)", con)
            cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
            cmd.Parameters.AddWithValue("SUBGRNNO", lblgrnno.Text)
            cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
            cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
            cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
            cmd.Parameters.AddWithValue("ITEMCODE", lblitemcode.Text)
            cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
            cmd.Parameters.AddWithValue("LENGTH", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("WIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
            cmd.Parameters.AddWithValue("AUTONUM", txttagrfid.Text)
            totalqty = Val(lblaqty.Text)
            cmd.Parameters.AddWithValue("THORNEQTY", totalqty)
            cmd.Parameters.AddWithValue("THORNEMTRS", totalqty * Val(lblthaanlength.Text))
            cmd.Parameters.AddWithValue("THRONECOLOR", lblthaancolor.Text)
            cmd.Parameters.AddWithValue("THRONESIZE", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("THRONEWIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("AQTY", lblaqty.Text)
            cmd.Parameters.AddWithValue("TOTALQTY", totalqty)
            cmd.Parameters.AddWithValue("WEIGHT", lblthaanweight.Text)
            cmd.Parameters.AddWithValue("DEVICE", cmbdevice.SelectedItem.Text)
            cmd.Parameters.AddWithValue("THAANBATCH", thaanbatchno)
            cmd.ExecuteNonQuery()
            lblerror.ForeColor = System.Drawing.Color.Green
            lblerror.BackColor = System.Drawing.Color.White
            lblerror.Text = "THAAN ADDED TO PACKING SLIP"
            txtqrcode.Text = ""
            txtqrcode.Focus()
        ElseIf lblitem.Text = "B Qty" Then
            Session("CATEGORY") = lblitem.Text
            Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[BQTY],[TOTALQTY],[WEIGHT],[DEVICE],[THAANBATCH])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@BQTY,@TOTALQTY,@WEIGHT,@DEVICE,@THAANBATCH)", con)
            cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
            cmd.Parameters.AddWithValue("SUBGRNNO", lblgrnno.Text)
            cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
            cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
            cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
            cmd.Parameters.AddWithValue("ITEMCODE", lblitemcode.Text)
            cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
            cmd.Parameters.AddWithValue("LENGTH", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("WIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
            cmd.Parameters.AddWithValue("AUTONUM", txttagrfid.Text)
            totalqty = Val(lblbqty.Text)
            cmd.Parameters.AddWithValue("THORNEQTY", totalqty)
            cmd.Parameters.AddWithValue("THORNEMTRS", totalqty * Val(lblthaanlength.Text))
            cmd.Parameters.AddWithValue("THRONECOLOR", lblthaancolor.Text)
            cmd.Parameters.AddWithValue("THRONESIZE", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("THRONEWIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BQTY", lblbqty.Text)
            cmd.Parameters.AddWithValue("TOTALQTY", totalqty)
            cmd.Parameters.AddWithValue("WEIGHT", lblthaanweight.Text)
            cmd.Parameters.AddWithValue("DEVICE", cmbdevice.SelectedItem.Text)
            cmd.Parameters.AddWithValue("THAANBATCH", thaanbatchno)
            cmd.ExecuteNonQuery()
            lblerror.ForeColor = System.Drawing.Color.Green
            lblerror.BackColor = System.Drawing.Color.White
            lblerror.Text = "THAAN ADDED TO PACKING SLIP"
            txtqrcode.Text = ""
            txtqrcode.Focus()
        ElseIf lblitem.Text = "C Qty" Then
            Session("CATEGORY") = lblitem.Text
            Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[CQTY],[TOTALQTY],[WEIGHT],[DEVICE],[THAANBATCH])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@CQTY,@TOTALQTY,@WEIGHT,@DEVICE,@THAANBATCH)", con)
            cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
            cmd.Parameters.AddWithValue("SUBGRNNO", lblgrnno.Text)
            cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
            cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
            cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
            cmd.Parameters.AddWithValue("ITEMCODE", lblitemcode.Text)
            cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
            cmd.Parameters.AddWithValue("LENGTH", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("WIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
            cmd.Parameters.AddWithValue("AUTONUM", txttagrfid.Text)
            totalqty = Val(lblcqty.Text)
            cmd.Parameters.AddWithValue("THORNEQTY", totalqty)
            cmd.Parameters.AddWithValue("THORNEMTRS", totalqty * Val(lblthaanlength.Text))
            cmd.Parameters.AddWithValue("THRONECOLOR", lblthaancolor.Text)
            cmd.Parameters.AddWithValue("THRONESIZE", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("THRONEWIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("CQTY", lblcqty.Text)
            cmd.Parameters.AddWithValue("TOTALQTY", totalqty)
            cmd.Parameters.AddWithValue("WEIGHT", lblthaanweight.Text)
            cmd.Parameters.AddWithValue("DEVICE", cmbdevice.SelectedItem.Text)
            cmd.Parameters.AddWithValue("THAANBATCH", thaanbatchno)
            cmd.ExecuteNonQuery()
            lblerror.ForeColor = System.Drawing.Color.Green
            lblerror.BackColor = System.Drawing.Color.White
            lblerror.Text = "THAAN ADDED TO PACKING SLIP"
            txtqrcode.Text = ""
            txtqrcode.Focus()
        ElseIf lblitem.Text = "Bit Qty" Then
            Session("CATEGORY") = lblitem.Text
            Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[BITQTY],[TOTALQTY],[WEIGHT],[DEVICE],[THAANBATCH])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@BITQTY,@TOTALQTY,@WEIGHT,@DEVICE,@THAANBATCH)", con)
            cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
            cmd.Parameters.AddWithValue("SUBGRNNO", lblgrnno.Text)
            cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
            cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
            cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
            cmd.Parameters.AddWithValue("ITEMCODE", lblitemcode.Text)
            cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
            cmd.Parameters.AddWithValue("LENGTH", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("WIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
            cmd.Parameters.AddWithValue("AUTONUM", txttagrfid.Text)
            totalqty = Val(lbldqty.Text)
            cmd.Parameters.AddWithValue("THORNEQTY", totalqty)
            cmd.Parameters.AddWithValue("THORNEMTRS", totalqty * Val(lblthaanlength.Text))
            cmd.Parameters.AddWithValue("THRONECOLOR", lblthaancolor.Text)
            cmd.Parameters.AddWithValue("THRONESIZE", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("THRONEWIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BITQTY", lbldqty.Text)
            cmd.Parameters.AddWithValue("TOTALQTY", totalqty)
            cmd.Parameters.AddWithValue("WEIGHT", lblthaanweight.Text)
            cmd.Parameters.AddWithValue("DEVICE", cmbdevice.SelectedItem.Text)
            cmd.Parameters.AddWithValue("THAANBATCH", thaanbatchno)
            cmd.ExecuteNonQuery()
            lblerror.ForeColor = System.Drawing.Color.Green
            lblerror.BackColor = System.Drawing.Color.White
            lblerror.Text = "THAAN ADDED TO PACKING SLIP"
            txtqrcode.Text = ""
            txtqrcode.Focus()
        ElseIf lblitem.Text = "A+B Qty" Then
            Session("CATEGORY") = lblitem.Text
            Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[AQTY],[BQTY],[TOTALQTY],[WEIGHT],[DEVICE],[THAANBATCH])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@AQTY,@BQTY,@TOTALQTY,@WEIGHT,@DEVICE,@THAANBATCH)", con)
            cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
            cmd.Parameters.AddWithValue("SUBGRNNO", lblgrnno.Text)
            cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
            cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
            cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
            cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
            cmd.Parameters.AddWithValue("ITEMCODE", lblitemcode.Text)
            cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
            cmd.Parameters.AddWithValue("LENGTH", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("WIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
            cmd.Parameters.AddWithValue("AUTONUM", txttagrfid.Text)
            totalqty = Val(lblaqty.Text) + Val(lblbqty.Text)
            cmd.Parameters.AddWithValue("THORNEQTY", totalqty)
            cmd.Parameters.AddWithValue("THORNEMTRS", totalqty * Val(lblthaanlength.Text))
            cmd.Parameters.AddWithValue("THRONECOLOR", lblthaancolor.Text)
            cmd.Parameters.AddWithValue("THRONESIZE", lblthaanlength.Text)
            cmd.Parameters.AddWithValue("THRONEWIDTH", lblthaanwidth.Text)
            cmd.Parameters.AddWithValue("AQTY", lblaqty.Text)
            cmd.Parameters.AddWithValue("BQTY", lblbqty.Text)
            cmd.Parameters.AddWithValue("TOTALQTY", totalqty)
            cmd.Parameters.AddWithValue("WEIGHT", lblweight.Text)
            cmd.Parameters.AddWithValue("DEVICE", cmbdevice.SelectedItem.Text)
            cmd.Parameters.AddWithValue("THAANBATCH", thaanbatchno)
            cmd.ExecuteNonQuery()
            lblerror.ForeColor = System.Drawing.Color.Green
            lblerror.BackColor = System.Drawing.Color.White
            lblerror.Text = "THAAN ADDED TO PACKING SLIP"
            txtqrcode.Text = ""
            txtqrcode.Focus()
        End If


    End Sub



    Private Sub txtqrcode_TextChanged(sender As Object, e As EventArgs) Handles txtqrcode.TextChanged
        Dim i1 As Integer
        LineOfText = txtqrcode.Text
        qrcode = LineOfText.Split("|")
        For i1 = 0 To UBound(qrcode)
            arraystring = qrcode(i1)
        Next i1
        thaanno = qrcode(1)
        stickercategory = qrcode(0)
        txttagrfid.Visible = True
        lblitem.Visible = True
        txttagrfid.Text = Trim(thaanno)
        lblitem.Text = stickercategory
        If lblitem.Text = "A+B Qty" Then
            weight = qrcode(8)
            lblweight.Text = weight
        End If
    End Sub
    Public Sub arrangegrid()
        If lblitem.Text = "A+B Qty" Or lblitem.Text = "A Qty" Or lblitem.Text = "B Qty" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and CQTY is NULL and BITQTY is null ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                rfidmap.DataSource = dt2
                ViewState("Data") = dt2
                rfidmap.DataBind()
                Dim i As Integer
                For i = 0 To rfidmap.Rows.Count - 1
                    If lblitem.Text = "A Qty" Then
                        totalAquantity = dt2.AsEnumerable().Sum(Function(row) row.Field(Of Decimal)("AQty"))
                        rfidmap.Columns(3).FooterText = totalAquantity.ToString("F2")

                    End If
                Next
            End If
        ElseIf lblitem.Text = "C Qty" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and AQTY is NULL and BQTY is null and BITQTY is null  ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                rfidmap.DataSource = dt2
                ViewState("Data") = dt2
                rfidmap.DataBind()
            End If
        ElseIf lblitem.Text = "Bit Qty" Then
            Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and AQTY is NULL and BQTY is null and CQTY  is null  ORDER BY DOCENTRY"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                rfidmap.DataSource = dt2
                ViewState("Data") = dt2
                rfidmap.DataBind()
            End If

        End If
    End Sub


    Private Sub cmbdevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbdevice.SelectedIndexChanged
        If setmachine = False Then
            cmbdevice.Enabled = True
            machinenum = Trim(cmbdevice.SelectedItem.ToString)
            Label1.Text = machinenum
            setmachine = True
            txtqrcode.Enabled = True
            cmbdevice.Visible = False
            txtqrcode.Focus()
        Else
            cmbdevice.Enabled = False
            cmbdevice.Visible = False
        End If
    End Sub

    Private Sub rfidmap_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles rfidmap.RowDeleting

    End Sub

    Private Sub btnfinish_Click(sender As Object, e As EventArgs) Handles btnfinish.Click
        Response.Redirect("packingprint.aspx")
        'con.Open()
        'If lblitem.Text = "A+B Qty" Or lblitem.Text = "A Qty" Or lblitem.Text = "B Qty" Then
        '    Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and CQTY is NULL and BITQTY is null ORDER BY DOCENTRY"
        '    Dim cmd1 As New SqlCommand(sql1, con)
        '    Dim dt1 As New DataTable()
        '    Dim ad1 As New SqlDataAdapter(cmd1)
        '    ad1.Fill(dt1)
        '    If dt1.Rows.Count > 0 Then
        '        CrystalReportViewer1.Visible = False
        '        Dim reportdocument As New ReportDocument()
        '        reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipaqty.rpt"))
        '        reportdocument.SetDataSource(dt1)
        '        CrystalReportViewer1.ReportSource = reportdocument
        '        'reportdocument.PrintToPrinter(1, False, 0, 0)
        '    End If
        'ElseIf lblitem.Text = "C Qty" Then
        '    Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and AQTY is NULL and BQTY is null and BITQTY is null ORDER BY DOCENTRY"
        '    Dim cmd1 As New SqlCommand(sql1, con)
        '    Dim dt1 As New DataTable()
        '    Dim ad1 As New SqlDataAdapter(cmd1)
        '    ad1.Fill(dt1)
        '    If dt1.Rows.Count > 0 Then
        '        CrystalReportViewer1.Visible = False
        '        Dim reportdocument As New ReportDocument()
        '        reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipcqty.rpt"))
        '        reportdocument.SetDataSource(dt1)
        '        CrystalReportViewer1.ReportSource = reportdocument
        '        ' reportdocument.PrintToPrinter(1, False, 0, 0)
        '    End If
        'ElseIf lblitem.Text = "Bit Qty" Then
        '    Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' and AQTY is NULL and BQTY is null and CQTY  is null ORDER BY DOCENTRY"
        '    Dim cmd1 As New SqlCommand(sql1, con)
        '    Dim dt1 As New DataTable()
        '    Dim ad1 As New SqlDataAdapter(cmd1)
        '    ad1.Fill(dt1)
        '    If dt1.Rows.Count > 0 Then
        '        CrystalReportViewer1.Visible = False
        '        Dim reportdocument As New ReportDocument()
        '        reportdocument.Load(Server.MapPath("~\Reports\subgrnpackingslipdqty.rpt"))
        '        reportdocument.SetDataSource(dt1)
        '        CrystalReportViewer1.ReportSource = reportdocument
        '        ' reportdocument.PrintToPrinter(1, False, 0, 0)
        '    End If
        'End If

        'con.Close()
    End Sub



    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        con.Open()
        For Each row As GridViewRow In rfidmap.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                autonumber = TryCast(row.Cells(2).FindControl("lblautonum"), Label).Text
                If chkRow.Checked = True Then
                    Dim cmd As New SqlCommand("DELETE FROM subgrnpacking WHERE subgrnno=@subgrnno and autonum =@autonum", con)
                    cmd.Parameters.AddWithValue("subgrnno", lblgrnno.Text)
                    cmd.Parameters.AddWithValue("autonum", autonumber)
                    cmd.ExecuteNonQuery()
                    lblerror.BackColor = System.Drawing.Color.White
                    lblerror.Text = "Thaan Packing Details Deleted"
                End If
            End If
        Next
        Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & lbldocnum.Text & "' ORDER BY DOCENTRY"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            rfidmap.DataSource = dt2
            ViewState("Data") = dt2
            rfidmap.DataBind()
        End If
        txtqrcode.Text = ""
        con.Close()

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        If Label1.Text = "-Select Location-" Then
            lblerror.Visible = True
            lblerror.Text = "Kindly Select Location"
        Else
            lblerror.Visible = False
            txtpack.Visible = False
            lbldocnum.Visible = True
            Dim sql As String = "select isnull(max(docnum), 0) as docnum from subgrnpacking"
            Dim cmd As New SqlCommand(sql, con)
            Dim dt As New DataTable()
            Dim ad As New SqlDataAdapter(cmd)
            ad.Fill(dt)
            If dt.Rows.Count > 0 Then
                lbldocnum.Text = dt.Rows(0)("docnum").ToString() + 1
                Session("DOCNO") = lbldocnum.Text
            End If
            rfidmap.DataSource = Nothing
            rfidmap.DataBind()
        End If
    End Sub

    Private Sub btnexist_Click(sender As Object, e As EventArgs) Handles btnexist.Click
        If Label1.Text = "-Select Location-" Then
            lblerror.Visible = True
            lblerror.Text = "Kindly Select Location"
        Else
            lblerror.Visible = False
            txtpack.Visible = True
            txtpack.Focus()
            lbldocnum.Visible = False
        End If
    End Sub

    Private Sub txtpack_TextChanged(sender As Object, e As EventArgs) Handles txtpack.TextChanged
        Dim sql1 As String = "Select * from subgrnpacking where DOCNUM ='" & txtpack.Text & "' and device = '" & cmbdevice.SelectedItem.Text & "' ORDER BY DOCENTRY"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            rfidmap.DataSource = dt2
            ViewState("Data") = dt2
            rfidmap.DataBind()
        End If
        lbldocnum.Visible = False
        txtqrcode.Text = ""
        txtqrcode.Focus()
        txttagrfid.Text = ""
    End Sub
End Class