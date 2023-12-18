<%@ Page Title="" Language="vb" EnableViewState="true" ViewStateEncryptionMode="Always" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="inspection.aspx.vb" Inherits="ACMGRNBEAM.inspection" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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

<script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
<link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
    rel="Stylesheet" type="text/css" />

<script type="text/javascript">
        $(function () {
            $(".Country").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'inspection.aspx/Getitemcode',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $(this).parent().find("input[type=hidden]").val(i.item.val);
                },
                minLength: 1
            }).focus(function () {
                $(this).autocomplete("search");
            });
        });
</script>
    
    <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="BEAMWISE INSPECTION ENTRY"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
    <table id="grnheader" class="grnalign"   >
        <tr>
            <td>
                <asp:Label ID="lblgrnno" runat="server" Text="Sub. GRN No" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label1" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtgrn" runat="server" Font-Names="Times New Roman" Font-Size="16px" autocomplete ="Off" Width="150px" CssClass="textbox"></asp:TextBox>
                <asp:Button ID="btnclear" class="button" runat="server" Text="Clear" Visible="False" />
            </td>
             <td style="width: 2px">&nbsp;&nbsp;&nbsp;
            </td>
            <td>
             <asp:Label ID="lbldate" runat="server" Text="GRN Date" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                   <asp:Label ID="Label5" runat="server" Text="Supp. DC No." Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                <asp:Label ID="Label9" runat="server" Text="Supp. DC Date" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                  <asp:Label ID="Label27" runat="server" Text="Vendor Code" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                 <asp:Label ID="Label12" runat="server" Text="Vendor Name" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label2" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lblvenname" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
            <td style="width:150px" >
                 <asp:Label ID="Label42" runat="server" Text="Warp Cons" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label3" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                <td>
                   <asp:Label ID="lblwarpcons" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
           <asp:Label ID="Label4" runat="server" Text="Item  Code" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label6" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
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
            <td style="width:150px" >
                 <asp:Label ID="Label43" runat="server" Text="Weft Cons" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label44" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
                <td>
                   <asp:Label ID="lblweftcons" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White" ></asp:Label>
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
              <asp:Label ID="lbltapelength" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
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
                 <asp:Label ID="Label16" runat="server" Text="St.Reed Space" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>     
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
                     <asp:Label ID="Label20" runat="server" Text="Batch No" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
             <td>
                <asp:Label ID="Label34" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblbatchno" runat="server" Font-Names="Times New Roman" Font-Size="20px" ForeColor="White"></asp:Label>
               <td style="width: 2px">
            </td>
            
            <td>
               <asp:Label ID="Label22" runat="server" Text="Total Quantity" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
          <asp:Label ID="Label25" runat="server" Text="Total Meters" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                     <asp:Label ID="Label17" runat="server" Text="GRN By" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
                   <asp:Label ID="Label19" runat="server" Text="Sizing Code" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
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
        <tr>
            <td>
              <asp:Label ID="Label36" runat="server" Text="Doc. No." Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
              <td>
                <asp:Label ID="Label50" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                  <asp:Label ID="lbldocnum" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White" ></asp:Label>
                   <asp:Label ID="lblcurrentdate" runat="server" Font-Names="Century Gothic" Font-Size="20px" ForeColor="White"  Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table> 
    
    <table>
        <tr>
            <td style="text-align:center">
                <asp:Button ID="btncalculate" class="button" runat="server" Text="CALCULATE" Width="180px" Visible="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnsave" class="button" runat="server" Text="SAVE DETAILS" Width="180px" Visible="False" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnerror" class="button" runat="server" Text="DAMAGE REASON" Width="180px"/>
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnitem" class="button" runat="server" Text="UPDATE ITEMCODE" Width="180px"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnaddthaan" class="button" runat="server" Text="EXTRA THAANS ADD" Width="180px"/>
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="errorpanel"  TargetControlID="btnerror" 
        CancelControlID="Button1" BackgroundCssClass="modalBackground" >  
    </cc1:ModalPopupExtender>  

                <asp:Panel ID="errorpanel" runat="server" CssClass="modalPopup" Visible="false" HorizontalAlign="Center" Width="1200px" Height="600px"  >
                    <iframe id="irm1" src="errorentry.aspx" runat="server"  style="width:1200px;height:600px" ></iframe>   
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="CLOSE" />
                </asp:Panel>  


                 <cc1:ModalPopupExtender ID="mp2" runat="server" PopupControlID="thaanpanel"  TargetControlID="btnaddthaan" 
        CancelControlID="Button2" BackgroundCssClass="modalBackground" >  
    </cc1:ModalPopupExtender>  

                <asp:Panel ID="thaanpanel" runat="server" CssClass="modalPopup" Visible="false" HorizontalAlign="Center" Width="1200px" Height="600px"  >
                    <iframe id="Iframe1" src="extrathaan.aspx" runat="server"  style="width:1300px;height:600px" ></iframe>   
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="CLOSE" />
                </asp:Panel>  

            </td>

        </tr>
        <tr>
            <td style="text-align:center ">
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
           <tr>
                    
                 <td>
                     <table class="grnalign " >
                         <tr>
                           <td>
                               <asp:Label ID="lblthaanno" runat="server" Text="Select Thaan Number" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF" Visible="false"  ></asp:Label> 
                           </td>
                              <td>
                   <asp:Label ID="lblcolan" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White" Visible="false" ></asp:Label>
            </td>
                   <td>
                  <asp:DropDownList ID="cmbthaan" runat="server"  AutoPostBack="true" Font-Names="Times New Roman" Font-Size="16px"  CssClass="textbox" Width="150px" Visible="false" >
          </asp:DropDownList>      
                    </td>
                <td>
                    <asp:Button ID="btnflilter" class="button" runat="server" Text="Clear Filter" Width="150px" Visible="false" />
                    </td> 
                         </tr>
                     </table>
                    
                                        </td>
                 
                 
            </tr>
        <tr>
            <td style="vertical-align: top">

                <asp:Panel ID="contenpanel" runat="server" Visible="false" CssClass="scrollbar" ScrollBars ="BOTH" Width="1500px"   >
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ShowFooter ="true">
                        <Columns>
                             <asp:TemplateField>
                             <ItemTemplate>
                                <asp:CheckBox ID="chkRow" runat="server"  AutoPostBack="true" onclick="RadioCheck(this);" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S.No"  HeaderStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AUTONUM" HeaderText="Thaan Number" >
                             <ItemStyle Width="80px" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="THORNEQTY" HeaderText="GRN Quantity"></asp:BoundField>
                             <asp:TemplateField HeaderText="Item Code" >
                                   <ItemTemplate >
                             <asp:TextBox ID="txtitemcode" runat="server" Width="150px"  Font-Names="Times New Roman"  CssClass="Country" Text='<%# Eval("itemcode") %>'   onclick="selectAllText(this)" autocomplete ="Off"></asp:TextBox>
                            <asp:HiddenField ID="hfitemid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="THORNEMTRS" HeaderText="GRN Meters" ></asp:BoundField>
                                <asp:TemplateField HeaderText="Color">
                                   <ItemTemplate>
                                    <asp:DropDownList ID="cmbcolor" CssClass="QuantityClass" runat="server"   Text='<%# Eval("THRONECOLOR")  %>'    >
<asp:listitem>--SELECT COLOR--</asp:listitem>
<asp:listitem>ALGIERS BLUE(17-4728)</asp:listitem>
<asp:listitem>BABA(15-1331)</asp:listitem>
<asp:listitem>BALEIN BLUE(19-4048)</asp:listitem>
<asp:listitem>BLACK(19-4007)</asp:listitem>
<asp:listitem>BOTTLE GREEN(19-5918)</asp:listitem>
<asp:listitem>BRIGHT GOLD(16-0947)</asp:listitem>
<asp:listitem>CITRONELLE(15-0548)</asp:listitem>
<asp:listitem>CLASSIC BLUE(19-4052)</asp:listitem>
<asp:listitem>COPPER JARI</asp:listitem>
<asp:listitem>CYAN</asp:listitem>
<asp:listitem>FIROJA(16-4529)</asp:listitem>
<asp:listitem>GERMAN BLUE(17-4245)</asp:listitem>
<asp:listitem>GOLD FUSHION(15-1062)</asp:listitem>
<asp:listitem>GOLD JARI</asp:listitem>
<asp:listitem>HOLIDAY GREEN(14-5413)</asp:listitem>
<asp:listitem>IBIS ROSE(17-2520)</asp:listitem>
<asp:listitem>J . SILVER</asp:listitem>
<asp:listitem>KADA</asp:listitem>
<asp:listitem>LAVENDER</asp:listitem>
<asp:listitem>LEMON YELLOW(13-0858)</asp:listitem>
<asp:listitem>LICORICE</asp:listitem>
<asp:listitem>LIGHT KHAKI(16-0737)</asp:listitem>
<asp:listitem>LIGHT LAVENDER(18-3943)</asp:listitem>
<asp:listitem>LIME GREEN(14-0452)</asp:listitem>
<asp:listitem>LT GARMENT 9961</asp:listitem>
<asp:listitem>MARMALADE(17-1140)</asp:listitem>
<asp:listitem>MAROON(18-1325)</asp:listitem>
<asp:listitem>MAVUE WOOD(17-1522)</asp:listitem>
<asp:listitem>MEJANTHA PURPLE(19-2428)</asp:listitem>
<asp:listitem>MEJANTHA(18-2929)</asp:listitem>
<asp:listitem>MILITARY GREEN(17-0133)</asp:listitem>
<asp:listitem>MIXED COLOR</asp:listitem>
<asp:listitem>MOON MIST(18-4105)</asp:listitem>
<asp:listitem>MOSS GREEN</asp:listitem>
<asp:listitem>MUSTARD(17-1048)</asp:listitem>
<asp:listitem>NAVY BLUE(19-3940)</asp:listitem>
<asp:listitem>NONE</asp:listitem>
<asp:listitem>OLIVE GREEN(18-0538)</asp:listitem>
<asp:listitem>ONION(17-1446)</asp:listitem>
<asp:listitem>ORANGE(17-1361)</asp:listitem>
<asp:listitem>PARROT GREEN(18-6024)</asp:listitem>
<asp:listitem>PEACOCK GREEN(18-4726)</asp:listitem>
<asp:listitem>PINK(16-3116)</asp:listitem>
<asp:listitem>PISTHA(12-0322)</asp:listitem>
<asp:listitem>POOL GREEN(16-5425)</asp:listitem>
<asp:listitem>PURPLE(19-3638)</asp:listitem>
<asp:listitem>RAMA GREEN</asp:listitem>
<asp:listitem>RED(18-1549)</asp:listitem>
<asp:listitem>ROSE GOLD</asp:listitem>
<asp:listitem>ROYAL BLUE</asp:listitem>
<asp:listitem>RUST(18-1340)</asp:listitem>
<asp:listitem>SANDAL</asp:listitem>
<asp:listitem>SILVER GREY(18-4105)</asp:listitem>
<asp:listitem>SILVER JARI</asp:listitem>
<asp:listitem>SKY BLUE(15-3920)</asp:listitem>
<asp:listitem>STEEL GREY</asp:listitem>
<asp:listitem>TURKISH TILE(18-4432)</asp:listitem>
<asp:listitem>UNIQUE FLORA</asp:listitem>
<asp:listitem>VIVACIOUS(19-2045)</asp:listitem>
<asp:listitem>WHITE(11-4001)</asp:listitem>
<asp:listitem>YELLOW(14-1052)</asp:listitem>
<asp:listitem>R01-C.GREEN</asp:listitem>
<asp:listitem>R02-L.T.RUST</asp:listitem>
<asp:listitem>R03-L.T.GOLD</asp:listitem>
<asp:listitem>R04-E.GREEN</asp:listitem>
<asp:listitem>R05-KANAGAPARAM</asp:listitem>
<asp:listitem>R06-RED</asp:listitem>
<asp:listitem>R07-C.BLUE</asp:listitem>
<asp:listitem>R08-FIROJA</asp:listitem>
<asp:listitem>R09-RUST</asp:listitem>
<asp:listitem>R10-SUMATHI</asp:listitem>
<asp:listitem>R11-KHAKI</asp:listitem>
<asp:listitem>R12-ANANTHA</asp:listitem>
<asp:listitem>R13-R.BLUE</asp:listitem>
<asp:listitem>R14-BISCUIT</asp:listitem>
<asp:listitem>R15-RANI ROSE</asp:listitem>
<asp:listitem>R16-L.ORANGE</asp:listitem>
<asp:listitem>R17-PISTHA</asp:listitem>
<asp:listitem>R18-FANTA ORANGE</asp:listitem>
<asp:listitem>R19-MEJANTHA</asp:listitem>
<asp:listitem>R20-L.T.BLUE</asp:listitem>
<asp:listitem>VC1-CORAL</asp:listitem>
<asp:listitem>VC2-YELLOW</asp:listitem>
<asp:listitem>VC3-TOMOTO</asp:listitem>
<asp:listitem>VC4-BABA</asp:listitem>
<asp:listitem>VC5-INK BLUE</asp:listitem>
<asp:listitem>VC7-MAROON</asp:listitem>
<asp:listitem>VC8-RAMER BLUE</asp:listitem>
<asp:listitem>VC9-NAVY</asp:listitem>
<asp:listitem>C82-PARROT GREEN</asp:listitem>
<asp:listitem>OX17-RANI ROSE</asp:listitem>
<asp:listitem>OX34-NAVY BLUE</asp:listitem>
<asp:listitem>OX35-YELLOW</asp:listitem>
<asp:listitem>OX37-MAROON</asp:listitem>
<asp:listitem>OX38-KHAKI</asp:listitem>
<asp:listitem>OX39-VIOLET</asp:listitem>
 </asp:DropDownList>

                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Size">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtlength" CssClass="QuantityClass" runat="server" Text='<%# Convert.ToDouble(Eval("U_LENGTH")).ToString("#####0.00")   %>' autocomplete ="Off" Width="30px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Width" >
                                   <ItemTemplate >
                                    <asp:TextBox ID="txtwidth" CssClass="QuantityClass" runat="server" autocomplete ="Off" Width="35px"  Text = <%# Convert.ToDouble((Eval("SWIDTH1")) / 2.54).ToString("#####0.00") %>  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Reed Space">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtreed" CssClass="QuantityClass" runat="server" Text='<%# Convert.ToDouble(Eval("U_REEDSPACE")).ToString("#####0.00")  %>' autocomplete ="Off" Width="30px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Pick">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtpick" CssClass="QuantityClass" runat="server" Text='<%# Convert.ToDouble(Eval("U_PICK")).ToString("#####0.00")  %>'  autocomplete ="Off" Width="30px"  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" A Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAQuantity" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" B Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBQuantity" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" C Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCQuantity" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"  onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Bit Qty.">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDQuantity" CssClass="QuantityClass" runat="server"  Text="0.00" autocomplete ="Off" Width="40px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Qty.">
                              
                                <ItemTemplate>
                                    <asp:Label ID="lbltotthqty" runat="server" Text="0.00"  CssClass="QuantityClass"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="A Mtrs" HeaderStyle-Width="60px">
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LABEL ID="txtthaanmtrsa" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"></asp:LABEL>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="B Mtrs" HeaderStyle-Width="60px">
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label  ID="txtthaanmtrsb" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="C Mtrs" HeaderStyle-Width="60px">
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label  ID="txtthaanmtrsc" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bit Mtrs" HeaderStyle-Width="60px">
                                  <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="txtthaanmtrsd" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Mtrs." >
                                 <ItemStyle HorizontalAlign="Right"></ItemStyle>
                             <ItemTemplate>
                                    <asp:Label ID="lbltotthmtrs" runat="server" Text="0.00"  CssClass="QuantityClass" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="TL. Mtrs." HeaderStyle-Width="60px">
                                  <ItemTemplate>
                                    <asp:TextBox ID="txttapelength" CssClass="QuantityClass" runat="server" Text="0.00"  autocomplete ="Off" Width="40px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="SETNO" HeaderText="Set No">
                                </asp:BoundField>
                             <asp:BoundField DataField="beamno" HeaderText="Beam No">
                           </asp:BoundField>
                             <asp:BoundField DataField="loomno" HeaderText="Loom No" >
                              </asp:BoundField>
                              <asp:TemplateField HeaderText="Weight [Kgs]" HeaderStyle-Width="60px">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtweight" CssClass="QuantityClass" runat="server" Text="0.00" autocomplete ="Off" Width="40px" onclick="selectAllText(this)"></asp:TextBox>
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
            
             
             
    </table>
  
    <table style="text-align:center" >
        <tr>
            <td style="vertical-align:top">
                  <asp:Panel ID="empwagepanel" runat="server" Visible="false">
                        <table  border="1" >
                            <tr>
                                <td>
                                    <asp:Label ID="lblaqty" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total A Quantity"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                               
                                <td>
                                    <asp:Label ID="lblbqty" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total B Quantity"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                  <td>
                                    <asp:Label ID="lblcqty" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total C Quantity"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                               
                                <td>
                                    <asp:Label ID="lbldqty" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total Bit Quantity"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                 <td>
                                    <asp:Label ID="lblamtrs" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total A Mtrs"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                               
                                <td>
                                    <asp:Label ID="lblbmtrs" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total B Mtrs"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                  <td>
                                    <asp:Label ID="lblcmtrs" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total C Mtrs"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                               
                                <td>
                                    <asp:Label ID="lbldmtrs" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total Bit Mtrs"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                   <td>
                                    <asp:Label ID="lblweight" runat="server"  Font-Size="30px" ForeColor="#FFCCFF" Text="Total Weight"  Font-Names="Agency FB"  Visible ="False" ></asp:Label><br /> 
                                </td>
                                </tr>
                            <tr>
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotaqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotbqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               <td style="text-align:center">
                                    <asp:Label ID="lbltotcqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotdqty" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                                 <td style="text-align:center">
                                    <asp:Label ID="lbltotamtrs" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotbmtrs" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               <td style="text-align:center">
                                    <asp:Label ID="lbltotcmtrs" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                               
                                <td style="text-align:center">
                                    <asp:Label ID="lbltotdmtrs" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>

                                  <td style="text-align:center">
                                    <asp:Label ID="lbltotweight" runat="server"  Font-Size="30px" ForeColor="White" Font-Names="Agency FB" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                      </asp:Panel>
            </td>

        </tr>
    </table>
   









</asp:Content>
