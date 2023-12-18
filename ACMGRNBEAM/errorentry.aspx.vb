Imports System.Data.SqlClient
Imports System.IO
Public Class errorentry
    Inherits System.Web.UI.Page
    Dim GRNNO, GRNDOCENTRY As String
    Dim GRNDATE As DateTime
    Dim bquantity, cquantity, totalquantity, totalmeters, totalerrorquantity, errorqty As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call dbMAIN()
        GRNNO = Session("GRNNO")
        GRNDATE = Session("GRNDATE")
        GRNDOCENTRY = Session("GRNDOCENTRY")
        If Not IsPostBack Then
            loadautonum()
            loaderror()
            loadgrid()
            gettotalquantity()
        End If
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

    Public Sub loaderror()
        Dim sql4 As String = "select distinct errorname from subgrnerrorlist group by errorid,errorname"
        Dim cmd4 As New SqlCommand(sql4, con)
        Dim dt4 As New DataTable()
        Dim ad4 As New SqlDataAdapter(cmd4)
        ad4.Fill(dt4)
        If dt4.Rows.Count > 0 Then
            GridView2.DataSource = dt4
            GridView2.DataBind()
        End If
    End Sub



    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub

    Private Sub cmbautonum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbautonum.SelectedIndexChanged
        lblmsg.Text = ""
        con.Open()
        Dim sql1 As String = "Select bqty,cqty,loomno from subgrninspection where autonum = '" & cmbautonum.SelectedItem.Text & "'"
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
    Public Sub savedetails()
        con.Open()
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                errorqty = TryCast(row.Cells(2).FindControl("txterrorqty"), TextBox).Text
                Dim errordesc As String = row.Cells(1).Text
                If errorqty <> "0.00" Then
                    Dim cmd As New SqlCommand("INSERT INTO SUBGRNERRORENTRY([SUBGRNNO],[SUBGRNDATE],[SUBGRNDOCENTRY],[AUTONUM],[BQTY],[CQTY],[ERRORCATEGORY],[ERROR],[ERRORQTY])  VALUES (@SUBGRNNO,@SUBGRNDATE,@SUBGRNDOCENTRY,@AUTONUM,@BQTY,@CQTY,@ERRORCATEGORY,@ERROR,@ERRORQTY)", con)
                    cmd.Parameters.AddWithValue("SUBGRNNO", GRNNO)
                    cmd.Parameters.AddWithValue("SUBGRNDATE", GRNDATE.ToString("yyyy/MM/dd"))
                    cmd.Parameters.AddWithValue("SUBGRNDOCENTRY", GRNDOCENTRY)
                    cmd.Parameters.AddWithValue("AUTONUM", cmbautonum.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("BQTY", lblbquantity.Text)
                    cmd.Parameters.AddWithValue("CQTY", lblcquantity.Text)
                    cmd.Parameters.AddWithValue("ERRORCATEGORY", cmbqtytype.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("ERROR", errordesc)
                    cmd.Parameters.AddWithValue("ERRORQTY", errorqty)
                    cmd.ExecuteNonQuery()
                    lblmsg.BackColor = System.Drawing.Color.White
                    lblmsg.ForeColor = System.Drawing.Color.Green
                    lblmsg.Visible = True
                    lblmsg.Text = "Error Details Saved Successfully"
                End If
            End If
        Next
        clearline()
        loadgrid()
        gettotalquantity()
    End Sub
    Public Sub loadgrid()
        GridView1.Visible = True
        Dim sql1 As String = "SELECT AUTONUM,BQTY,CQTY,ERRORCATEGORY,ERRORTYPE,ERROR,ERRORQTY FROM SUBGRNERRORENTRY where subgrnno = '" & GRNNO & "' ORDER BY DOCENTRY"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView1.DataSource = dt2
            GridView1.DataBind()
            empwagepanel.Visible = True
        End If
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        savedetails()
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
    Public Sub clearline()
        For Each row As GridViewRow In GridView2.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                errorqty = TryCast(row.Cells(2).FindControl("txterrorqty"), TextBox).Text
                If chkRow.Checked = True Then
                    chkRow.Checked = False
                    TryCast(row.Cells(2).FindControl("txterrorqty"), TextBox).Text = "0.00"
                End If
            End If
        Next
        cmbautonum.SelectedIndex = 0
        cmbqtytype.SelectedIndex = 0
        lblbquantity.Text = ""
        lblcquantity.Text = ""
        lblloomno.Text = ""
    End Sub
    Private Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        clearline()
    End Sub
End Class