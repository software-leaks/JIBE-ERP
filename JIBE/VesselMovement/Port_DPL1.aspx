<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Port_DPL1.aspx.cs" Inherits="VesselMovement_Port_DPL1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .style1
        {
            width: 5%;
            height: 55px;
        }
        .style2
        {
            width: 80%;
            height: 55px;
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
        <div style="color: Black;">
            <div style="border: 1px solid #cccccc" class="page-title">
                DPL Print View
            </div>
            <div id="dvpage-content" class="page-content-main" style="padding: 1px">
                <table width="100%" cellpadding="2" cellspacing="1">
                    <tr>
                        <td align="right" style="width: 10%">
                        </td>
                        <td align="left" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%">
                        </td>
                        <td align="left" style="width: 8%">
                        </td>
                        <td align="right" style="width: 8%">
                        </td>
                        <td align="left" style="width: 8%">
                        </td>
                        <td align="right" style="width: 5%">
                        </td>
                        <td align="right" style="width: 20%">
                        </td>
                        <td align="right" style="width: 20%">
                        </td>
                        <td align="right" style="width: 20%">
                            <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" Width="120px"
                                OnClientClick="javascript:printDiv('printablediv')" />
                        </td>
                    </tr>
                </table>
                <div id="printablediv" style="width: 98%; height: 100%; text-align: center; border: 1px solid #cccccc;
                    font-family: Tahoma; font-size: 12px; overflow: Auto; overflow-x: hidden;">
                    <asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound">
                        <ItemTemplate>
                            <div style="float: left; text-align: left; width: 100%; border: 1px solid #cccccc;
                                font-family: Tahoma; font-size: 12px; overflow: Auto; background-color: #ffffff;">
                                <table>
                                    <tr>
                                        <td valign="top" align="right">
                                            <table width="120px" runat="server" id="table1" style="height: 100px; overflow:auto;">
                                              <tr>
                                                    <td width="75%" style="color: Black; font-weight :bold" height="30px" align="right">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="75%" style="color: Black;"  align="right">
                                                        Port Name :
                                                    </td>
                                                </tr>
                                                <tr><td style="color: Black;height: 20px;">&nbsp;</td></tr>
                                                <tr>
                                                    <td width="75%"  style="color: Black;"  align="right">
                                                        Arrival :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="75%"  style="color: Black;"  align="right">
                                                        Berthing :
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="75%" style="color: Black;"  align="right">
                                                        Departure :
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:Repeater runat="server" ID="rpt2" OnItemDataBound="rpt2_ItemDataBound">
                                                <HeaderTemplate>
                                                    <table border="0">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Literal ID="litRowStart" runat="server"></asp:Literal>
                                                    <td>
                                                        <div style="float: left; text-align: center; width: 100%;
                                                            font-family: Tahoma; font-size: 12px; background-color: #ffffff;">
                                                            <table style="width: 150px;height:250px;margin-top:10px">
                                                                 <tr>
                                                                    <td valign="middle" style="color: Black; height: 30px; background-color:#E8E8E8" align="center">
                                                                        <%--<asp:Label ID="FromportName" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>--%>
                                                                        <asp:Label ID="lblPortName" Font-Bold="true" ForeColor ="Blue" Text='<%#Eval("Port_Name")%>' runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                <td style="height: 20px;">
                                                                   <asp:Image ID="imgShipCrane" runat="server" ImageUrl="../Images/gears.png"   AlternateText="Ship crane required." ToolTip="Ship crane required." />
                                                                   <asp:Image ID="imgWarzone" runat="server" ImageUrl="../Images/Soldier.png" AlternateText="War zone." ToolTip="War zone." />
                                                                </td>
                                                                </tr>

                                                                <tr>
                                                                    <td valign="top" style="color: Black; height: 20px;" align="center">
                                                                        <asp:Label ID="lblArrival" runat="server" Text='<%# Eval("Arrival","{0:dd MMM yy HHmm}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  valign="top" style="color: Black; height: 20px;" align="center">
                                                                        <asp:Label ID="lblBerthing" runat="server" Text='<%# Eval("Berthing","{0:dd MMM yy HHmm}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top" style="color: Black; height: 20px;" align="center">
                                                                        <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure","{0:dd MMM yy HHmm}")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="color: Black; height: 20px;" align="center">
                                                                        <asp:CheckBox ID="Iswarrisk" runat="server" Visible = "false" Enabled = "false" Checked='<%# Convert.ToBoolean(Eval("IsWarRisk")) %>'
                                                                            Text="WarRisk" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="color: Black; height: 20px;" align="center">
                                                                        <asp:CheckBox ID="IsShipCraneReq" runat="server"  Visible = "false" Enabled = "false" Checked='<%# Convert.ToBoolean(Eval("IsShipCraneReq")) %>'
                                                                            Text="ShipCrane Req." />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td  style="color: Black;  background-color:#E8E8E8" >
                                                                        <table>
                                                                            <tr>
                                                                                                                                                       
                                                                                <td  valign="top" style="color: Black; " align="center">
                                                                                    <asp:Label ID="lblCharter" runat="server" ForeColor ="Green" ></asp:Label>
                                                                                </td>
                                                                            </tr>
    
                                                                        <tr>
                                                                            <td  valign="top" style="color: Black; " align="center">
                                                                                <asp:Label ID="lblOwner" runat="server"  ForeColor="BlueViolet" ></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                            <asp:Image ID="imgPoAgencybtn" Enabled ="false" Visible="false" Height="12px" Width="12px" runat="server" 
                                                                            ForeColor="Black" ImageUrl="~/Images/Agency_PO.jpg"
                                                                            ToolTip="Agency PO" ></asp:Image>
                                                                            <asp:Image ID="ImgView" runat="server" Text="Update" Enabled="false" Visible="false"  Height="12px" Width="12px"
                                                                            ForeColor="Black" ImageUrl="~/Images/CrewChange.bmp" ToolTip='<%# Eval("CrewOff") %>'
                                                                            ></asp:Image>
                                                                            <asp:Image ID="imgWorkList" runat="server" Visible="false"   Text="Work List"  Height="12px" Width="12px"
                                                                            ForeColor="Black" ImageUrl="~/Images/alert.jpg" ToolTip="Work List">
                                                                            </asp:Image>
                                                                            <asp:Image ID="imgAgencyWork" runat="server"  Visible="false"  Height="12px" Width="12px"
                                                                            ForeColor="Black" ImageUrl="~/Images/Agency_Work.jpg" ToolTip='<%# Eval("Agency_Work") %>' ></asp:Image>
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                    <asp:Literal ID="litRowEnd" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </center>
    </div>
    </form>
</body>
</html>
