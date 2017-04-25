<%@ Page Title="Crew MailBox" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewMailBox.aspx.cs" Inherits="CrewMail_CrewMailBox" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link type="text/css" href="../styles/ui-lightness/jquery-ui-1.8.14.custom.css" rel="stylesheet" />
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script type="text/javascript" src="../scripts/jquery-ui-1.8.14.custom.min.js"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../Scripts/VesselInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <style type="text/css">
        input
        {
            font-family: Tahoma;
        }
        select
        {
            font-family: Tahoma;
        }
        .gradiant-css-browne
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#81F79F',EndColorStr='#088A4B');
            background: -webkit-gradient(linear, left top, left bottom, from(#81F79F), to(#088A4B));
            background: -moz-linear-gradient(top,  #81F79F,  #088A4B);
            color: Black;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
        
        .grid-view-row
        {
            border-bottom: 1px dashed #A9BCF5;
        }
        .overlay
        {
            visibility: hidden;
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
            z-index: 999;
        }
        
        #dvVesselMovement
        {
            visibility: hidden;
            position: absolute;
            left: 20%;
            top: 25%;
            width: 60%;
            text-align: center;
            z-index: 1000;
            border: 1px solid #333;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
    </style>
    <style id="TooltipStyle" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: Tahoma;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .grid-row
        {
            border: 1px solid #cccccc;
        }
        .grid-col-fixed
        {
            border: 1px solid #cccccc;
        }
        .grid-col
        {
            border: 1px solid #cccccc;
        }
    </style>
    <style id="Style1" type="text/css">
        .thdrcell
        {
            background: #F3F0E7;
            font-family: arial;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
        }
        .tdatacell
        {
            font-family: arial;
            font-size: 12px;
            padding: 5px;
            background: #FFFFFF;
        }
        .dvhdr1
        {
            font-family: Tahoma;
            font-size: 12px;
            font-weight: normal;
            padding: 5px;
            width: 200px;
            color: Black; /*background: #F5D0A9;*/
            border: 1px solid #F1C15F;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .dvbdy1
        {
            background: #FFFFFF;
            font-family: arial;
            font-size: 11px; /*border-left: 2px solid #3B0B0B;
            border-right: 2px solid #3B0B0B;
            border-bottom: 2px solid #3B0B0B;*/
            padding: 5px;
            width: 200px;
            background-color: #E8FDE8;
            border: 1px solid #F1C15F;
            color: Black;
        }
        p
        {
            margin-top: 20px;
        }
        h1
        {
            font-size: 13px;
        }
        .dogvdvhdr
        {
            width: 300;
            background: #C4D5E3;
            border: 1px solid #C4D5E3;
            font-weight: bold;
            padding: 10px;
        }
        .dogvdvbdy
        {
            width: 300;
            background: #FFFFFF;
            border-left: 1px solid #C4D5E3;
            border-right: 1px solid #C4D5E3;
            border-bottom: 1px solid #C4D5E3;
            padding: 10px;
        }
        .pgdiv
        {
            width: 320;
            height: 250;
            background: #E9EFF4;
            border: 1px solid #C4D5E3;
            padding: 10px;
            margin-bottom: 20;
            font-family: arial;
            font-size: 12px;
        }
        .gradiant-css-orange
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#F8D79B',EndColorStr='#F1C15F');
            background: -webkit-gradient(linear, left top, left bottom, from(#F8D79B), to(#F1C15F));
            background: -moz-linear-gradient(top,  #F8D79B,  #F1C15F);
            color: Black;
        }
        .gradiant-css-blue
        {
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#CEE3F6',EndColorStr='#99BBE6');
            background: -webkit-gradient(linear, left top, left bottom, from(#CEE3F6), to(#99BBE6));
            background: -moz-linear-gradient(top,  #CEE3F6,  #99BBE6);
            color: Black;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.draggable').draggable();
        });
        
    </script>
    <script language="javascript" type="text/javascript">

        var evt;
        function showChildItems(event, packetId) {
            evt = event;
            getChildItems(packetId);
        }
        var lastExecutor_WebServiceProxy;
        function getChildItems(packetId) {
            if (lastExecutor_WebServiceProxy != null)
                lastExecutor_WebServiceProxy.abort();
            var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewMail_PacketItems', false, { "PacketID": packetId, "DateFormat": DateFormat.toString() }, getChildItems_onSuccess, getChildItems_onFail);
            lastExecutor_WebServiceProxy = service.get_executor();
        }

        function getChildItems_onSuccess(retval) {
            js_ShowToolTip_Fixed(retval, evt, null, "Mail Items");
        }

        function getChildItems_onFail(err_) {
            alert(err_._message);
        }


        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
            //            $('#' + dv).animate({ height: 1 }, 1);
            //            $('#' + dv).animate({ height: 150 }, 1000);
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
            //$('#dvItemList').show();
        }
        function overlay() {
            var el;
            if (!document.getElementById("overlay")) {
                el = document.createElement("div");
                el.id = 'overlay';
                el.className = 'overlay';
            }
            else {
                el = document.getElementById("overlay");
            }

            el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";

            document.body.appendChild(el);

            el2 = document.getElementById("dvVesselMovement");
            el2.style.visibility = (el2.style.visibility == "visible") ? "hidden" : "visible";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Crew Mail Admin
    </div>
    <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
        min-height: 600px; padding: 5px; overflow: auto;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 60%; vertical-align: top;">
                    <asp:UpdatePanel ID="UpdatePanel_Search" runat="server">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        Fleet
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlFleetFilter" runat="server" Width="156px" OnSelectedIndexChanged="ddlFleetFilter_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Vessel
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVesselFilter" runat="server" Width="156px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlVesselFilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Search
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" OnTextChanged="txtSearchText_TextChanged"
                                            AutoPostBack="true"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel_AddItem" runat="server">
                        <ContentTemplate>
                            <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                                padding: 2px; font-size: 16px; margin-top: 5px; margin-bottom: 5px; color: #0B3861;">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            Items - Not yet added to packet
                                        </td>
                                        <td style="width: 50%; text-align: right">
                                            <asp:Button ID="btnNewItemItem" runat="server" Text="Add New Item" BorderStyle="Solid"
                                                BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5"
                                                OnClick="btnAddNewItem_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="pnlAddItem" runat="server" Visible="false" DefaultButton="btnSaveItemAndAdd">
                                <div id="dvAddItem" style="border: 1px solid #A9BCF5; background-color: #EFF2FB;
                                    color: Black; padding: 10px;">
                                    <div style="padding: 5px; font-size: 14px; font-weight: bold;">
                                        New Item Entry</div>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 100px">
                                                Date Placed
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDatePlaced" Width="80px" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="static"
                                                    ControlToValidate="txtDatePlaced" ErrorMessage="Select Date Placed!!" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDatePlaced">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Ref No
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRefNo" Width="250px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                Quantity
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" Width="50px" runat="server"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQty"
                                                    ErrorMessage="Please Enter Only Numbers" Style="z-index: 101; position: absolute;"
                                                    ValidationExpression="^\d+$" ValidationGroup="ValidEntry"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="static"
                                                    ControlToValidate="txtQty" ErrorMessage="Enter Quantity !!" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Description
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDesc" Width="250px" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="static"
                                                    ControlToValidate="txtDesc" ErrorMessage="Enter item description!!" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                Remarks
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" Width="250px" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center">
                                                <asp:CheckBox ID="chkAddToPacket" runat="server" Text="Add to Packet" Checked="true" />
                                                <asp:Button ID="btnSaveItemAndAdd" runat="server" Text="Save & Add New" ValidationGroup="ValidEntry"
                                                    BorderStyle="Solid" BorderColor="white" BorderWidth="1px" Font-Names="Tahoma"
                                                    Height="24px" Width="150px" BackColor="#81DAF5" OnClick="btnSaveItemAndAdd_Click" />
                                                <asp:Button ID="btnSaveItemAndClose" runat="server" Text="Save & Close" ValidationGroup="ValidEntry"
                                                    BorderStyle="Solid" BorderColor="white" BorderWidth="1px" Font-Names="Tahoma"
                                                    Height="24px" Width="150px" BackColor="#81DAF5" OnClick="btnSaveItemAndClose_Click" />
                                                <asp:Button ID="btnCloseAddItem" runat="server" Text="Cancel" BorderStyle="Solid"
                                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                                    BackColor="#81DAF5" OnClick="btnCloseAddItem_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                                * Mandatory fields
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel_Items" runat="server">
                        <ContentTemplate>
                            <div id="dvItemList">
                                <asp:GridView ID="GridView_MailItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID" EmptyDataText="No pending items found"
                                    GridLines="Horizontal" Width="100%" BorderStyle="None" BorderWidth="1px" OnRowDataBound="GridView_MailItems_RowDataBound"
                                    OnRowDeleting="GridView_MailItems_RowDeleting" CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="20px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnItemID" runat="server" Value='<%#Eval("ID")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="40px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVessel" runat="server" Text='<%#Eval("vessel_short_name")%>' class='vesselinfo'
                                                    vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitem_Desc" runat="server" Text='<%#Eval("item_Desc")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Ref" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemRef" runat="server" Text='<%#Eval("Item_Ref")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Item_Qty")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Placed By" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBy" runat="server" Text='<%#Eval("Created_By_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dept" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDept" runat="server" Text='<%#Eval("Dept")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Placed On" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDtPlaced" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("date_placed"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgRemarks" runat="server" ImageUrl="~/Images/remark.gif" AlternateText='<%# Eval("Item_Remarks")%>'
                                                    CssClass="imgMsg"></asp:Image>
                                                <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="False" CommandName="Select" />
                                                <asp:ImageButton ID="ImgDelete" ToolTip="Delete" runat="server" AlternateText="Delete"
                                                    CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png"
                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle CssClass="grid-view-row" BackColor="White" />
                                    <AlternatingRowStyle CssClass="grid-view-row" BackColor="#EFF8FB" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPager_PendingItems" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Items"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="Load_PendingItems" />
                            </div>
                            <asp:Panel ID="pnlAddToPacket" runat="server">
                                <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                                    padding: 2px; font-size: 16px; margin-top: 5px; margin-bottom: 5px; color: #0B3861;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 50%">
                                            </td>
                                            <td style="width: 50%; text-align: right">
                                                <asp:Button ID="btnAddToPacket" runat="server" Text="Add to Packet >>" OnClick="btnAddToPacket_Click"
                                                    BorderStyle="Solid" BorderColor="white" BorderWidth="1px" Font-Names="Tahoma"
                                                    Height="24px" BackColor="#81DAF5" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 40%; vertical-align: top;">
                    <div style="border: 1px solid gray; min-height: 500px; padding: 2px;">
                        <asp:UpdatePanel ID="UpdatePanel_SelectedItems" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlPacket" runat="server" DefaultButton="btnSavePacket">
                                    <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                                        padding: 2px; font-size: 14px; margin-top: 2px; margin-bottom: 5px;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="border: 1px solid gray; background-color: White; width: 45px;">
                                                    <img src="../Images/box-open2.png" style="width: 40px" />
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Panel ID="pnlNewPacket" runat="server" Visible="false">
                                                        Packet Ref No:
                                                        <asp:DropDownList ID="ddlVesselPackets" runat="server" OnSelectedIndexChanged="ddlVesselPackets_SelectedIndexChanged"
                                                            DataTextField="PacketStatus" DataValueField="PacketID" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnPacketID" runat="server" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="dvSelectedItems" style="border: 1px solid #A9BCF5; min-height: 350px;">
                                        <asp:GridView ID="GridView_SelectedItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID" GridLines="Horizontal"
                                            Width="100%" BorderStyle="None" BorderWidth="1px" OnRowDeleting="GridView_SelectedItems_RowDeleting"
                                            OnRowDataBound="GridView_SelectedItems_RowDataBound" CssClass="gridmain-css">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitem_Desc" runat="server" Text='<%#Eval("item_Desc")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Ref" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemRef" runat="server" Text='<%#Eval("Item_Ref")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Item_Qty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Image ID="ImgRemarks" runat="server" ImageUrl="~/Images/remark.gif" AlternateText='<%# Eval("Item_Remarks")%>'
                                                            CssClass="imgMsg"></asp:Image>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDelete" runat="server" AlternateText="Delete" CausesValidation="False"
                                                            CommandName="Delete" ImageUrl="~/images/reject.png" ToolTip="Remove from packet"
                                                            OnClientClick="return confirm('Are you sure, you want to  remove item from packet ?')" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                            <RowStyle CssClass="grid-view-row" BackColor="White" />
                                            <AlternatingRowStyle CssClass="grid-view-row" BackColor="#EFF8FB" />
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                        </asp:GridView>
                                    </div>
                                    <div style="border: 1px solid #A9BCF5; background-color: #EFF2FB; margin-top: 2px;">
                                        <asp:Panel ID="pnlSavePacket" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Send Date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDateSent" runat="server"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtDateSent">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="lnkPortCall" runat="server" OnClick="lnkPortCall_Click">Select
                                                        Delivery Port</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Sent Using
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSentUsing" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Port
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="hdnPortID" runat="server" />
                                                        <asp:TextBox ID="txtPort" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Airway Bill#
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAirwayBill" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        Arrival
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtArrival" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Remarks
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txtApproverRemarks" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Button ID="btnSavePacket" runat="server" Text="Save" BorderStyle="Solid" BorderColor="white"
                                                            BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px" BackColor="#81DAF5"
                                                            OnClick="btnSavePacket_Click" />
                                                        <asp:Button ID="btnSaveAndSend" runat="server" Text="Save & Send to Vessel" BorderStyle="Solid"
                                                            BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                                            BackColor="#81DAF5" OnClick="btnSaveAndSend_Click" />
                                                        <asp:Button ID="btnDiscardPacket" runat="server" Text="Discard Packet" BorderStyle="Solid"
                                                            BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                                            BackColor="#81DAF5" OnClick="btnDiscardPacket_Click" OnClientClick="return confirm('Are you sure, you want to  discard the packet?')" />
                                                        <%--<asp:Button ID="btnCoverLetter" runat="server" Text="Cover Letter" BorderStyle="Solid"
                                                BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                                BackColor="#81DAF5" OnClick="btnCoverLetter_Click" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="dvPacketItems">
                        <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                            padding: 2px; font-size: 16px; margin-top: 10px; margin-bottom: 5px; color: #0B3861;">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%">
                                        Packets - Not yet sent to vessel
                                    </td>
                                    <td style="width: 50%; text-align: right">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:UpdatePanel ID="update_packetitems" runat="server">
                            <ContentTemplate>
                                <%--<asp:Repeater runat="server" ID="rpt1" OnItemDataBound="rpt1_ItemDataBound" OnItemCommand="rpt1_ItemCommand">
                                    <HeaderTemplate>
                                        <table style="border: 1px solid gray; width: 100%;" cellpadding="2" cellspacing="1">
                                            <tr style="background-color: #F6D8CE; color: Black; font-weight: bold; border-bottom: 2px solid black">
                                                <td style="text-align: center; width: 10px;">
                                                </td>
                                                <td style="width: 150px">
                                                    Packet Ref
                                                </td>
                                                <td style="width: 100px">
                                                    AirwayBill
                                                </td>
                                                <td style="width: 100px">
                                                    Port of Delivery
                                                </td>
                                                <td style="width: 70px">
                                                    ETA
                                                </td>
                                                <td style="width: 100px">
                                                    PIC
                                                </td>
                                                <td style="width: 100px">
                                                    Date Sent from Office
                                                </td>
                                                <td style="width: 100px">
                                                    Sent Using
                                                </td>
                                                <td style="width: 50px">
                                                    Packet Remarks
                                                </td>
                                                <td style="width: 50px">
                                                    Received ONBD
                                                </td>
                                                <td style="width: 100px">
                                                    Vessel Remarks
                                                </td>
                                                <td style="width: 150px">
                                                    Packet Status
                                                </td>
                                                <td style="width: 50px">
                                                Cover Letter
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="background-color: #EFF2FB;">
                                            <td style="text-align: center; width: 10px;">
                                                <img src="../Images/Plus.png" alt="" class="dbx-toggle" child='<%# Eval("PacketID")%>'
                                                    onclick="toggleChild()" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnNotifyMO" runat="server" Text='<%# Eval("PacketRef")%>' CommandName="EditPacket"
                                                    CssClass="linkbutton" CommandArgument='<%# Eval("PacketID")%>'></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("AirwayBill")%>
                                            </td>
                                            <td>
                                                <%# Eval("PORT_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ETA","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("Verified_By")%>
                                            </td>
                                            <td>
                                                <%# Eval("Expected_Date_Dispatch", "{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SentUsing")%>
                                            </td>
                                            <td>
                                                <asp:Image ID="ImgVerifiedRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                    AlternateText='<%# Eval("Verified_Remarks")%>' CssClass="imgMsg">
                                                </asp:Image>
                                            </td>
                                            <td>
                                                <%# Eval("DateReceived_ONBD", "{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <asp:Image ID="ImgVesselPICRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                    AlternateText='<%# Eval("VesselPICRemarks")%>' CssClass="imgMsg">
                                                </asp:Image>
                                            </td>
                                            <td>
                                                <%# Eval("PacketStatus")%>
                                            </td>
                                            <td style="text-align:center">
                                                <asp:ImageButton ID="ImgCoverLetter" runat="server" AlternateText='Message Vessel'
                                                    CommandName="CoverLetter" ImageUrl="~/Images/printer.png" CommandArgument='<%# Eval("PacketID")%>'>
                                                </asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr style="display: none;" class='<%# Eval("PacketID")%>'>
                                            <td>
                                            </td>
                                            <td colspan="12">
                                                <asp:Repeater runat="server" ID="rpt2" DataSource='<%# ((System.Data.DataRowView) Container.DataItem).Row.GetChildRows("PacketItems") %>'>
                                                    <HeaderTemplate>
                                                        <table style="border: 1px solid #E0E0F8; width: 100%">
                                                            <tr style="background-color: #B1B1E6; color: Black; border-bottom: 1px solid gray;
                                                                display: block;" class='toggle-content'>
                                                                <td>
                                                                    Item ID
                                                                </td>
                                                                <td style="width: 250px">
                                                                    Item Description
                                                                </td>
                                                                <td style="width: 100px">
                                                                    Item Ref
                                                                </td>
                                                                <td style="width: 50px">
                                                                    Quantity
                                                                </td>
                                                                <td style="width: 100px">
                                                                    Placed By
                                                                </td>
                                                                <td style="width: 100px">
                                                                    Dept
                                                                </td>
                                                                <td style="width: 100px">
                                                                    Date Placed
                                                                </td>
                                                                <td>
                                                                    Item Remarks
                                                                </td>
                                                                <td style="width: 100px">
                                                                    Received ONBD
                                                                </td>
                                                                <td>
                                                                    Vessel Remarks
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr style="background-color: #E0F8F7;">
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["ID"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Item_Desc"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Item_Ref"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Item_Qty"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Placed_By"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Dept"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Date_Placed"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["Item_Remarks"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["ItemReceivedStatus"]%>
                                                            </td>
                                                            <td>
                                                                <%# ((System.Data.DataRow)Container.DataItem)["VesselRemarks"]%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>--%>
                                <asp:GridView ID="GridView_Packets" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="PacketID" EmptyDataText="No Item Found"
                                    GridLines="Horizontal" Width="100%" BorderStyle="None" BackColor="White" BorderWidth="1px"
                                    OnRowDataBound="GridView_Packets_RowDataBound" CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Packet Ref">
                                            <ItemTemplate>
                                                <img src="../Images/Plus.png" alt="" class="dbx-toggle" onclick="showChildItems(event, <%# Eval("PacketID")%>)" />
                                                <%# Eval("PacketRef")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Airway Bill" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("AirwayBill")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Port" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("PORT_NAME")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ETA" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("ETA","{0:dd/MM/yyyy}")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PIC" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Eval("Verified_By")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Sent from Office" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Expected_Date_Dispatch")))%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sent Using" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Eval("SentUsing")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packet Remarks" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgVerifiedRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                    AlternateText='<%# Eval("Verified_Remarks")%>' CssClass="imgMsg"></asp:Image>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received ONBD">
                                            <ItemTemplate>
                                                <%# Eval("DateReceived_ONBD", "{0:dd/MM/yyyy}")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vessel Remarks">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgVesselPICRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                    AlternateText='<%# Eval("VesselPICRemarks")%>' CssClass="imgMsg"></asp:Image>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packet Status">
                                            <ItemTemplate>
                                                <%# Eval("PacketStatus")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="White" />
                                    <AlternatingRowStyle BackColor="#EFF8FB" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPager_PacketItems" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Items"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="Load_PacketItemReport" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvVesselMovement" class="draggable">
        <asp:UpdatePanel ID="UpdatePanel_PortCalls" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; border: 1px solid #aabbee; background-color: #A9D0F5;">
                    <tr>
                        <td style="text-align: left; color: Black; font-weight: bold; font-size: 12px;">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td>
                                        Select Port Call
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <img src="../Images/close.png" onclick='overlay()' alt="" height="16px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: top;">
                            <div id="dvContainer" style="background-color: White;">
                                <table style="width: 100%; border: 1px solid gray; margin-top: 5px;">
                                    <tr>
                                        <td class="grid-container">
                                            <asp:GridView ID="gvPortCalls" runat="server" AutoGenerateColumns="False" BorderStyle="Double"
                                                BorderWidth="1px" CellPadding="2" AllowPaging="true" PageSize="15" GridLines="Horizontal"
                                                DataKeyNames="Port_Call_ID" Font-Size="11px" Width="100%" CssClass="gridmain-css">
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                <RowStyle CssClass="RowStyle-css" />
                                                <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Vessel">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVessel_Short_Name" runat="server" Text='<%# Eval("Vessel_Short_Name")%>'
                                                                class='vesselinfo' vid='<%# Eval("Vessel_ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPort_Name" runat="server" Text='<%# Eval("Port_Name")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdnPortID" runat="server" Value='<%# Eval("Port_ID")%>'></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Arrival">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArrival" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Arrival")))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Departure" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeparture" runat="server" Text='<%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Departure")))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Owners Agent" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOwners_Agent" runat="server" Text='<%# Eval("Owners_Agent")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Charterers Agent" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCharterers_Agent" runat="server" Text='<%# Eval("Charterers_Agent")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkSelect" runat="server" ImageUrl="~/images/select1.gif" CausesValidation="False"
                                                                CommandName="Select" AlternateText="Select"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblNoRec" runat="server" Text="No record found."></asp:Label>
                                                </EmptyDataTemplate>
                                                <FooterStyle BackColor="White" ForeColor="#333333" />
                                                <PagerStyle BackColor="#ffffff" ForeColor="Black" HorizontalAlign="Center" Font-Size="Large"
                                                    CssClass="pager" />
                                                <RowStyle BackColor="White" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#F7BE81" Font-Bold="True" ForeColor="Black" />
                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#487575" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#275353" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="border: 1px solid #aabbee; background-color: White; margin-top: 2px;
                                padding: 5px; text-align: right;">
                                <asp:Button ID="btnSavePortCall" runat="server" Text="Save & Close" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="150px"
                                    BackColor="#81DAF5" OnClientClick="overlay();" OnClick="btnSavePortCall_Click" />
                                <asp:Button ID="btnClosePortCall" runat="server" Text="Cancel" BorderStyle="Solid"
                                    BorderColor="white" BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px"
                                    BackColor="#81DAF5" OnClientClick="overlay();return false;" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvCoverLetter" style="display: none; background-color: #CBE1EF; border-color: #5C87B2;
        border-style: solid; border-width: 1px; position: absolute; top: 105px; color: black">
        <div class="header">
            <div style="right: 0px; position: absolute; cursor: pointer;">
                <img src="../Images/Close.gif" onclick="closeDiv('dvCoverLetter');" alt="Close" />
            </div>
            <h4>
                Cover Letter</h4>
        </div>
        <div class="content">
            <asp:UpdatePanel ID="update1" runat="server">
                <ContentTemplate>
                    <CKEditor:CKEditorControl ID="txtCoverLetter" runat="server"></CKEditor:CKEditorControl>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSavePacket.ClientID %>", function () {
                if ($.trim($("#<%=txtDateSent.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtDateSent.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>')) {
                        alert("Enter valid Send Date<%=UDFLib.DateFormatMessage() %>");
                        $("#<%=txtDateSent.ClientID %>").focus();
                        return false;
                    }
                }
            });

            $("body").on("click", "#<%=btnSaveAndSend.ClientID %>", function () {
                if ($.trim($("#<%=txtDateSent.ClientID %>").val()) == "") {
                    alert("Enter Send Date");
                    $("#<%=txtDateSent.ClientID %>").focus();
                    return false;
                }
                if ($.trim($("#<%=txtDateSent.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtDateSent.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>')) {
                        alert("Enter valid Send Date<%=UDFLib.DateFormatMessage() %>");
                        $("#<%=txtDateSent.ClientID %>").focus();
                        return false;
                    }
                }

                if ($.trim($("#<%=txtDateSent.ClientID %>").val()) != "") {
                    if (new DateAsFormat($.trim($("#<%=txtDateSent.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>').getDateOnly() > new Date().getDateOnly()) {
                        alert("Send Date can not be a future date");
                        $("#<%=txtDateSent.ClientID %>").focus();
                        return false;
                    }
                }
            });
        });
    </script>
</asp:Content>
