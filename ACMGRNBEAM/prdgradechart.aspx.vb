Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Drawing.Printing
Imports System.IO
Imports Neodynamic.SDK
Imports Neodynamic.SDK.Web
Imports System.Text.RegularExpressions
Public Class prdgradechart
    Inherits System.Web.UI.Page
    Dim grndate, dcdate, autonumdate As DateTime
    Dim lin, n, k As Integer
    Dim dir, mdir, printer, itemcode As String
    Dim builder As New StringBuilder
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Dim docnumber, rowcount, totallooms, docentry As Integer
    Dim seqnumber, autonumber, selectedautonumber As String
    Dim inchperhour, mtrperhour, warpknot, weavedrop, prdmtrperhour, avergdhothi24, avergdhothi20, avergdhothi16, avergdhothi10, avergdhothi6, incen24, incen20, incen16, incen10, incen6 As Decimal
    Dim averg24, averg20, averg16, averg10, averg6 As Integer
    Dim grnquantity, grnmeters, grnlength, totalquantity, grnwidth, totalmeters, reedspace, warpcons, weftcons, pick, length1 As Decimal
    Dim grndocentry As String
    Dim dt2 As DataTable

    Private Sub beamwise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call dbMAIN()
        Dim lblUserName As Label = TryCast(Me.Master.FindControl("lbluserName"), Label)
        lblUserName.Text = lblUserName.Text
        If lblUserName.Text = "ganesh" Or lblUserName.Text = "GANESH" Or lblUserName.Text = "it" Then
            btnapprove.Visible = True
            btnsave.Visible = True
        End If
        loadgrid()
        If Not IsPostBack Then
            'Dim sql1 As String = "SELECT distinct u_itemcode from [@INT_SPL1]  where  U_Process  = 'w2' and U_ItemCode  NOT IN (SELECT ITEMCODE FROM SUBGRNPRDGRADE ) group by U_ItemCode"
            Dim sql1 As String = "SELECT distinct u_itemcode from [@INT_SPL1]  where  U_Process  = 'w2' group by U_ItemCode"
            Dim cmd1 As New SqlCommand(sql1, con)
            Dim dt2 As New DataTable
            Dim ad1 As New SqlDataAdapter(cmd1)
            ad1.Fill(dt2)
            If dt2.Rows.Count > 0 Then
                cmbitemcode.DataTextField = "u_itemcode"
                cmbitemcode.DataValueField = "u_itemcode"
                cmbitemcode.DataSource = dt2
                cmbitemcode.DataBind()
            End If
        End If
    End Sub


    Public Sub loadvalues()
        Dim sql1 As String = "SELECT ISNULL(FIELD1,0) as FIELD1,ISNULL(FIELD2,0) AS FIELD2,ISNULL(FIELD3,0) AS FIELD3 FROM SUBGRNPRDGRADE WHERE ITEMCODE='" & cmbitemcode.SelectedItem.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            cmbmtrperhour.Text = dt1.Rows(0)("FIELD1").ToString().ToUpper
            DropDownList1.Text = dt1.Rows(0)("FIELD2").ToString().ToUpper
            DropDownList2.Text = dt1.Rows(0)("FIELD3").ToString().ToUpper
        Else
            cmbmtrperhour.SelectedIndex = 0
            DropDownList1.SelectedIndex = 0
            DropDownList2.SelectedIndex = 0
        End If

    End Sub
    Public Sub loadwidth()
        Dim sql1 As String = "SELECT SWIDTH1,SLENGTH1,U_REEDSPACE,U_PICK,ISNULL(U_WARPCONS,0) as warpcons,isnUll(U_WEFTCONS,0) as weftcons,U_category,u_subcategory,u_commodityname,itemname FROM OITM WHERE ITEMCODE='" & cmbitemcode.SelectedItem.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblnewcode.Text = cmbitemcode.SelectedItem.Text
            grnwidth = dt1.Rows(0)("swidth1").ToString()
            lblwidth.Text = Math.Round((grnwidth / 2.54))
            length1 = dt1.Rows(0)("slength1").ToString()
            lbllength.Text = length1.ToString("F2")
            pick = dt1.Rows(0)("u_pick").ToString()
            lblpick.Text = pick.ToString("F2")
            lblitemname.Text = dt1.Rows(0)("itemname").ToString()
            warpcons = dt1.Rows(0)("WARPCONS").ToString()
            weftcons = dt1.Rows(0)("WEFTCONS").ToString()
            lblcategory.Text = dt1.Rows(0)("u_category").ToString().ToUpper
            lblsubcategory.Text = dt1.Rows(0)("u_subcategory").ToString().ToUpper
            lblequipment.Text = dt1.Rows(0)("U_CommodityName").ToString().ToUpper
        End If
    End Sub

    Public Sub loadgrid()
        Dim sql1 As String = "SELECT * from subgrnprdgrade order by itemcode asc"
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
    End Sub
    Protected Sub OnRowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        con.Open()
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                itemcode = row.Cells(1).Text
                docentry = row.Cells(7).Text
            End If
        Next
        Dim cmd As New SqlCommand("DELETE FROM subgrnprdgrade WHERE itemcode = @itemcode and docentry = @docentry", con)
        cmd.Parameters.AddWithValue("itemcode", itemcode)
        cmd.Parameters.AddWithValue("docentry", docentry)
        cmd.ExecuteNonQuery()
        con.Close()
        loadgrid()
    End Sub
    Public Sub grnclear()
        lblitemname.Text = ""
        lblnewcode.Text = ""
        lbllength.Text = ""
        lblequipment.Text = ""
        lblcategory.Text = ""
        lblsubcategory.Text = ""
        lblpick.Text = ""
        lblwidth.Text = ""
        contenpanel.Visible = False
        btnclear.Visible = False
    End Sub

    Protected Sub btnclear_Click(sender As Object, e As EventArgs) Handles btnclear.Click
        grnclear()
    End Sub

    Private Sub cmbitemcode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbitemcode.SelectedIndexChanged
        loadvalues()
        loadwidth()
        If cmbmtrperhour.Text <> "0.00" And DropDownList1.Text <> "0.00" And DropDownList2.Text <> "0.00" Then
            calculate()
        Else
            cmbmtrperhour.Focus()
        End If
    End Sub
    Public Sub calculate()
        lblrpm.Text = Val(cmbmtrperhour.Text) * 2
        lblrpmpickhour.Text = Val(lblrpm.Text) * Val(lblrpmminutes.Text)
        lblrpmpickinch.Text = lblpick.Text
        inchperhour = Val(lblrpmpickhour.Text) / Val(lblrpmpickinch.Text)
        lblrpminchhour.Text = inchperhour.ToString("F2")
        mtrperhour = (Val(lblrpminchhour.Text) * 2.54) / 100
        lblmtrperhour.Text = mtrperhour.ToString("F2")
        warpknot = Val(lblmtrperhour.Text) * (Val(DropDownList1.Text) / 100)
        lblwarpknott.Text = warpknot.ToString("F2")
        weavedrop = Val(lblmtrperhour.Text) * (Val(DropDownList2.Text) / 100)
        lblweave.Text = weavedrop.ToString("F2")
        lblprdperhour.Text = Val(lblmtrperhour.Text) - (Val(lblwarpknott.Text) + Val(lblweave.Text))
        lblprd24.Text = Val(lblprdperhour.Text) * Val(lblhour24.Text)
        lblprd20.Text = Val(lblprdperhour.Text) * Val(lblhour20.Text)
        lblprd16.Text = Val(lblprdperhour.Text) * Val(lblhour16.Text)
        lblprd10.Text = Val(lblprdperhour.Text) * Val(lblhour10.Text)
        lblprd6.Text = Val(lblprdperhour.Text) * Val(lblhour6.Text)
        averg24 = Math.Round(Val(lblprd24.Text), 2)
        averg20 = Math.Round(Val(Val(lblprd20.Text)), 2)
        averg16 = Math.Round((Val(lblprd16.Text)), 2)
        averg10 = Math.Round((Val(lblprd10.Text)), 2)
        averg6 = Math.Round((Val(lblprd6.Text)), 2)
        lblaver24.Text = averg24.ToString("F2")
        lblaver20.Text = averg20.ToString("F2")
        lblaver16.Text = averg16.ToString("F2")
        lblaver10.Text = averg10.ToString("F2")
        lblaver6.Text = averg6.ToString("F2")
        avergdhothi24 = Val(lblaver24.Text) / Val(lbllength.Text)
        avergdhothi20 = Val(lblaver20.Text) / Val(lbllength.Text)
        avergdhothi16 = Val(lblaver16.Text) / Val(lbllength.Text)
        avergdhothi10 = Val(lblaver10.Text) / Val(lbllength.Text)
        avergdhothi6 = Val(lblaver6.Text) / Val(lbllength.Text)
        lblavgdhothi24.Text = avergdhothi24.ToString("F2")
        lblavgdhothi20.Text = avergdhothi20.ToString("F2")
        lblavgdhothi16.Text = avergdhothi16.ToString("F2")
        lblavgdhothi10.Text = avergdhothi10.ToString("F2")
        lblavgdhothi6.Text = avergdhothi6.ToString("F2")
        lblincen24.Text = Val(lblaver24.Text) - 1
        lblincen20.Text = Val(lblaver20.Text) - 1
        lblincen16.Text = Val(lblaver16.Text) - 1
        lblincen10.Text = Val(lblaver10.Text) - 1
        lblincen6.Text = Val(lblaver6.Text) - 1
    End Sub

    Private Sub btnweave_Click(sender As Object, e As EventArgs) Handles btnweave.Click
        lblrpm.Text = Val(cmbmtrperhour.Text) * 2
        lblrpmpickhour.Text = Val(lblrpm.Text) * Val(lblrpmminutes.Text)
        lblrpmpickinch.Text = lblpick.Text
        inchperhour = Val(lblrpmpickhour.Text) / Val(lblrpmpickinch.Text)
        lblrpminchhour.Text = inchperhour.ToString("F2")
        mtrperhour = (Val(lblrpminchhour.Text) * 2.54) / 100
        lblmtrperhour.Text = mtrperhour.ToString("F2")
        warpknot = Val(lblmtrperhour.Text) * (Val(DropDownList1.Text) / 100)
        lblwarpknott.Text = warpknot.ToString("F2")
        weavedrop = Val(lblmtrperhour.Text) * (Val(DropDownList2.Text) / 100)
        lblweave.Text = weavedrop.ToString("F2")
        lblprdperhour.Text = Val(lblmtrperhour.Text) - (Val(lblwarpknott.Text) + Val(lblweave.Text))
        lblprd24.Text = Val(lblprdperhour.Text) * Val(lblhour24.Text)
        lblprd20.Text = Val(lblprdperhour.Text) * Val(lblhour20.Text)
        lblprd16.Text = Val(lblprdperhour.Text) * Val(lblhour16.Text)
        lblprd10.Text = Val(lblprdperhour.Text) * Val(lblhour10.Text)
        lblprd6.Text = Val(lblprdperhour.Text) * Val(lblhour6.Text)
        averg24 = Math.Round(Val(lblprd24.Text), 2)
        averg20 = Math.Round(Val(Val(lblprd20.Text)), 2)
        averg16 = Math.Round((Val(lblprd16.Text)), 2)
        averg10 = Math.Round((Val(lblprd10.Text)), 2)
        averg6 = Math.Round((Val(lblprd6.Text)), 2)
        lblaver24.Text = averg24.ToString("F2")
        lblaver20.Text = averg20.ToString("F2")
        lblaver16.Text = averg16.ToString("F2")
        lblaver10.Text = averg10.ToString("F2")
        lblaver6.Text = averg6.ToString("F2")
        avergdhothi24 = Val(lblaver24.Text) / Val(lbllength.Text)
        avergdhothi20 = Val(lblaver20.Text) / Val(lbllength.Text)
        avergdhothi16 = Val(lblaver16.Text) / Val(lbllength.Text)
        avergdhothi10 = Val(lblaver10.Text) / Val(lbllength.Text)
        avergdhothi6 = Val(lblaver6.Text) / Val(lbllength.Text)
        lblavgdhothi24.Text = avergdhothi24.ToString("F2")
        lblavgdhothi20.Text = avergdhothi20.ToString("F2")
        lblavgdhothi16.Text = avergdhothi16.ToString("F2")
        lblavgdhothi10.Text = avergdhothi10.ToString("F2")
        lblavgdhothi6.Text = avergdhothi6.ToString("F2")
        lblincen24.Text = Val(lblaver24.Text) - 1
        lblincen20.Text = Val(lblaver20.Text) - 1
        lblincen16.Text = Val(lblaver16.Text) - 1
        lblincen10.Text = Val(lblaver10.Text) - 1
        lblincen6.Text = Val(lblaver6.Text) - 1
    End Sub


    Private Sub btninsert_Click(sender As Object, e As EventArgs) Handles btninsert.Click
        con.Open()
        Dim cmd As New SqlCommand("INSERT INTO SUBGRNPRDGRADE([itemcode],[FIELD1],[FIELD2],[FIELD3])  VALUES (@itemcode,@FIELD1,@FIELD2,@FIELD3)", con)
        cmd.Parameters.AddWithValue("FIELD1", cmbmtrperhour.Text)
        cmd.Parameters.AddWithValue("FIELD2", DropDownList1.Text)
        cmd.Parameters.AddWithValue("FIELD3", DropDownList2.Text)
        cmd.Parameters.AddWithValue("ITEMCODE", cmbitemcode.SelectedItem.Text)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Visible = True
        lblmsg.Text = "Percentage Details Saved Successfully"
        con.Close()
    End Sub

    Private Sub btnapprove_Click(sender As Object, e As EventArgs) Handles btnapprove.Click
        con.Open()
        Dim cmd As New SqlCommand("Update SUBGRNPRDGRADE set [FIELD1]=@FIELD1,[FIELD2]=@FIELD2,[FIELD3]=@FIELD3 WHERE ITEMCODE = @ITEMCODE", con)
        cmd.Parameters.AddWithValue("FIELD1", cmbmtrperhour.Text)
        cmd.Parameters.AddWithValue("FIELD2", DropDownList1.Text)
        cmd.Parameters.AddWithValue("FIELD3", DropDownList2.Text)
        cmd.Parameters.AddWithValue("ITEMCODE", cmbitemcode.SelectedItem.Text)
        cmd.ExecuteNonQuery()
        lblmsg.BackColor = System.Drawing.Color.White
        lblmsg.ForeColor = System.Drawing.Color.Green
        lblmsg.Visible = True
        lblmsg.Text = "Production Grade Details Approved Successfully"
        con.Close()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If lblincen24.Text <> "0.00" Or lblincen20.Text <> "0.00" Or lblincen16.Text <> "0.00" Or lblincen10.Text <> "0.00" Or lblincen6.Text <> "0.00" Then
            con.Open()
            Dim cmd As New SqlCommand("Update SUBGRNPRDGRADE set [aplusgrade]=@aplusgrade,[agrade]=@agrade,[bplusgrade]=@bplusgrade,[bgrade]=@bgrade,[cgrade]=@cgrade where itemcode = @itemcode", con)
            cmd.Parameters.AddWithValue("itemcode", cmbitemcode.SelectedItem.Text)
            cmd.Parameters.AddWithValue("aplusgrade", lblincen24.Text.Trim)
            cmd.Parameters.AddWithValue("agrade", lblincen20.Text.Trim)
            cmd.Parameters.AddWithValue("bplusgrade", lblincen16.Text.Trim)
            cmd.Parameters.AddWithValue("bgrade", lblincen10.Text.Trim)
            cmd.Parameters.AddWithValue("cgrade", lblincen6.Text.Trim)
            cmd.ExecuteNonQuery()
            lblmsg.BackColor = System.Drawing.Color.White
            lblmsg.ForeColor = System.Drawing.Color.Green
            lblmsg.Visible = True
            lblmsg.Text = "Production Grade Saved Successfully"
            loadgrid()
        End If
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        btnweave_Click(sender, e)
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        btnweave_Click(sender, e)
    End Sub

    Private Sub cmbmtrperhour_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbmtrperhour.SelectedIndexChanged
        btnweave_Click(sender, e)
    End Sub
End Class
