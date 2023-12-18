Imports System.Net
Imports System.Data.SqlClient
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text.RegularExpressions
Public Class THAANPACKING
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

    Protected Sub btnget_Click(sender As Object, e As EventArgs) Handles btnget.Click
        Dim sql As String = "select distinct THRONEWIDTH  from subgrninspection where subgrnno = '" & txtgrn.Text & "'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            cmblength.DataSource = dt
            cmblength.DataTextField = "THRONEWIDTH"
            cmblength.DataValueField = "THRONEWIDTH"
            cmblength.DataBind()
        End If
    End Sub

    Private Sub rbaqty_CheckedChanged(sender As Object, e As EventArgs) Handles rbaqty.CheckedChanged
        CLEAR()
        Dim sql As String = "SELECT autonum as [Thaan Number],aqty as [A Qty], AMTRS as [A Mtrs],bqty as [B Qty], bMTRS as [B Mtrs],thronecolor as [Color],THRONEWIDTH as [Thaan Width],loomno as [Loom No],suppdc as [Supp. DC],convert(varchar,suppdate,105) as [Supp. Date],subgrnno as [GRN No],vencode as [Vendor Code],batchno as [Batch No],itemcode as [Item Code],length as [Length],Weight,venname as [Vendor Name] FROM subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "'  AND AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNPACKING) ORDER BY DOCENTRY"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
            btnsave.Visible = True
            Dim sql1 As String = "select distinct thronecolor  from subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' ORDER BY thronecolor"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                cmbcolor.DataSource = dt1
                cmbcolor.DataTextField = "thronecolor"
                cmbcolor.DataValueField = "thronecolor"
                cmbcolor.DataBind()
                btnsave.Visible = True
            End If
        End If
    End Sub



    Private Sub rbcqty_CheckedChanged(sender As Object, e As EventArgs) Handles rbcqty.CheckedChanged
        CLEAR()
        Dim sql As String = "SELECT autonum as [Thaan Number],aqty as [A Qty], AMTRS as [A Mtrs],bqty as [B Qty], bMTRS as [B Mtrs],cqty as [C Qty], cMTRS as [C Mtrs],thronecolor as [Color],THRONEWIDTH as [Thaan Width],loomno as [Loom No],suppdc as [Supp. DC],convert(varchar,suppdate,105) as [Supp. Date],subgrnno as [GRN No],vencode as [Vendor Code],batchno as [Batch No],itemcode as [Item Code],length as [Length],Weight,venname as [Vendor Name] FROM subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' AND AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNPACKING) ORDER BY DOCENTRY"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
            btnsave.Visible = True
            Dim sql1 As String = "select distinct thronecolor  from subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' ORDER BY thronecolor"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                cmbcolor.DataSource = dt1
                cmbcolor.DataTextField = "thronecolor"
                cmbcolor.DataValueField = "thronecolor"
                cmbcolor.DataBind()
                btnsave.Visible = True
            End If
        End If
    End Sub

    Private Sub rbdqty_CheckedChanged(sender As Object, e As EventArgs) Handles rbdqty.CheckedChanged
        Dim sql As String = "SELECT autonum as [Thaan Number],bitqty as [Bit Qty], BITMTRS as [Bit Mtrs],thronecolor as [Color],THRONEWIDTH as [Thaan Width],loomno as [Loom No],suppdc as [Supp. DC],convert(varchar,suppdate,105) as [Supp. Date],subgrnno as [GRN No],vencode as [Vendor Code],batchno as [Batch No],itemcode as [Item Code],length as [Length],Weight FROM subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' and bitqty <> 0.00 ORDER BY DOCENTRY"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
            btnsave.Visible = True
            btnsave.Visible = True
            Dim sql1 As String = "select distinct thronecolor  from subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' ORDER BY thronecolor"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt1 As New DataTable()
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt1)
            If dt1.Rows.Count > 0 Then
                cmbcolor.DataSource = dt1
                cmbcolor.DataTextField = "thronecolor"
                cmbcolor.DataValueField = "thronecolor"
                cmbcolor.DataBind()
                btnsave.Visible = True
            End If
        End If
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
    Public Sub updatedocnum()
        con.Open()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked = True Then
                    Dim cmd1 As New SqlCommand("update subgrnpacking set docnum = '" & lbldocnum.Text & "' WHERE subgrnno=@subgrnno and autonum =@autonum", con)
                    cmd1.Parameters.AddWithValue("SUBGRNNO", txtgrn.Text)
                    cmd1.Parameters.AddWithValue("AUTONUM", row.Cells(2).Text.ToString)
                    cmd1.ExecuteNonQuery()
                End If
            End If
        Next
        con.Close()
    End Sub
    Protected Sub Headercheckbox_changed(sender As Object, e As EventArgs)
        Dim count As Integer
        Dim totalqty, totmtrs, totweight As Decimal
        If rbaqty.Checked = True Or rbdqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                Dim chkHead As CheckBox = CType(GridView1.HeaderRow.FindControl("checkAll"), CheckBox)
                If chkHead.Checked = True Then
                    lbl2.Visible = True
                    lbltotthaans.Visible = True
                    lbl3.Visible = True
                    lbltotqty.Visible = True
                    lbl4.Visible = True
                    lbltotmtrs.Visible = True
                    lbl5.Visible = True
                    lbltotweight.Visible = True
                    count += 1
                    totalqty += Val(row.Cells(3).Text) + Val(row.Cells(5).Text)
                    totmtrs += Val(row.Cells(4).Text) + Val(row.Cells(6).Text)
                    totweight += row.Cells(17).Text
                Else
                    count = 0
                    totalqty = 0
                    totweight = 0
                End If
            Next
            lbltotthaans.Text = count
            lbltotqty.Text = totalqty.ToString("F2")
            lbltotmtrs.Text = totmtrs.ToString("F2")
            lbltotweight.Text = totweight.ToString("F2")
        ElseIf rbcqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        lbl2.Visible = True
                        lbltotthaans.Visible = True
                        lbl3.Visible = True
                        lbltotqty.Visible = True
                        lbl4.Visible = True
                        lbltotmtrs.Visible = True
                        lbl5.Visible = True
                        lbltotweight.Visible = True
                        count += 1
                        totalqty += Val(row.Cells(3).Text) + Val(row.Cells(5).Text) + Val(row.Cells(7).Text)
                        totmtrs += Val(row.Cells(4).Text) + Val(row.Cells(6).Text) + Val(row.Cells(8).Text)
                        totweight += row.Cells(19).Text
                    End If
                End If
            Next
            lbltotthaans.Text = count
            lbltotqty.Text = totalqty.ToString("F2")
            lbltotmtrs.Text = totmtrs.ToString("F2")
            lbltotweight.Text = totweight.ToString("F2")
        End If
    End Sub
    Protected Sub CheckBox_Changed(sender As Object, e As EventArgs)
        Dim count As Integer
        Dim totalqty, totmtrs, totweight As Decimal
        If rbaqty.Checked = True Or rbdqty.Checked = True Then

            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        lbl2.Visible = True
                        lbltotthaans.Visible = True
                        lbl3.Visible = True
                        lbltotqty.Visible = True
                        lbl4.Visible = True
                        lbltotmtrs.Visible = True
                        lbl5.Visible = True
                        lbltotweight.Visible = True
                        count += 1
                        totalqty += Val(row.Cells(3).Text) + Val(row.Cells(5).Text)
                        totmtrs += Val(row.Cells(4).Text) + Val(row.Cells(6).Text)
                        totweight += row.Cells(17).Text
                    End If
                End If
            Next
            lbltotthaans.Text = count
            lbltotqty.Text = totalqty.ToString("F2")
            lbltotmtrs.Text = totmtrs.ToString("F2")
            lbltotweight.Text = totweight.ToString("F2")
        ElseIf rbcqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        lbl2.Visible = True
                        lbltotthaans.Visible = True
                        lbl3.Visible = True
                        lbltotqty.Visible = True
                        lbl4.Visible = True
                        lbltotmtrs.Visible = True
                        lbl5.Visible = True
                        lbltotweight.Visible = True
                        count += 1
                        totalqty += Val(row.Cells(3).Text) + Val(row.Cells(5).Text) + Val(row.Cells(7).Text)
                        totmtrs += Val(row.Cells(4).Text) + Val(row.Cells(6).Text) + Val(row.Cells(8).Text)
                        totweight += row.Cells(19).Text
                    End If
                End If
            Next
            lbltotthaans.Text = count
            lbltotqty.Text = totalqty.ToString("F2")
            lbltotmtrs.Text = totmtrs.ToString("F2")
            lbltotweight.Text = totweight.ToString("F2")
        End If

    End Sub
    Public Sub CLEAR()
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        lbltotthaans.Text = ""
        lbltotqty.Text = ""
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        lbltotmtrs.Text = ""
        lbltotweight.Text = ""
    End Sub
    Public Sub savedata()
        generateslipno()
        If rbaqty.Checked = True Then
            insertaqty()
        ElseIf rbcqty.Checked = True Then
            insertcqty()
        ElseIf rbdqty.Checked = True Then
            insertbitqty()
        End If
        updatedocnum()
        CLEAR()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        savedata()
        Session("DOCNO") = lbldocnum.Text
        lblerror.ForeColor = System.Drawing.Color.Green
        lblerror.BackColor = System.Drawing.Color.White
        lblerror.Text = "SELECTED THAANS ADDED TO PACKING SLIP"
        btnfinish.Visible = True
    End Sub

    Private Sub btnfinish_Click(sender As Object, e As EventArgs) Handles btnfinish.Click
        Response.Redirect("packingprint.aspx")
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
    Public Sub insertaqty()
        con.Open()
        If rbaqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([SUBGRNNO],[SUPPDC],[SUPPDATE],[VENCODE],[ITEMCODE],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[AQTY],[BQTY],[TOTALQTY],[TOTALPCS],[TOTALKGS],[Weight],[USER],[FIELD1])  VALUES (@SUBGRNNO,@SUPPDC,@SUPPDATE,@VENCODE,@ITEMCODE,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@AQTY,@BQTY,@TOTALQTY,@TOTALPCS,@TOTALKGS,@Weight,@USER,@FIELD1)", con)
                        cmd.Parameters.AddWithValue("SUBGRNNO", Convert.ToInt32(row.Cells(12).Text))
                        cmd.Parameters.AddWithValue("SUPPDC", row.Cells(10).Text)
                        loadlength()
                        cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                        cmd.Parameters.AddWithValue("VENCODE", row.Cells(13).Text)
                        cmd.Parameters.AddWithValue("ITEMCODE", row.Cells(15).Text)
                        cmd.Parameters.AddWithValue("LENGTH", row.Cells(16).Text)
                        cmd.Parameters.AddWithValue("WIDTH", row.Cells(8).Text)
                        cmd.Parameters.AddWithValue("BATCHNO", row.Cells(14).Text)
                        cmd.Parameters.AddWithValue("AUTONUM", row.Cells(2).Text)
                        AQTY = row.Cells(3).Text
                        BQTY = row.Cells(3).Text
                        AMTRS = row.Cells(4).Text
                        BMTRS = row.Cells(6).Text
                        cmd.Parameters.AddWithValue("THORNEQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("THORNEMTRS", row.Cells(5).Text)
                        cmd.Parameters.AddWithValue("THRONECOLOR", row.Cells(7).Text)
                        cmd.Parameters.AddWithValue("THRONESIZE", row.Cells(16).Text)
                        cmd.Parameters.AddWithValue("THRONEWIDTH", row.Cells(8).Text)
                        cmd.Parameters.AddWithValue("AQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("BQTY", row.Cells(5).Text)
                        cmd.Parameters.AddWithValue("TOTALQTY", lbltotqty.Text)
                        cmd.Parameters.AddWithValue("TOTALPCS", lbltotthaans.Text)
                        cmd.Parameters.AddWithValue("TOTALKGS", lbltotweight.Text)
                        cmd.Parameters.AddWithValue("USER", lblusername.Text.ToUpper)
                        cmd.Parameters.AddWithValue("Weight", row.Cells(17).Text)
                        cmd.Parameters.AddWithValue("FIELD1", row.Cells(18).Text)
                        cmd.ExecuteNonQuery()
                        Session("CATEGORY") = rbaqty.Text 
                    End If
                End If
            Next
        End If
        con.Close()

    End Sub

    Public Sub insertcqty()
        con.Open()
        If rbcqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([SUBGRNNO],[SUPPDC],[SUPPDATE],[VENCODE],[ITEMCODE],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[AQTY],[BQTY],[CQTY],[TOTALQTY],[TOTALPCS],[TOTALKGS],[Weight],[USER],[FIELD1])  VALUES (@SUBGRNNO,@SUPPDC,@SUPPDATE,@VENCODE,@ITEMCODE,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@AQTY,@BQTY,@CQTY,@TOTALQTY,@TOTALPCS,@TOTALKGS,@Weight,@USER,@FIELD1)", con)
                        cmd.Parameters.AddWithValue("SUBGRNNO", Convert.ToInt32(row.Cells(12).Text))
                        cmd.Parameters.AddWithValue("SUPPDC", row.Cells(10).Text)
                        loadlength()
                        cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                        cmd.Parameters.AddWithValue("VENCODE", row.Cells(13).Text)
                        cmd.Parameters.AddWithValue("ITEMCODE", row.Cells(15).Text)
                        cmd.Parameters.AddWithValue("LENGTH", row.Cells(16).Text)
                        cmd.Parameters.AddWithValue("WIDTH", row.Cells(8).Text)
                        cmd.Parameters.AddWithValue("BATCHNO", row.Cells(14).Text)
                        cmd.Parameters.AddWithValue("AUTONUM", row.Cells(2).Text)
                        AQTY = row.Cells(3).Text
                        BQTY = row.Cells(3).Text
                        AMTRS = row.Cells(4).Text
                        BMTRS = row.Cells(6).Text
                        cmd.Parameters.AddWithValue("THORNEQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("THORNEMTRS", row.Cells(5).Text)
                        cmd.Parameters.AddWithValue("THRONECOLOR", row.Cells(7).Text)
                        cmd.Parameters.AddWithValue("THRONESIZE", row.Cells(16).Text)
                        cmd.Parameters.AddWithValue("THRONEWIDTH", row.Cells(8).Text)
                        cmd.Parameters.AddWithValue("AQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("BQTY", row.Cells(5).Text)
                        cmd.Parameters.AddWithValue("CQTY", row.Cells(7).Text)
                        cmd.Parameters.AddWithValue("TOTALQTY", lbltotqty.Text)
                        cmd.Parameters.AddWithValue("TOTALPCS", lbltotthaans.Text)
                        cmd.Parameters.AddWithValue("TOTALKGS", lbltotweight.Text)
                        cmd.Parameters.AddWithValue("USER", lblusername.Text.ToUpper)
                        cmd.Parameters.AddWithValue("Weight", row.Cells(17).Text)
                        cmd.Parameters.AddWithValue("FIELD1", row.Cells(18).Text)
                        cmd.ExecuteNonQuery()
                        Session("CATEGORY") = rbcqty.Text
                    End If
                End If
            Next
        End If
        con.Close()
    End Sub
    Public Sub insertbitqty()
        con.Open()
        If rbdqty.Checked = True Then
            For Each row As GridViewRow In GridView1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                    If chkRow.Checked = True Then
                        Dim cmd As New SqlCommand("INSERT INTO SUBGRNPACKING([SUBGRNNO],[SUPPDC],[SUPPDATE],[VENCODE],[ITEMCODE],[LENGTH],[WIDTH],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[BITQTY],[TOTALQTY],[TOTALPCS],[TOTALKGS],[Weight],[USER])  VALUES (@SUBGRNNO,@SUPPDC,@SUPPDATE,@VENCODE,@ITEMCODE,@LENGTH,@WIDTH,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@BITQTY,@TOTALQTY,@TOTALPCS,@TOTALKGS,@Weight,@USER)", con)
                        cmd.Parameters.AddWithValue("SUBGRNNO", Convert.ToInt32(row.Cells(10).Text))
                        cmd.Parameters.AddWithValue("SUPPDC", row.Cells(8).Text)
                        dcdate = row.Cells(9).Text
                        cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                        cmd.Parameters.AddWithValue("VENCODE", row.Cells(11).Text)
                        cmd.Parameters.AddWithValue("ITEMCODE", row.Cells(13).Text)
                        cmd.Parameters.AddWithValue("LENGTH", row.Cells(14).Text)
                        cmd.Parameters.AddWithValue("WIDTH", row.Cells(6).Text)
                        cmd.Parameters.AddWithValue("BATCHNO", row.Cells(12).Text)
                        cmd.Parameters.AddWithValue("AUTONUM", row.Cells(2).Text)
                        cmd.Parameters.AddWithValue("THORNEQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("THORNEMTRS", row.Cells(4).Text)
                        cmd.Parameters.AddWithValue("THRONECOLOR", row.Cells(5).Text)
                        cmd.Parameters.AddWithValue("THRONESIZE", row.Cells(14).Text)
                        cmd.Parameters.AddWithValue("THRONEWIDTH", row.Cells(6).Text)
                        cmd.Parameters.AddWithValue("BITQTY", row.Cells(3).Text)
                        cmd.Parameters.AddWithValue("TOTALQTY", lbltotqty.Text)
                        cmd.Parameters.AddWithValue("TOTALPCS", lbltotthaans.Text)
                        cmd.Parameters.AddWithValue("TOTALKGS", lbltotthaans.Text)
                        cmd.Parameters.AddWithValue("USER", lblusername.Text.ToUpper)
                        cmd.Parameters.AddWithValue("Weight", row.Cells(15).Text)
                        cmd.ExecuteNonQuery()
                        Session("CATEGORY") = rbdqty.Text
                    End If
                End If
            Next
        End If
        con.Close()
    End Sub


    Private Sub cmbcolor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcolor.SelectedIndexChanged
        Dim sql As String = "SELECT autonum as [Thaan Number],aqty as [A Qty], AMTRS as [A Mtrs],bqty as [B Qty], bMTRS as [B Mtrs],thronecolor as [Color],THRONEWIDTH as [Thaan Width],loomno as [Loom No],suppdc as [Supp. DC],suppdate as [Supp. Date],subgrnno as [GRN No],vencode as [Vendor Code],batchno as [Batch No],itemcode as [Item Code],length as [Length],Weight,venname as [Vendor Name]FROM subgrninspection where subgrnno = '" & txtgrn.Text & "' and autonum <> '' and THRONEWIDTH = '" & cmblength.Text & "' and thronecolor = '" & cmbcolor.Text & "' ORDER BY DOCENTRY"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub
End Class