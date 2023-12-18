Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports ZXing

Public Class thaansticker
    Inherits System.Web.UI.Page
    Dim grndate, dcdate, autonumdate As DateTime
    Dim lin, n, k As Integer
    Dim dir, mdir, printer As String
    Dim builder As New StringBuilder
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Dim docnumber, rowcount, docentry As Integer
    Dim seqnumber, autonumber, selectedautonumber, color, itemcode, thaanbatchno, stickerqrcode, stickercategory As String
    Dim grnquantity, grnmeters, gridgrnqty, gridgrnmtrs, grnlength, grnwidth, totalquantity, totalmeters, totalaqty, totalbqty, totalcqty, totaldqty, totalamtrs, totalbmtrs, totalcmtrs, totaldmtrs, tapelength, standardtapelength, thaanqtytotal, thaanmtrstotal, weight, length, width, reedspace, pick, warpcons, weftcons As Decimal
    Dim aquantity, bquantity, cquantity, totalthaanqty, tolerance, grandtotalthaanqty, setno, beamno, loomno As Integer
    Dim ameters, bmeters, cmeters, dmeters, dquantity, acceptmtrsa, acceptmtrsb, acceptmtrsc, acceptmtrsd, totalthaanmtrs, grandtotalthaanmtrs, errorquantity As Decimal
    Dim grndocentry, errorcategory, errortype, errordesc As String
    Dim dt2 As DataTable

    Private Sub beamwise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call dbMAIN()
        txtgrn.Focus()

    End Sub
    Public Sub getgrndetails()
        con.Open()
        Dim sql As String = "Select max(docentry) from  [@INT_OPDN] where docnum ='" & txtgrn.Text & "' and U_Process ='W2'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            grndocentry = dt.Rows(0)(0).ToString()
        End If
        Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,B.u_reedspace,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum ='" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND U_Quantity <> 0"
        'Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_ItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND  A.DOCnum  NOT IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNINSPECTION)"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then

            grndate = dt1.Rows(0)("createdate").ToString()
            lblgrndate.Text = grndate.ToString("dd/MM/yyyy")
            lblmsg.Text = ""
            loadgrid()
            btnclear.Visible = True
        Else
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Enter Correct GRN No."
            txtgrn.Text = ""
            txtgrn.Focus()

        End If
        con.Close()
    End Sub



    Private Sub txtgrn_TextChanged(sender As Object, e As EventArgs) Handles txtgrn.TextChanged
        btnsave.Focus()
        btnsave_Click(sender, e)
    End Sub

    Public Sub loadgrid()

        Dim sql1 As String = "select * from  SUBGRNINSPECTION where subgrnno = '" & txtgrn.Text & "' order by autonum"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.Visible = True
            GridView1.DataSource = dt2
            GridView1.DataBind()
            GridView1.FooterRow.Visible = False
            btnsave.Visible = True
        End If


    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        getgrndetails()
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        GridView1.DataSource = Nothing
        GridView1.DataBind()
        txtgrn.Text = ""
        lblgrndate.Text = ""
        txtgrn.Focus()
    End Sub
    Protected Sub CheckBoxA_Changed(sender As Object, e As EventArgs)
        btncalculate.Visible = True
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowA.Checked = True Then
                    chkRowB.Checked = False
                    chkRowC.Checked = False
                    chkRowAB.Checked = False
                    chkRowD.Checked = False
                    chkRowA.BackColor = System.Drawing.Color.Aquamarine
                    chkRowB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowAB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowC.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowD.BackColor = System.Drawing.Color.LightGoldenrodYellow
                End If
            End If
        Next
    End Sub
    Protected Sub CheckBoxB_Changed(sender As Object, e As EventArgs)
        btncalculate.Visible = True
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowB.Checked = True Then
                    chkRowA.Checked = False
                    chkRowC.Checked = False
                    chkRowAB.Checked = False
                    chkRowD.Checked = False
                    chkRowB.BackColor = System.Drawing.Color.Aquamarine
                    chkRowA.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowAB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowC.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowD.BackColor = System.Drawing.Color.LightGoldenrodYellow
                End If
            End If
        Next
    End Sub
    Protected Sub CheckBoxAB_Changed(sender As Object, e As EventArgs)
        btncalculate.Visible = True
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowAB.Checked = True Then
                    chkRowA.Checked = False
                    chkRowB.Checked = False
                    chkRowC.Checked = False
                    chkRowD.Checked = False
                    chkRowAB.BackColor = System.Drawing.Color.Aquamarine
                    chkRowA.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowC.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowD.BackColor = System.Drawing.Color.LightGoldenrodYellow
                End If
            End If
        Next
    End Sub
    Protected Sub CheckBoxC_Changed(sender As Object, e As EventArgs)
        btncalculate.Visible = True
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowC.Checked = True Then
                    chkRowA.Checked = False
                    chkRowB.Checked = False
                    chkRowAB.Checked = False
                    chkRowD.Checked = False
                    chkRowC.BackColor = System.Drawing.Color.Aquamarine
                    chkRowA.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowAB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowD.BackColor = System.Drawing.Color.LightGoldenrodYellow
                End If
            End If
        Next
    End Sub
    Protected Sub CheckBoxD_Changed(sender As Object, e As EventArgs)
        btncalculate.Visible = True
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)

                If chkRowD.Checked = True Then
                    chkRowA.Checked = False
                    chkRowB.Checked = False
                    chkRowAB.Checked = False
                    chkRowC.Checked = False
                    chkRowD.BackColor = System.Drawing.Color.Aquamarine
                    chkRowA.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowAB.BackColor = System.Drawing.Color.LightGoldenrodYellow
                    chkRowC.BackColor = System.Drawing.Color.LightGoldenrodYellow
                End If
            End If
        Next
    End Sub


    Protected Sub RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)")
            e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)")
        End If
    End Sub

    Private Sub btncalculate_Click(sender As Object, e As EventArgs) Handles btncalculate.Click
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                length = row.Cells(2).Text
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                ameters = TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text
                bmeters = TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text
                cmeters = TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text
                dmeters = TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowA.Checked = True Then
                    ameters = Val(chkRowA.Text) * length
                    TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text = ameters.ToString("F2")
                    row.Cells(9).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text = 0.00
                    TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text = 0.00
                    TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text = 0.00
                    row.Cells(10).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(11).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(12).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(8).Text = Val(chkRowA.Text)
                    row.Cells(13).Text = ameters.ToString
                    stickercategory = GridView1.HeaderRow.Cells(3).Text
                    row.Cells(22).Text = stickercategory
                End If

                If chkRowB.Checked = True Then
                    bmeters = Val(chkRowB.Text) * length
                    bquantity = Val(chkRowB.Text)
                    TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text = bmeters.ToString("F2")
                    row.Cells(10).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text = 0.00
                    TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text = 0.00
                    TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text = 0.00
                    row.Cells(9).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(11).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(12).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(8).Text = Val(chkRowB.Text)
                    row.Cells(13).Text = bmeters.ToString
                    stickercategory = GridView1.HeaderRow.Cells(4).Text
                    row.Cells(22).Text = stickercategory
                End If

                If chkRowAB.Checked = True Then
                    ameters = Val(chkRowA.Text) * length
                    TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text = ameters.ToString("F2")
                    row.Cells(9).BackColor = System.Drawing.Color.Aquamarine
                    bmeters = Val(chkRowB.Text) * length
                    aquantity = Val(chkRowA.Text)
                    bquantity = Val(chkRowB.Text)
                    TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text = bmeters.ToString("F2")
                    row.Cells(10).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text = 0.00
                    TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text = 0.00
                    row.Cells(11).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(12).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(8).Text = aquantity + bquantity
                    row.Cells(13).Text = Val(ameters.ToString) + Val(bmeters.ToString)
                    row.Cells(8).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(13).BackColor = System.Drawing.Color.Aquamarine
                    stickercategory = GridView1.HeaderRow.Cells(5).Text
                    row.Cells(22).Text = stickercategory
                End If


                If chkRowC.Checked = True Then
                    cmeters = Val(chkRowC.Text) * length
                    cquantity = Val(chkRowC.Text)
                    TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text = cmeters.ToString("F2")
                    row.Cells(11).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text = 0.00
                    TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text = 0.00
                    TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text = 0.00
                    row.Cells(9).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(10).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(12).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(8).Text = Val(chkRowC.Text)
                    row.Cells(13).Text = cmeters.ToString
                    stickercategory = GridView1.HeaderRow.Cells(6).Text
                    row.Cells(22).Text = stickercategory
                End If

                If chkRowD.Checked = True Then
                    dquantity = Val(chkRowD.Text)
                    dmeters = Val(chkRowD.Text)
                    TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text = dmeters.ToString("F2")
                    row.Cells(12).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text = 0.00
                    TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text = 0.00
                    TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text = 0.00
                    row.Cells(9).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(10).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(11).BackColor = System.Drawing.Color.LightGoldenrodYellow
                    row.Cells(8).Text = Val(chkRowD.Text)
                    row.Cells(13).Text = dmeters.ToString
                    stickercategory = GridView1.HeaderRow.Cells(7).Text
                    row.Cells(22).Text = stickercategory
                End If


            End If

        Next
        btnprint.Visible = True
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Dim dir, mdir As String
        dir = System.AppDomain.CurrentDomain.BaseDirectory()
        mdir = Trim(dir) & "Qrbarcode.txt"
        FileOpen(1, mdir, OpenMode.Output)
        lin = 0


        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                length = row.Cells(2).Text
                Dim chkRowA As CheckBox = TryCast(row.Cells(3).FindControl("CheckBoxA"), CheckBox)
                Dim chkRowB As CheckBox = TryCast(row.Cells(4).FindControl("CheckBoxB"), CheckBox)
                Dim chkRowAB As CheckBox = TryCast(row.Cells(5).FindControl("CheckBoxAB"), CheckBox)
                Dim chkRowC As CheckBox = TryCast(row.Cells(6).FindControl("CheckBoxC"), CheckBox)
                Dim chkRowD As CheckBox = TryCast(row.Cells(7).FindControl("CheckBoxD"), CheckBox)
                ameters = TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text
                bmeters = TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text
                cmeters = TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text
                dmeters = TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text
                weight = TryCast(row.Cells(14).FindControl("txtweight"), TextBox).Text
                stickercategory = TryCast(row.Cells(22).FindControl("lblcategory"), Label).Text
                If chkRowA.Checked = True Then
                    row.Cells(22).Text = GridView1.HeaderRow.Cells(3).Text
                    stickerqrcode = String.Concat(GridView1.HeaderRow.Cells(3).Text & "|" & row.Cells(1).Text & "|" & row.Cells(17).Text & "|" & row.Cells(18).Text & "|" & row.Cells(19).Text & "|" & row.Cells(20).Text)
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LL304")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MD0")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MNY")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(1).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN     :" & "   " & row.Cells(17).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & row.Cells(18).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & row.Cells(19).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.    :" & "   " & row.Cells(20).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FDA Qty    :" & "   " & chkRowA.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDA Mtrs. :" & "   " & TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDWeight  :" & "   " & weight & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,240^A0N,20,25^CI13^FR^FD" & row.Cells(15).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,60^A0N,25,30^CI13^FR^FDWidth   :" & "   " & row.Cells(21).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,280^A0N,25,30^CI13^FR^FD" & row.Cells(22).Text & "   " & "Only^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
                    lin = lin + 1


                ElseIf chkRowB.Checked = True Then
                    stickercategory = GridView1.HeaderRow.Cells(4).Text
                    row.Cells(22).Text = stickercategory
                    stickerqrcode = String.Concat(row.Cells(22).Text & "|" & row.Cells(1).Text & "|" & row.Cells(17).Text & "|" & row.Cells(18).Text & "|" & row.Cells(19).Text & "|" & row.Cells(20).Text & "|" & chkRowB.Text & "|" & TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text & "|" & weight & "|" & row.Cells(15).Text)
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LL304")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MD0")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MNY")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(1).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN     :" & "   " & row.Cells(17).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & row.Cells(18).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & row.Cells(19).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.    :" & "   " & row.Cells(20).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FDB Qty    :" & "   " & chkRowB.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDB Mtrs. :" & "   " & TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDWeight  :" & "   " & weight & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,240^A0N,20,25^CI13^FR^FD" & row.Cells(15).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,60^A0N,25,30^CI13^FR^FDWidth   :" & "   " & row.Cells(21).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,270^A0N,25,30^CI13^FR^FD" & row.Cells(22).Text & "   " & "Only^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
                    lin = lin + 1

                ElseIf chkRowAB.Checked = True Then
                    stickercategory = GridView1.HeaderRow.Cells(5).Text
                    row.Cells(22).Text = stickercategory
                    stickerqrcode = String.Concat(row.Cells(22).Text & "|" & row.Cells(1).Text & "|" & row.Cells(17).Text & "|" & row.Cells(18).Text & "|" & row.Cells(19).Text & "|" & row.Cells(20).Text & "|" & chkRowA.Text & "|" & chkRowB.Text & "|" & weight & "|" & row.Cells(15).Text)
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LL304")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MD0")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MNY")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(1).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN     :" & "   " & row.Cells(17).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & row.Cells(18).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & row.Cells(19).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.    :" & "   " & row.Cells(20).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FDA Qty    :" & "   " & chkRowA.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDA Mtrs. :" & "   " & TryCast(row.Cells(9).FindControl("txtthaanmtrsa"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDB Qty    :" & "   " & chkRowB.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,240^A0N,25,30^CI13^FR^FDB Mtrs. :" & "   " & TryCast(row.Cells(10).FindControl("txtthaanmtrsb"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,60^A0N,25,30^CI13^FR^FDWidth :" & "   " & row.Cells(21).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,270^A0N,25,30^CI13^FR^FDWeight   :" & "   " & weight & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,300^A0N,25,30^CI13^FR^FDTotal Qty. :" & "   " & row.Cells(8).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,330^A0N,25,30^CI13^FR^FDTotal Mtrs.:" & "   " & row.Cells(13).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,90^A0N,20,25^CI13^FR^FD" & row.Cells(15).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
                    lin = lin + 1


                ElseIf chkRowC.Checked = True Then
                    stickercategory = GridView1.HeaderRow.Cells(6).Text
                    row.Cells(22).Text = stickercategory
                    stickerqrcode = String.Concat(row.Cells(22).Text & "|" & row.Cells(1).Text & "|" & row.Cells(17).Text & "|" & row.Cells(18).Text & "|" & row.Cells(19).Text & "|" & row.Cells(20).Text & "|" & chkRowC.Text & "|" & TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text & "|" & weight & "|" & row.Cells(15).Text)
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LL304")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MD0")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MNY")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(1).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN     :" & "   " & row.Cells(17).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & row.Cells(18).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & row.Cells(19).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.    :" & "   " & row.Cells(20).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FDC Qty    :" & "   " & chkRowC.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDC Mtrs. :" & "   " & TryCast(row.Cells(11).FindControl("txtthaanmtrsc"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDWeight  :" & "   " & weight & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,240^A0N,20,25^CI13^FR^FD" & row.Cells(15).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,60^A0N,25,30^CI13^FR^FDWidth   :" & "   " & row.Cells(21).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,270^A0N,25,30^CI13^FR^FD" & row.Cells(22).Text & "   " & "Only^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
                    lin = lin + 1

                ElseIf chkRowD.Checked = True Then
                    stickercategory = GridView1.HeaderRow.Cells(7).Text
                    row.Cells(22).Text = stickercategory

                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LL304")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MD0")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^MNY")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^LH0,0^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^XA")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(1).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN     :" & "   " & row.Cells(17).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & row.Cells(18).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & row.Cells(19).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.    :" & "   " & row.Cells(20).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FDD Qty    :" & "   " & chkRowD.Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDD Mtrs. :" & "   " & TryCast(row.Cells(12).FindControl("txtthaanmtrsd"), Label).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDWeight  :" & "   " & weight & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,240^A0N,20,25^CI13^FR^FD" & row.Cells(15).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO450,60^A0N,25,30^CI13^FR^FDWidth   :" & "   " & row.Cells(21).Text & "^FS")
                    lin = lin + 1
                    PrintLine(1, TAB(0), "^FO120,270^A0N,25,30^CI13^FR^FD" & row.Cells(22).Text & "   " & "Only^FS")
                    lin = lin + 1
                    stickerqrcode = String.Concat(GridView1.HeaderRow.Cells(7).Text & "|" & row.Cells(1).Text & "|" & row.Cells(17).Text & "|" & row.Cells(18).Text & "|" & row.Cells(19).Text & "|" & row.Cells(20).Text & "|" & chkRowD.Text)
                    PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
                    lin = lin + 1
                End If
            End If
        Next
        FileClose(1)
        mdir = Trim(dir) & "Qrbarcode.txt"
        Using Reader As StreamReader = New StreamReader(mdir)
            builder = New StringBuilder()
            builder.Append(Reader.ReadToEnd())
            Dim settings As New PrinterSettings()
            BarcodePrint.SendStringToPrinter(settings.PrinterName, builder.ToString)
        End Using
    End Sub
End Class
