<%@ Page Title="" Language="vb" EnableViewState="true" ViewStateEncryptionMode="Always" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="jobworkorder.aspx.vb" Inherits="ACMGRNBEAM.jobworkorder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
  /*margin-bottom: 100px;*/
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
     
      
    
     
    <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="JOBWORK ORDER CONFIRMATION REPORT"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
 <table id="grnheader" class="grnalign"   >
        <tr>
            <td>
                <asp:Label ID="lblgrnno" runat="server" Text="Select Itemcode" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label1" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cmbitemcode" runat="server"  AutoPostBack="true" Font-Names="Berlin Sans FB" Font-Size="20px"></asp:DropDownList>

                <asp:Button ID="btnclear" class="button" runat="server" Text="Clear" Visible="False" />
            </td>
                 </tr>
        
        
        <tr>
            <td>
           <asp:Label ID="Label3" runat="server" Text="Item  Code" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                  <asp:Label ID="Label14" runat="server" Text="Item Name" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblitemname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                &nbsp;&nbsp;&nbsp;
            </td>
          
        </tr>
        <tr>
            <td>
                  <asp:Label ID="Label11" runat="server" Text="St.Length" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
               <asp:Label ID="Label28" runat="server" Text="St.Width" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>  
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
                 <asp:Label ID="Label16" runat="server" Text="Loom M/C Type" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>     
            </td>
             <td>
                <asp:Label ID="Label18" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                   <asp:DropDownList ID="cmbmachinetype" runat="server"  AutoPostBack="true" Font-Names="Berlin Sans FB" Font-Size="20px">
                          <asp:ListItem >Rapier Loom</asp:ListItem>
                          <asp:ListItem >Airjet Loom</asp:ListItem>
                          <asp:ListItem >Power Loom</asp:ListItem>
                          </asp:DropDownList>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
              <td>
              <asp:Label ID="Label32" runat="server" Text="St.Pick" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>     
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
                  <asp:Label ID="Label26" runat="server" Text="Equipment" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                  <asp:Label ID="Label37" runat="server" Text="Category" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                  <asp:Label ID="Label39" runat="server" Text="Sub Category" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label40" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                  <asp:Label ID="lblsubcategory" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        
    </table>
    
    <br />


    <table>
        <tr>
            <td>
<center>
    <asp:Panel ID="contenpanel" runat="server" BackColor="#CCCCFF">
        <table border="1">
          <tr>
            <td>
                  <asp:Label ID="lblkuridesign"   runat="server"   Text="Kuri Design :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                      <asp:DropDownList ID="cmbkuridesign" runat="server"  AutoPostBack="true" Font-Names="Berlin Sans FB" Font-Size="20px">
                          <asp:ListItem >Pondhu</asp:ListItem>
                          <asp:ListItem >Satha</asp:ListItem>
                          <asp:ListItem >Satin</asp:ListItem>
                          <asp:ListItem >Drill</asp:ListItem>
                          <asp:ListItem >Fancy Dobby</asp:ListItem>
                          <asp:ListItem >Fancy Pondhu</asp:ListItem>
                          <asp:ListItem >Vaikal</asp:ListItem>
                      </asp:DropDownList>
                  
            </td>


              <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>


                <td>
                  <asp:Label ID="lblborder"   runat="server"   Text="Border :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                    <asp:DropDownList ID="cmbborder" runat="server"  AutoPostBack="true" Font-Names="Berlin Sans FB" Font-Size="20px">
                          <asp:ListItem >Satha</asp:ListItem>
                          <asp:ListItem >Satin</asp:ListItem>
                          <asp:ListItem >Drill</asp:ListItem>
                          <asp:ListItem >Diamond Jari</asp:ListItem>
                       </asp:DropDownList>
            </td>
                        
              </tr>
           
            </table>
        <table border="1"  >
            <tr style="text-align:center">
                    <td colspan="3" >
                  <asp:Label ID="lblshuttle"   runat="server"   Text="Single Side Shutter taken [Beating] :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
                  <td>
                         <asp:Label ID="Label2"   runat="server"   Text="RPM :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label5"   runat="server"   Text="Minutes :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label6"   runat="server"   Text="Pick Per Hour :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label7"   runat="server"   Text="Pick Per inch :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="Label8"   runat="server"   Text="Inch Per Hour :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
            </tr>


            <tr  style="text-align:center">
                <td colspan="3" style="text-align:center" >
                      <asp:TextBox ID="txtmtrperhour" runat="server" Font-Names="Times New Roman" Font-Size="14px"  autocomplete ="Off" Width="150px"  CssClass="textbox" ></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btncalculate" class="button" runat="server" Text="CALCULATE" Width="180px" />
                </td>
                 <td>
                  <asp:Label ID="lblrpm" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmminutes" runat="server" Font-Names="Times New Roman" Text="60" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmpickhour" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmpickinch" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpminchhour" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            </tr>
            </table> 
            

        <table border="1" style="width:800px">
             <tr  style="text-align:center"> 
                 
                <td style="text-align:center" >
                  <asp:Label ID="Label12"   runat="server"   Text="Mtr/Hour :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
                   <td colspan="2" style="text-align:center" >
                         <asp:Label ID="Label17"   runat="server"   Text="Warp &amp; Weft Breakage[Warp knotting 15% Dropping] :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td colspan="2" style="text-align:center" >
                         <asp:Label ID="Label19"   runat="server"   Text="Small Border Dhoti Kuri [Weaving 12% Dropping] :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td style="text-align:center" >
                         <asp:Label ID="Label20"   runat="server"   Text="Production Mtr/Hour :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                
                </tr>
            <tr>
                <td>
                     <asp:Label ID="lblmtrperhour" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                
                  
                      <td colspan="2" style="text-align:center" >
                        <asp:Label ID="lblwarpknott"   runat="server"   Text="0.6" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>   
                      <td colspan="2" style="text-align:center" >
                       <asp:Label ID="lblweave"   runat="server"   Text="0.6" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
                      <td style="text-align:center" >
                       <asp:Label ID="lblprdperhour" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>   
            </tr>
            </table>
         <br />
    <br />

        <table border="1" >
             <tr style="text-align:center">
                               
                <td>
                  <asp:Label ID="Label9"   runat="server"   Text="Grade :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>
                   <td>
                         <asp:Label ID="lblaplusgrade"   runat="server"   Text="A+ :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="lblagrade"   runat="server"   Text="A :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="lblbplusgrade"   runat="server"   Text="B+ :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="lblbgrade"   runat="server"   Text="B :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblcgrade"   runat="server"   Text="C :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                  <td>
                <asp:Label ID="Label27"   runat="server"   Text="Job Work Loom Shed Shift :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                 <asp:Label ID="lbljobworkaplusgrade"   runat="server"   Text="2 + OT" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                       <asp:Label ID="lbljobworkagrade"   runat="server"   Text="2" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkbplusgrade"   runat="server"   Text="1.5" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkbgrade"   runat="server"   Text="1" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkcgrade"   runat="server"   Text="0.6" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
            </tr>
               
            <tr style="text-align:center">
                  <td>
                <asp:Label ID="Label42"   runat="server"   Text="Production hours per day :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour24"   runat="server"   Text="24" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour20"   runat="server"   Text="20" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour16"   runat="server"   Text="16" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour10"   runat="server"   Text="10" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour6"   runat="server"   Text="6" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                 </td>
            </tr>
               
            <tr style="text-align:center">
                  <td>
                <asp:Label ID="Label49"   runat="server"   Text="Production Mtrs per day :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
               <asp:Label ID="lblprd24" runat="server" Font-Names="Times New Roman"  Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd20" runat="server" Font-Names="Times New Roman"  Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd16" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd10" runat="server" Font-Names="Times New Roman"  Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd6" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                  <td>
                <asp:Label ID="Label50"   runat="server"   Text="Avg. Mtrs per Loom :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver24" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver20" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver16" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver10" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver6" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center" >
                  <td>
                <asp:Label ID="Label29"   runat="server"   Text="Avg. Dhoti per Loom :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
              <asp:Label ID="lblavgdhothi24" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblavgdhothi20" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi16" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi10" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi6" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                  <td>
                <asp:Label ID="Label31"   runat="server"   Text="Incentive Mtrs per Loom :"  Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                   <asp:Label ID="lblincen24" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen20" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen16" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen10" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen6" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                <td>
                       <asp:Label ID="Label34"   runat="server"   Text="Qualtity Grade :"  Font-Names="Agency FB" Font-Size="40px" ForeColor="White" ></asp:Label>
                </td>
                   <td colspan="5" >
                       <asp:Label ID="Label35"   runat="server"   Text="Rate per Mtr :"  Font-Names="Agency FB" Font-Size="40px" ForeColor="White" ></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center" >
                               
                <td>
                  <asp:Label ID="Label36"   runat="server"   Text="Below 2% :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label41"   runat="server"   Text="A" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label43"   runat="server"   Text="1.50" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label44"   runat="server"   Text="1.25" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label45"   runat="server"   Text="1.00" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="Label46"   runat="server"   Text="0.75" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label54"   runat="server"   Text="0.50" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                               
                <td>
                  <asp:Label ID="Label47"   runat="server"   Text="2% to 5% :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label48"   runat="server"   Text="B" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label51"   runat="server"   Text="1.00" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label52"   runat="server"   Text="0.75" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label53"   runat="server"   Text="0.50" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                   <td>
                      <asp:Label ID="Label59"   runat="server"   Text="0.25" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                               
                <td>
                  <asp:Label ID="Label55"   runat="server"   Text="Above 5% :" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label56"   runat="server"   Text="C" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label57"   runat="server"   Text="0.50" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label58"   runat="server"   Text="0.25" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                </td>
                
                </tr>
            </table> 


                  
       
    </asp:Panel>  
    </center>

            </td>
            

        </tr>
    </table> 

    
   

 
    
    



</asp:Content>
