<%@ Page Title="DPL Print View" Language="C#"  AutoEventWireup="true"
    CodeFile="Port_DPL.aspx.cs" Inherits="VesselMovement_Port_DPL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/AsyncResponse.js" type="text/javascript"></script>
     <style type="text/css">
           body, html
        {
            overflow-x: hidden;
        }
         
         .page

        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }

  .page
        {
            width: 100%;
           
        }
         </style>
    <script language="javascript" type="text/javascript">

        function validationOnSave() {
            var txteta = document.getElementById("ctl00_MainContent_txtFrom").value;
            var txtetd = document.getElementById("ctl00_MainContent_txtTo").value; ;



            var dt1 = parseInt(txteta.substring(0, 2), 10);
            var mon1 = parseInt(txteta.substring(3, 5), 10);
            var yr1 = parseInt(txteta.substring(6, 10), 10);

            var dt2 = parseInt(txtetd.substring(0, 2), 10);
            var mon2 = parseInt(txtetd.substring(3, 5), 10);
            var yr2 = parseInt(txtetd.substring(6, 10), 10);


            var ArrivalDt = new Date(yr1, mon1, dt1);
            var DepartureDate = new Date(yr2, mon2, dt2);

            if (txteta != "" && txtetd != "") {
                if (ArrivalDt > DepartureDate) {
                    alert("From Date can't be before of To Date.");
                    return false;
                }
            }
            return true;
        }
        function printDiv(divID) {
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              divElements + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;


        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loader.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <center>
        <div style="color: Black">
            <div style="display:none" class="page-title">
                Port Calls
            </div>
            <div id="dvpage-content" class="page-content-main" style="padding: 1px">

                        <table>
                            <tr>
                                <td>
                                    <div style="width: 100%;text-align: center; 
                                       font-family: Tahoma; font-size: 12px; overflow: Auto;">
                                        <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                                            <ItemTemplate>
                                                <div style="float: left; text-align: left; width: 100%;  border: 1px solid #cccccc;
                                                    font-family: Tahoma; font-size: 12px; overflow: Auto; background-color: #ffffff;" >
                                                  <asp:UpdatePanel ID="Update2" runat="server">
                                                    <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td valign="top" align="right" >
                                                                   <table width="120px" runat="server" id="table1" style="height: 100px; overflow:auto;">
                                                                   <tr>
                                                                        <td  width="75%"  align="right" style="background-color:Menu;height:25px; font-weight: bold" valign="top"   colspan="2">
                                                                            <asp:Label ID="FVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 20px;" align="right" colspan="2">
                                                                            Port Name :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px;" align="right"  colspan="2">
                                                                            Arrival :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px;" align="right"  colspan="2">
                                                                            Berthing :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;height: 15px" align="right"  colspan="2">
                                                                            Departure :
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="75%" style="color: Black;" align="right"  colspan="2">
                                                                        </td>
                                                                    </tr>

                                                                     <tr>
                                                                        <td style="font-weight: bold" align="right"  colspan="2">
                                                                            <asp:Button ID="btnAddNew" runat="server"
                                                                                Text="Add Port Call"  Visible="false" Width="90px"/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 10px" align="right" colspan="2">
                                                                            <asp:Button ID="btntemplate" 
                                                                                runat="server" Visible="false" Text="Template" Width="90px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                </div>
                                                            </td>
                                                            <td valign="top" style="height:260px" >
                                                                <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound"  >
                                                                    <HeaderTemplate>
                                                                        <table border="0">
                                                                        <tr  style="height:15px">
                                                                        </tr>
                                                                        <tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Literal ID="litRowStart" runat="server"></asp:Literal>
                                                                        <td>
                                                                            <div style="float: left; text-align: center; width: 100%; height:260px; border: 1px solid #cccccc;
                                                                                font-family: Tahoma; font-size: 12px; background-color: #ffffff;">
                                                                                 <asp:UpdatePanel ID="updatePortCall" runat="server"> 
                                                                                 <ContentTemplate>
                                                                                    <table style="width: 112px"  >
                                                                                    <tr>
                                                                                        <td width="75%" valign="bottom" style="color: Black; height: 30px; font-weight:bold; " align="center">
                                                                                          <asp:LinkButton ID="SelectButton"  Enabled="false" Text='<%#Eval("Port_Name")%>'
                                                                                                ForeColor="Blue" ToolTip="Edit" CommandName="Select" runat="server" />
                                                                                                                                                                                      

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black;height:15px"  align="center">
                                                                                            <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Arrival")))%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color:Black;height:15px" align="center">
                                                                                            <asp:Label ID="lblBerthing" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Berthing")))%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black;height:15px"  align="center">
                                                                                            <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Departure")))%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" style="color: Black; height: 15px;" align="center">
                                                                                            <asp:ImageButton ID="Iswarrisk" Enabled = "false" Visible="false" ToolTip ="War risk" runat="server" ImageUrl="~/Images/Soldier.png" Text="WarRisk" />
                                                                                             <asp:ImageButton ID="IsShipCraneReq" Text="ShipCrane Req." ToolTip="ShipCrane Req." Enabled = "false" Visible="false" ImageUrl="~/Images/gears.png" runat="server" />    
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black; height: 10px;" align="center">
                                                                                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text='<%#Eval("Port_Call_Status")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                        <tr>
                                                                                                                                                       
                                                                                            <td  valign="top" style="color: Black; height: 15px;" align="center">
                                                                                                <asp:Label ID="lblCharter" Text='<%#Eval("CSUPPLIER")%>' runat="server" ForeColor ="Green" ></asp:Label>
                                                                                            </td>
                                                                                        </tr>
    
                                                                                    <tr>
                                                                                        <td  valign="top" style="color: Black;height: 15px; " align="center">
                                                                                            <asp:Label ID="lblOwner" runat="server"  Text='<%#Eval("OSUPPLIER")%>' ForeColor="BlueViolet" ></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="75%" valign="top" style="color: Black; height: 20px;" align="center">
                                                                                            <asp:Label ID="lbPortRemarks" runat="server" Text='<%#Eval("Port_Remarks")%>' ForeColor="Brown" ></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    <tr>
                                                                                        <td width="75%" style="color: Black; font-weight: bold; height: 15px;">
                                                                                            <asp:ImageButton ID="imgPurchasebtn" runat="server"  Enabled="false" CommandArgument='<%#Eval("[Port_Call_ID]")%>'
                                                                                                ForeColor="Black" ImageUrl="~/Images/supply_icon.jpg" ToolTip="Purchase Order"
                                                                                                Height="16px"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgPoAgencybtn" Visible="false" runat="server"  Enabled="false"
                                                                                                CommandArgument='<%#Eval("[Port_Call_ID]")%>' ForeColor="Black" ImageUrl="~/Images/Agency_PO.jpg"
                                                                                                ToolTip="Agency PO" Height="16px"></asp:ImageButton>
                                                                                        </td>
                                                                                        </tr>
                                                                                         <tr>
                                                                                        <td width="75%" style="color: Black; height: 15px;">
                                                                                            <asp:ImageButton ID="ImgView" runat="server" Text="Update" Enabled="false"  CssClass="ImgDisabled"
                                                                                                ForeColor="Black" ImageUrl="~/Images/CrewChange.bmp" ToolTip='<%# Eval("CrewOff") %>'
                                                                                                Height="16px"></asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgWorkList" runat="server" Enabled="false" CssClass="ImgDisabled"
                                                                                                ForeColor="Black" ImageUrl="~/Images/alert.jpg" ToolTip="Work List" Height="16px">
                                                                                            </asp:ImageButton>
                                                                                            <asp:ImageButton ID="imgAgencyWork" runat="server" Text="Update" Enabled="false"  CssClass="ImgDisabled"
                                                                                                ForeColor="Black" ImageUrl="~/Images/Agency_Work.jpg" ToolTip='<%# Eval("Agency_Work") %>'
                                                                                                Height="16px"></asp:ImageButton>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                                    </ContentTemplate>
                                                                                 </asp:UpdatePanel>
                                                                            </div>
                                                                        </td>
                                                                        <asp:Literal ID="litRowEnd" runat="server"></asp:Literal>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                    </tr>
                                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                            <td valign="top" align="left">
                                                            &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>

                                                 </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%-- Label used for showing Error Message --%>
                                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="errMsg" Text="NO RECORDS FOUND"
                                                    Visible="false">
                                                </asp:Label>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>



                                </td>
                            </tr>
                        </table>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
</center>
</div>
</form>
</body>
</html>