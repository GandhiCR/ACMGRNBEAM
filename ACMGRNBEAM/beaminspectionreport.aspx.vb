Imports System.Data.SqlClient
Imports System.IO
Public Class beaminspectionreport
    Inherits System.Web.UI.Page
    Dim totalvendors, totalitems, totalquantity, totalamount, totalmeters As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles datepicker1.SelectionChanged
        txtfromdate.Text = datepicker1.SelectedDate.ToString("yyyy-MM-dd")
        datepicker1.Visible = False
    End Sub

    Protected Sub lnkpickdate1_Click(sender As Object, e As EventArgs) Handles lnkpickdate1.Click
        datepicker1.Visible = True
    End Sub

    Private Sub datepicker2_SelectionChanged(sender As Object, e As EventArgs) Handles datepicker2.SelectionChanged
        txttodate.Text = datepicker2.SelectedDate.ToString("yyyy-MM-dd")
        datepicker2.Visible = False
    End Sub

    Private Sub lnkpickdate2_Click(sender As Object, e As EventArgs) Handles lnkpickdate2.Click
        datepicker2.Visible = True
    End Sub
    'Public Sub loadvendor()
    '    Dim da5 As New SqlDataAdapter, ds5 As New DataSet
    '    da5.SelectCommand = New SqlCommand
    '    da5.SelectCommand.Connection = con
    '    da5.SelectCommand.CommandType = CommandType.Text
    '    da5.SelectCommand.CommandText = "SELECT distinct venname from  subgrnbeam  order by venname asc"
    '    da5.Fill(ds5, "tbl2")
    '    cmbvendor.DataSource = ds5.Tables("tbl2")
    '    cmbvendor.DataTextField = "venname"
    '    cmbvendor.DataValueField = "venname"
    '    cmbvendor.DataBind()
    '    cmbvendor.Items.Insert(0, New ListItem("-Select Vendor-", "0"))
    'End Sub
    Public Sub grnwise()
        invgrid.Visible = True
        'summarygrid.Visible = True
        con.Open()
        'Dim sql1 As String = "SELECT PCENTRY AS [PCE ENTRTY],CONVERT(VARCHAR,DOCDATE,103) AS [DATE],OPERATION AS [OPERATION],PRODUCTDESC AS [PRODUCT],LINE AS [LINE NO],ID AS [EMP. ID],NAME AS [EMP. NAME],JOBROLL AS [EMPLOYEE TYPE],PROCESS AS [PROCESS],QUANTITY AS [QUANTITY], RATE AS [WAGES RATE],TARGET AS [TARGET], AMOUNT AS [AMOUNT],[USER] AS [USER] FROM EMPWAGES WHERE DOCDATE BETWEEN '" & txtfromdate.Text & "' and  '" & txttodate.Text & "' ORDER BY PCENTRY ASC"
        Dim sql1 As String = "SELECT SUBGRNNO AS [GRN NUMBER],CONVERT(VARCHAR(30),DOCDATE,105) AS [CREATED DATE],VENNAME AS [VENDOR NAME],ITEMCODE AS [ITEM CODE],GRNQTY AS [GRN QTY.],GRNMTRS AS [GRN MTRS.],BATCHNO AS [BATCH],AUTONUM AS [THAAN NO.],AQTY AS [A QTY],AMTRS AS [A METERS],BQTY AS [B QTY],BMTRS AS [B METERS],CQTY  AS [C QTY],CMTRS AS [C METERS],BITQTY AS [BIT QTY],BITMTRS AS [BIT MTRS],[TOTALMTRS],[TAPELENGTH],[SETNO],[BEAMNO],[LOOMNO],[WEIGHT]  FROM SUBGRNINSPECTION WHERE SUBGRNNO = '" & txtgrnno.Text & "' ORDER BY DOCENTRY ASC"

        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            invgrid.DataSource = dt1
            invgrid.DataBind()
            Dim query = invgrid.Rows.Cast(Of GridViewRow).
           GroupBy(Function(g) New With {Key .Color = g.Cells(14).Text}).
           Select(Function(group) New With {
                                     group.Key.Color,
                                     .Totalthaan = group.Count,
                              .Quantity = group.Sum(Function(a) a.Cells(12).Text),
                             .Meters = group.Sum(Function(a) Convert.ToDecimal(a.Cells(13).Text))
                             })

            summarygrid.DataSource = query
            summarygrid.DataBind()

            lblmsg.Text = ""
            Dim totalqty, totalmtrs As Decimal
            'lblsummary.Visible = True
            'lblsummary.Text = "Colorwise Summary"
            For Each row As GridViewRow In invgrid.Rows
                Dim qty As Decimal = Convert.ToDecimal(row.Cells(12).Text)
                Dim mtrs As Decimal = Convert.ToDecimal(row.Cells(13).Text)
                totalqty += qty
                totalmtrs += mtrs
            Next
            invgrid.FooterRow.Cells(11).Text = "Total"
            invgrid.FooterRow.Cells(12).HorizontalAlign = HorizontalAlign.Right
            invgrid.FooterRow.Cells(12).Text = totalqty.ToString("F2")
            invgrid.FooterRow.Cells(13).HorizontalAlign = HorizontalAlign.Right
            invgrid.FooterRow.Cells(13).Text = totalmtrs.ToString("F2")


            showfooter()
        Else
            invgrid.DataSource = Nothing
            invgrid.DataBind()
            lblmsg.Text = "No Data Found"
        End If
        con.Close()



    End Sub
    Public Sub datewise()
        invgrid.Visible = True
        con.Open()
        'Dim sql1 As String = "SELECT A.SUBGRNNO as [GRN Number],A.SUBGRNDATE as [GRN Date],A.VENCODE AS [Vendor Code],a.VENNAME as [Vendor_Name],a.ITEMCODE as [Item Code],a.GRNQTY as [Quantity],a.GRNMTRS as [Mtrs],a.BATCHNO as [Batch],a.AUTONUM as [Thaan No.],a.AQTY as [A QTY],a.AMTRS as [A Meters],a.BQTY as [B QTY],a.BMTRS as [B Meters],a.CQTY  as [C QTY],a.CMTRS as [C Meters],a.BITQTY AS [BIT QTY],a.BITMTRS AS [BIT MTRS],a.Totalmtrs as [Total Mtrs],b.U_TapeLength as [ACM TapeLength],a.Tapelength as [Actual Tapelength],a.SETNO AS [Set No],a.BEAMNO AS [Beam No],a.Loomno AS [Loom No],a.THRONECOLOR as [Color],a.Weight  FROM SUBGRNINSPECTION A inner join OITM  B on a.ITEMCODE = b.ItemCode WHERE DOCDATE BETWEEN '" & txtfromdate.Text & "' AND  '" & txttodate.Text & "' ORDER BY A.DOCENTRY  ASC"
        Dim sql1 As String = "EXEC [dbo].[Subgrninspectionselect] @FDATE = '" & txtfromdate.Text & "', @TDATE = '" & txttodate.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            invgrid.DataSource = dt1
            invgrid.DataBind()
            lblmsg.Text = ""
        Else
            invgrid.DataSource = Nothing
            invgrid.DataBind()
            lblmsg.Text = "No Data Found"
        End If
        con.Close()

    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        datewise()
        lblopeninvoice.Text = "THAAN DETAILS FROM " & "    " & datepicker1.SelectedDate.ToString("dd-MM-yyyy") & "    " & "to" & "     " & datepicker2.SelectedDate.ToString("dd-MM-yyyy")
    End Sub




    Public Sub showfooter()
        Dim totalqty, totalmtrs As Decimal
        Dim totalthaan As Integer
        For Each row As GridViewRow In summarygrid.Rows
            Dim thaan As Integer = Convert.ToDecimal(row.Cells(2).Text)
            Dim qty As Decimal = Convert.ToDecimal(row.Cells(3).Text)
            Dim mtrs As Decimal = Convert.ToDecimal(row.Cells(4).Text)
            totalthaan += thaan
            totalqty += qty
            totalmtrs += mtrs
        Next
        summarygrid.FooterRow.Cells(1).Text = "Total"
        summarygrid.FooterRow.Cells(2).Text = totalthaan.ToString()
        summarygrid.FooterRow.Cells(3).HorizontalAlign = HorizontalAlign.Right
        summarygrid.FooterRow.Cells(3).Text = totalqty.ToString("F2")
        summarygrid.FooterRow.Cells(4).HorizontalAlign = HorizontalAlign.Right
        summarygrid.FooterRow.Cells(4).Text = totalmtrs.ToString("F2")
    End Sub
    Public Sub showempfooter()
        Dim total As Decimal = 0
        For Each row As GridViewRow In invgrid.Rows
            Dim price As Decimal = Convert.ToDecimal(row.Cells(12).Text)
            total += price
        Next
        invgrid.FooterRow.Cells(10).Text = "Total"
        invgrid.FooterRow.Cells(12).HorizontalAlign = HorizontalAlign.Right
        invgrid.FooterRow.Cells(12).Text = Math.Round(total).ToString("F2")
    End Sub
    Public Sub INSPECTIONEXPORTEXCEL()
        'Response.Clear()
        'Response.Buffer = True
        'Response.ClearContent()
        'Response.ClearHeaders()
        'Response.Charset = ""
        'Dim FileName As String = "INSPECTION REPORT FROM" & "  " & txtfromdate.Text & "  " & "TO" & txttodate.Text & ".xls"
        'Dim strwritter As StringWriter = New StringWriter()
        'Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
        'invgrid.RenderControl(htmltextwrtter)
        'Response.Write(strwritter.ToString())
        'Response.End()

        Response.Clear()
        Dim strwritter As StringWriter = New StringWriter()
        Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
        Response.Buffer = True

        Dim FileName As String = "FROM" & "  " & txtfromdate.Text & "  " & "TO" & txttodate.Text & ".xls"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        PrepareForExport(invgrid)

        For Each item As GridViewRow In invgrid.Rows
            For i As Integer = 0 To item.Cells.Count - 1
                item.Cells(i).Attributes.Add("style", "mso-number-format:\@")
            Next
        Next

        Dim tb As New Table()
        Dim tr1 As New TableRow()
        Dim cell1 As New TableCell()
        cell1.Controls.Add(invgrid)
        tr1.Cells.Add(cell1)
        Dim cell2 As New TableCell()
        cell2.Text = "&nbsp;"
        ' If rbPreference.SelectedValue = "2" Then
        tr1.Cells.Add(cell2)
        tb.Rows.Add(tr1)
        tb.RenderControl(htmltextwrtter)
        'style to format numbers to string
        Dim style As String = "<style> .textmode { } </style>"
        Response.Write(strwritter.ToString())
        Response.Flush()
        Response.[End]()

        invgrid.GridLines = GridLines.Both
        invgrid.HeaderStyle.Font.Bold = True


    End Sub


    Protected Sub PrepareForExport(ByVal Gridview As GridView)
        'Change the Header Row back to white color
        Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF")
        'Apply style to Individual Cells


        For k As Integer = 0 To Gridview.HeaderRow.Cells.Count - 1
            Gridview.HeaderRow.Cells(k).Style.Add("background-color", "Bisque")
        Next
        For i As Integer = 0 To Gridview.Rows.Count - 1
            Dim row As GridViewRow = Gridview.Rows(i)
            'Change Color back to white
            row.BackColor = System.Drawing.Color.White
            'Apply text style to each Row
            'row.Attributes.Add("class", "textmode")
            'Apply style to Individual Cells of Alternating Row
            If i Mod 2 <> 0 Then
                For j As Integer = 0 To Gridview.Rows(i).Cells.Count - 1
                    row.Cells(j).Style.Add("background-color", "")
                Next
            End If
        Next

    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
    Private Sub btnexportexcel_Click(sender As Object, e As EventArgs) Handles btnexportexcel.Click
        INSPECTIONEXPORTEXCEL()
    End Sub

    Function NumberInWords(num As String)
        'Constants are Defined
        Dim digit(100) As String
        digit(0) = ""
        digit(1) = "One "
        digit(2) = "Two "
        digit(3) = "Three "
        digit(4) = "Four "
        digit(5) = "Five "
        digit(6) = "Six "
        digit(7) = "Seven "
        digit(8) = "Eight "
        digit(9) = "Nine "
        digit(10) = "Ten "
        digit(11) = "Eleven "
        digit(12) = "Twelve "
        digit(13) = "Thirteen "
        digit(14) = "Fourteen "
        digit(15) = "Fifteen "
        digit(16) = "Sixteen "
        digit(17) = "Seventeen "
        digit(18) = "Eighteen "
        digit(19) = "Ninteen "
        digit(20) = "Twenty "
        digit(30) = "Thirty "
        digit(40) = "Fourty "
        digit(50) = "Fifty "
        digit(60) = "Sixty "
        digit(70) = "Seventy "
        digit(80) = "Eighty "
        digit(90) = "Ninty "
        digit(100) = "Hundred "
        Dim tt(5) As String
        tt(2) = "Thousand "
        tt(3) = "Lakh "
        tt(4) = "Crore "
        tt(5) = "Hundred Crore "
        'Separating the Whole Number and Digits
        Dim nn As String
        Dim dd As String = ""
        nn = Math.Round(Val(num), 2)
        If InStr(nn, ".") <> 0 Then
            dd = Mid(nn, InStr(nn, ".") + 1)
            nn = Mid(nn, 1, InStr(nn, ".") - 1)
        End If

        'Variable nn stores the whole number and dd stores the digits
        'Finding the Word for numbers

        Dim x As Integer
        Dim y As Integer = 0
        x = nn.Length - 1
        Dim z As String
        Dim str As String = ""
        Dim str1 As String = ""
        If x > 1 Then
            While (x > -1)
                'First Loop Last two digits of Number is evaluated(ones and Tens)
                If y = 0 Then
                    z = Mid(nn, x, 2)
                    If Val(z) < 21 And Val(z) > 0 Then
                        str = digit(Val(z))
                    ElseIf Val(z) > 0 Then
                        str = digit(Val(z(0)) * 10)
                        str = str & digit(Val(z(1)))
                    End If
                    x = x - 1
                End If


                'Second Loop 3rd digits of Number is evaluated(Hundred)

                If y = 1 Then
                    z = Mid(nn, x, 1)
                    If Val(z) <> 0 Then
                        str = digit(Val(z)) & "Hundred " & str
                    End If
                    x = x - 2
                End If

                'Subsequent Loop Next two digits sequence of Number is evaluated(Thousands,Lakhs,Crore,etc)


                If y > 1 Then
                    If x <> 0 Then
                        z = Mid(nn, x, 2)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = x - 2
                    Else
                        z = Mid(nn, 1, 1)
                        If Val(z) < 21 And Val(z) > 0 Then
                            str = digit(Val(z)) & tt(y) & str
                        ElseIf Val(z) > 0 Then
                            str1 = digit(Val(z(0)) * 10)
                            str = str1 & digit(Val(z(1))) & tt(y) & str
                        End If
                        x = -1
                    End If
                End If
                y = y + 1
            End While
        Else
            If Val(nn) < 21 And Val(nn) > 0 Then
                str = digit(Val(nn))
            ElseIf Val(nn) > 0 Then
                str = digit(Val(nn(0)) * 10)
                str = str & digit(Val(nn(1)))
            End If

            'str = digit(nn)

        End If
        If str = "" Then
            str = "Zero "
        End If
        str = str & "Rupees "

        'Digits are evaluated(Paise)

        If Val(dd) > 0 Then
            If dd.Length = 1 Then
                z = Val(dd) * 10
            Else
                z = dd
            End If

            If Val(z) < 21 And Val(z) > 0 Then
                str = str & "and " & digit(Val(z)) & "Paise"
            ElseIf Val(z) > 0 Then
                str1 = digit(Val(z(0)) * 10)
                str = str & "and " & str1 & digit(Val(z(1))) & "Paise"
            End If
        End If

        'Word string is returned

        NumberInWords = str
    End Function


End Class