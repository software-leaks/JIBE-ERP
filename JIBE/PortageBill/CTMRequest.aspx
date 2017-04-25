<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CTMRequest.aspx.cs"
    Inherits="PortageBill_CTMRequest" Title="CTM Request" %>

<%@ Register Src="~/UserControl/ctlPortList.ascx" TagName="PortList" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../UserControl/ucPortCalls.ascx" TagName="ucPortCalls" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js?v=2" type="text/javascript"></script>
    <%-- <script src="../Scripts/CrewSailingInfo.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            var id = '<% =Request.QueryString["ID"]%>';
            var vesselid = '<% =Request.QueryString["Vessel_ID"]%>';
            var wh;
            if (id == '')
                id = 0;
            if (vesselid == '')
                vesselid = 0;

            var wh = 'ID='+id +'and Vessel_id='+ vesselid;
            Get_Record_Information_Details('ACC_LIB_CTM', wh);
        });

        function CalculateTotal() {

            var griditems = document.getElementById('<%=gvDenominations.ClientID%>');
            var griditems_ctlID = '<%=gvDenominations.ClientID%>'

            var Tot_Amount = 0;
            var k = 0;
            for (var j = 2; j < griditems.rows.length; j++) {

                var denomination_id = 0, Amount_id = 0, NoOfNotes_by_Office_id = 0;
                denomination_id = (j < 10) ? griditems_ctlID + "_ctl0" + j.toString() + "_txtDenomination" : griditems_ctlID + "_ctl" + j.toString() + "_txtDenomination";
                Amount_id = (j < 10) ? griditems_ctlID + "_ctl0" + j.toString() + "_lblOfficeTotalAmt" : griditems_ctlID + "_ctl" + j.toString() + "_lblOfficeTotalAmt";
                NoOfNotes_by_Office_id = (j < 10) ? griditems_ctlID + "_ctl0" + j.toString() + "_txtNoOfNotes_by_Office" : griditems_ctlID + "_ctl" + j.toString() + "_txtNoOfNotes_by_Office";


                var rowamt = parseFloat(document.getElementById(denomination_id).value) * parseFloat(document.getElementById(NoOfNotes_by_Office_id).value);
                document.getElementById(Amount_id).innerHTML = FormatAmount(rowamt.toFixed(2));
              
                if (rowamt > 0)
                    Tot_Amount = Tot_Amount + rowamt;

                k = j;
            }
            if (k != 0) {
                document.getElementById(griditems_ctlID + "_ctl0" + (k + 1).toString() + "_lblTotal_AmtOffice").innerHTML = FormatAmount(Tot_Amount.toFixed(2));
            }
        }
    </script>

    <script>
        var __app_name = location.pathname.split('/')[1];

        var isModalOpen = 0;
        var ModalPopUpID = "";

        (function ($) {
            $.fn.center = function () { this.css("position", "absolute"); this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px"); this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"); return this; }
        })(jQuery);

        function showModalPortage(dvPopUpID, isDraggable, callbackFunction, callback_Help_Function) {
            debugger;
            try {
                if (dvPopUpID) {
                    var dvPopUp = document.getElementById(dvPopUpID);

                    $(dvPopUp).show();
                    $(dvPopUp).css({ 'border': '1px solid #aabbee', 'background-color': 'white' });

                    if (!document.getElementById('overlay')) {
                        var dvOverlay = document.createElement('div');
                        dvOverlay.id = 'overlay';
                        document.body.appendChild(dvOverlay);
                    }

                    //Remove the already added header (if exists)
                    //-------------------------------------------
                    if (document.getElementById(dvPopUpID + '_dvModalPopupHeader'))
                        dvPopUp.removeChild(document.getElementById(dvPopUpID + '_dvModalPopupHeader'));

                    //create new header
                    //-------------------------------------------            
                    var dvModalPopupHeader = document.createElement('div');
                    dvModalPopupHeader.id = dvPopUpID + '_dvModalPopupHeader';

                    isModalOpen = 1;
                    ModalPopUpID = dvPopUpID;
                 
                    var title = 'Select Port Call';

                    dvModalPopupHeader.innerHTML = "<span id='" + dvPopUpID + '_dvModalPopupTitle' + "' >" + title + "</span>";
                    $(dvModalPopupHeader).css({ 'width': '100%', 'height': 22, backgroundColor: "transparent", color: 'Black', 'background-color': '#A9D0F5', 'font-weight': 'Bold', 'cursor': 'move' });

                    $(dvPopUp).attr('title', '');

                    var dvModalPopupControlBox = document.createElement('div');
                    $(dvModalPopupControlBox).css({ 'height': 20, backgroundColor: "transparent", 'right': 2, 'top': 2, 'position': 'absolute' });

                    var dvModalPopupCloseButton = document.createElement('div');
                    dvModalPopupCloseButton.id = dvPopUpID + '_dvModalPopupCloseButton';
                    dvModalPopupCloseButton.innerHTML = '<img id="closePopupbutton" src="/' + __app_name + '/images/close.png" style="cursor:pointer;" alt="Press ESC to Close" id="imgbtnPopupClose">';
                    $(dvModalPopupCloseButton).css({ 'height': 20, backgroundColor: "transparent", 'top': 0, 'float': 'right' });
                    $(dvModalPopupCloseButton).click(function () { $('#overlay').hide(); $(dvPopUp).hide(); try { setTimeout(callbackFunction, 1); } catch (ex) { } });
                    dvModalPopupControlBox.appendChild(dvModalPopupCloseButton);

                    if (callback_Help_Function) {
                        var dvModalPopupHelpButton = document.createElement('div');
                        dvModalPopupHelpButton.id = dvPopUpID + '_dvModalPopupHelpButton';
                        dvModalPopupHelpButton.innerHTML = '<img  src="/' + __app_name + '/images/help16.png" style="cursor:pointer;">';
                        $(dvModalPopupHelpButton).css({ 'height': 20, backgroundColor: "transparent", 'padding-right': 5, 'top': 0, 'float': 'right' });
                        $(dvModalPopupHelpButton).click(function () { try { setTimeout(callback_Help_Function, 1); } catch (ex) { } });
                        dvModalPopupControlBox.appendChild(dvModalPopupHelpButton);
                    }

                    dvModalPopupHeader.appendChild(dvModalPopupControlBox);
                    dvPopUp.insertBefore(dvModalPopupHeader, dvPopUp.firstChild);

                    //$(dvModalPopupHeader).bind('mousedown', function () { $(dvPopUp.firstChild).next().hide(); });
                    //$(dvModalPopupHeader).bind('mouseup', function () { $(dvPopUp.firstChild).next().show(); });

                    var maskHeight = $(document).height();
                    var maskWidth = $(document).width();
                    var h = dvPopUp.clientHeight;
                    var w = dvPopUp.clientWidth;

                    var t = ($(window).height() / 2 - h / 2) ;
                    var l = $(window).width() / 2 - w / 2;

                    $('#overlay').css({ 'width': maskWidth, 'height': maskHeight, backgroundColor: "black", 'position': "absolute", 'top': 0, 'left': 0, 'z-index': 999 });

                    $(dvPopUp).css({ 'top': t, 'left': l, 'z-index': 1000, 'position': "absolute", 'border': '4px solid #A9D0F5', 'background-color': 'white', 'padding': 0 });


                    isDraggable = typeof (isDraggable) == 'undefined' ? true : isDraggable;

                    $('#overlay').fadeTo("fast", 0.5);
                    //$(dvPopUp).center();
                  moveToCenter(dvPopUp);

                    if (isDraggable)
                        $(dvPopUp).draggable();
                }
            }
            catch (e) { }

        }
        function hideModal(dvPopUpID) {
            if (dvPopUpID) {
                $('#overlay').hide();
                $('#' + dvPopUpID).hide();

            }
            return true;
        }

        function moveToCenter(obj) {
            if (obj) {
                var topY = ($(window).height() - $(obj).height()) / 2 + $(window).scrollTop();
                if (topY < 0)
                    topY = 5;
                $(obj).css("position", "absolute");
                  $(obj).css("left", ($(window).width() - $(obj).width()) / 2 + $(window).scrollLeft() + "px");
            }
            return true;
        }


        //jQuery.fn.center = function () { this.css("position", "absolute"); this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px"); this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"); return this; }


        $(document).keydown(function (e) {
            // ESCAPE key pressed 
            try {
                if (e.keyCode == 27) {
                    if (isModalOpen == 1) {
                        hideModal(ModalPopUpID);
                    }
                }
            }
            catch (ex) { }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
                    <div id="page-title" style="margin: 2px; border: 1px solid #cccccc; height: 20px;
                        vertical-align: bottom; background: url(../Images/bg.png) left -10px repeat-x;
                        color: Black; text-align: left; padding: 2px; background-color: #F6CEE3;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 33%;">
                                </td>
                                <td style="width: 33%; text-align: center; font-weight: bold;">
                                    <asp:Label ID="lblPageTitle" runat="server" Text="CASH TO MASTER REQUEST"></asp:Label>
                                </td>
                                <td style="width: 33%; text-align: right;">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px; color: #333">
                        <table style="width: 80%" border="0">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td align="left" rowspan="4" style="vertical-align: bottom" colspan="2">
                                    Remark :<br />
                                    <asp:TextBox ID="txtCTMRemark" TextMode="MultiLine" Height="70px" Width="500px" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px; font-size: 14px;">
                                    Vessel :
                                </td>
                                <td align="left" colspan="2" style="font-size: 14px; font-weight: bold">
                                    <asp:DropDownList ID="DDLVessel" runat="server" AutoPostBack="true" Width="120px"
                                        OnSelectedIndexChanged="DDLVessel_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Created By :
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCTMCreatedBy" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Created On :
                                </td>
                                <td align="left">
                                    <asp:Label ID="txtRequestedOn" Width="118px" Enabled="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%" border="0">
                            <tr>
                                <td colspan="3" style="text-align: left">
                                    <b>CTM Calculation </b>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    BOW Calculated
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:TextBox ID="txtBOWCalculated" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    Cash On-board
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:TextBox ID="txtCashOnBoard" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    CTM Request/Calculated
                                </td>
                                <td align="left" style="width: 150px;">
                                    <asp:TextBox ID="txtCTMRequested" ForeColor="Red" BackColor="Yellow" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%" border="0">
                            <tr>
                                <td align="left">
                                    Order Code
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblOrderCode" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    Order Date
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                                </td>
                                <td style="width: 150px">
                                </td>
                                <td align="left">
                                    Delivery Code
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDeliveryCode" runat="server"></asp:Label>
                                </td>
                                <td align="left">
                                    Delivery Date
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDeliveryDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="8">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%" border="0">
                            <tr>
                                <td align="left" colspan="2">
                                    <b>Detailed BOW of off-signers </b>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:GridView ID="gvCTM_OffSigners" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" AllowPaging="false" PageSize="25" Width="100%" EmptyDataText="No Record Found"
                                        CaptionAlign="Bottom" GridLines="None" ShowFooter="true" OnRowDataBound="gvCTM_OffSigners_RowDataBound"
                                        CssClass="GridView-css">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Staff Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_Code" runat="server" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo"></asp:Label>
                                                    <asp:HiddenField ID="hdfCrewid" runat="server" Value='<%#Eval("CrewID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStaff_FullName" runat="server" Text='<%# Eval("OffSignerName")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdfRankID" runat="server" Value='<%#Eval("Rankid")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rank">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sign-Off Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateOfSignOff" runat="server"  Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DateOfSignOff"))) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOW (USD)" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOWAmt" runat="server" Text='<%# Eval("BOWAmt")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Total&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblOffSignerBowTotal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActiveStatus" runat="server" ForeColor="Red" Font-Bold="true" Text='<%#Eval("Active_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="FooterStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <EditRowStyle CssClass="RowStyle-css" BackColor="#7C6F57" />
                                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Denomination Required</b>
                                </td>
                                <td align="center" style="width: 128px">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:GridView ID="gvDenominations" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="true" CellPadding="4" AllowPaging="True" PageSize="20" Width="100%"
                                        EmptyDataText="No Record Found" CaptionAlign="Bottom" GridLines="None" OnRowDataBound="gvDenominations_RowDataBound"
                                        ShowFooter="true" CssClass="GridView-css">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Denomination">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDenomination" runat="server" Enabled="false" Text='<%# Eval("Denomination")%>'
                                                        Style="text-align: right" onchange="CalculateTotal();return false;"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="btnAddNewItem" Text="Add new item" BackColor="#0066cc" BorderStyle="None"
                                                        Visible="false" ForeColor="WhiteSmoke" runat="server" OnClick="btnAddNewItem_Click" />
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="No of Notes by Capt">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNoOfNotes_by_Capt" runat="server" Text='<%# Eval("NoOfNotes_by_Capt")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_NoOfNotes_by_Capt" runat="server" Text=''></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requested Total (USD)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaptTotalAmt" runat="server" Text='<%#  GetTotal(Eval("Denomination").ToString(),Eval("NoOfNotes_by_Capt").ToString())  %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_AmtCapt" runat="server" Text=''></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Notes by Office" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNoOfNotes_by_Office" runat="server" onchange="CalculateTotal()"
                                                        Text='<%# Bind("NoOfNotes_by_Office")%>' Width="80px" Enabled='<%# IsApproved==true?false:true %>'
                                                        BackColor="#FFFFE6" Style="text-align: right" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeTotalAmt" runat="server" Text='<%#  GetTotal(Eval("Denomination").ToString(),Eval("NoOfNotes_by_Office").ToString())  %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_AmtOffice" ForeColor="Red" Text='' runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="FooterStyle-css" />
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="6">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlDenominationApprove" runat="server">
                            <table cellpadding="2" width="80%" style="border-collapse: collapse; font-size: 11px">
                                <tr>
                                    <td align="left">
                                        <b>Port Details</b>
                                    </td>
                                    <td colspan="5" align="left">
                                        <a href="#" style="font-size: 11px" onclick="showModalPortage('dvShowPortCalls',false)">Select
                                            Port from port calls</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="border: 1px solid #cccccc; background-color: #efefef; width: 190px;
                                        text-align: right; padding-right: 3px; font-weight: bold">
                                        Date
                                    </td>
                                    <td align="center" style="border: 1px solid #cccccc; background-color: #efefef; width: 128px">
                                        <asp:Label ID="txtCTM_Supply_Date" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" style="border: 1px solid #cccccc; background-color: #efefef; width: 80px;
                                        font-weight: bold">
                                        Port
                                    </td>
                                    <td align="left" style="border: 1px solid #cccccc; background-color: #efefef; width: 180px;">
                                        <asp:Label ID="lblportname" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdfPort_ID" runat="server" />
                                    </td>
                                    <td align="left" style="border: 1px solid #cccccc; background-color: #efefef;">
                                        <asp:Label ID="lblPortcallMessage" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label>
                                        <asp:HiddenField ID="hdfPortCall_ID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="6">
                                        <b>Supplier Details</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="border: 1px solid #cccccc; background-color: #efefef; text-align: right;
                                        padding-right: 3px; font-weight: bold">
                                        Supplier
                                    </td>
                                    <td align="left" style="border: 1px solid #cccccc; background-color: #efefef;" colspan="4">
                                        <uc2:uc_SupplierList ID="uc_SupplierListCTM" Width="250px" Supplier_Category="CTM"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="border: 1px solid #cccccc; background-color: #efefef; text-align: right;
                                        padding-right: 3px; font-weight: bold">
                                        Supplier Commission (in USD)
                                    </td>
                                    <td align="left" style="border: 1px solid #cccccc; background-color: #efefef;" colspan="4">
                                        <asp:TextBox ID="txtSupplierCommission" onchange="checkNumber('ctl00_MainContent_txtSupplierCommission')"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtSupplierCommission" runat="server" ControlToValidate="txtSupplierCommission"
                                            ValidationGroup="SupplierCommission" Display="None" ErrorMessage="Please enter Supplier Commission !"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vextxtSupplierCommission" TargetControlID="rfvtxtSupplierCommission"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table cellpadding="2" width="80%" style="border-collapse: collapse">
                            <tr>
                                <td colspan="2" align="left">
                                    <b>Approval</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: #efefef; border: 1px solid #cccccc; vertical-align: bottom">
                                    <asp:Panel ID="pnlApprove" HorizontalAlign="Left" runat="server" Width="100%">
                                        <asp:TextBox ID="txtApprovalRemark" runat="server" TextMode="MultiLine" Width="500px"
                                            BackColor="#FFFFE6" />
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnApprv" runat="server" OnClick="btnApprv_Click" Height="35px" Text="Save & Approve CTM"
                                             CommandArgument="1" /><%--ValidationGroup="SupplierCommission"--%>
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnApprv_Click" Height="35px"
                                            Text="Cancel CTM Request" CommandArgument="-1" />
                                    </asp:Panel>
                                </td>
                                <td style="background-color: #efefef; border: 1px solid #cccccc; vertical-align: bottom">
                                    <asp:Button ID="btnRework" runat="server" Height="35px" Text="Rollback " OnClick="btnRework_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%">
                            <tr>
                                <td align="left">
                                    <b>Last CTM Received</b>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="border: 1px solid #cccccc; background-color: #efefef;">
                                    <asp:GridView ID="gvLastCTM" DataKeyNames="id" runat="server" AutoGenerateColumns="False"
                                        CellPadding="4" AllowPaging="True" PageSize="20" Width="100%" EmptyDataText="No Record Found"
                                        CaptionAlign="Bottom" GridLines="None" ForeColor="#333333" OnRowDataBound="gvLastCTM_OnRowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("ApprovedAmt")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Port" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPort" runat="server" Text='<%#  Eval("Port_Name")  %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                        <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle"
                                            BackColor="White" />
                                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#E3EAEB" />
                                        <EditRowStyle CssClass="RowStyle-css" BackColor="#F5F6CE" />
                                        <PagerStyle CssClass="PagerStyle-css" BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 80%">
                            <tr>
                                <td align="left">
                                    <b>Approval By Manager</b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="max-height: 200px; overflow: auto; width: 100%">
                                        <asp:GridView ID="gvApprovals" runat="server" CellPadding="4" CellSpacing="0" GridLines="None"
                                            CssClass="GridView-css" AutoGenerateColumns="true">
                                            <HeaderStyle CssClass="HeaderStyle-css" />
                                            <RowStyle CssClass="RowStyle-css" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                        <div style="width: 80%;text-align:left">
                            <div id="dvRecordInformation" style="float: left; width: 100%">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="dvShowPortCalls" style="display: none; padding: 5px" title="Select Port Call">
                    <uc1:ucPortCalls ID="ucPortCallsCtm" OnSelect="ucPortCallsCtm_Selected" runat="server" />
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
