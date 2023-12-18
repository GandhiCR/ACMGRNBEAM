Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Drawing.Printing
Imports System.IO
Imports Neodynamic.SDK
Imports Neodynamic.SDK.Web
Imports System.Text.RegularExpressions
Public Class beamwise
    Inherits System.Web.UI.Page
    Dim grndate, dcdate, autonumdate As DateTime
    Dim lin, n, k As Integer
    Dim dir, mdir, printer, location As String
    Dim builder As New StringBuilder
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Dim docnumber, rowcount, totallooms As Integer
    Dim seqnumber, autonumber, selectedautonumber As String
    Dim grnquantity, grnmeters, grnlength, grnwidth, totalquantity, totalmeters, reedspace, warpcons, weftcons, pick As Decimal
    Dim grndocentry As String
    Dim dt2 As DataTable

    Private Sub beamwise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call dbMAIN()
        txtgrn.Focus()

    End Sub
    Public Sub getgrndetails()
        con.Open()
        Dim sql As String = "select max(docentry) from  [@INT_OPDN] where docnum ='" & txtgrn.Text & "' and U_Process ='W2'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            grndocentry = dt.Rows(0)(0).ToString()
        End If
        Dim sql1 As String = "select distinct A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,sum(B.U_Quantity)[U_Quantity],sum(B.U_Mtrs)[U_Mtrs],B.u_reedspace,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum ='" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND U_Quantity <> 0 group by  A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.u_reedspace,A.U_SUPPDCDt"
        'Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,B.u_reedspace,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' "
        'Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_ItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND  A.DOCnum  NOT IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNINSPECTION)"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            grndate = dt1.Rows(0)("createdate").ToString()
            lblgrndate.Text = grndate.ToString("dd/MM/yyyy")
            lblnewcode.Text = dt1.Rows(0)("U_Newitemcode").ToString()
            lblitemname.Text = dt1.Rows(0)("U_newitemname").ToString()
            lbldcno.Text = dt1.Rows(0)("u_suppdcno").ToString()
            dcdate = dt1.Rows(0)("u_SUPPDCdt").ToString()
            lbldcdate.Text = dcdate.ToString("dd/MM/yyyy")
            lblvencode.Text = dt1.Rows(0)("u_cardcode").ToString()
            lblvenname.Text = dt1.Rows(0)("u_cardname").ToString()
            lblusername.Text = dt1.Rows(0)("creator").ToString()
            lblbatchno.Text = dt1.Rows(0)("u_batchno").ToString()
            grnquantity = dt1.Rows(0)("u_quantity").ToString()
            reedspace = dt1.Rows(0)("u_reedspace").ToString()
            lblreedspace.Text = reedspace.ToString("F2")
            lblgrnqty.Text = grnquantity.ToString("F2")
            grnlength = dt1.Rows(0)("U_Length").ToString()
            lbllength.Text = grnlength.ToString("F2")
            grnmeters = dt1.Rows(0)("u_mtrs").ToString()
            lblgrnmeters.Text = grnmeters.ToString("F2")
            contenpanel.Visible = True
            btnclear.Visible = True
            lblmsg.Text = ""
            loadgrid()
            loadsizingcode()
            loadwidth()
            gottotallooms()
        Else
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Enter Correct GRN No."
            txtgrn.Text = ""
            txtgrn.Focus()
            grnclear()
        End If
        con.Close()
    End Sub
    Public Sub loadsizingcode()
        Dim sql1 As String = "SELECT u_itemcode,u_itemname from [@INT_PDN3] where  U_Formula = 'w1' and docentry='" & grndocentry & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblsizecode.Text = dt1.Rows(0)("u_itemcode").ToString()
            lblcategory.Text = dt1.Rows(0)("u_itemname").ToString()
        End If


    End Sub

    Public Sub loadwidth()
        Dim sql1 As String = "SELECT SWIDTH1,U_REEDSPACE,U_PICK,ISNULL(U_WARPCONS,0) as warpcons,isnUll(U_WEFTCONS,0) as weftcons,U_category,u_subcategory,u_commodityname FROM OITM WHERE ITEMCODE='" & lblnewcode.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            grnwidth = dt1.Rows(0)("swidth1").ToString()
            lblwidth.Text = (grnwidth / 2.54).ToString("F2")
            pick = dt1.Rows(0)("u_pick").ToString()
            lblpick.Text = pick.ToString("F2")
            warpcons = dt1.Rows(0)("WARPCONS").ToString()
            weftcons = dt1.Rows(0)("WEFTCONS").ToString()
            lblcategory.Text = dt1.Rows(0)("u_category").ToString().ToUpper
            lblsubcategory.Text = dt1.Rows(0)("u_subcategory").ToString().ToUpper
            lblequipment.Text = dt1.Rows(0)("U_CommodityName").ToString().ToUpper
        End If
    End Sub



    Public Sub grnclear()
        lblgrndate.Text = ""
        lblitemname.Text = ""
        lbldcno.Text = ""
        lbldcdate.Text = ""
        lblvencode.Text = ""
        txtgrn.Text = ""
        lblvenname.Text = ""
        lblusername.Text = ""
        lblrunlooms.Text = ""
        lblloomsno.Text = ""
        lblbatchno.Text = ""
        lblgrnqty.Text = ""
        lblnewcode.Text = ""
        empwagepanel.Visible = False
        GridView2.DataSource = Nothing
        GridView2.DataBind()
        lbllength.Text = ""
        lblsizecode.Text = ""
        lblequipment.Text = ""
        lblcategory.Text = ""
        lblsubcategory.Text = ""
        lbldocnum.Text = ""
        lblgrnmeters.Text = ""
        lblreedspace.Text = ""
        lblpick.Text = ""
        lblwidth.Text = ""
        contenpanel.Visible = False
        txtsetno.Focus()
        btnclear.Visible = False
        txtgrn.Focus()
    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        grnclear()
    End Sub

    Private Sub txtgrn_TextChanged(sender As Object, e As EventArgs) Handles txtgrn.TextChanged
        If System.Text.RegularExpressions.Regex.IsMatch(txtgrn.Text, "^[0-9]*$") Then
            getgrndetails()
        Else
            txtgrn.Text = ""
            txtgrn.Focus()
            ' lblmsg.Text = "Enter Correct GRN NO"
        End If
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If txtsetno.Text = "" And txtbeamno.Text = "" And txtloomno.Text = "" And txtthorneqty.Text = "" And cmbcolor.SelectedIndex = 0 Then
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Enter Values of All Fields"
        Else
            savewagedetails()
        End If
    End Sub

    'Protected Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
    '    Dim dir, mdir As String
    '    dir = Server.MapPath("~\Content\")
    '    mdir = Trim(dir) & "Qrbarcode.txt"
    '    FileOpen(1, mdir, OpenMode.Output)
    '    lin = 0


    '    For Each row As GridViewRow In GridView2.Rows
    '        If row.RowType = DataControlRowType.DataRow Then
    '            autonumber = row.Cells(2).Text
    '            Dim setnumber As String = row.Cells(3).Text
    '            Dim beamno As String = row.Cells(4).Text
    '            Dim loomno As String = row.Cells(5).Text
    '            Dim stickerqrcode As String
    '            Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), RadioButton)
    '            PrintLine(1, TAB(0), "^XA")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^LH0,0^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^LL304")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^MD0")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^MNY")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^LH0,0^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^XA")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,30^A0N,25,30^CI13^FR^FDThaan No    :" & "   " & row.Cells(2).Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO480,30^A0N,25,30^CI13^FR^FDGRN NO:" & txtgrn.Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,60^A0N,25,30^CI13^FR^FDVen. Code   :" & "   " & lblvencode.Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,90^A0N,25,30^CI13^FR^FDItem Code   :" & "   " & lblnewcode.Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,120^A0N,25,30^CI13^FR^FDBatch No.  :" & "   " & lblbatchno.Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,150^A0N,25,30^CI13^FR^FdGRN Qty    :" & "   " & row.Cells(6).Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,180^A0N,25,30^CI13^FR^FDGRN Mtrs.  :" & "   " & row.Cells(7).Text & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,210^A0N,25,30^CI13^FR^FDSet No.      :" & "   " & setnumber & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,240^A0N,25,30^CI13^FR^FDBeam No.   :" & "   " & beamno & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,270^A0N,25,30^CI13^FR^FDLoom No.   :" & "   " & loomno & "^FS")
    '            lin = lin + 1
    '            PrintLine(1, TAB(0), "^FO120,300^A0N,20,25^CI13^FR^FDColor  :" & row.Cells(8).Text & "^FS")
    '            lin = lin + 1
    '            stickerqrcode = String.Concat(row.Cells(2).Text & "|" & txtgrn.Text & "|" & lblvencode.Text & "|" & lblnewcode.Text & "|" & lblbatchno.Text)
    '            PrintLine(1, TAB(0), "^FO450,160^BQN,4,6^FD000" & stickerqrcode & "^FS^XZ")
    '            lin = lin + 1
    '        End If
    '    Next
    '    FileClose(1)
    '    mdir = Trim(dir) & "Qrbarcode.txt"
    '    Using Reader As StreamReader = New StreamReader(mdir)
    '        builder = New StringBuilder()
    '        builder.Append(Reader.ReadToEnd())
    '        Dim settings As New PrinterSettings()
    '        Dim printdoc As New System.Drawing.Printing.PrintDocument()
    '        printdoc.PrinterSettings = settings

    '        'BarcodePrint.SendStringToPrinter(settings.PrinterName, builder.ToString)
    '    End Using

    'End Sub

    Public Sub loadgrid()
        Dim sql1 As String = "SELECT isnull(docnum,0) as docnum,autonum,setno,loomno,beamno,thorneqty,thornemtrs,thronecolor FROM SUBGRNBEAM where subgrnno = '" & txtgrn.Text & "' ORDER BY DOCENTRY"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView2.DataSource = dt2
            GridView2.DataBind()
            empwagepanel.Visible = True
            btnupdate.Visible = False
            btndelete.Visible = False
            'btnprint.Visible = True
            docnumber = dt2.Rows(0)("docnum").ToString()
            lbldocnum.Text = docnumber
            Dim sql2 As String = "select count( DISTINCT loomno) as loomno  from subgrnbeam where subgrnno = '" & txtgrn.Text & "'"
            Dim cmd2 As New SqlCommand(sql2, con)
            Dim dt3 As New DataTable
            Dim ad2 As New SqlDataAdapter(cmd2)
            ad2.Fill(dt3)
            If dt3.Rows.Count > 0 Then
                lblrunlooms.Text = dt3.Rows(0)("loomno").ToString()
            End If
        Else
            Dim sql As String = "select isnull(max(docnum),0) as docnum from subgrnbeam"
            Dim cmd As New SqlCommand(sql, con)
            Dim dt As New DataTable
            Dim ad As New SqlDataAdapter(cmd)
            ad.Fill(dt)
            If dt.Rows.Count > 0 Then
                lbldocnum.Text = dt.Rows(0)("docnum").ToString() + 1
            End If
        End If
        gettotalquantity()
        gettotalmeters()
    End Sub

    Public Sub gettotalquantity()

        lbl1.Visible = True
        Dim sql1 As String = "select isnull(sum(thorneqty),0) as [totalquantity] from subgrnbeam where subgrnno = '" & txtgrn.Text & "' "
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            totalquantity = dt1.Rows(0)(0).ToString()
            lbltotqty.Text = totalquantity.ToString("F2")
        Else
            totalquantity = 0
        End If
    End Sub
    Public Sub gettotalmeters()

        lbl2.Visible = True
        Dim sql1 As String = "select isnull(sum(thornemtrs),0) as [totalquantity] from subgrnbeam where subgrnno = '" & txtgrn.Text & "' "
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            totalquantity = dt1.Rows(0)(0).ToString()
            lbltotmtrs.Text = totalquantity.ToString("F2")
        Else
            totalquantity = 0
        End If
    End Sub
    Public Sub gottotallooms()

        lbl2.Visible = True
        Dim sql1 As String = "SELECT t0.U_Looms as TotalLooms FROM [@INT_OSPL] t0 left join [@INT_SPL1] t1 on t0.DocEntry = t1.DocEntry where t1.U_Looms > 0 and t0.U_Cardcode = '" & lblvencode.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblloomsno.Text = dt1.Rows(0)(0).ToString()
        End If
    End Sub




    Protected Sub CheckBox_Changed(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As RadioButton = TryCast(row.Cells(0).FindControl("chkRow"), RadioButton)
                If chkRow.Checked = True Then
                    btnupdate.Visible = True
                    btndelete.Visible = True
                    btnsave.Visible = False
                    txtsetno.Text = row.Cells(3).Text
                    txtbeamno.Text = row.Cells(4).Text
                    txtloomno.Text = row.Cells(5).Text
                    txtthorneqty.Text = row.Cells(6).Text
                    txtmtrs.Text = row.Cells(7).Text
                    cmbcolor.Text = row.Cells(8).Text
                    lblmsg.Text = ""
                End If
            End If
        Next
    End Sub
    Private Sub txtsetno_TextChanged(sender As Object, e As EventArgs) Handles txtsetno.TextChanged
        If System.Text.RegularExpressions.Regex.IsMatch(txtsetno.Text, "^[0-9]*$") Then
            txtbeamno.Focus()
            lblmsg.Text = ""
        Else
            txtsetno.Text = ""
            txtsetno.Focus()
            ' lblmsg.Text = "Enter Correct GRN NO"
        End If
    End Sub
    Public Sub loadautonumber()
        Dim lblUserName As Label = TryCast(Me.Master.FindControl("lbluserName"), Label)
        lblUserName.Text = lblUserName.Text
        Dim sql As String = "select location from usermaster  with (nolock) where username = '" & lblUserName.Text & "'"
        Dim cmd As New SqlCommand(sql, con)
        Dim ad As New SqlDataAdapter(cmd)
        Dim dt As DataTable = New DataTable()
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            location = dt.Rows(0)("location").ToString()
        End If
        Dim st As String = location.ToUpper
        Dim subst As String = st.Substring(0, 2)
        Dim sql1 As String = "select (CAST(( YEAR( GETDATE() ) % 100 ) as varchar)  + '-'  + cast((max(docentry)+1) as varchar)) as autonum from SUBGRNBEAM with (nolock)"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim ad1 As New SqlDataAdapter(cmd1)
        Dim dt1 As DataTable = New DataTable()
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            autonumber = dt1.Rows(0)("autonum").ToString()
            autonumber = subst & autonumber
        End If
    End Sub

    Public Sub savewagedetails()
        loadautonumber()
        getgrndetails()
        gettotalmeters()
        con.Open()
        Dim lblUserName As Label = TryCast(Me.Master.FindControl("lbluserName"), Label)
        lblUserName.Text = lblUserName.Text
        Dim cmd As New SqlCommand("INSERT INTO SUBGRNBEAM([DOCNUM],[SUBGRNNO],[SUBGRNDOCENTRY],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[GRNQTY],[GRNMTRS],[BATCHNO],[AUTONUM],[SETNO],[BEAMNO],[LOOMNO],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[USER])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDOCENTRY,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@GRNQTY,@GRNMTRS,@BATCHNO,@AUTONUM,@SETNO,@BEAMNO,@LOOMNO,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@USER)", con)
        cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
        cmd.Parameters.AddWithValue("SUBGRNNO", txtgrn.Text)
        cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", grndocentry)
        cmd.Parameters.AddWithValue("DOCDATE", String.Format("{0:yyyy-MM-dd}", DateTime.Now.ToString))
        cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
        cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
        cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
        cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
        cmd.Parameters.AddWithValue("ITEMCODE", lblnewcode.Text)
        cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
        cmd.Parameters.AddWithValue("GRNQTY", lblgrnqty.Text)
        cmd.Parameters.AddWithValue("GRNMTRS", lblgrnmeters.Text)
        cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
        'cmd.Parameters.AddWithValue("AUTONUM", "TH" + String.Format("{0:yyyy}", DateTime.Now) & " - " & rowcount + 1)
        cmd.Parameters.AddWithValue("AUTONUM", autonumber)
        cmd.Parameters.AddWithValue("SETNO", txtsetno.Text)
        cmd.Parameters.AddWithValue("BEAMNO", txtbeamno.Text)
        cmd.Parameters.AddWithValue("LOOMNO", txtloomno.Text)
        cmd.Parameters.AddWithValue("THORNEQTY", txtthorneqty.Text)
        cmd.Parameters.AddWithValue("THORNEMTRS", txtmtrs.Text)
        cmd.Parameters.AddWithValue("thronecolor", lblcolor.Text)
        cmd.Parameters.AddWithValue("USER", lblUserName.Text.ToUpper)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Text = "Details Saved Successfully"
        loadgrid()
        entryclear()
        con.Close()
    End Sub

    Public Sub entryclear()
        txtsetno.Text = ""
        txtbeamno.Text = ""
        txtmtrs.Text = ""
        txtloomno.Text = ""
        txtthorneqty.Text = ""
        cmbcolor.SelectedIndex = 0
    End Sub

    Private Sub txtthorneqty_TextChanged(sender As Object, e As EventArgs) Handles txtthorneqty.TextChanged
        txtmtrs.Text = Val(txtthorneqty.Text) * Val(lbllength.Text)
        Dim totalmeters As Decimal
        totalmeters = Val(txtmtrs.Text) + Val(lbltotmtrs.Text)
        If totalmeters > lblgrnmeters.Text Then
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Quantity exceeds..Kindly check Quantity"
            txtmtrs.Text = ""
            txtthorneqty.Text = ""
            txtthorneqty.Focus()
        Else
            lblmsg.Text = ""
            txtmtrs.Focus()
        End If
    End Sub
    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click

        updatedetails()
    End Sub

    Public Sub updatedetails()
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As RadioButton = TryCast(row.Cells(0).FindControl("chkRow"), RadioButton)
                If chkRow.Checked = True Then
                    selectedautonumber = row.Cells(2).Text
                End If
            End If
        Next
        con.Open()
        Dim cmd As New SqlCommand("update  [SUBGRNBEAM] set [SETNO]=@SETNO,[BEAMNO]=@BEAMNO,[LOOMNO]=@LOOMNO,[THORNEQTY]=@THORNEQTY,[THORNEMTRS]=@THORNEMTRS,[THRONECOLOR]=@THRONECOLOR WHERE SUBGRNNO=@SUBGRNNO and autonum =@autonum", con)
        cmd.Parameters.AddWithValue("setno", txtsetno.Text)
        cmd.Parameters.AddWithValue("beamno", txtbeamno.Text)
        cmd.Parameters.AddWithValue("loomno", txtloomno.Text)
        cmd.Parameters.AddWithValue("thorneqty", txtthorneqty.Text)
        cmd.Parameters.AddWithValue("thornemtrs", txtmtrs.Text)
        cmd.Parameters.AddWithValue("thronecolor", cmbcolor.SelectedItem.Text)
        cmd.Parameters.AddWithValue("subgrnno", txtgrn.Text)
        cmd.Parameters.AddWithValue("autonum", selectedautonumber)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Text = "Thaan Details Updated"
        loadgrid()
        con.Close()
        entryclear()
        txtsetno.Focus()
    End Sub

    Public Sub deletedetails()
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As RadioButton = TryCast(row.Cells(0).FindControl("chkRow"), RadioButton)
                If chkRow.Checked = True Then
                    selectedautonumber = row.Cells(2).Text
                End If
            End If
        Next
        con.Open()
        Dim cmd As New SqlCommand("DELETE FROM [subgrnbeam] WHERE subgrnno=@subgrnno and autonum =@autonum", con)
        cmd.Parameters.AddWithValue("subgrnno", txtgrn.Text)
        cmd.Parameters.AddWithValue("autonum", selectedautonumber)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.Text = "Thaan Details Deleted"
        loadgrid()
        con.Close()
        entryclear()
        txtsetno.Focus()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        deletedetails()
    End Sub

    Private Sub cmbcolor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcolor.SelectedIndexChanged
        lblcolor.Text = cmbcolor.SelectedItem.Text
        If cmbcolor.Text = "NONE" Then
            lblcolor.Text = "NONE"
        End If
    End Sub



End Class
