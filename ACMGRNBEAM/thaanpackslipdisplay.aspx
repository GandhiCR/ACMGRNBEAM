<%@ Page Title="PACKING SLIP - THAANWISE PROJECT - DESIGNED BY IT TEAM" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="thaanpackslipdisplay.aspx.vb" Inherits="ACMGRNBEAM.thaanpackslipdisplay" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>





<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .scrollbar {
            max-height: 400px;
        }

        .form-control {
        }

        .button {
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
        }
         .button:hover  {
              background-color: blueviolet ;
            }
        /*.parent {
display: grid;
grid-template-columns: repeat(5, 1fr);
grid-template-rows: repeat(5, 1fr);
grid-column-gap: 0px;
grid-row-gap: 0px;
}*/
        .ChkBoxClass input {
            width: 25px;
            height: 25px;
        }

        .GridHeader {
            text-align: center !important;
        }

        .gridViewPager td {
            padding-left: 4px;
            padding-right: 4px;
            padding-top: 1px;
              padding-bottom: 2px;
        }

        .Gridrow {
            text-transform: uppercase;
        }
    

        .auto-style1 {
            font-size: 18px;
        }

        .auto-style2 {
            width: 8px;
        }
            .tab {
  padding-left: 40px;
}
        .grnalign {
  margin-top: 40px;

  margin-right: 150px;
  margin-left: 80px;
        }
        .textbox {
  width: 60%;
  margin: 8px 0;
  box-sizing: border-box;
  border: 1px solid #555;
  outline: none;
}
            .textbox:focus {
              background-color: aquamarine;
            }
        .QuantityClass:focus {
            background-color: aquamarine;
        }
        .Country:focus {
              background-color: aquamarine;
            }                          
      
      </style>

    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
  padding-top: 10px;
        padding-left: 10px;
      
    }
</style>
   
      <script type="text/javascript">  
        function Print() {  
            var dvReport = document.getElementById("dvReport");  
            var frame1 = dvReport.getElementsByTagName("iframe")[0];  
            if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {  
                frame1.name = frame1.id;  
                window.frames[frame1.id].focus();  
                window.frames[frame1.id].print();  
            } else {  
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;  
                frameDoc.print();  
            }  
        }  
    </script>  
 

     <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="BEAMWISE PACKING SLIP"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
        <table style="width:1000px">
            <tr>
                <td>
                    <table style="height:100px;width:650px" class ="grnalign" >
                      <tr>
               <td>
                <asp:Label ID="lblgrnno" runat="server" Text="SUB. GRN NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label6" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtgrn" runat="server" Font-Names="Times New Roman" Font-Size="16px" autocomplete ="Off" Width="150px" CssClass="textbox"></asp:TextBox>
                   <asp:Button ID="btnget" class="button" runat="server" Text="GET DETAILS" Width="160px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Visible="False"/>
            </td>

            </tr>
                 <tr>
                 <td>
                    <asp:Label ID="lbltagqr" runat="server" Text="Select Slip No" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label> 
                                        </td>
                 
                 <td>
                   <asp:Label ID="Label1" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                    <td>
                  <asp:DropDownList ID="cmbdocnum" runat="server"  AutoPostBack="true" Font-Names="Times New Roman" Font-Size="16px"  CssClass="textbox" Width="150px">
          </asp:DropDownList>      
                    </td>
                
            </tr>       
               </table>


                </td>
                <td>
                    <table style="width:220px;">
                     <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl2" runat="server"  Text="TOTAL NO. OF THAANS" Font-Size="20px" ForeColor="Aqua" Font-Names="Berlin Sans FB" Visible="False"></asp:Label><br />
                        <asp:Label ID="lbltotthaans" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="White" Visible="False"   ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                        <asp:Label ID="lbl3" runat="server"  Text="TOTAL QUANTITY" Font-Size="20px" ForeColor="Aqua" Font-Names="Berlin Sans FB" Visible="False"></asp:Label><br />
                        <asp:Label ID="lbltotqty" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="White" Visible="False"   ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl4" runat="server"  Text="TOTAL METERS" Font-Size="20px" ForeColor="Aqua" Font-Names="Berlin Sans FB" Visible="False"></asp:Label><br />
                        <asp:Label ID="lbltotmtrs" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="White" Visible="False"   ></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl5" runat="server"  Text="TOTAL WEIGHT" Font-Size="20px" ForeColor="Aqua" Font-Names="Berlin Sans FB" Visible="False"></asp:Label><br />
                        <asp:Label ID="lbltotweight" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="White" Visible="False"   ></asp:Label>
                        </td>
                    </tr>
                </table>


                </td>
            </tr>
        </table>
      
        
    
        
               <table id="count" class ="grnalign" style="width:900px;vertical-align:top " >
                   <tr>
                       <td style="vertical-align:top">
                          
                  <asp:Panel ID="invpanel" ScrollBars="Vertical" runat="server"  >
      
<div id="dvReport">
  <%--  <cr:crystalreportviewer runat="server" autodatabind="true" Height="600" Width="1200" BestFitPage="False" EnableDatabaseLogonPrompt="False" ToolPanelView="ParameterPanel" ></cr:crystalreportviewer>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
</div>
<br />
<input type="button" id="btnPrint" value="Print" onclick="Print()"  class ="button"/>

           </asp:Panel> 
            </td>
                       
                 </tr>
                   <tr>
                        <td colspan="2">
              <asp:Label ID="Label45" runat="server" Text="PACKING SLIP NO." Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
              <asp:Label ID="Label46" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
               <asp:Label ID="lbldocnum" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White" ></asp:Label>              
                 <asp:Label ID="lblusername" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" Visible ="false" ></asp:Label>
            </td>
               </tr>
                   <tr>
                       <td colspan="3"  style="text-align:center">
                          <asp:Label ID="lblerror" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="30px"  ForeColor ="#FF5050" ></asp:Label>
                       </td>
                   </tr>
                  
                   </table>
    
 
    
   
</asp:Content>





