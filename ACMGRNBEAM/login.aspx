<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="ACMGRNBEAM.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	 <title>RAMRAJ - ACM GRN DETAILS  - THAAN NWISE DETAILS</title>
	
<!--===============================================================================================-->	
	<link href="Content/PngItem_770782.ico" rel="shortcut icon" type="image/x-icon" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css" />
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css" />
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css" />
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="css/util.css" />
	<link rel="stylesheet" type="text/css" href="css/main.css" />
<!--===============================================================================================-->

     <style type='text/css'>
        body { 
          
                background-repeat: no-repeat;
         background-attachment: fixed;
    background-size :cover 
        }
    </style>
</head>
   <body style="background-image:url(images/BackgroundACM.jpg);" >
         <div  style="background-image:url(images/glassybig.png); width:100%" >
    <center>
	<table style="text-align:center;vertical-align:top;margin-top:0px;table-layout:auto;">
            <tr>
                <td style="text-align:center" >
                   <asp:Image runat="server" ImageUrl="images/acmlogo.png" width="80px" Height="80px"/>  
                   </td>
                 <td>
                     
                    <asp:Label ID="lbltit" runat="server"  FORECOLOR ="#FEED00" Font-Size="35px" Text="ACM GRN WEAVING BEAMWISE DETAILS" Font-Bold="True" Font-Names="Agency FB"  BackColor="#004F2A"  ></asp:Label>
                     
                </td>
            </tr>
            </table>
        </center>
	                    <div >
	
				<form runat="server">
                    <center>
                             
                    <asp:Panel ID="loginpanel" Width="500px" Height="600px" runat="server" BackColor="Wheat" >
					<span class="login100-form-title p-b-43">
						Login
					</span>
						<div class="wrap-input100 validate-input">		
                        <asp:textbox ID="txtuser" runat="server" AutoCompleteType="Disabled" CssClass="input100" Width="200px" ></asp:textbox>  
                            	<span class="focus-input100"></span>
						<span class="label-input100">Username</span>
                    </div>
			
                    <div class="wrap-input100 validate-input" data-validate="Password is required">
                    <asp:textbox ID="txtpassword" runat="server" AutoCompleteType="Disabled" TextMode="Password"  CssClass="input100" Width="200px"></asp:textbox>
                    <span class="focus-input100"></span>
						<span class="label-input100">Password</span>
                        </div>
                        
					<div class="container-login100-form-btn">
                        <asp:Button ID="btnlogin" runat="server" Text ="LOGIN" CssClass="login100-form-btn" /> 
					</div>
                    <div style="text-align:center">
					<asp:Label ID="lblmsg" runat="server" Font-Bold="False" Font-Names="Berlin Sans FB" Font-Size="30px" ForeColor="Red" ></asp:Label>
				</div> 
                        
                        </asp:Panel>
                               
                        </center>
           
				</form>
</div>
               </div> 
			
	
  
	
	

	
	
<!--===============================================================================================-->
	<script src="vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/bootstrap/js/popper.js"></script>
	<script src="vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script src="vendor/daterangepicker/moment.min.js"></script>
	<script src="vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script src="vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script src="js/main.js"></script>

</body>
</html>
