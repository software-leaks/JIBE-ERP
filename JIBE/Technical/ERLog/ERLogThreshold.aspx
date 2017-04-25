<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ERLogThreshold.aspx.cs" Inherits="ERLogThreshold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">
         function MaskMoney(evt) 
         {
             if (!(evt.keyCode == 9 || evt.keyCode == 110 || evt.keyCode == 8 || evt.keyCode == 37 || evt.keyCode == 39 || evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57) || (evt.keyCode >= 96 && evt.keyCode <= 105)))
             {
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
        </div>
    </div>
    <center>
        <div style="text-align: left; float: inherit" id="dvPageContent" class="page-content-main">
            <center>
                <table cellspacing="0" cellpadding="3" rules="all" border="0" style="background-color: White;
                    border-collapse: collapse; border-color: #efefef; border-width: 1px; border-style: None;"
                    font-size="Small">
                    <tr id="trfoda" runat="server">
                        <td valign="top" align="center">
                            <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                border-color: #efefef; width: 100%; border-collapse: collapse;">
                                <tr class ="HeaderCellColor1" >
                                    <td colspan="8" align="center" style="font-weight: bold">
                                        FUEL OIL DAILY ACCOUNT (MT)
                                    </td>
                                </tr>
                                <tr align="center" style="font-weight: bold" class ="HeaderCellColor">
                                    <td colspan="2" rowspan="2">
                                    </td>
                                    <td colspan="2">
                                        H. O.
                                    </td>
                                    <td colspan="2">
                                        D. O.
                                    </td>
                                    <td colspan="2">
                                        G. O.
                                    </td>
                                </tr>
                                <tr align="center" style="font-weight: bold" class ="HeaderCellColor">
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        ROB, Prev. Noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_ROB_PNN" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_ROB_PNN_max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_ROB_PNN" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_ROB_PNN_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_ROB_PNN" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_ROB_PNN_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="7" class ="HeaderCellColor">
                                        <asp:Label ID="Label52" runat="server" class="verticaltext" > CONSUMPTIONS </asp:Label>
                                    </td>
                                    <td>
                                        Mani Engine
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_ME" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_ME_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_ME" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_ME_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_ME" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_ME_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Aux Engine
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_AE" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_AE_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_AE" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_AE_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_AE" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_AE_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Boiler
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_BLR" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_BLR_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_BLR" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_BLR_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_BLR" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_BLR_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Tk Clean
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_TC" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_TC_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_TC" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_TC_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_TC" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_TC_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Heat'g/IG
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_HTG" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_HTG_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_HTG" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_HTG_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_HTG" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_HTG_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        --
                                    </td>
                                    <td>
                                        <asp:Label ID="Label68" runat="server" Text=""> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label69" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label105" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text=""> </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_TTL" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_CONS_TTL_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_TTL" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_CONS_TTL_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_TTL" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_CONS_TTL_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Received
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_RCVD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_RCVD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_RCVD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_RCVD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_RCVD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_RCVD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Amended
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_AMEND" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_AMEND_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_AMEND" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_AMEND_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_AMEND" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_AMEND_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        ROB This Noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_ROB" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);"  Width="60px" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_HO_ROB_Max" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_ROB" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);"  runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_DO_ROB_Max" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_ROB" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);"  runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFODA_GO_ROB_Max" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trfwda" runat="server">
                        <td valign="top">
                            <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                border-color: #efefef; width: 100%; border-collapse: collapse;">
                                <tr class ="HeaderCellColor1">
                                    <td colspan="9" align="center" style="font-weight: bold">
                                        FRESH WATER DAILY ACCOUNT
                                    </td>
                                </tr>
                                <tr align="center" style="font-weight: bold" class ="HeaderCellColor">
                                    <td rowspan="3">
                                    </td>
                                    <td rowspan="2" colspan="2">
                                        PORTABLE
                                    </td>
                                    <td colspan="4">
                                        WASH
                                    </td>
                                    <td rowspan="2" colspan="2">
                                        DISTL'D
                                    </td>
                                </tr>
                                <tr align="center" style="font-weight: bold"  class ="HeaderCellColor">
                                    <td colspan="2">
                                        PORT
                                    </td>
                                    <td colspan="2">
                                        STBD
                                    </td>
                                </tr>
                                <tr class ="HeaderCellColor">
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ROB Prev. Noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_ROB_PNN" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_ROB_PNN_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_ROB_PNN" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHS_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_ROB_PNN_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_DISTL_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_ROB_PNN" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_POT_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_ROB_PNN_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHP_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_ROB_PNN" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_ROB_PNN_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_ROB_PNN") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Produced
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_PROD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_PROD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_PROD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_PROD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_PROD" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_PROD_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_PROD" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_PROD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_PROD_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_PROD") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Received
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_RCVD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_RCVD_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_RCVD" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_RCVD_max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_RCVD" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_POT_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_RCVD_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_RCVD" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_RCVD") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_RCVD_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_RCVD") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Domastic Cons.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_CNSMP" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_CNSMP_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_CNSMP" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_CNSMP_max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_CNSMP" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_CNSMP_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_CNSMP" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_CNSMP") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_CNSMP_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_CNSMP") %>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ..
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ROB This Noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_ROB" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_ROB") %>'> </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_POT_ROB_max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_POT_ROB") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_ROB" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_ROB") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHP_ROB_Max" runat="server" CssClass="input centeralinment" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Width="60px" Text='<%# Bind("FWDA_WASHP_ROB") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_ROB" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_ROB") %>'> </asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_WASHS_ROB_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_WASHS_ROB") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_ROB" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_ROB") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFWDA_DISTL_ROB_Max" runat="server" CssClass="input centeralinment" Width="60px" onKeydown="JavaScript:return MaskMoney(event);" 
                                            Text='<%# Bind("FWDA_DISTL_ROB") %>'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trworkingh" runat="server">
                        <td valign="top">
                            <label>
                                <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                    border-color: #efefef; width: 100%; border-collapse: collapse;">
                                    <tr class ="HeaderCellColor1">
                                        <td colspan="8" align="center" style="font-weight: bold">
                                            WORKING HOURS
                                        </td>
                                    </tr>
                                    <tr align="center" style="font-weight: bold;" class ="HeaderCellColor">
                                        <td>
                                            NOON TO NOON
                                        </td>
                                        <td>
                                            M/E
                                        </td>
                                        <td>
                                            AE-1
                                        </td>
                                        <td>
                                            AE-2
                                        </td>
                                        <td>
                                            AE-3
                                        </td>
                                        <td>
                                            AE-4
                                        </td>
                                        <td>
                                            TA
                                        </td>
                                        <td>
                                            SE
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Min
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_ME_NN_Min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE1_NN_min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE2_NN_min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE3_NN_min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE4_NN_min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_TA_NN_Min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_SG_NN_Min" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="border: 1px solid ActiveBorder">
                                        <td>
                                            Max
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_ME_NN_max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE1_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE2_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE3_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_AE4_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_TA_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);" CssClass="input centeralinment"
                                                Width="60px"> </asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWRKHRS_SG_NN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment"
                                                Width="60px"> </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </label>
                        </td>
                    </tr>
                    <tr id="trdp" runat="server">
                        <td>
                            <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                border-color: #efefef; width: 100%; border-collapse: collapse;">
                                <tr class ="HeaderCellColor">
                                    <td colspan="13" align="center" style="font-weight: bold">
                                        DAILY PERFORMANCE
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Wind Force
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_WIND_FORCE" runat="server" CssClass="input" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Rel. Direction
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_WIND_DIR" runat="server" CssClass="input " Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Sea Cond
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_SEA_COND" runat="server" CssClass="input " Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Swell Height
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_SWELL" runat="server" CssClass="input " Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Rel. Direction
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_SWELL_DIR" runat="server" CssClass="input " Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Curr.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_CURR" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        REV. Noon to Noon
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDP_REVS_NTN" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Engine Distance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_ENG_DIST" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Objerved Distance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_OBS_DIST" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Total Distance
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDP_TTL_DIST" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Hours Full Speed
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDP_HRS_FUL_SPD" runat="server" CssClass="input centeralinment"
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Average RPM
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_AVG_RPM" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Slip%
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_SLIP" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Distance To Go
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDP_DIST_TOGO" runat="server" CssClass="input centeralinment"
                                            Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Hrs Received Spd
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDP_HRS_RED_SPD" runat="server" CssClass="input centeralinment"
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Hours Speed
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_HRS_STOPD" runat="server" CssClass="input centeralinment"
                                            Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        Objerved Speed
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDP_OBS_SPD" runat="server" CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        ETA
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtETA" runat="server" CssClass="input" Width="60px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtETA"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trloda" runat="server">
                        <td style="width: 100%; border-right: solid; border-right-color: Gray; border-right-width: 1px"
                            align="left" valign="top">
                            <table cellspacing="0" cellpadding="3" rules="all" border="1" style="background-color: White;
                                border-color: #efefef; width: 100%; border-collapse: collapse;">
                                <tr class ="HeaderCellColor1">
                                    <td colspan="7" align="center" style="font-weight: bold">
                                        LUBRICATING OIL DAILY ACCOUNT (LTR)
                                    </td>
                                </tr>
                                <tr align="center" style="font-weight: bold" class ="HeaderCellColor">
                                    <td rowspan="2">
                                        Grade
                                    </td>
                                    <td colspan="2">
                                        MECC
                                    </td>
                                    <td colspan="2">
                                        MECYL
                                    </td>
                                    <td colspan="2">
                                        AECC
                                    </td>
                                </tr>
                                <tr align="center" class ="HeaderCellColor">
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                    <td>
                                        Min
                                    </td>
                                    <td>
                                        Max
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ROB Prev Noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_ROB_PNN" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_ROB_PNN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_ROB_PNN" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_ROB_PNN_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_ROB_PNN" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_ROB_PNN_max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Received
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_RCVD" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_RCVD_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_RCVD" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_RCVD_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_RCVD" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_RCVD_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Consumed
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_CNSMP" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_CNSMP_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_CNSMP" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_CNSMP_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_CNSMP" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_CNSMP_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        ROB This noon
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_ROB" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECC_ROB_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_ROB" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_MECYL_ROB_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_ROB" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLODA_AECC_ROB_Max" runat="server" onKeydown="JavaScript:return MaskMoney(event);"  CssClass="input centeralinment" Width="60px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </center>
</asp:Content>
