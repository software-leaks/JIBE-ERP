<%@ Page Title="CrewMail" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewMailIndex.aspx.cs" Inherits="CrewMail_CrewMailIndex" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
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
        .YES
        {
        }
        .NO
        {
            background-color: Red;
            color: Yellow;
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

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_CrewMail_PacketItems', false, { "PacketID": packetId, "DateFormat": '<%= UDFLib.GetDateFormat() %>' }, getChildItems_onSuccess, getChildItems_onFail);
            lastExecutor_WebServiceProxy = service.get_executor();
        }

        function getChildItems_onSuccess(retval) {
            js_ShowToolTip_Fixed(retval, evt, null, "Mail Items");
        }

        function getChildItems_onFail(err_) {
            // alert(err_._message);
        }

        function showDiv(dv) {
            if (dv) {
                $('#' + dv).show();
            }
            //$('#dvItemList').animate({opacity: 0.2}, 1000);
        }
        function closeDiv(dv) {
            if (dv) {
                $('#' + dv).hide();
            }
            //$('#dvItemList').animate({ opacity: 1 }, 1000);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        CrewMail
    </div>
    <div id="page-content" style="border: 1px solid #CEE3F6; z-index: -2; margin-top: -1px;
        min-height: 600px; padding: 5px; overflow: auto;">
        <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <table style="width: 100%;">
            <tr>
                <td style="vertical-align: top;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlSearch" runat="server">
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
                                            Packet Status
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPacketStatus" runat="server" Width="156px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlPacketStatus_SelectedIndexChanged">
                                                <asp:ListItem Text="- SELECT ALL -" Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Not sent to Vessel" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Sent to Vessel" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Received by Vessel" Value="2"></asp:ListItem>
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
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                        padding: 2px; font-size: 16px; margin-top: 5px; margin-bottom: 5px; color: #0B3861;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; font-weight: bold;">
                                    MailBox - Pending Items
                                </td>
                                <td style="width: 50%; text-align: right">
                                    <asp:Button ID="btnNewItem" runat="server" ToolTip="Add New Item" Text="Add New Item"
                                        BorderStyle="Solid" OnClientClick="showDiv('dvAddItem'); return false;" BorderColor="white"
                                        BorderWidth="1px" Font-Names="Tahoma" Height="24px" BackColor="#81DAF5" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvAddItem" style="display: none; border: 1px solid #A9BCF5; background-color: #EFF2FB;
                        color: Black; padding: 10px;">
                        <asp:Panel ID="pnlAddItem" runat="server" DefaultButton="btnSaveAndAdd">
                            <asp:UpdatePanel ID="UpdatePanel_AddItem" runat="server">
                                <ContentTemplate>
                                    <div style="padding: 5px; font-size: 14px; font-weight: bold;">
                                        New Item Entry</div>
                                    <table>
                                        <tr>
                                            <td style="width: 100px">
                                                Date Placed
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
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
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Ref No
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRefNo" Width="250px" runat="server" MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td>
                                                Quantity
                                            </td>
                                            <td style="color: #FF0000; width: 1%" align="right">
                                                *
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQty" Width="50px" runat="server"></asp:TextBox>
                                                <%--<ajaxToolkit:FilteredTextBoxExtender runat="server" ID="flttxtAty" TargetControlID="txtQty" FilterType="Numbers">
                                                </ajaxToolkit:FilteredTextBoxExtender>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="static"
                                                    ControlToValidate="txtQty" ErrorMessage="Enter item quantity!!" ValidationGroup="ValidEntry"></asp:RequiredFieldValidator>
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
                                            <td style="color: #FF0000; width: 1%" align="right">
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemarks" Width="250px" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="text-align: center">
                                                <asp:Button ID="btnSaveAndAdd" runat="server" Text="Save & Add New" ValidationGroup="ValidEntry"
                                                    BorderStyle="Solid" BorderColor="white" BorderWidth="1px" Font-Names="Tahoma"
                                                    Height="24px" Width="150px" BackColor="#81DAF5" OnClick="btnSaveAndAdd_Click" />
                                                <asp:Button ID="btnSave" runat="server" Text="Save & Close" ValidationGroup="ValidEntry"
                                                    BorderStyle="Solid" BorderColor="white" BorderWidth="1px" Font-Names="Tahoma"
                                                    Height="24px" Width="150px" BackColor="#81DAF5" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" BorderStyle="Solid" BorderColor="white"
                                                    BorderWidth="1px" Font-Names="Tahoma" Height="24px" Width="100px" BackColor="#81DAF5"
                                                    OnClientClick="closeDiv('dvAddItem'); return false;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" align="right" style="color: #FF0000; font-size: small;">
                                                * Mandatory fields
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                    <div id="dvItemList">
                        <asp:UpdatePanel ID="UpdatePanel_Items" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView_MailItems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CaptionAlign="Bottom" CellPadding="4" DataKeyNames="ID" EmptyDataText="No Item Found"
                                    ForeColor="Black" GridLines="Horizontal" Width="100%" BorderStyle="None" OnRowDeleting="GridView_MailItems_RowDeleting"
                                    CssClass="gridmain-css">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No">
                                            <ItemTemplate>
                                                <%# ((GridViewRow)Container).RowIndex + 1%>
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
                                        <asp:TemplateField HeaderText="Placed On" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDtPlaced" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("date_placed"))) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%#Eval("Item_Remarks")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" ToolTip="Delete" runat="server" AlternateText="Delete"
                                                    CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png"
                                                    OnClientClick="return confirm('Are you sure, you want to  delete ?')" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
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
                                <uc1:ucCustomPager ID="ucCustomPager_PendingItems" runat="server" RecordCountCaption="&nbsp;&nbsp;&nbsp;&nbsp;Total Items"
                                    AlwaysGetRecordsCount="true" OnBindDataItem="Load_PendingItems" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <hr />
                    <div style="border-top: 1px solid #A9BCF5; border-bottom: 1px solid #A9BCF5; background-color: #EFF2FB;
                        padding: 2px; font-size: 16px; margin-top: 20px; margin-bottom: 5px; color: #0B3861;">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 50%; font-weight: bold;">
                                    Packet list
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
                                            <td style="width: 100px">
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
                                            <td style="width: 100px">
                                                Packet Remarks
                                            </td>
                                            <td style="width: 100px">
                                                Received ONBD
                                            </td>
                                            <td style="width: 100px">
                                                Vessel Remarks
                                            </td>
                                            <td style="width: 100px">
                                                Packet Status
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
                                            <%# Eval("PacketRef")%>
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
                                        <td style="text-align:center;">
                                            <asp:Image ID="ImgVerifiedRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                AlternateText='<%# Eval("Verified_Remarks")%>' CssClass="imgMsg">
                                            </asp:Image>
                                        </td>
                                        <td>
                                            <%# Eval("DateReceived_ONBD", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td style="text-align:center;">
                                            <asp:Image ID="ImgVesselPICRemarks" runat="server" ImageUrl="~/Images/remark.gif"
                                                AlternateText='<%# Eval("VesselPICRemarks")%>' CssClass="imgMsg">
                                            </asp:Image>
                                        </td>
                                        <td style="width:150px">
                                            <%# Eval("PacketStatus")%>
                                        </td>
                                        
                                    </tr>
                                    <tr style="display: none;" class='<%# Eval("PacketID")%>'>
                                        <td>
                                        </td>
                                        <td colspan="11">
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
                                                        <td style="text-align:center" class='<%# ((System.Data.DataRow)Container.DataItem)["ItemReceivedStatus"]%>'>
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
                                GridLines="Horizontal" Width="100%" BorderStyle="None" BackColor="White" OnRowDataBound="GridView_Packets_RowDataBound"
                                CssClass="gridmain-css">
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
                                            <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("ETA")))%>
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
                                            <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Expected_Date_Dispatch"))) %>
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
                                            <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateReceived_ONBD"))) %>
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
