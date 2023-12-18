
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web

Public Class prdgrade
    Inherits System.Web.UI.Page
    Dim aplusgrade, agrade, bplusgrade, bgrade, cgrade As Decimal
    Dim seqnumber, autonumber, selectedautonumber, color, itemcode, thaanbatchno As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        loadpage()

    End Sub
    Public Sub loadpage()
        Call dbMAIN()

        If Not IsPostBack Then

            Dim sql1 As String = "SELECT distinct u_itemcode from [@INT_SPL1]  where  U_Process  = 'w2' and U_ItemCode  NOT IN (SELECT ITEMCODE FROM SUBGRNPRDGRADE ) group by U_ItemCode"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                GridView1.Visible = True
                GridView1.DataSource = dt2
                GridView1.DataBind()
                GridView1.FooterRow.Visible = False

            End If
        End If
        loadgrid()
    End Sub


    Public Sub savegradedetails()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                itemcode = TryCast(row.Cells(0).FindControl("txtitemcode"), Label).Text
                aplusgrade = TryCast(row.Cells(1).FindControl("txtaplusgrade"), TextBox).Text
                agrade = TryCast(row.Cells(2).FindControl("txtagrade"), TextBox).Text
                bplusgrade = TryCast(row.Cells(3).FindControl("txtbplusgrade"), TextBox).Text
                bgrade = TryCast(row.Cells(4).FindControl("txtbgrade"), TextBox).Text
                cgrade = TryCast(row.Cells(5).FindControl("txtcgrade"), TextBox).Text
                If aplusgrade <> "0.00" Or agrade <> "0.00" Or bplusgrade <> "0.00" Or bgrade <> "0.00" Or cgrade <> "0.00" Then
                    con.Open()
                    Dim cmd As New SqlCommand("INSERT INTO SUBGRNPRDGRADE([itemcode],[aplusgrade],[agrade],[bplusgrade],[bgrade],[cgrade])  VALUES (@itemcode,@aplusgrade,@agrade,@bplusgrade,@bgrade,@cgrade)", con)
                    cmd.Parameters.AddWithValue("itemcode", itemcode)
                    cmd.Parameters.AddWithValue("aplusgrade", aplusgrade)
                    cmd.Parameters.AddWithValue("agrade", agrade)
                    cmd.Parameters.AddWithValue("bplusgrade", bplusgrade)
                    cmd.Parameters.AddWithValue("bgrade", bgrade)
                    cmd.Parameters.AddWithValue("cgrade", cgrade)
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
    Public Sub loadgrid()
        Dim sql1 As String = "SELECT * from subgrnprdgrade order by itemcode asc"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt2 As New DataTable
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt2)
        If dt2.Rows.Count > 0 Then
            GridView2.Visible = True
            GridView2.DataSource = dt2
            GridView2.DataBind()
            GridView2.FooterRow.Visible = False
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        savegradedetails()
    End Sub
End Class