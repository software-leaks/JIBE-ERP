<%@ Page Title=" SlopChest Report " Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="SlopChest_Report.aspx.cs" Inherits="SlopChest_SlopChest_Report" %>

<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min1.9.js" type="text/javascript"></script>
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        debugger;
        function Validate() {
            var ddVessel = document.getElementById("<%=ddVessel.ClientID%>").value;
            var ddYear = document.getElementById("<%=ddYear.ClientID%>").value;
            var ddMonth = document.getElementById("<%=ddMonth.ClientID%>").value;

            if (ddVessel == 0 && ddMonth == 0 && ddYear == 0) {
                alert("Please Select Vessel, Year And Month");
                return false;
            }
            if (ddVessel == 0) {
                alert("Please Select Vessel");
                return false;
            }
            if (ddYear == 0) {
                alert("Please Select Year");
                return false;
            }
            if (ddMonth == 0) {
                alert("Please Select Month");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
            <div class="page-title">
                SlopChest Report
            </div>
            <div runat="server" style="width: 100%; height: 650px; color: Black;">
                <asp:UpdatePanel ID="UpdatePaneConsumption" runat="server">
                    <ContentTemplate>
                        <div id="dvTableFilter" runat="server" style="padding-top: 15px; padding-bottom: 5px;
                            height: 70px; width: 100%; margin-left: auto; margin-right: auto; text-align: center;">
                            <table id="tblfilter" runat="server" width="100%" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Vessel:<span style="color:Red;">*</span> &nbsp;
                                            </label>
                                            <asp:DropDownList ID="ddVessel" runat="server" Width="70%" AppendDataBoundItems="true"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Year:<span style="color:Red;">*</span> &nbsp;
                                            </label>
                                            <asp:DropDownList ID="ddYear" runat="server" Width="70%" AppendDataBoundItems="true"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <div>
                                            <label class="filterlabel">
                                                Month:<span style="color:Red;">*</span> &nbsp;
                                            </label>
                                            <asp:DropDownList ID="ddMonth" runat="server" Width="70%" AppendDataBoundItems="true"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td align="right" style="width: 5%">
                                        <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" OnClientClick="return Validate();"
                                            ToolTip="Search" ImageUrl="~/Images/SearchButton.png" Style="padding-top: 2px" />
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" ToolTip="Refresh"
                                            Text="Clear Filter" Height="23px" />
                                    </td>
                                    <td style="width: 5%; text-align: left;">
                                        <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                            ImageUrl="~/Images/Exptoexcel.png" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divReportTable" runat="server" style="padding: 5px; width: 100%; text-align: center;">
                            <table id="tblreport" runat="server" cellpadding="5" cellspacing="5" border="1" align="center"
                                width="90%" style="border-collapse: collapse; border: 1px solid gray;">
                                <tr>
                                    <td colspan="1" style="font-weight: bold; font-size: 12px; color: Black; background-color: #D8D8D8">
                                        <asp:Label ID="lblIn" Text="IN" runat="server" Style="text-align: left;"></asp:Label>
                                    </td>
                                    <td colspan="1" style="font-weight: bold; font-size: 12px; color: Black; background-color: #D8D8D8">
                                        <asp:Label ID="lblInAmount" Text="Amount" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="1" style="font-weight: bold; font-size: 12px; color: Black; background-color: #D8D8D8">
                                        <asp:Label ID="lblOut" Text="Out" runat="server" Style="text-align: left;"></asp:Label>
                                    </td>
                                    <td colspan="1" style="font-weight: bold; font-size: 12px; color: Black; background-color: #D8D8D8">
                                        <asp:Label ID="lblOutAmount" Text="Amount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Stock B/F From Last Month:
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblStockBF" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Sold To Crew:
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblSoldToCrew" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td rowspan="2" style="width: 15%; font-size: 12px; font-weight: normal">
                                        Total Purchase:
                                    </td>
                                    <td rowspan="2" style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblTotalPurchase" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Representation(Owners):
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblRepresentationOwner" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Representation(Charterers):
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblRepresentationChaterer" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td colspan="4">
                                        &nbsp
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Total Stock On Board:
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lbltotal" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                    <td rowspan="3" style="width: 15%; font-size: 12px; font-weight: normal">
                                        Total Taken Out:
                                    </td>
                                    <td rowspan="3" style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lbltotaltakenout" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Total Out:
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lbltotalout" ForeColor="Red" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td style="width: 15%; font-size: 12px; font-weight: normal">
                                        Total Stock In Hand:
                                    </td>
                                    <td style="width: 12%; font-size: 12px;">
                                        <asp:Label ID="lblstockinhand" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ImgExpExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </center>
</asp:Content>
