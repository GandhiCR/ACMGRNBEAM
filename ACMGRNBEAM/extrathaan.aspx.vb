Imports System.Data.SqlClient
Imports System.IO
Public Class extrathaan
    Inherits System.Web.UI.Page
    Dim GRNNO, GRNDOCENTRY, DOCNUMBER As String
    Dim grndate, dcdate, autonumdate As DateTime
    Dim seqnumber, autonumber, selectedautonumber, color, itemcode, thaanbatchno As String
    Dim grnquantity, grnmeters, gridgrnqty, gridgrnmtrs, grnlength, grnwidth, totalquantity, totalmeters, totalaqty, totalbqty, totalcqty, totaldqty, totalamtrs, totalbmtrs, totalcmtrs, totaldmtrs, tapelength, standardtapelength, thaanqtytotal, thaanmtrstotal, weight, length, width, reedspace, pick, warpcons, weftcons As Decimal
    Dim aquantity, bquantity, cquantity, totalthaanqty, tolerance, grandtotalthaanqty, setno, beamno, loomno As Integer
    Dim ameters, bmeters, cmeters, dmeters, dquantity, acceptmtrsa, acceptmtrsb, acceptmtrsc, acceptmtrsd, totalthaanmtrs, grandtotalthaanmtrs, errorquantity, akada, bkada, ckada As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadpage()
    End Sub
    Public Sub loadpage()
        Call dbMAIN()
        GRNNO = Session("GRNNO")
        txtgrn.Text = GRNNO
        grndate = Session("GRNDATE")
        GRNDOCENTRY = Session("GRNDOCENTRY")
        lbldocnum.Text = Session("DOCNUM")
        If Not IsPostBack Then
            loadautonum()
            loadgrid()
            getgrndetails()
            Dim sql1 As String = "SELECT A.AUTONUM,A.TOTALQTY,A.TOTALMTRS,A.SUBGRNNO   FROM [SUBGRNINSPECTION]  A INNER JOIN [OITM] B ON A.ITEMCODE = B.ITEMCODE INNER JOIN [@INT_PDN1] C  ON A.SUBGRNDOCENTRY=C.DOCENTRY WHERE AUTONUM NOT IN (SELECT A.AUTONUM  FROM [SUBGRNBEAM] A INNER JOIN [OITM] B ON A.ITEMCODE = B.ITEMCODE INNER JOIN [@INT_PDN1] C  ON A.SUBGRNDOCENTRY=C.DOCENTRY ) AND SUBGRNNO  = '" & GRNNO & "' AND C.U_REEDSPACE IS NOT NULL  "
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                GridView2.Visible = True
                GridView2.DataSource = dt2
                GridView2.DataBind()
                GridView2.FooterRow.Visible = False
                btnsave.Visible = True
            End If
        End If
        btnexport.Visible = True
    End Sub
    Public Sub getgrndetails()
        con.Open()
        Dim sql As String = "Select max(docentry) from  [@INT_OPDN] where docnum ='" & txtgrn.Text & "' and U_Process ='W2'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GRNDOCENTRY = dt.Rows(0)(0).ToString()
            Session("GRNDOCENTRY") = GRNDOCENTRY
        End If
        Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_NewItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,B.u_reedspace,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum ='" & txtgrn.Text & "' and A.DOCENTRY = '" & GRNDOCENTRY & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND U_Quantity <> 0"
        'Dim sql1 As String = "select A.DocNum, A.CreateDate,B.U_ItemName,B.U_NewItemCode,A.U_SUPPDCNo ,A.U_CardCode,B.U_Length,B.U_width,A.U_CardName,A.Creator,B.U_BatchNo,B.U_Quantity,B.U_Mtrs,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & grndocentry & "' AND  A.DOCnum  IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNBEAM) AND  A.DOCnum  NOT IN (SELECT DISTINCT SUBGRNNO FROM SUBGRNINSPECTION)"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            Session("GRNNO") = txtgrn.Text
            GRNDATE = dt1.Rows(0)("createdate").ToString()
            lblgrndate.Text = GRNDATE.ToString("dd/MM/yyyy")
            Session("GRNDATE") = GRNDATE
            lblnewcode.Text = dt1.Rows(0)("U_Newitemcode").ToString()
            Session("ITEMCODE") = lblnewcode.Text
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
            Session("REEDSPACE") = reedspace
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
        Else
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.Text = "Enter Correct GRN No."
            txtgrn.Text = ""
            txtgrn.Focus()
        End If
        con.Close()
    End Sub



    Public Sub loadautonum()
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim da5 As New SqlDataAdapter, ds5 As New DataSet
        da5.SelectCommand = New SqlCommand
        da5.SelectCommand.Connection = con
        da5.SelectCommand.CommandType = CommandType.Text
        da5.SelectCommand.CommandText = "SELECT distinct autonum from subgrninspection where subgrnno ='" & GRNNO & "'"
        da5.Fill(ds5, "tbl2")
        cmbautonum.DataSource = ds5.Tables("tbl2")
        cmbautonum.DataTextField = "autonum"
        cmbautonum.DataValueField = "autonum"
        cmbautonum.DataBind()
        con.Close()
        cmbautonum.Items.Insert(0, New ListItem("--Select Thaanno--", "0"))
    End Sub




    Public Sub loadgrid()
        GridView1.Visible = True
        Dim sql1 As String = "select A.AUTONUM,A.THORNEQTY,A.ITEMCODE,A.THORNEMTRS,A.THRONECOLOR,A.SETNO,A.BEAMNO,A.LOOMNO,B.SWIDTH1,C.U_ReedSpace,C.U_Pick,c.U_LENGTH  from [SUBGRNBEAM] A INNER JOIN [OItM] B On A.itemcode = B.itemcode inner join [@INT_PDN1] c  on a.SUBGRNDOCENTRY=c.DocEntry where A.SUBGRNNO  = '" & GRNNO & "' AND C.U_ReedSpace IS NOT NULL and  C.U_pick IS NOT NULL AND A.AUTONUM = '" & cmbautonum.SelectedItem.Text & "' "
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = dt2
            GridView1.DataBind()
            empwagepanel.Visible = True
            btncalculate.Visible = True
            btnupdate.Visible = False
            btndelete.Visible = False
            GridView1.FooterRow.Visible = False
        Else
            GridView1.DataSource = Nothing
            GridView1.DataBind()
        End If

        'gettotalmeters()
    End Sub

    Public Sub clear()
        cmbautonum.SelectedIndex = 0
    End Sub

    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        clear()
    End Sub
    Protected Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click


        Dim sql1 As String = "select * FROM SUBGRNERRORENTRY where SUBGRNNO = '" & GRNNO & "'"
        'Dim sql1 As String = "select AUTONUM,THORNEQTY,THORNEMTRS,THRONECOLOR,SETNO,BEAMNO,LOOMNO,LENGTH from [SUBGRNBEAM] where SUBGRNNO = '" & txtgrn.Text & "' AND AUTONUM NOT IN (SELECT AUTONUM FROM SUBGRNINSPECTION) ORDER BY DOCENTRY ASC"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = Nothing
            GridView1.DataBind()
            GridView1.Visible = False
            GridView1.Visible = True
            GridView1.DataSource = dt2
            GridView1.DataBind()
            GridView1.FooterRow.Visible = False

        End If


        Response.Clear()
        Response.Buffer = True
        Response.ClearContent()
        Response.ClearHeaders()
        Response.Charset = ""
        Dim FileName As String = GRNNO & "   " & "DAMAGE ENTRY REPORT" & ".xls"
        Dim strwritter As StringWriter = New StringWriter()
        Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
        GridView1.GridLines = GridLines.Both
        GridView1.HeaderStyle.Font.Bold = True
        GridView1.RenderControl(htmltextwrtter)
        Response.Write(strwritter.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        loadgrid()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                autonumber = row.Cells(2).Text
            End If
            row.Cells(2).Text = cmbautonum.SelectedItem.Text & cmbalphabet.SelectedItem.Text
        Next
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
                    ElseIf color = "KADA" Then
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
                    ElseIf color = "KADA" Then
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
                    ElseIf color = "KADA" Then
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
                        TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).BackColor = System.Drawing.Color.Aquamarine
                        TryCast(row.Cells(19).FindControl("txtthaanmtrsd"), Label).Text = dmeters.ToString("F2")
                        row.Cells(19).BackColor = System.Drawing.Color.Aquamarine
                    End If


                    loadwidth()

                    totalthaanqty = aquantity + bquantity + cquantity + dquantity
                    totalthaanmtrs = ameters + bmeters + cmeters + dmeters

                    row.Cells(2).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(3).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(4).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(5).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(6).BackColor = System.Drawing.Color.Aquamarine
                    TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text = weight.ToString("F2")
                    row.Cells(25).BackColor = System.Drawing.Color.Aquamarine
                    GridView1.FooterRow.Visible = True
                    row.Cells(15).Text = totalthaanqty
                    row.Cells(15).BackColor = System.Drawing.Color.Aquamarine
                    row.Cells(20).Text = totalthaanmtrs
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
    End Sub
    Public Sub loadlength()
        Dim sql As String = "select max(docentry) from  [@INT_OPDN] where docnum ='" & GRNNO & "' and U_Process ='W2'"
        Dim cmd As New SqlCommand(sql, con)
        Dim dt As New DataTable()
        Dim ad As New SqlDataAdapter(cmd)
        ad.Fill(dt)
        If dt.Rows.Count > 0 Then
            GRNDOCENTRY = dt.Rows(0)(0).ToString()
        End If
        Dim sql1 As String = "select  A.CreateDate,B.U_Length,B.U_Width,A.U_SUPPDCDt from [@INT_OPDN] A INNER JOIN [@INT_PDN1] B On A.DocEntry = B.DocEntry where A.DocNum = '" & txtgrn.Text & "' and A.DOCENTRY = '" & GRNDOCENTRY & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            grnlength = dt1.Rows(0)("U_Length").ToString()
            grnwidth = dt1.Rows(0)("U_Width").ToString()
            dcdate = dt1.Rows(0)("u_SUPPDCdt").ToString()
            GRNDATE = dt1.Rows(0)("createdate").ToString()
        End If
    End Sub
    Public Sub loadwidth()
        Dim sql1 As String = "SELECT SWIDTH1,U_SIZE,U_REEDSPACE,U_PICK,ISNULL(U_WARPCONS,0) as warpcons,isnUll(U_WEFTCONS,0) as weftcons,U_category,u_subcategory,u_commodityname,isnull(u_tapelength,0) as [u_tapelength] FROM OITM WHERE ITEMCODE='" & lblnewcode.Text & "'"
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
            standardtapelength = dt1.Rows(0)("U_tapelength").ToString().ToUpper
            lbltapelength.Text = standardtapelength.ToString("F2")
        End If
    End Sub
    Public Sub loadsizingcode()
        Dim sql1 As String = "SELECT u_itemcode,u_itemname from [@INT_PDN3] where  U_Formula = 'w1' and docentry='" & GRNDOCENTRY & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblsizecode.Text = dt1.Rows(0)("u_itemcode").ToString()
            lblcategory.Text = dt1.Rows(0)("u_itemname").ToString()
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        saveinspectiondetails()
    End Sub
    Public Sub saveinspectiondetails()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
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
                    loadlength()
                    bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                    thaanqtytotal = row.Cells(15).Text
                    ameters = TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text
                    bmeters = TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text
                    cmeters = TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text
                    dmeters = TryCast(row.Cells(19).FindControl("txtthaanmtrsd"), Label).Text
                    thaanmtrstotal = row.Cells(20).Text
                    tapelength = TryCast(row.Cells(21).FindControl("txttapelength"), TextBox).Text
                    setno = row.Cells(22).Text
                    beamno = row.Cells(23).Text
                    loomno = row.Cells(24).Text
                    weight = TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text

                    con.Open()
                    Dim cmd As New SqlCommand("INSERT INTO SUBGRNINSPECTION([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUBGRNDOCENTRY],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[GRNQTY],[GRNMTRS],[LENGTH],[WIDTH],[REEDSPACE],[PICK],[SIZINGCODE],[EQUIPMENT],[CATEGORY],[SUBCATEGORY],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[THRONEREEDSPACE],[THRONEPICK],[AQTY],[BQTY],[CQTY],[BITQTY],[TOTALQTY],[AMTRS],[BMTRS],[CMTRS],[BITMTRS],[TOTALMTRS],[TAPELENGTH],[SETNO],[BEAMNO],[LOOMNO],[WEIGHT],[THAANBATCH],[USER])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUBGRNDOCENTRY,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@GRNQTY,@GRNMTRS,@LENGTH,@WIDTH,@REEDSPACE,@PICK,@SIZINGCODE,@EQUIPMENT,@CATEGORY,@SUBCATEGORY,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@THRONEREEDSPACE,@THRONEPICK,@AQTY,@BQTY,@CQTY,@BITQTY,@TOTALQTY,@AMTRS,@BMTRS,@CMTRS,@BITMTRS,@TOTALMTRS,@TAPELENGTH,@SETNO,@BEAMNO,@LOOMNO,@WEIGHT,@THAANBATCH,@USER)", con)
                    cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
                    cmd.Parameters.AddWithValue("SUBGRNNO", txtgrn.Text)
                    cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", GRNDOCENTRY)
                    cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
                    cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
                    cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
                    cmd.Parameters.AddWithValue("ITEMCODE", itemcode)
                    cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
                    cmd.Parameters.AddWithValue("GRNQTY", lblgrnqty.Text)
                    cmd.Parameters.AddWithValue("GRNMTRS", lblgrnmeters.Text)
                    cmd.Parameters.AddWithValue("LENGTH", lbllength.Text)
                    cmd.Parameters.AddWithValue("WIDTH", lblwidth.Text)
                    cmd.Parameters.AddWithValue("REEDSPACE", lblreedspace.Text)
                    cmd.Parameters.AddWithValue("PICK", lblpick.Text)
                    cmd.Parameters.AddWithValue("SIZINGCODE", lblsizecode.Text)
                    cmd.Parameters.AddWithValue("EQUIPMENT", lblequipment.Text)
                    cmd.Parameters.AddWithValue("CATEGORY", lblcategory.Text)
                    cmd.Parameters.AddWithValue("SUBCATEGORY", lblsubcategory.Text)
                    cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
                    cmd.Parameters.AddWithValue("AUTONUM", autonumber)
                    cmd.Parameters.AddWithValue("THORNEQTY", gridgrnqty)
                    cmd.Parameters.AddWithValue("THORNEMTRS", gridgrnmtrs)
                    cmd.Parameters.AddWithValue("THRONECOLOR", color)
                    cmd.Parameters.AddWithValue("THRONESIZE", length.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEWIDTH", width.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEREEDSPACE", reedspace.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEPICK", pick.ToString("F2"))
                    If color = "KADA" Then
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
                    cmd.Parameters.AddWithValue("THAANBATCH", String.Concat(lblvencode.Text & "|" & txtgrn.Text & "|" & itemcode & "|" & autonumber & "|" & thaanqtytotal & "|" & thaanmtrstotal & "|" & color))
                    cmd.Parameters.AddWithValue("USER", lblusername.Text.ToUpper)
                    cmd.ExecuteNonQuery()
                    lblmsg.BackColor = System.Drawing.Color.White
                    lblmsg.ForeColor = System.Drawing.Color.Green
                    lblmsg.Visible = True
                    lblmsg.Text = "Details Saved Successfully"

                End If
                con.Close()
            End If
        Next
        loadpage()
    End Sub
    Public Sub saveextrathaandetails()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                autonumber = row.Cells(2).Text
                gridgrnqty = row.Cells(3).Text
                itemcode = TryCast(row.Cells(4).FindControl("txtitemcode"), TextBox).Text
                gridgrnmtrs = row.Cells(5).Text
                color = row.Cells(6).Text
                length = TryCast(row.Cells(7).FindControl("txtlength"), TextBox).Text
                width = TryCast(row.Cells(8).FindControl("txtwidth"), TextBox).Text
                reedspace = TryCast(row.Cells(9).FindControl("txtreed"), TextBox).Text
                pick = TryCast(row.Cells(10).FindControl("txtpick"), TextBox).Text
                aquantity = TryCast(row.Cells(11).FindControl("txtAQuantity"), TextBox).Text
                bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                If aquantity <> "0.00" Or bquantity <> "0.00" Or cquantity <> "0.00" Then
                    loadlength()
                    bquantity = TryCast(row.Cells(12).FindControl("txtBQuantity"), TextBox).Text
                    cquantity = TryCast(row.Cells(13).FindControl("txtCQuantity"), TextBox).Text
                    dquantity = TryCast(row.Cells(14).FindControl("txtDQuantity"), TextBox).Text
                    thaanqtytotal = row.Cells(15).Text
                    ameters = TryCast(row.Cells(16).FindControl("txtthaanmtrsa"), Label).Text
                    bmeters = TryCast(row.Cells(17).FindControl("txtthaanmtrsb"), Label).Text
                    cmeters = TryCast(row.Cells(18).FindControl("txtthaanmtrsc"), Label).Text
                    dmeters = TryCast(row.Cells(19).FindControl("txtthaanmtrsd"), Label).Text
                    thaanmtrstotal = row.Cells(20).Text
                    tapelength = TryCast(row.Cells(21).FindControl("txttapelength"), TextBox).Text
                    setno = row.Cells(22).Text
                    beamno = row.Cells(23).Text
                    loomno = row.Cells(24).Text
                    weight = TryCast(row.Cells(25).FindControl("txtweight"), TextBox).Text

                    con.Open()
                    Dim cmd As New SqlCommand("INSERT INTO [SUBGRNEXTRATHAAN]([DOCNUM],[SUBGRNNO],[SUBGRNDATE],[SUBGRNDOCENTRY],[SUPPDC],[SUPPDATE],[VENCODE],[VENNAME],[ITEMCODE],[ITEMNAME],[GRNQTY],[GRNMTRS],[LENGTH],[WIDTH],[REEDSPACE],[PICK],[SIZINGCODE],[EQUIPMENT],[CATEGORY],[SUBCATEGORY],[BATCHNO],[AUTONUM],[THORNEQTY],[THORNEMTRS],[THRONECOLOR],[THRONESIZE],[THRONEWIDTH],[THRONEREEDSPACE],[THRONEPICK],[AQTY],[BQTY],[CQTY],[BITQTY],[TOTALQTY],[AMTRS],[BMTRS],[CMTRS],[BITMTRS],[TOTALMTRS],[TAPELENGTH],[SETNO],[BEAMNO],[LOOMNO],[WEIGHT],[THAANBATCH],[USER])  VALUES (@DOCNUM,@SUBGRNNO,@SUBGRNDATE,@SUBGRNDOCENTRY,@SUPPDC,@SUPPDATE,@VENCODE,@VENNAME,@ITEMCODE,@ITEMNAME,@GRNQTY,@GRNMTRS,@LENGTH,@WIDTH,@REEDSPACE,@PICK,@SIZINGCODE,@EQUIPMENT,@CATEGORY,@SUBCATEGORY,@BATCHNO,@AUTONUM,@THORNEQTY,@THORNEMTRS,@THRONECOLOR,@THRONESIZE,@THRONEWIDTH,@THRONEREEDSPACE,@THRONEPICK,@AQTY,@BQTY,@CQTY,@BITQTY,@TOTALQTY,@AMTRS,@BMTRS,@CMTRS,@BITMTRS,@TOTALMTRS,@TAPELENGTH,@SETNO,@BEAMNO,@LOOMNO,@WEIGHT,@THAANBATCH,@USER)", con)
                    cmd.Parameters.AddWithValue("DOCNUM", lbldocnum.Text)
                    cmd.Parameters.AddWithValue("SUBGRNNO", txtgrn.Text)
                    cmd.Parameters.AddWithValue("SUBGRNDATE", grndate.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", GRNDOCENTRY)
                    cmd.Parameters.AddWithValue("SUPPDC", lbldcno.Text)
                    cmd.Parameters.AddWithValue("SUPPDATE", dcdate.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("VENCODE", lblvencode.Text)
                    cmd.Parameters.AddWithValue("VENNAME", lblvenname.Text)
                    cmd.Parameters.AddWithValue("ITEMCODE", itemcode)
                    cmd.Parameters.AddWithValue("ITEMNAME", lblitemname.Text)
                    cmd.Parameters.AddWithValue("GRNQTY", lblgrnqty.Text)
                    cmd.Parameters.AddWithValue("GRNMTRS", lblgrnmeters.Text)
                    cmd.Parameters.AddWithValue("LENGTH", lbllength.Text)
                    cmd.Parameters.AddWithValue("WIDTH", lblwidth.Text)
                    cmd.Parameters.AddWithValue("REEDSPACE", lblreedspace.Text)
                    cmd.Parameters.AddWithValue("PICK", lblpick.Text)
                    cmd.Parameters.AddWithValue("SIZINGCODE", lblsizecode.Text)
                    cmd.Parameters.AddWithValue("EQUIPMENT", lblequipment.Text)
                    cmd.Parameters.AddWithValue("CATEGORY", lblcategory.Text)
                    cmd.Parameters.AddWithValue("SUBCATEGORY", lblsubcategory.Text)
                    cmd.Parameters.AddWithValue("BATCHNO", lblbatchno.Text)
                    cmd.Parameters.AddWithValue("AUTONUM", autonumber)
                    cmd.Parameters.AddWithValue("THORNEQTY", gridgrnqty)
                    cmd.Parameters.AddWithValue("THORNEMTRS", gridgrnmtrs)
                    cmd.Parameters.AddWithValue("THRONECOLOR", color)
                    cmd.Parameters.AddWithValue("THRONESIZE", length.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEWIDTH", width.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEREEDSPACE", reedspace.ToString("F2"))
                    cmd.Parameters.AddWithValue("THRONEPICK", pick.ToString("F2"))
                    cmd.Parameters.AddWithValue("AQTY", aquantity)
                    cmd.Parameters.AddWithValue("BQTY", bquantity)
                    cmd.Parameters.AddWithValue("CQTY", cquantity)
                    cmd.Parameters.AddWithValue("BITQTY", dquantity)
                    cmd.Parameters.AddWithValue("TOTALQTY", thaanqtytotal)
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
                    cmd.Parameters.AddWithValue("THAANBATCH", String.Concat(lblvencode.Text & "|" & txtgrn.Text & "|" & itemcode & "|" & autonumber & "|" & thaanqtytotal & "|" & thaanmtrstotal & "|" & color))
                    cmd.Parameters.AddWithValue("USER", lblusername.Text.ToUpper)
                    cmd.ExecuteNonQuery()
                    lblmsg.BackColor = System.Drawing.Color.White
                    lblmsg.ForeColor = System.Drawing.Color.Green
                    lblmsg.Visible = True
                    lblmsg.Text = "Details Saved Successfully"

                End If
                con.Close()
            End If
        Next

    End Sub
End Class