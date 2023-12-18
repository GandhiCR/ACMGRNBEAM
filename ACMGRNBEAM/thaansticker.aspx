<%@ Page Title="" Language="vb" EnableViewState="true" ViewStateEncryptionMode="Always" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="thaansticker.aspx.vb" Inherits="ACMGRNBEAM.thaansticker" EnableEventValidation="false" %>
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

    

<script type = "text/javascript">

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "aqua";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#C2D69B";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;



        }

</script>
    

<script type = "text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

</script> 



    <center>
    <table border="0" style="width:600px">
        <tr>
                <td colspan="5" style="text-align:center;border:dotted ">
                   
                     <asp:Label ID="lbltitle" runat="server" Text="BEAMWISE THAAN STICKER"  Font-Names="Berlins Sans FB"  Font-Bold="True" Font-Size="24px" ForeColor="Aqua" ></asp:Label>  
                  
                </td>
            </tr>
        </table>
      </center>
    <table id="grnheader" class="grnalign"  style="vertical-align:top" >
        <tr>
            <td>
                <asp:Label ID="lblgrnno" runat="server" Text="SUB. GRN NO" Font-Names="Century Gothic" Font-Size="18px" ForeColor="#CCFFFF"></asp:Label>
            </td>
            <td>
                   <asp:Label ID="Label6" runat="server" Text=":" Font-Names="Berlin Sans FB" Font-Size="20px" ForeColor="White"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtgrn" runat="server" Font-Names="Times New Roman" Font-Size="16px" Width="150px" autocomplete ="Off" CssClass="textbox"></asp:TextBox>
                <asp:Button ID="btnclear" class="button" runat="server" Text="CLEAR" Visible="False" width="100px"/>

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
       
    </table> 
    
    <table>
        <tr>
            <td style="text-align:center">
             
                <asp:Button ID="btnsave" class="button" runat="server" Text="GET DETAILS" Width="180px" Visible="False" />
                 &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btncalculate" class="button" runat="server" Text="CALCULATE" Width="180px" Visible="false"  />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnprint" class="button" runat="server" Text="PRINT STICKER" Width="180px" Visible="false"  />

            </td>

        </tr>
        <tr>
            <td style="text-align:center ">
                 <asp:Label ID="lblmsg" runat="server" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Tomato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ShowFooter ="true">
                        <Columns>
                              <asp:TemplateField HeaderText="S.No"  HeaderStyle-Width="20px" >
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AUTONUM" HeaderText="Thaan Number" >
                             <ItemStyle Width="80px" /> 
                            </asp:BoundField>
                            <asp:BoundField DataField="thronesize" HeaderText="Size" />
                          <asp:TemplateField HeaderText="A Qty">
                               <ItemTemplate>
     <asp:CheckBox ID="CheckBoxA" runat="server"  Text='<%# Convert.ToDouble(Eval("Aqty")).ToString("#####0.00")   %>'  OnCheckedChanged ="CheckBoxA_Changed"  AutoPostBack="true"    />
   </ItemTemplate>
</asp:TemplateField>
                        <asp:TemplateField HeaderText="B Qty">
                               <ItemTemplate>
     <asp:CheckBox ID="CheckBoxB" runat="server"   Text='<%# Convert.ToDouble(Eval("Bqty")).ToString("#####0.00")   %>'  OnCheckedChanged ="CheckBoxB_Changed"  AutoPostBack="true"  />
   </ItemTemplate>
</asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="A+B Qty">
                               <ItemTemplate>
     <asp:CheckBox ID="CheckBoxAB" runat="server"   Text='<%# Convert.ToDouble(Eval("Aqty") + Eval("Bqty")).ToString("#####0.00")   %>'  OnCheckedChanged ="CheckBoxAB_Changed"  AutoPostBack="true"  />
   </ItemTemplate>
</asp:TemplateField>
                
                        <asp:TemplateField HeaderText="C Qty">
                               <ItemTemplate>
     <asp:CheckBox ID="CheckBoxC" runat="server" Text='<%# Convert.ToDouble(Eval("Cqty")).ToString("#####0.00")   %>'  OnCheckedChanged ="CheckBoxC_Changed"  AutoPostBack="true"  />
   </ItemTemplate>
</asp:TemplateField>
                          <asp:TemplateField HeaderText="Bit Qty">
                               <ItemTemplate>
     <asp:CheckBox ID="CheckBoxD" runat="server"   Text='<%# Convert.ToDouble(Eval("Bitqty")).ToString("#####0.00")   %>'   OnCheckedChanged ="CheckBoxD_Changed"  AutoPostBack="true"  />
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
                           
                         <asp:TemplateField HeaderText="Weight [Kgs]" HeaderStyle-Width="60px">
                                   <ItemTemplate>
                                    <asp:TextBox ID="txtweight" CssClass="QuantityClass" runat="server" Text='<%# Convert.ToDouble(Eval("weight")).ToString("#####0.00")  %>' autocomplete ="Off" Width="40px" onclick="selectAllText(this)"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="thronecolor" HeaderText="Color"  />
                         <asp:BoundField DataField="thaanbatch" HeaderText="Thaan Batch" Visible ="false" />
                         <asp:BoundField DataField="subgrnno" HeaderText="GRN No." />
                         <asp:BoundField DataField ="vencode" HeaderText ="Ven. Code" />
                         <asp:BoundField DataField ="itemcode" HeaderText ="Item Code" />
                         <asp:BoundField DataField ="batchno" HeaderText ="Batch" /> 
                            <asp:BoundField DataField ="thronewidth" HeaderText ="Thaan Width" /> 
                            <asp:TemplateField HeaderText="Category" >
                                 <ItemStyle HorizontalAlign="Right"></ItemStyle>
                             <ItemTemplate>
                                    <asp:Label ID="lblcategory" runat="server"   CssClass="QuantityClass" ></asp:Label>
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





                
            </td>
           
        </tr>
        <tr>
            <td style="vertical-align: top">

              
              
                    

             


            </td>
            
             
             
    </table>
  
    
   









</asp:Content>
