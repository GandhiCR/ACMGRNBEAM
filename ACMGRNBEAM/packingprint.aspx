<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="packingprint.aspx.vb" Inherits="ACMGRNBEAM.packingprint" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .scrollbar {
            max-height: 250px;
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
           .grnalign {
  margin-top: 40px;
   margin-right: 150px;
  margin-left: 80px;
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

            .tab {
  padding-left: 20px;
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
</head>
<body>
       
    <form id="form1" runat="server">
       <asp:Panel ID="invpanel" ScrollBars="Vertical" runat="server"  >
      
<div id="dvReport">
<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
    Height="600" Width="1200" BestFitPage="False" EnableDatabaseLogonPrompt="False" ToolPanelView="ParameterPanel" />
</div>
<br />
<input type="button" id="btnPrint" value="Print" onclick="Print()"  class ="button"/>

           </asp:Panel> 
    </form>
</body>
</html>
