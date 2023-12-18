Imports System.Data.SqlClient
Imports System.IO
Public Class errordetail
    Inherits System.Web.UI.Page
    Dim GRNNO, GRNDOCENTRY As String
    Dim GRNDATE As DateTime
    Dim bquantity, cquantity, totalquantity, totalmeters, totalerrorquantity As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call dbMAIN()
        GRNNO = Session("GRNNO")
        GRNDATE = Session("GRNDATE")
        GRNDOCENTRY = Session("GRNDOCENTRY")
        If Not IsPostBack Then
            loadautonum()
            loadgrid()
        End If
        btnexport.Visible = True
    End Sub

    Public Sub loadautonum()
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim da5 As New SqlDataAdapter, ds5 As New DataSet
        da5.SelectCommand = New SqlCommand
        da5.SelectCommand.Connection = con
        da5.SelectCommand.CommandType = CommandType.Text
        da5.SelectCommand.CommandText = "SELECT distinct autonum from subgrninspection where subgrnno ='" & GRNNO & "' group by autonum"
        da5.Fill(ds5, "tbl2")
        cmbautonum.DataSource = ds5.Tables("tbl2")
        cmbautonum.DataTextField = "autonum"
        cmbautonum.DataValueField = "autonum"
        cmbautonum.DataBind()
        con.Close()
        cmbautonum.Items.Insert(0, New ListItem("--Select Thaanno--", "0"))

    End Sub

    Private Sub cmbautonum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbautonum.SelectedIndexChanged
        lblautonum.Text = Trim(cmbautonum.SelectedItem.Text)
        lblmsg.Text = ""
        con.Open()
        Dim sql1 As String = "Select bqty,cqty,loomno from subgrninspection where autonum = '" & lblautonum.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblbquantity.Text = dt1.Rows(0)("bqty").ToString()
            lblcquantity.Text = dt1.Rows(0)("cqty").ToString()
            lblloomno.Text = dt1.Rows(0)("loomno").ToString()
        End If
        con.Close()
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim sql1 As String = "select isnull(sum(ERRORQTY),0) as [totalquantity] from SUBGRNERRORENTRY where subgrnno = '" & GRNNO & "' AND AUTONUM = '" & cmbautonum.SelectedItem.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            totalquantity = dt1.Rows(0)(0).ToString()
            lbltotqty.Text = totalquantity.ToString("F2")
            totalerrorquantity = totalquantity + Val(txterrorqty.Text)
        End If
        If totalerrorquantity > (Val(lblbquantity.Text) + Val(lblcquantity.Text)) Then
            lblmsg.Text = "Quantity Exceeds...Kindly check"
        Else
            savedetails()
        End If

    End Sub
    Public Sub savedetails()
        con.Open()
        Dim cmd As New SqlCommand("INSERT INTO SUBGRNERRORENTRY([SUBGRNNO],[SUBGRNDATE],[SUBGRNDOCENTRY],[AUTONUM],[BQTY],[CQTY],[ERRORCATEGORY],[ERRORTYPE],[ERROR],[ERRORQTY])  VALUES (@SUBGRNNO,@SUBGRNDATE,@SUBGRNDOCENTRY,@AUTONUM,@BQTY,@CQTY,@ERRORCATEGORY,@ERRORTYPE,@ERROR,@ERRORQTY)", con)
        cmd.Parameters.AddWithValue("SUBGRNNO", GRNNO)
        cmd.Parameters.AddWithValue("SUBGRNDATE", GRNDATE.ToString("yyyy/MM/dd"))
        cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", GRNDOCENTRY)
        cmd.Parameters.AddWithValue("AUTONUM", cmbautonum.SelectedItem.Text)
        cmd.Parameters.AddWithValue("BQTY", lblbquantity.Text)
        cmd.Parameters.AddWithValue("CQTY", lblcquantity.Text)
        cmd.Parameters.AddWithValue("ERRORCATEGORY", cmberrorcategory.SelectedItem.Text)
        cmd.Parameters.AddWithValue("ERRORTYPE", cmberrortype.SelectedItem.Text)
        cmd.Parameters.AddWithValue("ERROR", cmberror.SelectedItem.Text)
        cmd.Parameters.AddWithValue("ERRORQTY", txterrorqty.Text)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Visible = True
        lblmsg.Text = "Error Details Saved Successfully"
        loadgrid()
        clear()
    End Sub

    Private Sub cmberrortype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmberrortype.SelectedIndexChanged
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim da5 As New SqlDataAdapter, ds5 As New DataSet
        da5.SelectCommand = New SqlCommand
        da5.SelectCommand.Connection = con
        da5.SelectCommand.CommandType = CommandType.Text
        da5.SelectCommand.CommandText = "SELECT distinct U_ResnCode  FROM [@INT_INW3] WHERE U_UniqID =1 AND U_ResnCode <>'' and U_Type ='" & cmberrortype.SelectedItem.Text & "'  AND U_Category = '" & cmberrorcategory.SelectedItem.Text & "' "
        da5.Fill(ds5, "tbl2")
        cmberror.DataSource = ds5.Tables("tbl2")
        cmberror.DataTextField = "U_ResnCode"
        cmberror.DataValueField = "U_ResnCode"
        cmberror.DataBind()
        con.Close()
        cmberror.Items.Insert(0, New ListItem("--Select Error--", "0"))
    End Sub
    Public Sub loadquantity()
        lblautonum.Text = Trim(cmbautonum.Text)
        con.Open()
        Dim sql1 As String = "select bqty,cqty from subgrninspection where autonum = '" & lblautonum.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblbquantity.Text = dt1.Rows(0)("bqty").ToString()
            lblcquantity.Text = dt1.Rows(0)("cqty").ToString()
        End If
        con.Close()
    End Sub

    Public Sub loadgrid()
        GridView2.Visible = True
        Dim sql1 As String = "SELECT AUTONUM,BQTY,CQTY,ERRORCATEGORY,ERRORTYPE,ERROR,ERRORQTY FROM SUBGRNERRORENTRY where subgrnno = '" & GRNNO & "' ORDER BY DOCENTRY"
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
            gettotalquantity()
        Else
            GridView2.DataSource = Nothing
            GridView2.DataBind()
        End If

        'gettotalmeters()
    End Sub
    Protected Sub CheckBox_Changed(sender As Object, e As EventArgs)
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As RadioButton = TryCast(row.Cells(0).FindControl("chkRow"), RadioButton)
                If chkRow.Checked = True Then
                    btnupdate.Visible = True
                    btndelete.Visible = True
                    cmbautonum.Text = row.Cells(3).Text
                    lblbquantity.Text = row.Cells(4).Text
                    lblcquantity.Text = row.Cells(5).Text
                    cmberrorcategory.Text = row.Cells(6).Text
                    cmberrortype.Text = row.Cells(7).Text
                    cmberror.Text = row.Cells(8).Text
                    txterrorqty.Text = row.Cells(9).Text
                    lblmsg.Text = ""
                End If
            End If
        Next
    End Sub
    Public Sub gettotalquantity()

        lbl1.Visible = True
        Dim sql1 As String = "select isnull(sum(ERRORQTY),0) as [totalquantity] from SUBGRNERRORENTRY where subgrnno = '" & GRNNO & "'"
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



    'Public Sub gettotalmeters()

    '    lbl2.Visible = True
    '    Dim sql1 As String = "Select isnull(sum(thornemtrs),0) As [totalqyantity] from subgrnbeam where subgrnno = '" & txtgrn.Text & "' "
    '    Dim cmd1 As New SqlCommand(sql1, con)
    '    Dim dt1 As New DataTable()
    '    Dim ad1 As New SqlDataAdapter(cmd1)
    '    ad1.Fill(dt1)
    '    If dt1.Rows.Count > 0 Then
    '        totalquantity = dt1.Rows(0)(0).ToString()
    '        lbltotmtrs.Text = totalquantity.ToString("F2")
    '    Else
    '        totalquantity = 0
    '    End If
    'End Sub
    Public Sub clear()
        txterrorqty.Text = ""
        cmberrorcategory.SelectedIndex = 0
        cmberrortype.SelectedIndex = 0
        cmberror.SelectedIndex = 0
        cmbautonum.SelectedIndex = 0
        lblbquantity.Text = ""
        lblcquantity.Text = ""
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
            GridView2.DataSource = Nothing
            GridView2.DataBind()
            GridView2.Visible = False
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
End Class