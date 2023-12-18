<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="errordetail.aspx.vb" Inherits="ACMGRNBEAM.errordetail" EnableEventValidation = "false"  %>

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

        .auto-style1 {
            font-size: 18px;
        }

        .auto-style2 {
            width: 8px;
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
     

</head>
<body>
    
   
    <form id="form1" runat="server">
        <div  style="background-image:url(images/glassy-3.png);">
            
                <asp:Panel ID="contenpanel" runat="server"   ScrollBars ="Vertical" >
              
                 <table class="grnalign" style="width:1000px" >
                     <tr>
                         <td>
           <asp:Label ID="Label3" runat="server" Text="THAAN NUMBER" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label4" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cmbautonum" runat="server" Font-Names="Berlin Sans FB" Font-Size="20px" CssClass="textbox"  Width="150px" AutoPostBack="true">
                 </asp:DropDownList>
            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
             <td>
             <asp:Label ID="lbldate" runat="server" Text="B QUANTITY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label29" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
             <asp:Label ID="lblbquantity" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
             <td>
             <asp:Label ID="Label1" runat="server" Text="C QUANTITY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label2" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
             <asp:Label ID="lblcquantity" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td> 
           </tr>
           <tr>
           <td>
             <asp:Label ID="Label5" runat="server" Text="ERROR CATEGORY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label6" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td>
     <asp:DropDownList ID="cmberrorcategory" CssClass="QuantityClass" runat="server" autocomplete ="Off" Width="100px"  Font-Size="20px" >
    <asp:ListItem>-Select-</asp:ListItem> 
     <asp:ListItem>D</asp:ListItem> 
    <asp:ListItem>M</asp:ListItem> 
  </asp:DropDownList>
               </td>
                <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
 <td>
             <asp:Label ID="Label7" runat="server" Text="ERROR TYPE" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label8" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td>
                 <asp:DropDownList ID="cmberrortype" CssClass="QuantityClass" runat="server"  Font-Size="20px" AutoPostBack="true"  Width="150px">
<asp:ListItem>-Select-</asp:ListItem> 
<asp:ListItem>L</asp:ListItem> 
<asp:ListItem>P</asp:ListItem> 
</asp:DropDownList>
                   </td>
 <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
               <td>
             <asp:Label ID="Label9" runat="server" Text="ERROR" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label10" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
               <td>
                 <asp:DropDownList ID="cmberror" CssClass="QuantityClass" runat="server"  Width="200px" Font-Size="20px"  AutoPostBack="true" >
</asp:DropDownList>
                   </td>
           </tr>
        <tr>
             <td>
                <asp:Label ID="lblerrorqty" runat="server" Text="ERROR QUANTITY" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label11" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
             <td>
                <asp:TextBox ID="txterrorqty" runat="server" Font-Names="Times New Roman" Font-Size="16px" autocomplete ="Off" Width="150px" CssClass="textbox"></asp:TextBox>
                 </td>
            <td>
                   <asp:Label ID="lblautonum"   runat="server"   Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"  Visible="False"></asp:Label>
            </td>

            <td>
                <asp:Label ID="Label12" runat="server" Text="LOOM NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label13" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;
                   <asp:Label ID="lblloomno"   runat="server"   Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White" ></asp:Label>
            </td>
        </tr>
                     <tr>
                         <td style="text-align :center" colspan="11" >
                                <asp:Button ID="btnsave" class="button" runat="server" Text="SAVE DETAILS" Width="180px"/>
                               &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Width="180px"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btnupdate" class="button" runat="server" Text="UPDATE" Visible="False" />
                     &nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btndelete" class="button" runat="server" Text="DELETE " Visible="False" />
                                  &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnexport" class="button" runat="server" Text="EXPORT TO EXCEL" Width="180px" Visible="false"/>
                         </td>
                     </tr>
                        <tr>
            <td style="text-align:center " colspan="11" >
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
                   </table>
                    <table style="text-align:center">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" BackColor="LightGoldenrodYellow"  BorderColor="Tan" BorderWidth="1px" CellPadding="2"  AutoGenerateColumns="false">
                    <Columns>
                      <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="autonum" HeaderText="THAAN NO" />
                        <asp:BoundField DataField="BQTY" HeaderText="B QUANTITY" />
                        <asp:BoundField DataField="CQTY" HeaderText="C QUANTITY" />
                        <asp:BoundField DataField="ERRORCATEGORY" HeaderText="ERROR CATEGORY" />
                        <asp:BoundField DataField="ERRORTYPE" HeaderText="ERROR TYPE" />
                        <asp:BoundField DataField="ERROR" HeaderText="ERROR" />
                        <asp:BoundField DataField="ERRORQTY" HeaderText="ERROR QUANTITY" />
                           <asp:BoundField DataField="subgrnno" HeaderText="GRN NO" >
                              </asp:BoundField>
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


                <asp:GridView ID="GridView2" runat="server" BackColor="LightGoldenrodYellow"  BorderColor="Tan" BorderWidth="1px" CellPadding="2"  AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkRow" runat="server"  AutoPostBack="true" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="autonum" HeaderText="THAAN NO" />
                        <asp:BoundField DataField="BQTY" HeaderText="B QUANTITY" />
                        <asp:BoundField DataField="CQTY" HeaderText="C QUANTITY" />
                        <asp:BoundField DataField="ERRORCATEGORY" HeaderText="ERROR CATEGORY" />
                        <asp:BoundField DataField="ERRORTYPE" HeaderText="ERROR TYPE" />
                        <asp:BoundField DataField="ERROR" HeaderText="ERROR" />
                        <asp:BoundField DataField="ERRORQTY" HeaderText="ERROR QUANTITY" />
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
                            
                                </tr>
                            <tr>
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                             
                            </tr>
                        </table>
                          
                 
                        </asp:Panel>
            </td>
        </tr>
    </table>
                </asp:Panel>
        </div>
    </form>
</body>
</html>
