<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="prdgrade.aspx.vb" Inherits="ACMGRNBEAM.prdgrade" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
   max-height: 450px;
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
               
               
 <center>
    <table border="0" style="width:600px">
        <tr>
                <td  style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="ITEMCODEWISE GRADE ENTRY"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
            
              <table class="grnalign">
                            <tr>
                                <td style="text-align:center " >
                                
                    <asp:Button ID="btnsave" class="button" runat="server" Text="ADD GRADE DETAILS" />
                                </td>
                             
                            </tr>
                            <tr>
            <td style="text-align:center ">
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
                  <tr>
                    <td>
                        <asp:Panel ID="contenpanel" runat="server" CssClass="grnalign" ScrollBars ="Vertical" Width="520px"  >
              
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ShowFooter ="true" CssClass ="scrollbar " Width="300px" >
         
                        <Columns>
                           
                            <asp:TemplateField HeaderText="S.No"  HeaderStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                                               
                             <asp:TemplateField HeaderText="Item Code" >
                                   <ItemTemplate >
                             <asp:label ID="txtitemcode" runat="server" Width="150px"  Font-Names="Times New Roman"  CssClass="Country" Text='<%# Eval("u_itemcode") %>'   onclick="selectAllText(this)" AutoCompleteType="Disabled"></asp:label>
                            <asp:HiddenField ID="hfitemid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                             
                              <asp:TemplateField HeaderText="A+ Grade">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtaplusgrade" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="30px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="A Grade" >
                                   <ItemTemplate >
                                    <asp:TextBox ID="txtagrade" CssClass="QuantityClass" runat="server" autocomplete ="Off" Width="35px"  Text="0.00" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="B+ Grade">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtbplusgrade" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="30px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="B Grade">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtbgrade" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="30px"  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" C Grade">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcgrade" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"  onclick="selectAllText(this)"></asp:TextBox>
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


                      <td style="vertical-align:top " > 

                          <asp:Panel ID="Panel1" runat="server" CssClass="scrollbar" ScrollBars ="Vertical" >
                
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ShowFooter ="true" CssClass ="scrollbar " Width="520px">
                        <Columns>
                           
                            <asp:TemplateField HeaderText="S.No"  HeaderStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="itemcode" HeaderText="Item Code" />
                            <asp:BoundField DataField="aplusgrade" HeaderText="A+ Grade" />
                            <asp:BoundField DataField="agrade" HeaderText="A Grade" />
                            <asp:BoundField DataField="bplusgrade" HeaderText="B+ Grade" />
                            <asp:BoundField DataField="bgrade" HeaderText="B+ Grade" />
                            <asp:BoundField DataField="cgrade" HeaderText="C Grade" />
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


                  </tr>
                        </table>
                 
              
  
                        
      
           
          
                
            
     
     
    
     
  
</asp:Content>
