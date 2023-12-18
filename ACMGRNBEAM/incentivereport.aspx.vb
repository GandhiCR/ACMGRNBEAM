Imports System.Data.SqlClient
Imports System.IO
Public Class incentivereport
    Inherits System.Web.UI.Page
    Dim totalvendors, totalitems, totalquantity, totalamount, totalmeters As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            loadvendor()
        End If
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
    Public Sub loadvendor()
        Dim da5 As New SqlDataAdapter, ds5 As New DataSet
        da5.SelectCommand = New SqlCommand
        da5.SelectCommand.Connection = con
        da5.SelectCommand.CommandType = CommandType.Text
        da5.SelectCommand.CommandText = "SELECT distinct venname from subgrninspection  order by venname asc"
        da5.Fill(ds5, "tbl2")
        cmbvendor.DataSource = ds5.Tables("tbl2")
        cmbvendor.DataTextField = "venname"
        cmbvendor.DataValueField = "venname"
        cmbvendor.DataBind()
        cmbvendor.Items.Insert(0, New ListItem("-Select Vendor-", "0"))
    End Sub

    Public Sub datewise()
        invgrid.Visible = True
        con.Open()
        Dim sql1 As String = "EXEC [dbo].[Subgrnincentive] @FDATE = '" & txtfromdate.Text & "', @TDATE = '" & txttodate.Text & "', @vendorname ='" & cmbvendor.SelectedItem.Text & "' "
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            invgrid.DataSource = dt1
            invgrid.DataBind()
            lblmsg.Text = ""
            showfooter()
        Else
            invgrid.DataSource = Nothing
            invgrid.DataBind()
            lblmsg.Text = "No Data Found"


        End If
        con.Close()

    End Sub


    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        datewise()
        lblopeninvoice.Text = "INCENTIVE REPORT FROM " & "    " & datepicker1.SelectedDate.ToString("dd-MM-yyyy") & "    " & "to" & "     " & datepicker2.SelectedDate.ToString("dd-MM-yyyy")
    End Sub




    Public Sub showfooter()
        Dim totalamtrs, totalbmtrs, totalcmtrs, totalmtrs As Decimal

        For Each row As GridViewRow In invgrid.Rows
            Dim amtrstotal As Decimal = Convert.ToDecimal(row.Cells(4).Text)
            Dim bmtrstotal As Decimal = Convert.ToDecimal(row.Cells(5).Text)
            Dim cmtrstotal As Decimal = Convert.ToDecimal(row.Cells(6).Text)
            Dim allmtrstotal As Decimal = Convert.ToDecimal(row.Cells(7).Text)
            totalamtrs += amtrstotal
            totalbmtrs += bmtrstotal
            totalcmtrs += cmtrstotal
            totalmtrs += allmtrstotal
            row.Cells(11).HorizontalAlign = HorizontalAlign.Center
            row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            row.Cells(14).HorizontalAlign = HorizontalAlign.Right
        Next

        invgrid.FooterRow.Cells(2).Text = "Total"
        invgrid.FooterRow.Cells(4).Text = totalamtrs
        invgrid.FooterRow.Cells(5).Text = totalbmtrs
        invgrid.FooterRow.Cells(6).Text = totalcmtrs
        invgrid.FooterRow.Cells(7).Text = totalmtrs
        invgrid.FooterRow.Cells(12).HorizontalAlign = HorizontalAlign.Right
        invgrid.FooterRow.Cells(13).HorizontalAlign = HorizontalAlign.Right
        invgrid.FooterRow.Cells(14).HorizontalAlign = HorizontalAlign.Right
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
        Response.Clear()
        Response.Buffer = True
        Response.ClearContent()
        Response.ClearHeaders()
        Response.Charset = ""
        Dim FileName As String = "INSPECTION REPORT FROM" & "  " & txtfromdate.Text & "  " & "TO" & txttodate.Text & ".xls"
        Dim strwritter As StringWriter = New StringWriter()
        Dim htmltextwrtter As HtmlTextWriter = New HtmlTextWriter(strwritter)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName)
        invgrid.RenderControl(htmltextwrtter)
        Response.Write(strwritter.ToString())
        Response.End()
    End Sub
    Public Sub ERROREXPORTEXCEL()
        Response.Clear()
        Response.Buffer = True
        Response.ClearContent()
        Response.ClearHeaders()
        Response.Charset = ""
        Dim FileName1 As String = "ERROR REPORT FROM" & "  " & txtfromdate.Text & "  " & "TO" & txttodate.Text & ".xls"
        Dim strwritter1 As StringWriter = New StringWriter()
        Dim htmltextwrtter1 As HtmlTextWriter = New HtmlTextWriter(strwritter1)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName1)
        summarygrid.RenderControl(htmltextwrtter1)
        Response.Write(strwritter1.ToString())
        Response.End()

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