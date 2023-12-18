<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="beaminspectionreport.aspx.vb" Inherits="ACMGRNBEAM.beaminspectionreport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
     .scrollbar { max-height:950px;}
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
        /*.parent {
display: grid;
grid-template-columns: repeat(5, 1fr);
grid-template-rows: repeat(5, 1fr);
grid-column-gap: 0px;
grid-row-gap: 0px;
}*/
        .ChkBoxClass input {width:25px; height:25px;}

        .GridHeader {
            text-align: center !important;
        }

        .gridViewPager td {
            padding-left: 4px;
            padding-right: 4px;
            padding-top: 1px;
            padding-bottom: 2px;
        }
    
        .rowstyle td
        {
            width:150px;
        }

    
         .Gridrow {
                        text-transform: uppercase;
        }
        .auto-style2 {
            font-size: 15px;
        }
    </style>

    <center>
    <table border="0" style="width:600px" >
        <tr>
                <td colspan="5" style="text-align:center;border:dotted;width:600px ">
 
                    <asp:Label ID="lbltitle" runat="server" Text="REPORT DETAILS - INSPECTION"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
      
                </td>
            </tr>
        </table>
     </center>

    <table>
        <tr>
            <td>
<table id="emptable" style="width:600px" >
                
                     <tr>
                          <td style="width:120px;text-align:right">
                            <asp:Label ID="lblfromdate"  runat="server"  Text="FROM DATE" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="Aqua" ></asp:Label>
                        </td>
                         
                        <td style="text-align:left">
                            <asp:Calendar ID="datepicker1" runat="server" Visible="False" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px"  >
                            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                        </asp:Calendar>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkpickdate1" runat="server" ForeColor="#66CCFF" Font-Size="20px">GetDate</asp:LinkButton>
                        <asp:textbox id="txtfromdate" runat="server" MaxLength="21" Width="100px"  AutoCompleteType="Disabled"  Font-Names="Times New Roman" Font-Size="15px" Height="25px" ReadOnly="True" ></asp:textbox>   
                        </td>
                         </tr> 
                    <tr>
                <td style="width:120px;text-align:right">
                    <asp:Label ID="lbltodate" runat="server" Text="TO DATE" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="Aqua"></asp:Label> 
                      
                </td>
                
                    <td>
                        <asp:Calendar ID="datepicker2" runat="server" Visible="False" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px"  >
                            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                        </asp:Calendar>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkpickdate2" runat="server" ForeColor="#66CCFF" Font-Size="20px">GetDate</asp:LinkButton>
                        <asp:textbox id="txttodate" runat="server" MaxLength="21" Width="100px"  AutoCompleteType="Disabled"  Font-Names="Times New Roman" Font-Size="15px" Height="25px" ReadOnly="True" ></asp:textbox> 
                           <asp:Label ID="Label3" runat="server" Visible="false" ></asp:Label> 
                    </td>
            </tr>
                  <%--  <tr>
                        <td style="width:270px;text-align:right">
                                <asp:Label ID="lblvendor"    runat="server"   Text="SELECT VENDOR" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="Aqua" Visible="False" CssClass="auto-style2"></asp:Label>
                        </td>
                        
                         <td style="width:200px;text-align:right">
                            <asp:DropDownList ID="cmbvendor" runat="server"  AutoPostBack="true" Font-Names="Times New Roman" Font-Size="20px" Visible="False" >
                            </asp:DropDownList>
                            </td>
                       
                    </tr>--%>
      <tr>
                        <td style="width:270px;text-align:right">
                                <asp:Label ID="lblgrnno"    runat="server"   Text="ENTER GRN NO." Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="Aqua" CssClass="auto-style2" Visible="False"></asp:Label>
                        </td>
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <td style="width:200px;text-align:center">
                               <asp:textbox id="txtgrnno" runat="server" MaxLength="21" Width="100px"  AutoCompleteType="Disabled"  Font-Names="Times New Roman" Font-Size="25px" Height="25px" Visible="False" ></asp:textbox>      
                            </td>
                       
                    </tr>
     
            <tr>
    <td style="text-align:right"  >
                    <asp:Button ID="btnsave" class="button" runat="server" Text="GET DETAILS" />  
          </td>
       
                <td style="text-align:left" > 
                  <asp:Button ID="btnexportexcel" class="button" runat="server" Text="EXPORT INSPECTION" />  
                    
                </td>
                           </tr>
    <tr>
      <td colspan="4" style="text-align:center " >
                 <asp:Label ID="lblmsg" runat="server"  Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"  ></asp:Label>
            </td>
        </tr>
                    </table>
        
          
            </td>
          
        </tr>

</table>
    
        
    <table>
        <tr>
            <td style="vertical-align:top">
                <center><asp:Label ID="lblopeninvoice" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#FFCCFF" ></asp:Label></center> 
                
               
                        
                            <asp:GridView ID="invgrid" runat="server"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2">
    <Columns>
       <asp:TemplateField HeaderText="S.No"  >
                            <ItemTemplate>
                                <asp:Label ID ="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                         </asp:TemplateField>
               
                    </Columns>
                                  <FooterStyle BackColor="Tan" />
    <HeaderStyle Font-Bold="True"  Font-Size="15px"  Font-Names="Agency FB"  CssClass="GridHeader" BackColor="Tan" />
                    <PagerStyle Font-Names="Calibri" Font-Size="15px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <RowStyle Font-Names="Times New Roman" Font-Size="14px"  />
                    <HeaderStyle VerticalAlign ="Middle" /> 
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
                           <br />
                  
            </td>
          
            <td style="vertical-align:top;width:300px">
                
                          <center><asp:Label ID="lblsummary" runat="server" Font-Bold="False"  Font-Names="Berlin Sans FB" Font-Size="25px" ForeColor="#FFCCFF" Visible="False" >WAGES SUMMARY</asp:Label></center> 
                            <asp:GridView ID="summarygrid" runat="server"  BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2"  >
    <Columns>
       <asp:TemplateField HeaderText="S.No"  >
                            <ItemTemplate>
                                <asp:Label ID ="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                         </asp:TemplateField>
               
                    </Columns>
                                 <FooterStyle BackColor="Tan" />
  <HeaderStyle Font-Bold="True"  Font-Size="15px"  Font-Names="Agency FB"  CssClass="GridHeader" BackColor="Tan" />
                    <PagerStyle Font-Names="Calibri" Font-Size="15px" CssClass="gridViewPager" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <RowStyle Font-Names="Times New Roman" Font-Size="12px"  CssClass="Gridrow" />
                    <HeaderStyle VerticalAlign ="Middle" /> 
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
</asp:GridView>
                   
                <br />
                  
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        
    </table>
    
</asp:Content>