Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("username") = String.Empty Then
            Response.Redirect("login.aspx")
        Else
            lblusername.Text = Session("Username")
        End If
        If lblusername.Text = "ganesh" Or lblusername.Text = "GANESH" Or lblusername.Text = "deepa" Or lblusername.Text = "it" Then
            lnkincentive.Visible = True
            LinkButton6.Visible = True
        Else
            lnkincentive.Visible = False
            LinkButton6.Visible = False
        End If
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Response.Redirect("inspectiondisplay.aspx")
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Response.Redirect("BEAMWISE.aspx")
    End Sub

    Private Sub lnklogout_Click(sender As Object, e As EventArgs) Handles lnklogout.Click
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        Response.Redirect("beamreport.aspx")
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        Response.Redirect("inspection.aspx")
    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click
        Response.Redirect("beaminspectionreport.aspx")
    End Sub

    Protected Sub LinkButton7_Click(sender As Object, e As EventArgs) Handles LinkButton7.Click
        Response.Redirect("thaanpacking.aspx")
    End Sub

    Protected Sub LinkButton8_Click(sender As Object, e As EventArgs) Handles lnkincentive.Click
        Response.Redirect("incentivereport.aspx")
    End Sub

    Protected Sub LinkButton6_Click(sender As Object, e As EventArgs) Handles LinkButton6.Click
        Response.Redirect("prdgrade.aspx")
    End Sub

    Protected Sub LinkButton8_Click1(sender As Object, e As EventArgs) Handles LinkButton8.Click
        Response.Redirect("prdgradechart.aspx")
    End Sub

    Protected Sub LinkButton9_Click(sender As Object, e As EventArgs) Handles LinkButton9.Click
        Response.Redirect("thaanpackslipdisplay.aspx")
    End Sub
End Class