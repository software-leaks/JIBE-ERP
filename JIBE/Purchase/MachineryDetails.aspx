<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MachineryDetails.aspx.cs"
    Inherits="Purchase_ReqsnAttachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
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
    <script type="text/javascript">



        function DocOpen(docpath) {
            window.open(docpath);
        }

        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;
        }

        function getImageopen(str) {
            window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
        }




        function fn_OnClose(sender, arg) {

            $('[id$=btnLoadFiles]').trigger('click');
            //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
        }


    </script>
    <style type="text/css">
        .tdh
        {
            text-align: right;
            padding: 1px 3px 1px 3px;
            width: 250px;
        }
        .tdd
        {
            text-align: left;
            padding: 1px 3px 1px 3px;
            width: 150px;
        }
    </style>
    <title></title>
</head>
<body style="background-color: White; font-size: 11px; font-family: Tahoma;">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scpMgr" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div style="padding-top: 2px; padding-bottom: 2px; width: 100%">
            <table cellpadding="1" cellspacing="0" border="1" align="left" width="100%" style="border-collapse: collapse;
                border: 1px solid gray;">
                <tr>
                    <td style="width: 100%" colspan="6">
                        <table width="100%" border="1" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                            <tr>
                                <td colspan="6" style="text-align: left; font-weight: bold; font-size: 11px; color: Black;
                                    background-color: #D8D8D8">
                                    Vessel Details:<asp:Label ID="lblVessel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Vessel Type:
                                </td>
                                <td style="width: 25%; font-size: 11px;">
                                    <asp:Label ID="lblVesselType" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%; font-size: 11px; font-weight: bold">
                                    Vessel Hull No:
                                </td>
                                <td style="width: 18%; font-size: 11px;">
                                    <asp:Label ID="lblVesselHullNo" runat="server"></asp:Label>
                                </td>
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Built Yard:
                                </td>
                                <td style="width: 40%; font-size: 11px;">
                                    <asp:Label ID="lblVesselYard" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Built Year:
                                </td>
                                <td style="width: 25%; font-size: 11px;">
                                    <asp:Label ID="lblVesselDelvDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%; font-size: 11px; font-weight: bold">
                                    IMO No:
                                </td>
                                <td style="width: 18%; font-size: 11px;">
                                    <asp:Label ID="lblIMOno" runat="server"></asp:Label>
                                </td>
                                <td style="width: 8%; font-size: 11px; font-weight: bold">
                                    Vessel Ex-Name(s):
                                </td>
                                <td style="width: 40%; font-size: 11px;">
                                    <asp:Label ID="lblVesselExName1" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; font-weight: bold; font-size: 11px; color: Black;
                        background-color: #D8D8D8">
                        Machinery/Maker Details &nbsp;( Machinery name :
                        <asp:Label ID="lblmachinaryname" runat="server"></asp:Label>):
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b>M/c Sr. No.: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMachinesrno" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Particulars: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblParticulars" Width="99%" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Model:</b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblModel" runat="server"></asp:Label>
                    </td>
                </tr>
               <%-- <tr>
                    <td style="font-weight: bold; font-size: 11px; text-align: left">
                        Set Installed :
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lblSetInstalled" runat="server"></asp:Label>
                    </td>
                </tr>--%>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b>Maker Name: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMakerName" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>Maker Contact: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerContact" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b style="">Phone: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerPh" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td style="width: 8%; font-size: 11px;">
                        <b style="">Email: </b>
                    </td>
                    <td style="width: 25%; font-size: 11px;">
                        <asp:Label ID="lblMakerEmail" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px;">
                        <b>City: </b>
                    </td>
                    <td style="width: 30%; font-size: 11px;">
                        <asp:Label ID="lblMakerCity" runat="server"></asp:Label>
                    </td>
                    <td style="width: 8%; font-size: 11px; font-family: Tahoma">
                        <b>Address: </b>
                    </td>
                    <td style="font-size: 11px; width: 30%" colspan="3">
                        <asp:Label ID="txtAddress" runat="server" Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr align="left" valign="top">
                    <td colspan="6" style="width: 8%; font-size: 11px;">
                        <asp:GridView ID="gvSubCatalogueDetails" runat="server" AutoGenerateColumns="False"
                            EmptyDataText="No attachment found." CellPadding="2" Width="100%" GridLines="None"
                            CssClass="GridView-css">
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="30px" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                            <Columns>
                                <asp:BoundField HeaderText="SubCatalogue Code" DataField="Subsystem_Code" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="SubCatalogue Name" DataField="Catalog" />
                                <asp:BoundField HeaderText="Particulars" DataField="Particulars" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Maker" DataField="MakerName" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Model" DataField="Model" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Serial No." DataField="System_Serial_Number" ItemStyle-HorizontalAlign="Left" />
                                
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
