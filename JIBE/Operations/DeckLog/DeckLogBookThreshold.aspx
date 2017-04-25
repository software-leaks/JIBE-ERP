<%@ Page Title="Deck Log Book Threshold" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="DeckLogBookThreshold.aspx.cs" Inherits="DeckLogBookThreshold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/Wizard.js" type="text/javascript"></script>
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.highlight.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ViewEdit(Id) {
            var value = document.getElementById('<%=lblLOGBOOKID.ClientID%>').value;
         //    window.open("ERLogEdit.aspx?ViewID=" + Id + "&LOGBOOKID=" + value, "Test", "", "");
        }

        function MaskMoney(evt) {
            //evt.target.value
            
            if (!(evt.keyCode == 9 || evt.keyCode == 109 || evt.keyCode == 46 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 190 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            if(evt.keyCode == 109)
                if (evt.target.value.indexOf("-") > -1) {
                    return false;
            }
            var parts = evt.srcElement.value('.');

            if (parts.length > 2) return false;
            if ((evt.keyCode == 46) && (parts.length == 1)) return false;
            if ((evt.keyCode == 190) && (parts.length == 2)) return false;
            if (parts[0].length >= 14) return false;
        }
        function stringStartsWith (string, prefix) {
    return string.slice(0, prefix.length) == prefix;
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
        function MaskMoney_Minus(evt) {
            if (!(evt.keyCode == 189 || evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 190 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split('.');

            if (evt.srcElement.value.length > 0 && evt.keyCode == 189) return false;
            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if ((evt.keyCode == 190) && (parts.length == 2)) return false;
            if (parts[0].length >= 14) return false;

        }

        function validation() {


            var e = document.getElementById('<%=ddlVessel.ClientID %>');
            var f = document.getElementById('<%=ddlVesselMain.ClientID %>');
            //var strUser = e.options[e.selectedIndex].value;
            if (e.selectedIndex == 0) {
                alert("Copy threshold from Vessel is not selected!");
                e.focus();
                return false;
            }

            if (f.selectedIndex == 0) {
                alert("Vessel is not selected!");
                f.focus();
                return false;
            }

            if (e.selectedIndex == f.selectedIndex) {
                alert("Copy threshold from Vessel and Current vessel should not be the same!");
                f.focus();
                return false;
            }
        }

        function alertmessage(a) {
            if (a == 0) {
                alert("Copy threshold from Vessel values are not present in the database!");
            }
            if (a == 1) {
                alert("Threshold value's are updated!");
            }
        }
 
    </script>
    <style type="text/css">
        .CellClass1
        {
            background-color: Red;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        .style1
        {
            width: 235px;
        }
        .style2
        {
            height: 10px;
            width: 235px;
        }
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updMain" runat="server">
        <ContentTemplate>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>DECK LOG BOOK THRESHOLD </b>
                </div>
                 
                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                    border-color: #efefef; width: 100%; border-collapse: collapse;">
                    <tr>
                        <td style="width: 10%; text-align: center;">
                            Vessel Name :
                        </td>
                        <td style="text-align: Left;">
                            <asp:DropDownList ID="ddlVesselMain" runat="server" AutoPostBack="true" Width="160"
                                OnSelectedIndexChanged="ddlVesselMain_SelectedIndexChanged" />
                        </td>
                        <td style="width: 10%; text-align: center;">
                            <asp:Label ID="lblVersion" runat="server" Text="History:"> </asp:Label>
                        </td>
                        <td style="text-align: Left;">
                            <asp:DropDownList ID="DDLVersion" runat="server" UseInHeader="false" AutoPostBack="true"
                                Width="200" OnSelectedIndexChanged="DDLVersion_SelectedIndexChanged" />
                        </td>
                        </td>
                        <td style="text-align: center;">
                            <%--	<asp:Button ID="btnThresholdActionSetting" runat="server" Text=" Thrshold Action Settings " 
						OnClientClick="showPopup('ThresholdActionSettingDew')" 
						 />--%>
                            <%--<input type="button" onclick="showPopup('ThresholdActionSettingDew')" value="Thrshold Action Settings" />--%>
                        </td>
                        <td style="width: 35%; text-align: center;">
                            <div style="border: 1px solid #0489B1; background-color: #A9D0F5; padding: 1px;">
                                Copy threshold from Vessel :
                                <asp:DropDownList ID="ddlVessel" runat="server">
                                </asp:DropDownList>
                                <%--<asp:Button ID="Button1" Text="Copy To" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
							BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false" Font-Names="verdana"
							Height="20px" ForeColor="Blue" />--%>
                                <asp:Button ID="btnCopy" runat="server" Text="Copy Threshold" OnClick="btnCopy_Click"
                                    OnClientClick="return validation();" />
                            </div>
                        </td>
                        <td style="width: 8%; text-align: center;">
                            <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
                                Width="80px" Font-Size="12px" Font-Bold="true" Font-Names="verdana" Height="25px"
                                ForeColor="Blue" />
                        </td>
                    </tr>
                </table>
                <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
                    <asp:FormView ID="FormView1" runat="server" Height="60px" Width="100%" BorderWidth="0px"
                        Font-Size="Small">
                        <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                        <ItemTemplate>
                            <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                                        align="left" valign="top">
                                        <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                            <tr>
                                                <td width="100%" colspan="3" valign="top">
                                                    <table cellspacing="1" cellpadding="3" rules="all" border="3" style="background-color: #efefef;
                                                        border-color: #efefef; width: 100%;">
                                                        <tr align="center" style="background-color: #BCF5A9">
                                                            <td align="center" colspan="16">
                                                                DECK LOG BOOK THRESHOLD DETAILS
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td rowspan="2">
                                                            </td>
                                                            <td colspan="2">
                                                                ERROR
                                                            </td>
                                                            <td colspan="1">
                                                                WINDS
                                                            </td>
                                                            <td rowspan="2">
                                                                Sea
                                                            </td>
                                                            <td rowspan="2">
                                                                Visibility
                                                            </td>
                                                            <td rowspan="2">
                                                                Barometer
                                                            </td>
                                                            <td rowspan="2">
                                                                Air Temp
                                                            </td>
                                                            <td rowspan="2">
                                                                Sea Temp
                                                            </td>
                                                        </tr>
                                                        <tr align="center" class="HeaderCellColor">
                                                            <td>
                                                                Gyro
                                                            </td>
                                                            <td>
                                                                Standard
                                                            </td>
                                                            <%--<td>
                                                                Direction
                                                            </td>--%>
                                                            <td>
                                                                Force
                                                            </td>
                                                        </tr>
                                                        <tr style="border: 0px solid ActiveBorder;">
                                                            <td align="center" style="height: 19px; background-color: #BCF5A9">
                                                                <asp:Label ID="LblMin" Width="60px" runat="server" Text='Min'> </asp:Label>
                                                                <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
                                                                <asp:Label ID="lblThresHoldId" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtErrorGyroMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Error_Gyro")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtErrorStandardMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Error_Standard")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                         <%--   <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtWindsDirectionMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Winds_Direction")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>--%>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtWindsForceMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Winds_Force")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSeaMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Sea")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtVisibilityMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Visibility")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtBarometerMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Barometer")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtAirTempMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_AirTemp")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSeaTempMin" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_SeaTemp")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 19px;">
                                                            <td align="center" style="height: 19px; background-color: #F78181">
                                                                <asp:Label ID="LabelMax1" runat="server" Width="90%" Text='Max'> </asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtErrorGyroMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Error_Gyro")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtErrorStandardMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Error_Standard")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <%--<td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtWindsDirectionMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Winds_Direction")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>--%>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtWindsForceMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Winds_Force")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSeaMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Sea")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtVisibilityMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Visibility")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtBarometerMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Barometer")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtAirTempMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_AirTemp")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 19px;">
                                                                <asp:TextBox ID="txtSeaTempMax" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_SeaTemp")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                    <td align="center" class="HeaderCellColor" rowspan="2">
                                                       
                                                    </td>
                                                    <td align="center" class="HeaderCellColor" colspan="4">
                                                        Water in hold Threshold
                                                    </td>
                                                     <td align="center" class="HeaderCellColor"  colspan="4">
                                                        Water in Tank Threshold
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td class="HeaderCellColor" colspan="4">
                                                        Max Capacity 100%
                                                    </td>
                                                    <td class="HeaderCellColor" colspan="4">
                                                        Max Capacity 100%
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="background-color: #BCF5A9">
                                                        MIN
                                                    </td>
                                                    <td align="center" colspan="4">

                                                     <asp:TextBox ID="txtCapacity100MinHold" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Water_In_Hold_Capacity100")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                           

                                                     
                                                    </td>
                                                              <td align="center" colspan="4">

                                                     <asp:TextBox ID="txtCapacity100MinTank" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "MIN_Water_In_Tank_Capacity100")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                           

                                                     
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="background-color: #F78181">
                                                        MAX
                                                    </td>
                                                    <td align="center" colspan="4">
                                                        
                                                                <asp:TextBox ID="txtCapacity100MaxHold" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Water_In_Hold_Capacity100")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                    </td>
                                                     <td align="center" colspan="4">
                                                        
                                                                <asp:TextBox ID="txtCapacity100MaxTank" runat="server" Width="50%" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_Water_In_Tank_Capacity100")%>'
                                                                    MaxLength="8" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"> </asp:TextBox>
                                                    </td>
                                                </tr>
                                                    </table>
                                             </td>
                                        </tr>
                                           
                                              
                                               
                                           
                            </table>
                    </td> </tr> </table>
                      </ItemTemplate>
                    </asp:FormView>
                    </td>
                    </tr>
                    </table>
                </div>
                 
                <asp:HiddenField ID="lblLOGBOOKID" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
