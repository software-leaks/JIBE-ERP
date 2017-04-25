<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ERLogTASGThresHold.aspx.cs" Inherits="ERLogTASGThresHold" %>

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
    <script type="text/javascript">
        function ViewEdit(Id) {
            var value = document.getElementById('<%=lblLogId.ClientID%>').value;
            window.open("ERLogEdit.aspx?ViewID=" + Id + "&LOGID=" + value, "Test", "", "");
        }

        function MaskMoney(evt) {
            if (!(evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105))) {
                return false;
            }
            var parts = evt.srcElement.value.split(',');

            if (parts.length > 2) return false;
            if (evt.keyCode == 46) return (parts.length == 1);
            if (parts[0].length >= 14) return false;
           
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
        .HeaderCellColor1
        {
            background-color: #BCF5A9;
            color: Black ;
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
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <div id="page-header" class="page-title">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 95%; text-align: center;">
                        <b>ENGINE ROOM LOG BOOK THRESHOLD </b>
                    </td>
                    <td style="width: 5%; text-align: right; border-right: 2px solid Transparent">
                        <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="btnSave_Click" BackColor="#AED7FF"
                            BorderStyle="None" Width="70px" Font-Size="11px" Font-Bold="false" Font-Names="verdana"
                            Height="20px" ForeColor="Blue" />
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="lblLogId" runat="server"></asp:TextBox>
        </div>
     
    </div>
    <div style="text-align: center; overflow: scroll" id="dvPageContent" class="page-content-main">
        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                    align="center" valign="top">
                    <asp:FormView ID="FormView1" runat="server" Height="60px" Width="90%" BorderWidth="0px"
                        Font-Size="Small" OnDataBound="FormView1_DataBound">
                        <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                        <ItemTemplate>
                            <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                <tr>
                                    <td valign="top" align="center" width="90%;">
                                        <asp:Repeater ID="Repeater2" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 100%;">
                                                    <tr class ="HeaderCellColor1">
                                                        <td >
                                                            
                                                        </td>
                                                        <td colspan="11" align="center">
                                                            TURBO ALTERNATOR
                                                        </td>
                                                        <td colspan="9" align="center">
                                                            SHAFT GENERATOR
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class ="HeaderCellColor">
                                                    <td></td>
                                                        <td>
                                                            Run Hrs.
                                                        </td>
                                                        <td>
                                                            <label id="Label190" class="verticaltext1">
                                                                Strim Press
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label1" class="verticaltext1">
                                                                Cond Vac.</label>
                                                        </td>
                                                        <td>
                                                            <label id="Label5" class="verticaltext1">
                                                                Gland Steam
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label7" class="verticaltext1">
                                                                LO Press
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label11" class="verticaltext1">
                                                                L. O. Temp
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label12" class="verticaltext1">
                                                                Thrust brg C
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label13" class="verticaltext1">
                                                                Free end C
                                                            </label>
                                                        </td>
                                                        <td>
                                                            KW
                                                        </td>
                                                        <td>
                                                            Amps.
                                                        </td>
                                                        <td>
                                                            Run Hrs.
                                                        </td>
                                                        <td>
                                                            <label id="Label14" class="verticaltext1">
                                                                Clutch Air Pr.
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label15" class="verticaltext1">
                                                                LO Press
                                                            </label>
                                                        </td>
                                                        <td>
                                                            <label id="Label16" class="verticaltext1">
                                                                LO Temp.
                                                            </label>
                                                        </td>
                                                        <td colspan="2">
                                                            SG Cont Brg Temp
                                                        </td>
                                                        <td>
                                                            KW
                                                        </td>
                                                        <td>
                                                            Amps.
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 18px;" class ="HeaderCellColor1">
                                                        <asp:Label ID="Label159" Width="60px" runat="server" Text='Min'> </asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt1" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_RUNNING_HR")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt2" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_STEAM_PRESS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt3" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_COND_VAC")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt4" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_GLAND_STEAM")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt5" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_LO_PRESS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt6" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_LO_TEMP")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt7" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_THUST_BIG")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt8" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_FREE_END")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt9" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_KW")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt10" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_TA_AMPS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt11" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_RUNNING_HRG")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt12" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_CLUTCH_AIR_PR")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt13" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_LO_PRESS")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt14" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_LO_TEMP")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt15" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_SG_COND_BRG_TEMP1")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt16" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_SG_COND_BRG_TEMP2")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt17" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_KW")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt18" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Min_SG_AMPS")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td align="left" style="height: 18px;  background-color: #F78181">
                                                        <asp:Label ID="Label2" Width="60px" runat="server" Text='MAX'> </asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt01" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_RUNNING_HR")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt02" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_STEAM_PRESS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt03" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_COND_VAC")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt04" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_GLAND_STEAM")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt05" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_LO_PRESS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt06" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_LO_TEMP")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt07" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_THUST_BIG")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt08" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_FREE_END")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt09" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_KW")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt010" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_TA_AMPS")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt011" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_RUNNING_HRG")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt012" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_CLUTCH_AIR_PR")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 18px;">
                                                        <asp:TextBox ID="txt013" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_LO_PRESS")%>'></asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt014" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_LO_TEMP")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt015" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_SG_COND_BRG_TEMP1")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt016" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_SG_COND_BRG_TEMP2")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt017" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_KW")%>'> </asp:TextBox>
                                                    </td>
                                                    <td align="left" style="height: 19px;">
                                                        <asp:TextBox ID="txt018" runat="server" Width="40px" CssClass="input centeralinment"
                                                             MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" Text='<%# DataBinder.Eval(Container.DataItem, "Max_SG_AMPS")%>'> </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
