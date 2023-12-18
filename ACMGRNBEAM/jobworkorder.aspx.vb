Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Drawing.Printing
Imports System.IO
Imports Neodynamic.SDK
Imports Neodynamic.SDK.Web
Imports System.Text.RegularExpressions
Public Class jobworkorder
    Inherits System.Web.UI.Page
    Dim grndate, dcdate, autonumdate As DateTime
    Dim lin, n, k As Integer
    Dim dir, mdir, printer As String
    Dim builder As New StringBuilder
    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" (ByVal PSZpRINTER As String) As Boolean
    Dim docnumber, rowcount, totallooms As Integer
    Dim seqnumber, autonumber, selectedautonumber As String
    Dim inchperhour, mtrperhour, warpknot, weavedrop, prdmtrperhour, avergdhothi24, avergdhothi20, avergdhothi16, avergdhothi10, avergdhothi6, incen24, incen20, incen16, incen10, incen6 As Decimal
    Dim averg24, averg20, averg16, averg10, averg6 As Integer
    Dim grnquantity, grnmeters, grnlength, grnwidth, totalquantity, totalmeters, reedspace, warpcons, weftcons, pick, length1 As Decimal
    Dim grndocentry As String
    Dim dt2 As DataTable

    Private Sub beamwise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call dbMAIN()

        If Not IsPostBack Then
            Dim sql1 As String = "SELECT distinct u_itemcode from [@INT_SPL1]  where  U_Process  = 'w2' and U_ItemCode  NOT IN (SELECT ITEMCODE FROM SUBGRNPRDGRADE ) group by U_ItemCode"
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



    Public Sub loadwidth()
        Dim sql1 As String = "SELECT SWIDTH1,SLENGTH1,U_REEDSPACE,U_PICK,ISNULL(U_WARPCONS,0) as warpcons,isnUll(U_WEFTCONS,0) as weftcons,U_category,u_subcategory,u_commodityname,itemname FROM OITM WHERE ITEMCODE='" & cmbitemcode.SelectedItem.Text & "'"
        Dim cmd1 As New SqlCommand(sql1, con)
        Dim dt1 As New DataTable()
        Dim ad1 As New SqlDataAdapter(cmd1)
        ad1.Fill(dt1)
        If dt1.Rows.Count > 0 Then
            lblnewcode.Text = cmbitemcode.SelectedItem.Text
            grnwidth = dt1.Rows(0)("swidth1").ToString()
            lblwidth.Text = (grnwidth / 2.54).ToString("F2")
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
        loadwidth()
    End Sub
    Public Sub calculate()
        lblrpm.Text = Val(txtmtrperhour.Text) * 2
        lblrpmpickhour.Text = Val(lblrpm.Text) * Val(lblrpmminutes.Text)
        lblrpmpickinch.Text = lblpick.Text
        inchperhour = Val(lblrpmpickhour.Text) / Val(lblrpmpickinch.Text)
        lblrpminchhour.Text = inchperhour.ToString("F2")
        mtrperhour = (Val(lblrpminchhour.Text) * 2.54) / 100
        lblmtrperhour.Text = mtrperhour.ToString("F2")
        warpknot = Val(lblmtrperhour.Text) * (15 / 100)
        lblwarpknott.Text = warpknot.ToString("F2")
        weavedrop = Val(lblmtrperhour.Text) * (23 / 100)
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
        avergdhothi24 = Val(lblaver24.Text) / 1.85
        avergdhothi20 = Val(lblaver20.Text) / 1.85
        avergdhothi16 = Val(lblaver16.Text) / 1.85
        avergdhothi10 = Val(lblaver10.Text) / 1.85
        avergdhothi6 = Val(lblaver6.Text) / 1.85
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
    Private Sub btncalculate_Click(sender As Object, e As EventArgs) Handles btncalculate.Click
        calculate()
    End Sub
End Class
