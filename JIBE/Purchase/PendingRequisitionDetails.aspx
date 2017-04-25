<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PendingRequisitionDetails.aspx.cs"
    Inherits="PendingRequisitionDetails" Title="Purchase Process" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"    TagPrefix="ucDDL" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="styles/premiere_blue/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            width: 155px;
        }
        /* For IE6 */* HTML .ob_iLboICBC li
        {
            -width: 135px;
        }
        * HTML .ob_iLboICBC li b
        {
            width: 135px;
            overflow: hidden;
        }
        .style1
        {
            width: 100%;
        }
    </style>
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

        .TableStyleCSS
        {
            width: 100%;
        }
        
        .QuestionCSS
        {
        }
        .QuestionCSS-Icon
        {
            width: 20px;
        }
        .QuestionCSS-FAQ
        {
            width: 90%;
        }
        .QuestionCSS-FAQ a
        {
            font-weight: bold;
            font-size: 12px;
            text-decoration: none !important;
        }
        .VideoCSS a
        {
            font-weight: bold;
            font-size: 12px;
            text-decoration: none !important;
        }
        .VideoCSS-Icon
        {
            width: 26px;
            width: 20px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function RefreshThispage() {

            location.reload(true);
        }

        function fixform() {
            if (opener.document.getElementById("aspnetForm").target != "_blank")
                return;
            opener.document.getElementById("aspnetForm").target = "";
            opener.document.getElementById("aspnetForm").action = opener.location.href;
        }



        var lo;
        function selMe(src) {
            try {
                var o;
                var p;
                if (src) {
                    o = document.getElementById(src);
                }
                else {
                    o = window.event.srcElement;
                }
                p = o.parentElement.parentElement;
                p.className = 'ih';
                if (lo) {
                    if (lo.id != p.id) {
                        lo.className = '';
                        lo = p;
                    }
                }
                else {
                    lo = p;
                }
            } catch (ex) {
            }
        }

        function showprogrss(v_status) 
        {
            var updateProgress = $get("<%=upUpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
            var hdrTitle = 'Purchase -> Purchase Process -> ' + v_status;
            $(".page-title").html(hdrTitle);
            switch(v_status) {
                case "Pending Supplier Confirmation":
                    $("#<%=lblhdrDate.ClientID %>").text('PO Issue Date :');
                    break;
                case "Pending Delivery Update":
                    $("#<%=lblhdrDate.ClientID %>").text('PO Issue Date :');
                    break;
                case "Delivered":
                    $("#<%=lblhdrDate.ClientID %>").text('Delivery Date :');
                    break;
                default:
                 $("#<%=lblhdrDate.ClientID %>").text('Receival Date :');
                
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="page-title">
                Purchase -> Purchase Process -> New Requisitions
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <center>
        <%--<div style="padding: 1px; background-color: #808080; color: #FFFFFF; text-align: center;">
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 95%">
                    <b style="font-size: small">Purchase Process</b>
                </td>
                <td valign="middle" align="left" style="width: 5%">
                   
                </td>
                
            </tr>
        </table>
        </div>--%>
        <table  style="height: 25px; width: 100%; background-color: #f4ffff; border: 1px solid gray;"      border="0">
            <tr>
                <td width="84%">
                    <table style="height: 25px; width: 100%; background-color: #f4ffff; border: 1px solid gray;">
                        <tr>
                            <td valign="top" style="border: 1px solid gray; color: Black">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="style1" width="50%">
                                        </table>
                                        <table class="style1" width="100%" cellpadding="4" cellspacing="2" >
                                            <tr>
                                                <td align="left" valign="top" width="17%">
                                                    PO Type :
                                                </td>
                                                <td valign="top" align="left" width="17%">
                                                    <ucDDL:ucCustomDropDownList ID="ddlPO_Type" runat="server" UseInHeader="false" OnApplySearch="ddlPO_Type_SelectedIndexChanged" />
                                                </td>
                                                <td align="left" valign="top" width="17%">
                                                    Requisition Type :
                                                </td>
                                                <td valign="top" align="left" width="17%">
                                                    <ucDDL:ucCustomDropDownList ID="ddlReqsnType" runat="server" UseInHeader="false"
                                                        OnApplySearch="ddlReqsnType_SelectedIndexChanged" />
                                                </td>
                                                <td align="left" width="90%">
                                                    Fleet :
                                                </td>
                                                <td valign="top" align="left">
                                                    <ucDDL:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                                        Height="150" Width="160" />
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    Department/Function:
                                                </td>
                                                <td valign="top" align="left">
                                                    <ucDDL:ucCustomDropDownList ID="cmbDept" Style="float: left" runat="server" UseInHeader="false"
                                                        OnApplySearch="cmbDept_OnSelectedIndexChanged" Height="200" Width="160" />
                                                </td>
                                                 <td align="left" valign="top">
                                                    Catalogue/System :
                                                </td>
                                                <td align="left">
                                                    <ucDDL:ucCustomDropDownList ID="ddlCatalogue" runat="server" UseInHeader="false" OnApplySearch="ddlCatalogue_SelectedIndexChanged" />
                                                </td>
                                                <td align="left" valign="top">
                                                    Account Type :
                                                </td>
                                                <td align="left" >
                                                    <ucDDL:ucCustomDropDownList ID="ddlAccType" runat="server" UseInHeader="false" OnApplySearch="ddlAccType_SelectedIndexChanged" />
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                            <td  align="left" valign="top">
                                                    Account Code :
                                                </td>
                                                <td align="left">
                                                    <ucDDL:ucCustomDropDownList ID="ddlAccClassification" runat="server" OnApplySearch="ddlAccClassification_SelectedIndexChanged"
                                                        UseInHeader="false" />
                                                </td>
                                                 <td align="left" >
                                                    Urgency Level :
                                                </td>
                                                <td align="left" >
                                                    <ucDDL:ucCustomDropDownList ID="ddlUrgencyLvl" runat="server" OnApplySearch="ddlUrgencyLvl_SelectedIndexChanged" UseInHeader="false"/>
                                                </td>
                                                <td align="left">
                                                    Requisition Status :
                                                </td>
                                                <td align="left">
                                                    <ucDDL:ucCustomDropDownList ID="ddlReqsnStatus" OnApplySearch="ddlReqsnStatus_SelectedIndexChanged" runat="server" 
                                                        UseInHeader="false" />
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                            <td align="left">
                                                    Vessel :
                                                </td>
                                                <td valign="top" align="left">
                                                    <ucDDL:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged" />
                                                </td>
                                            <td align="left">
                                                   <asp:Label ID ="lblhdrDate" Text ="Receival Date :" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <table width="80%">
                                                        <tr>

                                                            <td>
                                                                From:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtfrom" runat="server" Width="90px" Style="font-size: small" 
                                                                    ontextchanged="txtfrom_TextChanged"></asp:TextBox>
                                                                <tlk4:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtfrom"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                            </td>
                                                            <td>
                                                                To:
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTo" runat="server" Width="90px" Style="font-size: small" 
                                                                    ontextchanged="txtTo_TextChanged"></asp:TextBox>
                                                                <tlk4:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTo"
                                                                    Format="dd/MM/yyyy">
                                                                </tlk4:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                             <td align="left">
                                                    Requisition Number / Order Number :
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtReqsnOrderNo" runat="server" Width="200px"></asp:TextBox>
                                                    <tlk4:TextBoxWatermarkExtender ID="twReqsnOrder" runat="server" TargetControlID="txtReqsnOrderNo"
                                                        WatermarkText="Requisition / Order Number" WatermarkCssClass="">
                                                    </tlk4:TextBoxWatermarkExtender>
                                                    <asp:ImageButton ID="imgSearch" runat="server" ToolTip="Search Requisition Number/Order Number"
                                                        ImageUrl="~/Images/SearchButton.png" Width="20px" Height="18px" 
                                                        Style="vertical-align: top" onclick="imgSearch_Click" />
                                                </td>
                                            
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                </td>
                <td valign="top" style="border: 1px solid gray; color: Black" width="8%">
                    <%--  <asp:Panel runat="server" GroupingText="Requisition Stages"> --%>
                    <%--                            <asp:PlaceHolder ID="ListBox1Container" runat="server"></asp:PlaceHolder>
                    --%>
                    <%-- </asp:Panel> --%>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="ob_iLboIC ob_iLboIC_L" style="width: 430px; height: 120px; visibility: visible;
                                background-image: url(image/navmenubg.png); background-repeat: no-repeat;">
                                <div class="ob_iLboICH">
                                    <div class="ob_iLboICHCL">
                                    </div>
                                    <div class="ob_iLboICHCM">
                                    </div>
                                    <div class="ob_iLboICHCR">
                                    </div>
                                </div>
                                <div class="ob_iLboICB">
                                    <div class="ob_iLboICBL">
                                        <div class="ob_iLboICBLI">
                                        </div>
                                    </div>
                                    <ul class="ob_iLboICBC" style="height: 100px; min-height: 90px; float: left; width: 185px;
                                        margin-left: 2px;">
                                        <li id="li1"><b>
                                            <asp:LinkButton ID="lnkMenu1" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('New Requisitions');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="NRQ" Text="New Requisitions"></asp:LinkButton></b><i>NewRequisition.aspx?Type=NRQ</i></li>
                                        <li id="li2"><b>
                                            <asp:LinkButton ID="lnkMenu2" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('RFQ / Quotation');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="RFQ" Text="RFQ / Quotation"></asp:LinkButton></b><i>PendingReqGrid.aspx?Type=RFQ</i></li>
                                        <!-- <li id="li3"><b>
                                                        <asp:LinkButton ID="lnkMenu3" runat="server" OnClick="NavMenu_Click" CommandArgument="UPQ">Quotation Status</asp:LinkButton></b><i>PendingReqGrid.aspx?Type=UPQ</i></li> -->
                                        <li id="li4"><b>
                                            <asp:LinkButton ID="lnkMenu4" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Quotation Approval');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="QEV" Text="Quotation Approval"></asp:LinkButton></b><i>PendingEvalGrid.aspx?Type=QEV</i></li>
                                        <li id="li5"><b>
                                            <asp:LinkButton ID="lnkMenu6" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Approved Quotations');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="RPO" Text="Approved Quotations"></asp:LinkButton></b><i>PendingPOGrid.aspx?Type=RPO</i></li>
                                        <%-- <asp:LinkButton ID="lnkMenu5" Visible="false" runat="server" OnClick="NavMenu_Click" CommandArgument="PFA">Assign Budget code and Approve</asp:LinkButton></b><i>PendingPOApproveGrid.aspx?Type=PFA</i></li>--%>
                                        <li id="li3"><b>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Pending Supplier Confirmation');"
                                                Style="font-family: Verdana; font-size: 9.3px" CommandArgument="SCN">
                                                <asp:Label ID="lbls" runat="server" Text="Pending Supplier Confirmation"></asp:Label></asp:LinkButton></b><i>PendingPOConfirmGrid.aspx?Type=SCN</i></li>
                                    </ul>
                                    <ul class="ob_iLboICBC" style="height: 110px; min-height: 90px; float: left; width: 183px;
                                        margin-left: 2px; padding-left: 6px;">
                                        <li id="li8"><b>
                                            <asp:LinkButton ID="lnkMenu8" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Pending Delivery Update');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="DVS" Text="Pending Delivery Update"></asp:LinkButton></b><i>DeliveryStatusGrid.aspx?Type=DVS</i></li>
                                        <%--<li id="li9"><b>
                                                        <asp:LinkButton ID="lnkMenu9" runat="server" OnClick="NavMenu_Click" Style="font-family: Verdana;
                                                            font-size: 10px" CommandArgument="UPD">Pending Delivery Update</asp:LinkButton></b><i>PendingDeliveryGrid.aspx?Type=UPD</i></li>--%>
                                        <li id="li6"><b>
                                            <asp:LinkButton ID="lbtnDelivered" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Delivered');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="DLV" Text="Delivered"></asp:LinkButton></b><i>Delivered_Requisition.aspx?Type=DLV</i></li>
                                        <li id="li10"><b>
                                            <asp:LinkButton ID="lnkMenu10" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('ALL Status');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="ALL" Text="ALL Status"></asp:LinkButton></b><i>ALLRequisitioinStatusGrid.aspx?Type=ALL</i></li>
                                        <li id="li11"><b>
                                            <asp:LinkButton ID="lnkMenu11" runat="server" OnClick="NavMenu_Click" OnClientClick="showprogrss('Cancelled');"
                                                Style="font-family: Verdana; font-size: 10px" CommandArgument="CAN" Text="Cancelled"></asp:LinkButton></b><i>CancelledLog.aspx?Type=CAN</i></li>
                                    </ul>
                                    <div class="ob_iLboICBR">
                                        <div class="ob_iLboICBRI">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="txtSelMenu" runat="server"></asp:HiddenField>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="border: 1px solid gray; color: Black; vertical-align: bottom" width="6%">
                    <table width="100%">
                        <tr>
                            <td align="right" style="vertical-align: top">
                                <asp:ImageButton ID="btnExport" ToolTip="Export To Excel" ImageUrl="~/Images/XLS1.jpg"
                                    Height="25px" OnClick="btnExport_Click" runat="server" />
                            </td>
                             <td align="right" > <asp:Button ID="btnClear" runat="server"  CssClass="btnCSS"  OnClick="btnClear_Click"
                                   Text="Clear All Filters"/></td>
                        </tr>
                    </table>
                    <%--  <tr>
                               <td align="right" > <asp:Button ID="btnClear" runat="server"  CssClass="btnCSS"  OnClick="btnClear_Click"
                                   Text="Clear All Filters"/></td>
                               </tr>
                                      <asp:LinkButton ID="lbtnbulkpurchase" runat="server" Visible="false"></asp:LinkButton> 
                    --%>
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <iframe id="mainFrame" runat="server" frameborder="0" height="600px" width="100%"
                                scrolling="no" style="border-style: none; border-color: inherit; border-width: 0px;
                                padding: 0px; min-height: 600px; margin-left: 0px; margin-right: 0px; margin-top: 0px;"
                                onclick="return mainFrame_onclick();"></iframe>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="LinkUrl" runat="server" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
