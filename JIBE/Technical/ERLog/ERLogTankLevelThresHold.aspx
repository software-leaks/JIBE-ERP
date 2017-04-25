<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ERLogTankLevelThresHold.aspx.cs" Inherits="ERLogTankLevelThresHold" %>

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
       <%-- <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
            border-color: #efefef; width: 100%; border-collapse: collapse;">
            <tr>
                <td width="6%;">
                    M. V.
                </td>
                <td>
                    <asp:Label ID="lblMV" runat="server"> </asp:Label>
                </td>
                <td width="6%;">
                    FROM :
                </td>
                <td>
                    <asp:Label ID="lblfrom" runat="server"> </asp:Label>
                </td>
                <td width="6%;">
                    To:
                </td>
                <td>
                    <asp:Label ID="lblTo" runat="server"> </asp:Label>
                </td>
                <td width="6%;">
                    Date:
                </td>
                <td>
                    <asp:Label ID="lblDate" runat="server"> </asp:Label>
                </td>
                <td width="6%;">
                    Next Port:
                </td>
                <td width="15%;">
                    <asp:Label ID="lblNextPort" runat="server"> </asp:Label>
                    
                </td>
            </tr>
        </table>--%>
    </div>
    <div style="text-align: left; overflow: scroll" id="dvPageContent" class="page-content-main">
        <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
            border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                    align="left" valign="top">
                    <asp:FormView ID="FormView1" runat="server" Height="60px" Width="100%" BorderWidth="0px"
                        Font-Size="Small" OnDataBound="FormView1_DataBound">
                        <RowStyle CssClass="PMSGridRowStyle-css" Height="18" />
                        <ItemTemplate>
                            <table cellspacing="0" cellpadding="3" border="0.5" style="background-color: White;
                                border-color: #efefef; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                <tr align="center">
                                    <td valign="top">
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="3" rules="all" border="3" style="background-color: White;
                                                    border-color: #efefef; width: 60%;">
                                                    <tr class="HeaderCellColor1">
                                                        <td colspan="12" align="center">
                                                            TANK LEVEL
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor">
                                                       
                                                        <td rowspan="2" colspan="2">
                                                            CYL. OIL Day Tk
                                                        </td>
                                                        <td rowspan="2" colspan="2">
                                                            M/E Sump
                                                        </td>
                                                        <td colspan="4">
                                                            Heavy Oil
                                                        </td>
                                                        <td rowspan="2" colspan="2">
                                                            Blended Oil
                                                        </td>
                                                        <td rowspan="2" colspan="2">
                                                            D. O. Serv Tk
                                                        </td>
                                                    </tr>
                                                    <tr align="center" class="HeaderCellColor">
                                                        <td colspan="2">
                                                            Settl. Tk
                                                        </td>
                                                        <td colspan="2">
                                                            Serv Tk
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            Min
                                                        </td>
                                                        <td style ="background-color: #F78181">
                                                            Max
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="border: 1px solid ActiveBorder">
                                                    <td style="height: 18px;">                                                       
                                                        <asp:Label ID="lblid" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'> </asp:Label>
                                              
                                                        <asp:Label ID="lblVessel" Width="60px" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "VESSEL_ID")%>'> </asp:Label>
                                             
                                                        <asp:TextBox ID="txtCYL_OIL_DAY_TK" Width="60px" runat="server" CssClass="input centeralinment"   MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MIN_CYL_OIL_DAY_TK")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtCYL_OIL_DAY_TK_Max" runat="server" Width="60px" CssClass="input centeralinment"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MAX_CYL_OIL_DAY_TK")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtME_SUMP" Width="60px" CssClass="input centeralinment"   MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Min_ME_SUMP")%>'> </asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtME_SUMP_Max" Width="60px" CssClass="input centeralinment"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MAX_ME_SUMP")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtHEAVY_OIL_SETTL_TK" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MIN_HEAVY_OIL_SETTL_TK")%>'> </asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtHEAVY_OIL_SETTL_TK_Max" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MAX_HEAVY_OIL_SETTL_TK")%>'> </asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtHEAVY_OIL_SERV_TK" Width="60px" runat="server" CssClass="input centeralinment"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MIN_HEAVY_OIL_SERV_TK")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtHEAVY_OIL_SERV_TK_Max" runat="server" Width="60px" CssClass="input centeralinment"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MAX_HEAVY_OIL_SERV_TK")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtBELENDED_OIL" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MIN_BELENDED_OIL")%>'> </asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtBELENDED_OIL_Max" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MAX_BELENDED_OIL")%>'></asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtDO_SERV_TK" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MIN_DO_SERV_TK")%>'> </asp:TextBox>
                                                    </td>
                                                    <td style="height: 18px;">
                                                        <asp:TextBox ID="txtDO_SERV_TK_max" Width="60px" CssClass="input centeralinment" runat="server"  MaxLength="7" onKeydown="JavaScript:return MaskMoney(event);" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "MAX_DO_SERV_TK")%>'> </asp:TextBox>
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
