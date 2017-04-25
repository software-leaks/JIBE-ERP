<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallAlert.aspx.cs" Inherits="VesselMovement_PortCallAlert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Port Call Alert</title>
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
        function Onfail(retval) {
            alert(retval._message);


        }


        function onSucc_LoadFunction(retval, prm) {
            try {
                document.getElementById(prm[0]).innerHTML = retval;

                checkForMyAction(prm[1], retval);
            }
            catch (ex)
            { }
        }

    </script>
</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <center>
            <table width="100%" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <div style="border: 1px solid #cccccc" class="page-title">
                            Port Call Alert
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div style="background-color: White; max-height: 100%">
                            <table style="margin-top: 5px;" width="100%">
                                <tr>
                                    <td><asp:Label ID="lblWebAlertList" Title="Port Call Alert" runat="server" Width="100%">
                                        <div style=" border: 1px solid #cccccc;">
                                             <div id="dvAlertList">
                                             </div>
                                        </div>
                                        </asp:Label>
                                        <asp:GridView ID="gvPortAlert" runat="server" EmptyDataText="NO RECORDS FOUND" ShowHeaderWhenEmpty="True"
                                                            AutoGenerateColumns="False" DataKeyNames="Port_Call_ID,Notification_ID" OnRowDataBound = "gvPortAlert_RowDataBound" CellPadding="1" Width="100%">
                                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                                            <RowStyle CssClass="RowStyle-css" />
                                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Vessel Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVessel_Name" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Port Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPORT_NAME" Visible="true" runat="server"  Text='<%# Eval("PORT_NAME")%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Notification Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNotification" runat="server" Text='<%# Eval("Notification_Name")%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="20%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Arrival">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArrival" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Arrival")))%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Departure">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDeparture" runat="server" Text='<%#UDFLib.ConvertUserDateFormatTime(Convert.ToString(Eval("Departure")))%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="15%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Country">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPort_Remarks" Visible="true" runat="server"  Text='<%# Eval("Country_Name")%>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Left" Width="10%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRead" Visible="true" BackColor="Azure" runat="server" Text='<%# Eval("Created_Date")%>'></asp:Label>
                                                                        <asp:Button ID = "btnMarkRead" runat="server" Visible ="false" Text="Mark as read" OnCommand ="btnMarkRead_click" 
                                                                         CommandArgument='<%#Eval("[Port_Call_ID]") + "," + Eval("[Notification_ID]") %>'
                                                                        BackColor="Yellow" />
                                                                        </ItemTemplate>
                                                                    <ItemStyle Wrap="True" HorizontalAlign="Center" Width="20%" CssClass="PMSGridItemStyle-css">
                                                                    </ItemStyle>
                                                                    <HeaderStyle Wrap="True" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
