<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CP_Hire_Invoice_Print.aspx.cs"
    Inherits="CP_Hire_Invoice_Print" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>

</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 98%;
    height: 100%;">
    <form id="form1" runat="server">
    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB;">
    <table border="0" style="font-size:11px;Font-Family:Tahoma; width:800px">
    <tr>
    <td width="10%" align="left">&nbsp;</td>
    <td width="40%" align="left">&nbsp;</td>
    <td width="20%" align="right">&nbsp;</td>
    <td width="30%">&nbsp;</td>
    </tr>
    <tr>
    <td valign="bottom" colspan="3" align="left">
    <asp:Image ID="ImgCompanyLogo" Src="../Images/app_logo.bmp" runat="server" /> </td>
    <td align="left" width="20%" style="font-size:16px">
    <b>INVOICE</b></td>

    </tr>
    <tr>
    <td colspan="2" align="left" style="font-size:16;font-weight:bold">
         <asp:Label ID="lblOwner" runat="server"></asp:Label>
        </td>
      <td style="font-weight:bold" align="right" width="10%">
            <asp:Label ID="lblInvRef" Text="Invoice Ref :" runat="server"></asp:Label>
        </td>
        <td align="left">
        <asp:Literal ID="ltInvRef"  runat="server"></asp:Literal>
        </td>
    </tr>
      <tr>
        <td colspan="2" align="left" style="font-size:14">
         <asp:Label ID="lblCompanyAddress" runat="server"></asp:Label>
        </td>
        <td style="font-weight:bold" align="right" width="10%">
            <asp:Label ID="lblDueDate" Text="Due Date :" runat="server"></asp:Label>
        </td>
        <td width="20%" align="left">
        <asp:Literal ID="ltDueDate"  runat="server"></asp:Literal>
        </td>
    </tr>
      <tr>
      
        <td colspan="2" style="font-size:16;font-weight:bold" align="left">
        <asp:Literal ID="ltCharterer" runat="server" ></asp:Literal>
        </td>

    </tr>

    <tr>
    <td colspan="4">
    <table width="100%">
    <tr>
        <td  style="Height:2px;Background:black;" colspan="7"></td>
    </tr>
    <tr>
    <td rowspan="100" style="width:1px;background:Black;"></td>
   	<td style="Width:460px;Text-Align:Center;font-size:14" rowspan="3"><b>Description</b></td>
	<td rowspan="100" style="width:1px;background:Black;"></td>
   	<td style="Width:160px;text-align:Center;" colspan="3"><b>Amount in USD</b></td>
	<td rowspan="100" style="width:1px;background:Black;"></td>

	<td rowspan="100" style="Width:0px;"></td>
    </tr>
    <tr><td style="width:0px;background:Black;" colspan="4"></td></tr>

       <tr>
   	<td style="Width:80px;Text-Align:Center;"><b>Debit</b></td>
	<td rowspan="100" style="Width:1px;Background:Black;"></td>
   	<td style="Width:80px;Text-Align:Center;"><b>Credit</b></td>

  </tr>

  
  <tr><td style="Height:2px;Background:black;"></td><td  style="Height:2px;Background:black;"></td><td  style="Height:2px;Background:black;"></td></tr>


    <tr>
    <td style="Width:460px;Text-Align:Left;"><b>Reference :</b>
     <asp:Literal ID="ltVesselCharterer" runat="server"></asp:Literal>&nbsp;&nbsp;
     <br /><b>CP Date :</b>
     <asp:Literal ID="ltCPdate" runat="server"></asp:Literal>
     

     </td>
    <td style="Width:80px;Text-Align:Right;">&nbsp;</td>
    <td style="Width:80px;Text-Align:Right;">&nbsp;</td>
    </tr>
    <tr>
    <td  colspan="7" align="left">
    <asp:Literal ID="ltItemcontents" runat="server"></asp:Literal>
    </td>
    </tr>

      <tr><td style="Height:2px;Background:black;"></td><td  style="Height:2px;Background:black;"></td><td  style="Height:2px;Background:black;"></td></tr>
    <tr>
       	<td align="right" style="font-weight:bold"> <asp:Label ID ="lblRemarks"  runat="server"></asp:Label></td>
        <td align="right" style="font-weight:bold"><asp:Literal ID="ltAmountDue_O" runat="server"></asp:Literal></td>
        
        <td align="right" style="font-weight:bold">
        <asp:Literal ID="ltAmountDue_C" runat="server"></asp:Literal>
        </td> 
    </tr>


     <tr><td style="Height:2px;Background:black;"></td>
     
     
     <td  style="Height:2px;Background:black;"></td><td  style="Height:2px;Background:black;"></td></tr>
    </table>
    </td>
    </tr>
 
    <tr>
    <td align="left" style="font-weight:bold" colspan="2">
        <asp:Label ID="lblinfo" runat="server" Text="Please remit above amount to:"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align="right">
     <asp:Label ID="lblBank" runat="server" Text="BANK :"></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltBank" runat="server"></asp:Literal>
    </td>
    </tr>
        <tr>
    <td  align="right">
     <asp:Label ID="lblSwift" runat="server" Text="SWIFT :"></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltSwift" runat="server"></asp:Literal>
    </td>
    </tr>
        <tr>
    <td  align="right">
     <asp:Label ID="lblCredit" runat="server" Text="CREDIT :"></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltCredit" runat="server"></asp:Literal>
    </td>
    </tr>
        <tr>
    <td  align="right">
     <asp:Label ID="lblACCNO" runat="server" Text="ACCOUNT NO :"></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltAccNO" runat="server"></asp:Literal>
    </td>
    </tr>
        <tr>
    <td  align="right">
     <asp:Label ID="lblIbanType" runat="server" ></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltIBAN" runat="server"></asp:Literal>
    </td>
    </tr>
    <tr>
    <td  align="right">
     <asp:Label ID="lblref" runat="server" Text="REF :"></asp:Label>
    </td>
    <td align="left">
     <asp:Literal ID="ltVesselInv" runat="server"></asp:Literal>
    </td>
    </tr>
    </table>
       
       



</div>
</form>
</body>
</html>