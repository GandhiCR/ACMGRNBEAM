<%@ Page Title="" Language="vb" EnableViewState="true" ViewStateEncryptionMode="Always" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="prdgradechart.aspx.vb" Inherits="ACMGRNBEAM.prdgradechart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

            .button:hover {
                background-color: blueviolet;
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

        .auto-style3 {
            text-align: left;
            border-style: hidden;
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

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script type="text/javascript">
        function selectAllText(sender) {
            $(sender).focus().select();
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

    <table style="width: 1500px">
        <tr>
            <td>

                <table id="grnheader" class="grnalign">
                    <tr>
                        <td>
                            <asp:Label ID="lblgrnno" runat="server" Text="Select Itemcode" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbitemcode" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="16px"></asp:DropDownList>

                            <asp:Button ID="btnclear" class="button" runat="server" Text="Clear" Visible="False" />
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Item  Code" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblnewcode" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 2px">&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Item Name" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblitemname" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Std.Length" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbllength" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 2px">&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label28" runat="server" Text="Std.Width" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label15" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblwidth" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Loom Type" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbmachinetype" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="16px">
                                <asp:ListItem>Airjet Loom</asp:ListItem>
                                <asp:ListItem>Hand Loom</asp:ListItem>
                                <asp:ListItem>Power Loom</asp:ListItem>
                                <asp:ListItem>Rapier Loom</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 2px">&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label32" runat="server" Text="Std.Pick" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label33" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblpick" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="Label26" runat="server" Text="Equipment" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label30" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblequipment" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label37" runat="server" Text="Category" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label38" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcategory" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="width: 2px">&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="Label39" runat="server" Text="Sub Category" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label40" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsubcategory" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>

                </table>

            </td>
            <td rowspan="2" style="vertical-align: top">

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ShowFooter="true" CssClass="scrollbar " Width="520px" OnRowDeleting="OnRowDeleting">
                    <Columns>

                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="20px">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="itemcode" HeaderText="Item Code" />
                        <asp:BoundField DataField="aplusgrade" HeaderText="A+ Grade" />
                        <asp:BoundField DataField="agrade" HeaderText="A Grade" />
                        <asp:BoundField DataField="bplusgrade" HeaderText="B+ Grade" />
                        <asp:BoundField DataField="bgrade" HeaderText="B Grade" />
                        <asp:BoundField DataField="cgrade" HeaderText="C Grade" />
                        <asp:BoundField DataField="docentry" HeaderText="Doc Entry" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                    <FooterStyle BackColor="#9999ff" />
                    <HeaderStyle Font-Bold="True" Font-Size="16px" Font-Names="Agency FB" CssClass="GridHeader" BackColor="#66ccff" />
                    <PagerStyle Font-Names="Calibri" Font-Size="10px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <RowStyle Font-Names="Verdana" Font-Size="14px" CssClass="Gridrow" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
            </td>

        </tr>
        <tr>
            <td>
                <table style="border-style: hidden">
                    <tr>
                        <td>
                            <center>
    <asp:Panel ID="contenpanel" runat="server" BackColor="#FFFFCC">

       
        <table border="1" style="border-style:hidden" >
          <tr>
            <td>
                  <asp:Label ID="lblkuridesign"   runat="server"   Text="Kuri Design :" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                      <asp:DropDownList ID="cmbkuridesign" runat="server"  AutoPostBack="true" Font-Names="Verdana" Font-Size="18px">
                          <ASP:LISTITEM >PONDHU</ASP:LISTITEM>
                          <ASP:LISTITEM >PLAIN</ASP:LISTITEM>
                          <ASP:LISTITEM >SATTIN</ASP:LISTITEM>
                          <ASP:LISTITEM >DRILL</ASP:LISTITEM>
                          <ASP:LISTITEM >FANCY DOBBY</ASP:LISTITEM>
                          <ASP:LISTITEM >FANCY PONDHU</ASP:LISTITEM>
                          <ASP:LISTITEM >SEER PONDHU</ASP:LISTITEM>
                          <ASP:LISTITEM >VAIKAL</ASP:LISTITEM>
                      </asp:DropDownList>
                  
            </td>


              <td style="width:20px" >
                  &nbsp;&nbsp;
              </td>


                <td>
                  <asp:Label ID="lblborder"   runat="server"   Text="Border :" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
            </td>
             <td>
                    <asp:DropDownList ID="cmbborder" runat="server"  AutoPostBack="true" Font-Names="Verdana" Font-Size="18px">
                          <ASP:LISTITEM >PLAIN</ASP:LISTITEM>
                          <ASP:LISTITEM >SATTIN</ASP:LISTITEM>
                          <ASP:LISTITEM >DRILL</ASP:LISTITEM>
                          <ASP:LISTITEM >DIAMOND JARI</ASP:LISTITEM>
                          <ASP:LISTITEM >MAYILKAN</ASP:LISTITEM>
                          <ASP:LISTITEM >KORVAI PET</ASP:LISTITEM>
                       </asp:DropDownList>
            </td>
                        
              </tr>
           
            </table>
        <table border="1" style="border-style:hidden"  >
            <tr style="text-align:center">
                    <td colspan="3" >
                  <asp:Label ID="lblshuttle"   runat="server"   Text="One Side Shuttle Beating" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
            </td>
                  <td>
                         <asp:Label ID="Label2"   runat="server"   Text="RPM" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label5"   runat="server"   Text="Minutes" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label6"   runat="server"   Text="Pick/Hour" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label7"   runat="server"   Text="PPI" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="Label8"   runat="server"   Text="Inch/Hour" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
            </tr>


            <tr  style="text-align:center">
                <td colspan="3" style="text-align:center" >
                      <asp:DropDownList ID="cmbmtrperhour" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="16px">
                    <asp:ListItem>54</asp:ListItem>
                    <asp:ListItem>56</asp:ListItem>
                    <asp:ListItem>58</asp:ListItem>
                    <asp:ListItem>60</asp:ListItem>
                </asp:DropDownList>
                      
                    &nbsp;&nbsp;&nbsp;&nbsp;
                     <%--<asp:Button ID="btncalculate" class="button" runat="server" Text="CALCULATE" Width="180px" />--%>
                </td>
                 <td>
                  <asp:Label ID="lblrpm" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmminutes" runat="server" Font-Names="Verdana" Text="60" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmpickhour" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpmpickinch" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>
                 <td>
                  <asp:Label ID="lblrpminchhour" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>
            </tr>
            </table> 
            

        <table border="1" style="width:800px;border-style:hidden" class="grnalign" >
             <tr  style="text-align:center"> 
                 
                <td style="text-align:center" >
                  <asp:Label ID="Label12"   runat="server"   Text="Gross Mtr/Hour" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
            </td>
                   <td colspan="3" style="text-align:center;" >
                         <asp:Label ID="Label17"   runat="server"   Text="Warp &amp; Weft Breakage + Knotting Dropping" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label><br />
                     <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="16px">
                  <asp:ListItem>13.50</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16.50</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  </asp:DropDownList>%
                </td>
                    <td colspan="2" style="text-align:center" >
                         <asp:Label ID="Label19"   runat="server"   Text="Kuri Weaving Dropping" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label><br />
                         <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" Font-Names="Verdana" Font-Size="16px">
                  <asp:ListItem>12</asp:ListItem>
                  <asp:ListItem>15</asp:ListItem>
                  <asp:ListItem>16</asp:ListItem>
                  <asp:ListItem>18</asp:ListItem>
                  <asp:ListItem>20</asp:ListItem>
                  <asp:ListItem>22</asp:ListItem>
                  <asp:ListItem>24</asp:ListItem>
                  <asp:ListItem>28</asp:ListItem>
                  <asp:ListItem>35</asp:ListItem>

                </asp:DropDownList>%
                                        </td>
                <td style="text-align:center" >
                         <asp:Label ID="Label20"   runat="server"   Text="Nett Mtr/Hour" Font-Names="Lucida Sans" Font-Size="16px" ForeColor="#660066" ></asp:Label>
                </td>
                
                </tr>
            <tr>
                <td style="text-align:center" >
                     <asp:Label ID="lblmtrperhour" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                
                  
                      <td colspan="3" style="text-align:center" >
                                    
                            <asp:Label ID="lblwarpknott"   runat="server" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
            </td>   
                      <td colspan="2" style="text-align:center" >
                                    
                       <asp:Label ID="lblweave"   runat="server" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="#660066" ></asp:Label>
                          
            </td>
                      <td style="text-align:center" >
                        <asp:Label ID="lblprdperhour" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
            </td>   
            </tr>
             <tr>
            <td style="text-align:center;border-style:hidden " colspan="5"  >
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
            </table>
         <br />
    <br />
        <table style="width:800px;border-style:hidden " >
            <tr>
                <td>
                    <table border="1" style="border-style:hidden;width:550px">
             <tr style="text-align:center">
                               
                <td>
                  <asp:Label ID="Label9"   runat="server"   Text="Grade" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
            </td>
                   <td>
                         <asp:Label ID="lblaplusgrade"   runat="server"   Text="A+" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="lblagrade"   runat="server"   Text="A" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="lblbplusgrade"   runat="server"   Text="B+" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="lblbgrade"   runat="server"   Text="B" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblcgrade"   runat="server"   Text="C" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                  <td class="auto-style3">
                <asp:Label ID="Label27"   runat="server"   Text="Job Work Loom Shed Shift" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
                 <asp:Label ID="lbljobworkaplusgrade"   runat="server"   Text="2 + OT" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                       <asp:Label ID="lbljobworkagrade"   runat="server"   Text="2" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkbplusgrade"   runat="server"   Text="1.5" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkbgrade"   runat="server"   Text="1" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lbljobworkcgrade"   runat="server"   Text="0.5" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
            </tr>
               
            <tr style="text-align:center">
                  <td class="auto-style3">
                <asp:Label ID="Label42"   runat="server"   Text="Production hours/day" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour24"   runat="server"   Text="24" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour20"   runat="server"   Text="20" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour16"   runat="server"   Text="16" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour10"   runat="server"   Text="10" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblhour6"   runat="server"   Text="6" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                 </td>
            </tr>
               
            <tr style="text-align:center">
                  <td class="auto-style3">
                <asp:Label ID="Label49"   runat="server"   Text="Production Mtrs/day" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
               <asp:Label ID="lblprd24" runat="server" Font-Names="Verdana"  Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd20" runat="server" Font-Names="Verdana"  Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd16" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd10" runat="server" Font-Names="Verdana"  Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                    <asp:Label ID="lblprd6" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                  <td class="auto-style3">
                <asp:Label ID="Label50"   runat="server"   Text="Avg. Mtrs/Loom" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver24" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver20" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver16" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver10" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblaver6" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center" >
                  <td class="auto-style3">
                <asp:Label ID="Label29"   runat="server"   Text="Avg. Dhoti/Loom" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
              <asp:Label ID="lblavgdhothi24" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="lblavgdhothi20" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi16" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi10" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                        <asp:Label ID="lblavgdhothi6" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                  <td class="auto-style3">
                <asp:Label ID="Label31"   runat="server"   Text="Incentive Mtrs/Loom"  Font-Names="Lucida Sans" Font-Size="18px" ForeColor="Black" ></asp:Label>
                </td>
                  <td>
                   <asp:Label ID="lblincen24" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen20" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen16" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen10" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
                  <td>
                     <asp:Label ID="lblincen6" runat="server" Font-Names="Verdana" Font-Size="20px" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center">
                <td>
                       <asp:Label ID="Label34"   runat="server"   Text="Qualtity Grade"  Font-Names="Agency FB" Font-Size="30px" ForeColor="Black" ></asp:Label>
                </td>
                   <td colspan="5" >
                       <asp:Label ID="Label35"   runat="server"   Text="Incentive Rate/Mtr"  Font-Names="Agency FB" Font-Size="30px" ForeColor="Black" ></asp:Label>
                </td>
            </tr>
            <tr style="text-align:center" >
                               
                <td class="auto-style3">
                  <asp:Label ID="Label36"   runat="server"   Text="Below 2%" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label41"   runat="server"   Text="A" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label43"   runat="server"   Text="1.50" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label44"   runat="server"   Text="1.25" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label45"   runat="server"   Text="1.00" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                  <td>
                      <asp:Label ID="Label46"   runat="server"   Text="0.75" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label54"   runat="server"   Text="0.50" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                               
                <td class="auto-style3">
                  <asp:Label ID="Label47"   runat="server"   Text="2% to 5%" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
           &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label48"   runat="server"   Text="B" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label51"   runat="server"   Text="1.00" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label52"   runat="server"   Text="0.75" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                      <asp:Label ID="Label53"   runat="server"   Text="0.50" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                   <td>
                      <asp:Label ID="Label59"   runat="server"   Text="0.25" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                </tr>
            <tr style="text-align:center">
                               
                <td class="auto-style3">
                  <asp:Label ID="Label55"   runat="server"   Text="Above 5%" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                       &nbsp;&nbsp;
                                    <asp:Label ID="Label56"   runat="server"  Text="C" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                    <td>
                         <asp:Label ID="Label57"   runat="server"   Text="0.50" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                <td>
                         <asp:Label ID="Label58"   runat="server"   Text="0.25" Font-Names="Lucida Sans" Font-Size="18px" ForeColor="#660066" ></asp:Label>
                </td>
                
                </tr>
            </table>
                </td>

                <td style="vertical-align:top">
                    <asp:Button ID="btnweave" class="button" runat="server" Text="Change % Values"/> <br /><br />
                    <asp:Button ID="btninsert" class="button" runat="server" Text="Insert % Values"/> <br /><br />
                    <asp:Button ID="btnapprove" class="button" runat="server" Text="Approve Values" Visible="false"  /> <br /><br />
                    <asp:Button ID="btnsave" class="button" runat="server" Text="Save Production Grade" Visible="false" /> <br /><br />

                </td>

            </tr>
        </table>
         


                  
       
    </asp:Panel>  
    </center>

                        </td>


                    </tr>
                </table>
            </td>


        </tr>

    </table>








    <br />













</asp:Content>
