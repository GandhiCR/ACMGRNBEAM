<%@ Page Title="" Language="vb" EnableViewState="true" ViewStateEncryptionMode="Always" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="beamgrn.aspx.vb" Inherits="ACMGRNBEAM.BEAMGRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
  padding-left: 20px;
}
           .grnalign {
  margin-top: 40px;
  margin-bottom: 100px;
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
    </style>

    <script type="text/javascript">
        function numeric(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode;
            if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57)) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
     <script type="text/javascript">
         function RadioCheck(rb) {
             var gv = document.getElementById("<%=GridView2.ClientID%>");
             var rbs = gv.getElementsByTagName("input");
             for (var i = 0; i < rbs.length; i++) {
                 if (rbs[i].type == "radio") {
                     if (rbs[i].checked && rbs[i] != rb) {
                         rbs[i].checked = false;
                         break;
                     }
                 }
             }
         }
</script>
    <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="BEAMWISE SUMMARY ENTRY"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
 <table id="grnheader" class="grnalign"   >
        <tr>
            <td>
                <asp:Label ID="lblgrnno" runat="server" Text="SUB. GRN NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label1" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtgrn" runat="server" Font-Names="Times New Roman" Font-Size="16px" AutoCompleteType="Disabled" Width="150px" CssClass="textbox"></asp:TextBox>
                <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Visible="False" />
            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
             <asp:Label ID="lbldate" runat="server" Text="GRN DATE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label29" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
              <asp:Label ID="lblgrndate" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
        </tr>
        <tr>
            <td>
                   <asp:Label ID="Label5" runat="server" Text="SUPP. DC NUMBER" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label8" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbldcno" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="SUPP. DC DATE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label7" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
               <asp:Label ID="lbldcdate" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                  <asp:Label ID="Label27" runat="server" Text="VENDOR CODE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label31" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="lblvencode" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                 <asp:Label ID="Label12" runat="server" Text="VENDOR NAME" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label2" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblvenname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
           <asp:Label ID="Label3" runat="server" Text="ITEM CODE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label4" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblnewcode" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                  <asp:Label ID="Label14" runat="server" Text="ITEM NAME" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblitemname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                  <asp:Label ID="Label11" runat="server" Text="LENGTH" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label13" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
              <asp:Label ID="lbllength" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
               <asp:Label ID="Label28" runat="server" Text="WIDTH" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>  
            </td>
              <td>
                <asp:Label ID="Label15" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
               <asp:Label ID="lblwidth" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            </tr>
        <tr>
            <td>
                 <asp:Label ID="Label16" runat="server" Text="REED SPACE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>     
            </td>
             <td>
                <asp:Label ID="Label18" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblreedspace" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
              <td>
              <asp:Label ID="Label32" runat="server" Text="PICK" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>     
              </td>
             <td>
                <asp:Label ID="Label33" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblpick" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                     <asp:Label ID="Label20" runat="server" Text="BATCH NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label34" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbatchno" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
               <asp:Label ID="Label22" runat="server" Text="QUANTITY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label21" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblgrnqty" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
          <asp:Label ID="Label25" runat="server" Text="METERS" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label23" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblgrnmeters" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                     <asp:Label ID="Label17" runat="server" Text="GRN BY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label35" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblusername" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                   <asp:Label ID="Label19" runat="server" Text="SIZING CODE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label24" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td>
                  <asp:Label ID="lblsizecode" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                  <asp:Label ID="Label26" runat="server" Text="EQUIPMENT" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label30" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                  <asp:Label ID="lblequipment" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                  <asp:Label ID="Label37" runat="server" Text="CATEGORY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label38" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                  <asp:Label ID="lblcategory" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
             <td>
                  <asp:Label ID="Label39" runat="server" Text="SUB CATEGORY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label40" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                  <asp:Label ID="lblsubcategory" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
              <asp:Label ID="Label36" runat="server" Text="DOC. NUMBER" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label41" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lbldocnum" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White" ></asp:Label>
                   <asp:Label ID="lblcurrentdate" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White"  Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
    <%--<table id="grntable" style="width: 1200px;" class="tab"  >
        <tr>
            <td style="width: 280px">
                <strong>
                   
                    <asp:Label ID="lblgrnno" runat="server" Text="SUB. GRN NO" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                           
                 </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label8" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 300px">
                <asp:TextBox ID="txtgrn" runat="server" Font-Names="Times New Roman" Font-Size="24px" AutoCompleteType="Disabled" Width="150px"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Visible="False" />
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 150px; text-align: left">
                <strong>
                    <asp:Label ID="lbldate" runat="server" Text="GRN DATE" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label27" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lblgrndate" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 120px">
                <strong>
                    <asp:Label ID="Label5" runat="server" Text="SUPP. DC NUMBER" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900" CssClass="auto-style1"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label7" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lbldcno" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 280px">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="VENDOR CODE" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label2" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 300px">
                <asp:Label ID="lblvencode" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 150px; text-align: left">
                <strong>
                    <asp:Label ID="Label3" runat="server" Text="ITEM CODE" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label4" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lblnewcode" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 120px">
                <strong>
                    <asp:Label ID="Label9" runat="server" Text="SUPP. DC DATE" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900" CssClass="auto-style1"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lbldcdate" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 280px">
                <strong>
                    <asp:Label ID="Label12" runat="server" Text="VENDOR NAME" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label13" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 300px">
                <asp:Label ID="lblvenname" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 150px; text-align: left">
                <strong>
                    <asp:Label ID="Label14" runat="server" Text="ITEM NAME" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label15" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lblitemname" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 120px">
                <strong>
                    <asp:Label ID="Label11" runat="server" Text="LENGTH" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label18" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                   <asp:Label ID="lbllength" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 280px">
                <strong>
                    <asp:Label ID="Label20" runat="server" Text="BATCH NO" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label21" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 300px">
                <asp:Label ID="lblbatchno" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 150px; text-align: left">
                <strong>
                    <asp:Label ID="Label22" runat="server" Text="QUANTITY" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label23" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lblgrnqty" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 120px">
                <strong>
                  <asp:Label ID="Label28" runat="server" Text="WIDTH" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>  
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label26" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                <asp:Label ID="lblwidth" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
             <td style="width: 150px; text-align: left">
                <strong>
                    <asp:Label ID="Label19" runat="server" Text="DOC. NUMBER" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label24" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
                 <asp:Label ID="lbldocnum" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White" CssClass="auto-style1" Font-Bold="True"></asp:Label>
              
            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td style="width: 150px; text-align: left">
                <strong>
                   <asp:Label ID="Label25" runat="server" Text="METERS" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900" CssClass="auto-style1"></asp:Label>
                </strong>
            </td>

            <td style="width: 2px">
                <asp:Label ID="Label16" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>
            <td style="text-align:left">
               <asp:Label ID="lblgrnmeters" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
              
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            
            <td style="width: 120px">
                <strong>
                    <asp:Label ID="Label17" runat="server" Text="GRN BY" Font-Names="Century Gothic" Font-Size="20px" ForeColor="#FF9900" CssClass="auto-style1"></asp:Label>
                </strong>
            </td>
            <td style="width: 2px">
                <asp:Label ID="Label30" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="White"></asp:Label>
            </td>

            <td style="text-align: LEFT">
            <asp:Label ID="lblusername" runat="server" Font-Names="Times New Roman" Font-Size="25px" ForeColor="White"></asp:Label>
            </td>

        </tr>
    </table>--%>
    <br />


    <table>
        <tr>
            <td>
<center>
    <asp:Panel ID="contenpanel" runat="server" BackColor="BurlyWood"  Visible ="false"  >
        <table>
          <tr>
                <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>

            <td>
                  <asp:Label ID="lblsetno"   runat="server"   Text="SET NO :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                  <asp:TextBox ID="txtsetno" runat="server" Font-Names="Times New Roman" Font-Size="24px"  AutoCompleteType="Disabled" Width="150px"  onkeypress="return numeric(event);" CssClass="textbox" ></asp:TextBox>
            </td>


              <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>


                <td>
                  <asp:Label ID="lblbeamno"   runat="server"   Text="BEAM NO :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                  <asp:TextBox ID="txtbeamno" runat="server" Font-Names="Times New Roman" Font-Size="24px"  AutoCompleteType="Disabled" Width="150px"  onkeypress="return numeric(event);" CssClass="textbox" ></asp:TextBox>
            </td>

           
                   <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>
 
                  
               <td>
                  <asp:Label ID="lblloomno"   runat="server"   Text="LOOM NO :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                  <asp:TextBox ID="txtloomno" runat="server" Font-Names="Times New Roman" Font-Size="24px"  AutoCompleteType="Disabled" Width="150px" CssClass="textbox"></asp:TextBox>
            </td>

              </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;
                </td>

            </tr>

            <tr>
                  <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>

 
                <td>
                  <asp:Label ID="lblthorneqty"   runat="server"   Text="QUANTITY :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                  <asp:TextBox ID="txtthorneqty" runat="server" Font-Names="Times New Roman" Font-Size="24px"  AutoCompleteType="Disabled" Width="150px" AutoPostBack="True" CssClass="textbox" ></asp:TextBox>
            </td>   


                  <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>

                <td>
                  <asp:Label ID="lblmtrs"   runat="server"   Text="METERS :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                   <asp:TextBox ID="txtmtrs" runat="server" Font-Names="Times New Roman" Font-Size="24px"  AutoCompleteType="Disabled" Width="150px" AutoPostBack="True" CssClass="textbox"  ></asp:TextBox>
            </td>   

                 <td style="width:10px" >
                  &nbsp;&nbsp;
              </td>

                <td>
                  <asp:Label ID="Label6"   runat="server"   Text="COLOR :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                 
               <asp:DropDownList ID="cmbcolor" runat="server"  AutoPostBack="true" Font-Names="Berlin Sans FB" Font-Size="20px" CssClass="textbox" >
<asp:ListItem>NONE</asp:ListItem> 
<asp:ListItem>ALGIERS BLUE(17-4728)</asp:ListItem> 
   <asp:ListItem>BABA(15-1331)</asp:ListItem> 
   <asp:ListItem>BALEIN BLUE(19-4048)</asp:ListItem> 
   <asp:ListItem>BLACK(19-4007)</asp:ListItem> 
   <asp:ListItem>BOTTLE GREEN(19-5918)</asp:ListItem> 
   <asp:ListItem>BRIGHT GOLD(16-0947)</asp:ListItem> 
   <asp:ListItem>CITRONELLE(15-0548)</asp:ListItem> 
   <asp:ListItem>CLASSIC BLUE(19-4052)</asp:ListItem> 
   <asp:ListItem>COPPER JARI</asp:ListItem> 
   <asp:ListItem>FIROJA(16-4529)</asp:ListItem> 
   <asp:ListItem>GERMAN BLUE(17-4245)</asp:ListItem> 
   <asp:ListItem>GOLD FUSHION(15-1062)</asp:ListItem> 
   <asp:ListItem>GOLD JARI</asp:ListItem> 
   <asp:ListItem>HOLIDAY GREEN(14-5413)</asp:ListItem> 
   <asp:ListItem>IBIS ROSE(17-2520)</asp:ListItem> 
   <asp:ListItem>LEMON YELLOW(13-0858)</asp:ListItem> 
   <asp:ListItem>LIGHT KHAKI(16-0737)</asp:ListItem> 
   <asp:ListItem>LIGHT LAVENDER(18-3943)</asp:ListItem> 
   <asp:ListItem>LIME GREEN(14-0452)</asp:ListItem> 
   <asp:ListItem>LT GARMENT 9961</asp:ListItem> 
   <asp:ListItem>MARMALADE(17-1140)</asp:ListItem> 
   <asp:ListItem>MAROON(18-1325)</asp:ListItem> 
   <asp:ListItem>MAVUE WOOD(17-1522)</asp:ListItem> 
   <asp:ListItem>MEJANTHA PURPLE(19-2428)</asp:ListItem> 
   <asp:ListItem>MEJANTHA(18-2929)</asp:ListItem> 
   <asp:ListItem>MILITARY GREEN(17-0133)</asp:ListItem> 
   <asp:ListItem>MIXED COLOR</asp:ListItem> 
   <asp:ListItem>MOON MIST(18-4105)</asp:ListItem> 
   <asp:ListItem>MUSTARD(17-1048)</asp:ListItem> 
   <asp:ListItem>NAVY BLUE(19-3940)</asp:ListItem> 
   <asp:ListItem>OLIVE GREEN(18-0538)</asp:ListItem> 
   <asp:ListItem>ONION(17-1446)</asp:ListItem> 
   <asp:ListItem>ORANGE(17-1361)</asp:ListItem> 
   <asp:ListItem>PARROT GREEN(18-6024)</asp:ListItem> 
   <asp:ListItem>PEACOCK GREEN(18-4726)</asp:ListItem> 
   <asp:ListItem>PINK(16-3116)</asp:ListItem> 
   <asp:ListItem>PISTHA(12-0322)</asp:ListItem> 
   <asp:ListItem>POOL GREEN(16-5425)</asp:ListItem> 
   <asp:ListItem>PURPLE(19-3638)</asp:ListItem> 
   <asp:ListItem>RED(18-1549)</asp:ListItem> 
   <asp:ListItem>RUST(18-1340)</asp:ListItem> 
   <asp:ListItem>SILVER GREY(18-4105)</asp:ListItem> 
   <asp:ListItem>SILVER JARI</asp:ListItem> 
   <asp:ListItem>SKY BLUE(15-3920)</asp:ListItem> 
   <asp:ListItem>TURKISH TILE(18-4432)</asp:ListItem> 
   <asp:ListItem>VIVACIOUS(19-2045)</asp:ListItem> 
   <asp:ListItem>WHITE(11-4001)</asp:ListItem> 
   <asp:ListItem>YELLOW(14-1052)</asp:ListItem> 

                 </asp:DropDownList>
                   
            </td>   

        </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                 <td colspan ="8" style="text-align:center ">
                    <asp:Button ID="btnsave" class="button" runat="server" Text="ADD THAAN DETAILS" />
                     &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnupdate" class="button" runat="server" Text="UPDATE" Visible="False" />
                     &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btndelete" class="button" runat="server" Text="DELETE " Visible="False" />
                     <asp:Label ID="lblcolor"   runat="server"   Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066"  Visible="false"></asp:Label>
                      &nbsp;&nbsp;&nbsp;
                      <br /><br />
                    <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
              </td>
            </tr>
        </table>
    </asp:Panel>  
    </center>

            </td>
            

        </tr>
    </table> 

    
    <br />
    <br />

 
    <table style="text-align:center">
        <tr>
            <td>
                <asp:GridView ID="GridView2" runat="server" BackColor="LightGoldenrodYellow"  BorderColor="Tan" BorderWidth="1px" CellPadding="2"  AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkRow" runat="server" OnCheckedChanged="CheckBox_Changed" AutoPostBack="true" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="autonum" HeaderText="Auto No." />
                        <asp:BoundField DataField="setno" HeaderText="Set No." />
                        <asp:BoundField DataField="beamno" HeaderText="Beam No." />
                        <asp:BoundField DataField="loomno" HeaderText="Loom No" />
                        <asp:BoundField DataField="thorneqty" HeaderText="Quantity" />
                        <asp:BoundField DataField="thornemtrs" HeaderText="Meters" />
                        <asp:BoundField DataField="thronecolor" HeaderText="Color" />
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Size="20px" Font-Names="Agency FB" CssClass="GridHeader" BackColor="Tan" />
                    <PagerStyle Font-Names="Calibri" Font-Size="20px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <RowStyle Font-Names="Times New Roman" Font-Size="20px" CssClass="Gridrow" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
            </td>
         <td style="vertical-align:top">
                <asp:Panel ID="empwagepanel" runat="server" Visible="false">
                        <table border="1">
                            <tr>
                                <td>
                                    <asp:Label ID="lbl1" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total Quantity"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                               
                                <td>
                                    <asp:Label ID="lbl2" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total Meters"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                 
                                </tr>
                            <tr>
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotmtrs" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                            </tr>
                        </table>
                          
                 
                        </asp:Panel>
            </td>
        </tr>
    </table>
    



</asp:Content>
