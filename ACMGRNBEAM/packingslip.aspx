<%@ Page Title="PACKING SLIP - THAANWISE PROJECT - DESIGNED BY IT TEAM" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="packingslip.aspx.vb" Inherits="ACMGRNBEAM.packingslip" %>

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
       
     <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="BEAMWISE PACKING SLIP"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
        
      
        <table id="RFID" style="width:1000px;height:100px;" class ="grnalign" >
          

            <tr>
                <td >
                     <asp:Label ID="Label1" runat="server"   Text="Select Machine Number" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#660066"></asp:Label> 
                    </td>
                <td>
    <asp:DropDownList ID="cmbdevice" runat="server"  AutoPostBack="true" Enabled="False" Font-Names="Berlin Sans FB" Font-Size="20pt"  >
        <asp:ListItem>-Select Location-</asp:ListItem>
        <asp:ListItem >Erode-1</asp:ListItem>
        <asp:ListItem >Erode-2</asp:ListItem>
        <asp:ListItem >K.Patti-1</asp:ListItem>
        <asp:ListItem >K.Patti-2</asp:ListItem>
        <asp:ListItem >Nambiyur-1</asp:ListItem>
        <asp:ListItem>Nambiyur-2</asp:ListItem>
        <asp:ListItem>Kunnathur-1</asp:ListItem>
        <asp:ListItem>Kunnathur-2</asp:ListItem>
        <asp:ListItem>Salem-1</asp:ListItem>
        <asp:ListItem>Salem-2</asp:ListItem>
    </asp:DropDownList>  
                </td>

            </tr>
              
            <tr>
                 <td>
                    <asp:Label ID="lbltagqr" runat="server" Text="Thaan QR Code :" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#660066"></asp:Label> <br /><br />
                       <asp:Label ID="Label32" runat="server" Text="Thaan Number :" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#660066"></asp:Label> 
                </td>
                 
                    <td style="width:150px ">
                        <asp:TextBox ID="txtqrcode" runat="server" MaxLength="100" Width="500px"  AutoCompleteType="Disabled" Enabled="False" Font-Names="Times New Roman" Font-Size="24px" CssClass ="textbox "></asp:TextBox> 
                       <asp:Label ID="lblitem" runat="server" Visible="false" ></asp:Label> 
                        <asp:Label ID="lblweight" runat="server" Visible="false" ></asp:Label>
                           <asp:TextBox ID="txttagrfid" runat="server"  ></asp:TextBox> 
                    </td>
                <td>
                    <asp:Button ID="btnsave" class="button" runat="server" Text="Save" />  
                </td>
            </tr>
        
               </table>
        <asp:Panel ID="grnpanel" runat="server" Visible="true" >
            <table id="grnheader" class="grnalign"  style="vertical-align:top"   >
        <tr>
            <td>
                <asp:Label ID="lblgrn" runat="server" Text="SUB. GRN NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label6" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblgrnno" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
             <asp:Label ID="Label3" runat="server" Text="GRN DATE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                   <asp:Label ID="Label7" runat="server" Text="SUPP. DC NUMBER" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
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
                <asp:Label ID="Label11" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblvenname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
           <asp:Label ID="Label13" runat="server" Text="ITEM CODE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label14" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblitemcode" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
              <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                  <asp:Label ID="Label15" runat="server" Text="ITEM NAME" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label16" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                 <asp:Label ID="lblitemname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                  <asp:Label ID="Label17" runat="server" Text="THAAN LENGTH" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label18" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
              <asp:Label ID="lblthaanlength" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
               <asp:Label ID="Label28" runat="server" Text="THAAN WIDTH" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>  
            </td>
              <td>
                <asp:Label ID="Label19" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
               <asp:Label ID="lblthaanwidth" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            </tr>
        
        <tr>
            <td>
                     <asp:Label ID="Label22" runat="server" Text="BATCH NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
               <asp:Label ID="Label23" runat="server" Text="THAAN QUANTITY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label24" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblaqty" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                <asp:Label ID="lblbqty" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                <asp:Label ID="lblcqty" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
                 <asp:Label ID="lbldqty" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
          <asp:Label ID="Label25" runat="server" Text="THAAN COLOR" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label26" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblthaancolor" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
                     <asp:Label ID="Label30" runat="server" Text="THAAN WEIGHT" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label35" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblthaanweight" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>
                  </asp:Panel>
               <table id="count" class ="grnalign" style="width:900px" >
                   <tr>
                        <td>
              <asp:Label ID="Label45" runat="server" Text="PACKING SLIP NO." Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label46" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                  <td>
                   <asp:Label ID="lbldocnum" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White" ></asp:Label>
                         <asp:TextBox ID="txtpack" CssClass="QuantityClass" runat="server"  autocomplete ="Off" Width="40px" onclick="selectAllText(this)" Visible="false"></asp:TextBox>

                   <asp:Label ID="lblcurrentdate" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White"  Font-Bold="True"></asp:Label>
            </td>
                 <td>
                     
                     <asp:Button ID="btnnew" class="button" runat="server" Text="NEW SLIP" Width="200px" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnexist" class="button" runat="server" Text="EXISTING SLIP" Width="200px" />
                 </td>      
                       </tr>
                   <tr>
                       <td colspan="3"  style="text-align:center">
                            <asp:Button ID="btnfinish" class="button" runat="server" Text="FINISH"  Width="200px" />
                       </td>
                   </tr>
                  
                   </table>
    <asp:Panel ID="lastvaluepanel" ScrollBars="Vertical" runat="server"   CssClass="grnalign" Visible="false">
    <table border="1">
        <tr>
           <td>
                           <asp:Label ID="Label2" runat="server"  Text="Last Mapped Thaan No" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB" ></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lblthaanno" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White" ></asp:Label>
                        </td>
                       
                       </tr>
                   <tr>
                       <td>
                           <asp:Label ID="Label4" runat="server"  Text="Last Mapped Color" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB"></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lblthronecolor" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
                       </td>
                                         </tr>

                   <tr>
                       <td>
                           <asp:Label ID="Label5" runat="server"  Text="Last Total Quantity" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB"></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lbltotqty" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
                       </td>
                        <td>
                           <asp:Label ID="Label21" runat="server"  Text="Last A Quantity" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB"></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lbllastaqty" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
                       </td>
                        <td>
                           <asp:Label ID="Label33" runat="server"  Text="Last B Quantity" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB"></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lbllastbqty" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
                       </td>
                        <td>
                           <asp:Label ID="Label37" runat="server"  Text="Last D Quantity" Font-Size="20px" ForeColor="#FF9900" Font-Names="Berlin Sans FB"></asp:Label>
                           </td>
                       <td>
                         <asp:Label ID="lbllastdqty" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
                       </td>
                   </tr>

                   
                   <tr>
                       <td colspan="6" style="text-align:center">
                                <asp:Label ID="lblerror" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="30px"  ForeColor ="#FF5050" ></asp:Label>
                       </td>         
                      
                   </tr>
        
               </table> 
        </asp:Panel>
  <asp:Panel ID="invpanel" ScrollBars="Vertical" runat="server"   CssClass="grnalign" Visible="false">
        <div id="dvReport">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
           <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
              
           </CR:CrystalReportSource>
            </div>
           </asp:Panel>
       <center>


    <table style="width: 800px">
        <tr>
        
            <td>
              
                <asp:GridView ID="rfidmap" runat="server"   ForeColor="Black" Width="100%" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px"  AutoGenerateColumns="false" ShowFooter ="true"  >
                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                     <Columns>
                         <asp:TemplateField>
                             <ItemTemplate>
                                <asp:CheckBox ID="chkRow" runat="server"  AutoPostBack="true" onclick="RadioCheck(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No"  >
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>

                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Thaan No."  >
                           <ItemTemplate>
                                    <asp:Label ID="lblautonum" runat="server"  Text='<%# Eval("autonum")  %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="aqty" HeaderText="A Qty." />   
                                 <asp:BoundField DataField="bqty" HeaderText="B Qty." />    
                             <asp:BoundField DataField="cqty" HeaderText="C Qty." />   
                             <asp:BoundField DataField="bitqty" HeaderText="Bit Qty." />   
                            <asp:BoundField DataField="thronecolor" HeaderText="Thaan Color" />   
                         <asp:BoundField DataField="weight" HeaderText="Weight" />   
                           <asp:BoundField DataField="thronewidth" HeaderText="Width" />   
                             <asp:BoundField DataField="thornemtrs" HeaderText="Meters" />   
                                        </Columns>
                    <FooterStyle BackColor="Tan" />
                    <HeaderStyle Font-Bold="True"  Font-Size="20px"  Font-Names="Agency FB"   BackColor="Tan" />
                    <PagerStyle Font-Names="Calibri" Font-Size="20px" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"/>
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
                   
            </td>
            <td>
                    <asp:Button ID="btndelete" class="button" runat="server" Text="DELETE" Width="180px"/>
            </td>
        </tr>

    </table>
    
    </center>
</asp:Content>





