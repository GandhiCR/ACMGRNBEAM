Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Web.Services

Public Class inspectiondisplay
    Inherits System.Web.UI.Page
    Dim grndate, dcdate, autonumdate As DateTime
    Dim postdate As Date
    Dim docnumber, rowcount, docentry As Integer
    Dim seqnumber, autonumber, selectedautonumber, color, itemcode As String
    Dim grnquantity, grnmeters, gridgrnqty, gridgrnmtrs, grnlength, grnwidth, totalquantity, totalmeters, totalaqty, totalbqty, totalcqty, totaldqty, totalamtrs, totalbmtrs, totalcmtrs, totaldmtrs, tapelength, thaanqtytotal, thaanmtrstotal, weight, length, width, reedspace, pick, warpcons, weftcons, totalkadaqty As Decimal
    Dim aquantity, bquantity, cquantity, totalthaanqty, tolerance, grandtotalthaanqty, setno, beamno, loomno As Integer
    Dim ameters, bmeters, cmeters, dmeters, dquantity, acceptmtrsa, acceptmtrsb, acceptmtrsc, acceptmtrsd, totalthaanmtrs, grandtotalthaanmtrs, akada, bkada, ckada As Decimal
    Dim grndocentry As String
    Dim dt2 As DataTable
    <WebMethod()>
    Public Shared Function Getitemcode(prefix As String) As String()
        Dim item As New List(Of String)()
        Using cmd As New SqlCommand()
            cmd.CommandText = "SELECT DISTINCT ITEMCODE FROM  oitm WHERE ITEMCODE LIKE @SEARCHTEXT + '%' ORDER BY ITEMCODE ASC"
            cmd.Parameters.AddWithValue("@SearchText", prefix)
            cmd.Connection = con
            con.Open()
            Using sdr As SqlDataReader = cmd.ExecuteReader()
                While sdr.Read()
                    item.Add(String.Format("{0}", sdr("itemcode")))
                End While
            End Using
            con.Close()
        End Using

        Return item.ToArray()
    End Function
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
            Session("GRNDOCENTRY") = grndocentry
        End If
        Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,B.u_reedspace,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM)"
        'Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_ItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND  A.DOCnum  NOT IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNINSPECTION)"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            loaddocnum()
            Session("GRNNO") = txtgrn.Text
            grndate = dt1.Rows(0)("createdate").ToString()
            lblgrndate.Text = grndate.ToString("dd/MM/yyyy")
            Session("GRNDATE") = grndate
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
            loadthaanno()
        Else
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Enter Correct GRN No."
            txtgrn.Text = ""
            txtgrn.Focus()
            grnclear()
        End If
        con.Close()
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
        Dim sql1 As String = "select B.U_Length,B.U_Width,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            grnlength = dt1.Rows(0)("U_Length").ToString()
            grnwidth = dt1.Rows(0)("U_Width").ToString()
            dcdate = dt1.Rows(0)("u_SUPPDCdt").ToString()
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
            lblwarpcons.Text = dt1.Rows(0)("WARPCONS").ToString()
            lblweftcons.Text = dt1.Rows(0)("WEFTCONS").ToString()
            lblcategory.Text = dt1.Rows(0)("u_category").ToString().ToUpper
            lblsubcategory.Text = dt1.Rows(0)("u_subcategory").ToString().ToUpper
            lblequipment.Text = dt1.Rows(0)("U_CommodityName").ToString().ToUpper
        End If
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
    Public Sub loadthaanno()
        lblthaanno.Visible = True
        lblcolan.Visible = True
        cmbthaan.Visible = True
        btnflilter.Visible = True
        Dim sql1 As String = "SELECT autonum FROM (select distinct autonum,DOCENTRY  from subgrninspection where subgrnno = '" & txtgrn.Text & "') A ORDER BY DOCENTRY ASC"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            cmbthaan.DataSource = dt1
            cmbthaan.DataTextField = "autonum"
            cmbthaan.DataValueField = "autonum"
            cmbthaan.DataBind()
            cmbthaan.Items.Insert(0, New ListItem("-Select Thaanno-", ""))
            btnsave.Visible = True
        End If
    End Sub

    Public Sub grnclear()
        lblgrndate.Text = ""
        lblwidth.Text = ""
        lblitemname.Text = ""
        lblnewcode.Text = ""
        txtgrn.Text = ""
        lblvencode.Text = ""
        lbldcno.Text = ""
        lbldcdate.Text = ""
        lblusername.Text = ""
        lblvenname.Text = ""
        lblbatchno.Text = ""
        lblgrnqty.Text = ""
        empwagepanel.Visible = False
        lblgrnmeters.Text = ""
        contenpanel.Visible = False
        lblsizecode.Text = ""
        lblequipment.Text = ""
        lblcategory.Text = ""
        lblsubcategory.Text = ""
        btnclear.Visible = False
        lbllength.Text = ""
        lblreedspace.Text = ""
        lblpick.Text = ""
        lbldocnum.Text = ""
        txtgrn.Focus()
        Formulapanel.Visible = False
        btncalculate.Visible = False
        btnexport.Visible = False
        btnerror.Visible = False
        btnsave.Visible = False

    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        grnclear()
        lblmsg.Text = ""
    End Sub

    Private Sub txtgrn_TextChanged(sender As Object, e As EventArgs) Handles txtgrn.TextChanged
        If System.Text.RegularExpressions.Regex.IsMatch(txtgrn.Text, "^[0-9]*$") Then
            getgrndetails()
        Else
            txtgrn.Text = ""
            txtgrn.Focus()
            lblmsg.Text = "Enter Correct GRN NO"
        End If
    End Sub



    Public Sub loadgrid()

        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(2).Text
            End If
        Next
        'Dim sql1 As String = "select A.AUTONUM,A.THORNEQTY,A.ITEMCODE,A.THORNEMTRS,A.THRONECOLOR,A.SETNO,A.BEAMNO,A.THRONESIZE,A.LOOMNO,B.SWIDTH1,C.U_ReedSpace,C.U_Pick,c.U_LENGTH  from [SUBGRNBEAM] A INNER JOIN [OItM] B On A.itemcode = B.itemcode inner join [@INT_PDN1] c  on a.SUBGRNDOCENTRY=c.DocEntry where A.SUBGRNNO  = '" & txtgrn.Text & "' AND C.U_ReedSpace IS NOT NULL AND A.AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNINSPECTION) ORDER BY A.DOCENTRY"
        Dim sql1 As String = "select * FROM SUBGRNINSPECTION where SUBGRNNO = '" & txtgrn.Text & "' order by docentry"
        'Dim sql1 As String = "select AUTONUM,THORNEQTY,THORNEMTRS,THRONECOLOR,SETNO,BEAMNO,LOOMNO,LENGTH from [SUBGRNBEAM] where SUBGRNNO = '" & txtgrn.Text & "' AND AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNINSPECTION) ORDER BY DOCENTRY ASC"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = dt2
            GridView1.DataBind()
            empwagepanel.Visible = True
            GridView1.FooterRow.Visible = False
            btncalculate.Visible = True
            btnexport.Visible = True
            gettotalquantity()
        End If


    End Sub


    Public Sub loaddocnum()
        Dim sql As String = "Select docnum from subgrninspection where subgrnno ='" & txtgrn.Text & "' "
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            docnumber = dt.Rows(0)("docnum").ToString()
            lbldocnum.Text = docnumber
        End If
    End Sub



    Public Sub updatedetails()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(2).Text
                gridgrnqty = row.Cells(3).Text
                itemcode = TryCast(row.Cells(4).FindControl("txtitemcode"), TextBox).Text
                gridgrnmtrs = row.Cells(5).Text
                color = TryCast(row.Cells(6).FindControl("cmbcolor"), DropDownList).SelectedItem.Text
                length = TryCast(row.Cells(7).FindControl("txtlength"), TextBox).Text
                width = TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).Text
                reedspace = TryCast(row.Cells(9).FindControl("txtreed"), TextBox).Text
                pick = TryCast(row.Cells(10).FindControl("txtpick"), TextBox).Text
                aquantity = TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).Text
                bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                If aquantity <> "0.00" Or bquantity <> "0.00" Or cquantity <> "0.00" Then
                    bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                    akada = TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).Text
                    bkada = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    ckada = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    ameters = TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text
                    bmeters = TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text
                    cmeters = TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text
                    dmeters = TryCast(row.Cells(19).FindControl("txtthaanmtrsd"), Label).Text

                    tapelength = TryCast(row.Cells(21).FindControl("txttapelength"), TextBox).Text
                    setno = row.Cells(22).Text
                    beamno = row.Cells(23).Text
                    loomno = row.Cells(24).Text
                    weight = TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text
                    con.Open()
                    Dim lblUserName As Label = TryCast(Me.Master.FindControl("lbluserName"), Label)
                    lblUserName.Text = lblUserName.Text
                    loadlength()
                    Dim cmd As New SqlCommand("UPDATE SUBGRNINSPECTION SET [DOCNUM]= @DOCNUM, [SUBGRNNO]=@SUBGRNNO,[SUBGRNDOCENTRY]=@SUBGRNDOCENTRY,[SUPPDC]=@SUPPDC,[SUPPDATE]=@SUPPDATE,[VENCODE]=@VENCODE,[VENNAME]=@VENNAME,[ITEMCODE]=@ITEMCODE,[ITEMNAME]=@ITEMNAME,[LENGTH]=@LENGTH,[WIDTH]=@WIDTH,[REEDSPACE]=@REEDSPACE,[PICK]=@PICK,[GRNQTY]=@GRNQTY,[GRNMTRS]=@GRNMTRS,[BATCHNO]=@BATCHNO,[AUTONUM]=@AUTONUM,[THORNEQTY]=@THORNEQTY,[THORNEMTRS]=@THORNEMTRS,[SETNO]=@SETNO,[BEAMNO]=@BEAMNO,[LOOMNO]=@LOOMNO,[AQTY]=@AQTY,[AMTRS]=@AMTRS,[BQTY]=@BQTY,[BMTRS]=@BMTRS,[CQTY]=@CQTY,[CMTRS]=@CMTRS,[BITQTY]=@BITQTY,[BITMTRS]=@BITMTRS,[TOTALQTY]=@TOTALQTY,[TOTALMTRS]=@TOTALMTRS,[THRONECOLOR]=@THRONECOLOR,[THRONESIZE]=@THRONESIZE,[THRONEREEDSPACE]=@THRONEREEDSPACE,[THRONEPICK]=@THRONEPICK,[THRONEWIDTH]=@THRONEWIDTH,[USER]=@USER,[TAPELENGTH]=@TAPELENGTH,[WEIGHT]=@WEIGHT WHERE SUBGRNNO = '" & txtgrn.Text & "'  and autonum = '" & autonumber & "'", con)
                    cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
                    cmd.Parameters.AddWithValue("SUBGRNNO", txtgrn.Text)
                    cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", grndocentry)
                    cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
                    cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
                    cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
                    cmd.Parameters.AddWithValue("ITEMCODE", lblnewcode.Text)
                    cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
                    cmd.Parameters.AddWithValue("GRNQTY", lblgrnqty.Text)
                    cmd.Parameters.AddWithValue("GRNMTRS", lblgrnmeters.Text)
                    cmd.Parameters.AddWithValue("LENGTH", length)
                    cmd.Parameters.AddWithValue("WIDTH", lblwidth.Text)
                    cmd.Parameters.AddWithValue("REEDSPACE", lblreedspace.Text)
                    cmd.Parameters.AddWithValue("PICK", lblpick.Text)
                    cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
                    cmd.Parameters.AddWithValue("AUTONUM", autonumber)
                    cmd.Parameters.AddWithValue("THORNEQTY", gridgrnqty)
                    cmd.Parameters.AddWithValue("THORNEMTRS", gridgrnmtrs)
                    cmd.Parameters.AddWithValue("THRONECOLOR", color)
                    cmd.Parameters.AddWithValue("THRONESIZE", length.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEWIDTH", width.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEREEDSPACE", reedspace.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEPICK", pick.ToString("F2"))
                    If length = "1.00" Then
                        cmd.Parameters.AddWithValue("AQTY", ameters)
                        cmd.Parameters.AddWithValue("BQTY", bmeters)
                        cmd.Parameters.AddWithValue("CQTY", cmeters)
                        cmd.Parameters.AddWithValue("TOTALQTY", thaanmtrstotal)
                    Else
                        cmd.Parameters.AddWithValue("AQTY", aquantity)
                        cmd.Parameters.AddWithValue("BQTY", bquantity)
                        cmd.Parameters.AddWithValue("CQTY", cquantity)
                        cmd.Parameters.AddWithValue("TOTALQTY", thaanqtytotal)
                    End If
                    cmd.Parameters.AddWithValue("BITQTY", dquantity)
                    cmd.Parameters.AddWithValue("AMTRS", ameters)
                    cmd.Parameters.AddWithValue("BMTRS", bmeters)
                    cmd.Parameters.AddWithValue("CMTRS", cmeters)
                    cmd.Parameters.AddWithValue("BITMTRS", dmeters)
                    cmd.Parameters.AddWithValue("TOTALMTRS", thaanmtrstotal)
                    cmd.Parameters.AddWithValue("tapelength", tapelength)
                    cmd.Parameters.AddWithValue("setno", setno)
                    cmd.Parameters.AddWithValue("beamno", beamno)
                    cmd.Parameters.AddWithValue("loomno", loomno)
                    cmd.Parameters.AddWithValue("WEIGHT", weight)
                    cmd.Parameters.AddWithValue("USER", lblUserName.Text.ToUpper)
                    cmd.ExecuteNonQuery()
                    lblmsg.BackColor = System.Drawing.Color.White
                    lblmsg.ForeColor = System.Drawing.Color.Green
                    lblmsg.Visible = True
                    lblmsg.Text = "Details Updated Successfully"
                    con.Close()
                    loadgrid()
                    gettotalquantity()
                End If
            End If
        Next

    End Sub
    Protected Sub btncolor_Click(sender As Object, e As EventArgs) Handles btncolor.Click
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(2).Text
                color = TryCast(row.Cells(6).FindControl("cmbcolor"), DropDownList).SelectedItem.Text
            End If
        Next
        con.Open()
        Dim cmd As New SqlCommand("UPDATE SUBGRNINSPECTION SET [THRONECOLOR]=@THRONECOLOR WHERE SUBGRNNO = '" & txtgrn.Text & "'  and autonum = '" & autonumber & "'", con)
        cmd.Parameters.AddWithValue("THRONECOLOR", color)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Visible = True
        lblmsg.Text = "Color Updated Successfully"
        con.Close()

    End Sub
    Public Sub gettotalquantity()
        empwagepanel.Visible = True
        lblaqty.Visible = True
        lblbqty.Visible = True
        lblcqty.Visible = True
        lbldqty.Visible = True
        lblamtrs.Visible = True
        lblbmtrs.Visible = True
        lblcmtrs.Visible = True
        lbldmtrs.Visible = True
        lblweight.Visible = True
        Dim sql1 As String = "select isnull(sum(AQTY),0) As [aquantity], isnull(sum(BQTY), 0) As [bquantity], isnull(sum(CQTY), 0) As [cquantity], isnull(sum(BITQTY), 0) As [bitquantity], isnull(sum(AMTRS), 0) As [amtrs], isnull(sum(BMTRS), 0) As [bmtrs], isnull(sum(CMTRS), 0) As [cmtrs], isnull(sum(BITMTRS), 0) As [bitmtrs], isnull(sum(weight), 0) As [weight] from SUBGRNINSPECTION  where subgrnno = '" & txtgrn.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            aquantity = dt1.Rows(0)(0).ToString()
            bquantity = dt1.Rows(0)(1).ToString()
            cquantity = dt1.Rows(0)(2).ToString()
            dquantity = dt1.Rows(0)(3).ToString()
            acceptmtrsa = dt1.Rows(0)(4).ToString()
            acceptmtrsb = dt1.Rows(0)(5).ToString()
            acceptmtrsc = dt1.Rows(0)(6).ToString()
            acceptmtrsd = dt1.Rows(0)(7).ToString()
            weight = dt1.Rows(0)(8).ToString()
            lbltotaqty.Text = aquantity
            lbltotbqty.Text = bquantity
            lbltotcqty.Text = cquantity
            lbltotdqty.Text = dquantity
            lbltotamtrs.Text = acceptmtrsa
            lbltotbmtrs.Text = acceptmtrsb
            lbltotcmtrs.Text = acceptmtrsc
            lbltotdmtrs.Text = acceptmtrsd
            lbltotweight.Text = weight
        Else
            totalquantity = 0
        End If
    End Sub


    Private Sub btncalculate_Click(sender As Object, e As EventArgs) Handles btncalculate.Click
        lblmsg.Text = ""
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(2).Text
                gridgrnqty = row.Cells(3).Text
                itemcode = TryCast(row.Cells(4).FindControl("txtitemcode"), TextBox).Text
                gridgrnmtrs = row.Cells(5).Text
                color = TryCast(row.Cells(6).FindControl("cmbcolor"), DropDownList).SelectedItem.Text
                length = TryCast(row.Cells(7).FindControl("txtlength"), TextBox).Text
                width = TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).Text
                reedspace = TryCast(row.Cells(9).FindControl("txtreed"), TextBox).Text
                pick = TryCast(row.Cells(10).FindControl("txtpick"), TextBox).Text
                aquantity = TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).Text
                bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                If aquantity <> "0.00" Or bquantity <> "0.00" Or cquantity <> "0.00" Then
                    bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                    akada = TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).Text
                    bkada = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    ckada = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    ameters = TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text
                    bmeters = TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text
                    cmeters = TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text
                    dmeters = TryCast(row.Cells(19).FindControl("txtthaanmtrsd"), Label).Text

                    tapelength = TryCast(row.Cells(21).FindControl("txttapelength"), TextBox).Text
                    setno = row.Cells(22).Text
                    beamno = row.Cells(23).Text
                    loomno = row.Cells(24).Text
                    weight = TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text

                    If aquantity.ToString = 0.00 Then
                        ameters = 0.00
                    ElseIf length = "1.00" Then
                        ameters = akada * length
                        TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text = ameters.ToString("F2")
                        TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        row.Cells(16).BackColor = System.Drawing.Color.Aquamarine
                    Else
                        ameters = aquantity * length
                        TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text = ameters.ToString("F2")
                        TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        row.Cells(16).BackColor = System.Drawing.Color.Aquamarine
                    End If

                    If bquantity.ToString = 0.00 Then
                        bmeters = 0.00
                    ElseIf length = "1.00" Then
                        bmeters = bkada * length
                        TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text = bmeters.ToString("F2")
                        row.Cells(17).BackColor = System.Drawing.Color.Aquamarine
                    Else
                        bmeters = bquantity * length
                        TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text = bmeters.ToString("F2")
                        row.Cells(17).BackColor = System.Drawing.Color.Aquamarine
                    End If

                    If cquantity.ToString = 0.00 Then
                        cmeters = 0.00
                    ElseIf length = "1.00" Then
                        cmeters = ckada * length
                        TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text = cmeters.ToString("F2")
                        row.Cells(18).BackColor = System.Drawing.Color.Aquamarine
                    Else
                        cmeters = cquantity * length
                        TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text = cmeters.ToString("F2")
                        row.Cells(18).BackColor = System.Drawing.Color.Aquamarine
                    End If
                    If dquantity.ToString = 0.00 Then
                        dmeters = 0.00
                    Else
                        dmeters = dquantity
                        TryCast(row.Cells(18).FindControl("txtthaanmtrsd"), Label).Text = dmeters.ToString("F2")
                        row.Cells(18).BackColor = System.Drawing.Color.Aquamarine
                    End If
                    loadwidth()
                    totalthaanmtrs = ameters + bmeters + cmeters + dmeters
                    If length = "1.00" Then
                        totalkadaqty = akada + bkada + ckada + dquantity
                        row.Cells(15).Text = totalkadaqty
                    Else
                        totalthaanqty = aquantity + bquantity + cquantity + dquantity
                        row.Cells(15).Text = totalthaanqty
                    End If
                    row.Cells(2).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(3).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(4).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(5).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(6).BackColor = System.Drawing.Color.Aquamarine
                    ' TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text = weight.ToString("F2")
                    row.Cells(25).BackColor = System.Drawing.Color.Aquamarine
                    GridView1.FooterRow.Visible = True
                    row.Cells(15).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(20).Text = totalthaanmtrs.ToString("F2")
                    row.Cells(20).BackColor = System.Drawing.Color.Aquamarine
                    totalaqty += aquantity
                    totalbqty += bquantity
                    totalcqty += cquantity
                    totaldqty += dquantity
                    totalamtrs += ameters
                    totalbmtrs += bmeters
                    totalcmtrs += cmeters
                    totaldmtrs += dmeters
                    grandtotalthaanqty += totalthaanqty
                    grandtotalthaanmtrs += totalthaanmtrs

                    GridView1.FooterRow.Cells(10).Text = "TOTAL"
                    GridView1.FooterRow.Cells(11).Text = totalaqty.ToString("F2")
                    GridView1.FooterRow.Cells(12).Text = totalbqty.ToString("F2")
                    GridView1.FooterRow.Cells(13).Text = totalcqty.ToString("F2")
                    GridView1.FooterRow.Cells(14).Text = totaldqty.ToString("F2")
                    GridView1.FooterRow.Cells(15).Text = grandtotalthaanqty.ToString()

                    GridView1.FooterRow.Cells(16).Text = totalamtrs.ToString("F2")
                    GridView1.FooterRow.Cells(17).Text = totalbmtrs.ToString("F2")
                    GridView1.FooterRow.Cells(18).Text = totalcmtrs.ToString("F2")
                    GridView1.FooterRow.Cells(19).Text = totaldmtrs.ToString("F2")


                    GridView1.FooterRow.Cells(20).HorizontalAlign = HorizontalAlign.Right
                    GridView1.FooterRow.Cells(20).Text = grandtotalthaanmtrs.ToString("F2")
                    Dim textbox1 As TextBox = TryCast(row.FindControl("txtAQuantity"), TextBox)
                    textbox1.Focus()
                End If

            End If
        Next
        btnsave.Visible = True
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        updatedetails()
    End Sub
    Protected Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(1).Text
            End If
        Next
        ' Dim sql1 As String = "select A.AUTONUM,A.THORNEQTY,A.THORNEMTRS,A.THRONECOLOR,A.THRONESIZE,A.THRONEWIDTH,A.THRONEREEDSPACE,A.THRONEPICK,A.ERRORCATEGORY,A.ERRORTYPE,A.ERROR,A.ERRORQTY,A.SETNO,A.BEAMNO,A.LOOMNO,A.LENGTH,ISNULL(c.AQTY,0) as AQTY,isnull(c.BQTY,0) AS BQTY,isnull(c.CQTY,0) AS CQTY,isnull(c.BITQTY,0) AS BITQTY, ISNULL(C.TOTALQTY,0) totalqty, ISNULL(C.AMTRS,0)AS AMTRS,ISNULL(C.BMTRS,0) bmtrs,ISNULL(C.CMTRS,0) CMTRS,ISNULL(c.BITMTRS,0)  AS BITMTRS,isnull(c.TOTALMTRS,0) AS TOTALMTRS,isnull(c.TAPELENGTH,0) AS TAPELENGTH,isnull(c.WEIGHT,0)  AS [WEIGHT]from [SUBGRNBEAM]  A LEFT JOIN (select B.SUBGRNDOCENTRY,B.AUTONUM, B.THORNEQTY,B.THORNEMTRS,B.THRONECOLOR,B.THRONESIZE,B.THRONEWIDTH,B.THRONEREEDSPACE,B.THRONEPICK,B.ERRORCATEGORY,B.ERRORTYPE,B.ERROR,B.ERRORQTY,B.SETNO,B.BEAMNO,B.LOOMNO,B.AQTY,B.BQTY,B.CQTY,B.BITQTY,B.TOTALQTY,B.AMTRS,B.BMTRS,B.CMTRS,B.BITMTRS,B.TOTALMTRS,B.TAPELENGTH,B.[WEIGHT] from  [SUBGRNINSPECTION] B )c ON C.SUBGRNDOCENTRY=A.SUBGRNDOCENTRY AND A.AUTONUM=C.AUTONUM where A.SUBGRNNO = '" & txtgrn.Text & "'"
        Dim sql1 As String = "select * FROM SUBGRNINSPECTION where SUBGRNNO = '" & txtgrn.Text & "'"
        'Dim sql1 As String = "select AUTONUM,THORNEQTY,THORNEMTRS,THRONECOLOR,SETNO,BEAMNO,LOOMNO,LENGTH from [SUBGRNBEAM] where SUBGRNNO = '" & txtgrn.Text & "' AND AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNINSPECTION) ORDER BY DOCENTRY ASC"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            GridView1.Visible = False


        End If

        Dim FileName As String = txtgrn.Text & "   " & "INSPECTION ENTRY REPORT" & ".xls"
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages





            For Each row As GridViewRow In GridView1.Rows
                row.BackColor = System.Drawing.Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView1.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView1.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
    Private Sub btnerror_Click(sender As Object, e As EventArgs) Handles btnerror.Click
        errorpanel.Visible = True
    End Sub

    Private Sub btnitem_Click(sender As Object, e As EventArgs) Handles btnitem.Click
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                autonumber = row.Cells(2).Text
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                itemcode = TryCast(row.Cells(4).FindControl("txtitemcode"), TextBox).Text
                length = TryCast(row.Cells(7).FindControl("txtlength"), TextBox).Text
                width = TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).Text
                reedspace = TryCast(row.Cells(9).FindControl("txtreed"), TextBox).Text
                pick = TryCast(row.Cells(10).FindControl("txtpick"), TextBox).Text

                If chkRow.Checked = True Then
                    con.Open()
                    Dim sql1 As String = "SELECT SWIDTH1,SLength1,CONVERT(DECIMAL(10,2),(SWIDTH1/2.54)) AS REEDSPACE,U_PICK,ISNULL(U_WARPCONS,0) as warpcons,isnUll(U_WEFTCONS,0) as weftcons,U_category,u_subcategory,u_commodityname,isnull(u_tapelength,0) as tapelength FROM OITM WHERE ITEMCODE='" & itemcode & "'"
                    Dim cmd1 As New SqlCommand(sql1, con)
                    Dim dt1 As New DataTable()
                    Dim ad1 As New SqlDataAdapter(cmd1)
                    ad1.Fill(dt1)
                    If dt1.Rows.Count > 0 Then
                        grnlength = dt1.Rows(0)("slength1").ToString()
                        length = grnlength.ToString("F2")
                        grnwidth = dt1.Rows(0)("swidth1").ToString()
                        reedspace = dt1.Rows(0)("reedspace").ToString()
                        lblwidth.Text = (grnwidth / 2.54).ToString("F2")
                        pick = dt1.Rows(0)("u_pick").ToString()
                        TryCast(row.Cells(7).FindControl("txtlength"), TextBox).Text = length
                        TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).Text = width
                        TryCast(row.Cells(9).FindControl("txtreed"), TextBox).Text = reedspace
                        TryCast(row.Cells(10).FindControl("txtpick"), TextBox).Text = pick
                        TryCast(row.Cells(7).FindControl("txtlength"), TextBox).BackColor = System.Drawing.Color.Bisque
                        TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).BackColor = System.Drawing.Color.Bisque
                        TryCast(row.Cells(9).FindControl("txtreed"), TextBox).BackColor = System.Drawing.Color.Bisque
                        TryCast(row.Cells(10).FindControl("txtpick"), TextBox).BackColor = System.Drawing.Color.Bisque
                        Dim cmd As New SqlCommand("UPDATE SUBGRNINSPECTION SET itemcode=@itemcode WHERE SUBGRNNO = '" & txtgrn.Text & "'  and autonum = '" & autonumber & "'", con)
                        cmd.Parameters.AddWithValue("itemcode", itemcode)
                        cmd.ExecuteNonQuery()
                        con.Close()
                        lblmsg.BackColor = System.Drawing.Color.White
                        lblmsg.ForeColor = System.Drawing.Color.Green
                        lblmsg.Visible = True
                        lblmsg.Text = "Itemcode Updated Successfully"
                    End If
                End If
            End If

        Next

    End Sub

    Private Sub cmbthaan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbthaan.SelectedIndexChanged
        Dim sql1 As String = "select * FROM SUBGRNINSPECTION where SUBGRNNO = '" & txtgrn.Text & "' and autonum = '" & cmbthaan.Text & "' order by docentry"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = dt2
            GridView1.DataBind()
            empwagepanel.Visible = True
            GridView1.Focus()
            GridView1.ShowFooter = False
        End If
    End Sub

    Private Sub btnflilter_Click(sender As Object, e As EventArgs) Handles btnflilter.Click
        loadthaanno()
        loadgrid()
        cmbthaan.SelectedIndex = 0
        GridView1.Focus()
        GridView1.ShowFooter = False
    End Sub
End Class
