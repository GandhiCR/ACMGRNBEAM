<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="errorentry.aspx.vb" Inherits="ACMGRNBEAM.errorentry" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .scrollbar {
            max-height: 550px;
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script type="text/javascript">
        function selectAllText(sender) {
            $(sender).focus().select();
        }
    </script>

</head>
<body>


    <form id="form1" runat="server">
        <div style="background-image: url(images/Glass-2Acm.png); width: 100%">

            i<asp:Panel ID="contenpanel" runat="server">

             
              
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
            <td style="text-align:center " colspan="8" >
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
                      <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="Label5" runat="server" Text="Select Quantity Type" Font-Names="Century Gothic" Font-Size="18px" ForeColor="White"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="cmbqtytype" runat="server" AutoPostBack="true" FFont-Names="Century Gothic" Font-Size="18px">
                                <asp:ListItem>-Select Quantity-</asp:ListItem>
                            <asp:ListItem>B Quantity</asp:ListItem>
                                <asp:ListItem>C Quantity</asp:ListItem>
                                 <asp:ListItem>Bit Quantity</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>
                </table>





                <center>
                    <table style="text-align:center;font-size:24px;font-family:'Agency FB'"  >
                        <tr>
                
                            <td>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="scrollbar" ScrollBars="Vertical" >
                            <asp:GridView ID="GridView2" runat="server" BackColor="LightGoldenrodYellow"  BorderColor="Tan" BorderWidth="1px" CellPadding="2" AutoGenerateColumns="false" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:checkbox ID="chkRow" runat="server"  AutoPostBack="true" onclick="RadioCheck(this);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="ERRORNAME" HeaderText="Damage Reason" />
                          <asp:TemplateField HeaderText="Error Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txterrorqty" CssClass="QuantityClass" runat="server" autocomplete="Off"  Text="0.00" Width="80px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                     <FooterStyle BackColor="#9999ff" />
                        <HeaderStyle Font-Bold="True" Font-Size="16px" Font-Names="Agency FB" CssClass="GridHeader" BackColor="#66ccff" />
                        <PagerStyle Font-Names="Calibri" Font-Size="10px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <RowStyle Font-Names="Times New Roman" Font-Size="14px" CssClass="Gridrow" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <SortedAscendingCellStyle BackColor="#FAFAE7" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
                                          </asp:Panel>    
                            </td>
                            <td style="vertical-align:top">
                        <asp:Button ID="btnsave" class="button" runat="server" Text="SAVE DETAILS" Width="180px"/><br /><br />
                         <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Width="180px"/>
                            </td>
                        <td style="width: 2px">&nbsp;&nbsp;&nbsp;
                        </td> 
                            <td style="vertical-align:top">
                                  <asp:GridView ID="GridView1" runat="server" BackColor="LightGoldenrodYellow"  BorderColor="Tan" BorderWidth="1px" CellPadding="2" AutoGenerateColumns="false" >
                    <Columns>
                          <asp:TemplateField HeaderText="S.No"  HeaderStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="autonum" HeaderText="Thaan Number" />
                         <asp:BoundField DataField="ERRORCATEGORY" HeaderText="QTY TYPE" />
                      <asp:BoundField DataField="ERROR" HeaderText="ERROR" />
                        <asp:BoundField DataField="ERRORQTY" HeaderText="ERROR QUANTITY" />
                    </Columns>
                     <FooterStyle BackColor="#9999ff" />
                        <HeaderStyle Font-Bold="True" Font-Size="16px" Font-Names="Agency FB" CssClass="GridHeader" BackColor="#66ccff" />
                        <PagerStyle Font-Names="Calibri" Font-Size="10px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <RowStyle Font-Names="Times New Roman" Font-Size="14px" CssClass="Gridrow" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <SortedAscendingCellStyle BackColor="#FAFAE7" />
                        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                        <SortedDescendingCellStyle BackColor="#E1DB9C" />
                        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                </asp:GridView>
                         
                               </td>
                            <td style="vertical-align:top" >

                                 <asp:Panel ID="empwagepanel" runat="server" Visible="false">

                                <table border="1" style="vertical-align:top;text-align:center" >
                                    <tr>
                                        <td style="text-align:center">
                                            <asp:Label ID="lbl1" runat="server" Font-Size="30px" ForeColor="#FFCCFF" Text="Total Quantity" Font-Names="Agency FB" Visible="False"></asp:Label><br />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Label ID="lbltotqty" runat="server" Font-Size="30px" ForeColor="White" Font-Names="Agency FB"></asp:Label>
                                        </td>
                                     </tr>
                                </table>


                            </asp:Panel>
                                </td> 
                            </tr> 
                        </table> 
                              
                                
                                    </center>
                        
              
            </asp:Panel>
        </div>
    </form>
</body>
</html>
