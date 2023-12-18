<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="packprint.aspx.vb" Inherits="ACMGRNBEAM.packprint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        
    <style type="text/css">
      .grnalign {
  margin-top: 40px;

  margin-right: 150px;
  margin-left: 80px;
        }
    </style>
    <table class ="grnalign">
             <tr>
                <td colspan="5" style="text-align:center">
 
                    <asp:Label ID="lbltitle" runat="server" Text="PACKING SLIP PRINT - ACM"  Font-Names="Copperplate Gothic Light"  Font-Bold="True" Font-Size="30px" ForeColor="Aqua" ></asp:Label>  
                      <asp:Label ID="lblusername" runat="server"  Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#CC00FF" Visible="false"  ></asp:Label>
      
                </td>
            </tr>
        
       
         </table>
     
 
       <asp:Panel ID="invpanel" ScrollBars="Vertical" runat="server"   CssClass="grnalign" >

           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
           <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
               <Report FileName="subgrnpackingslipaqty.rpt">
               </Report>
           </CR:CrystalReportSource>
           </asp:Panel>
</asp:Content>
