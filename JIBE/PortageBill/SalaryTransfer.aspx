<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SalaryTransfer.aspx.cs"
    EnableEventValidation="false" Title="Salary by Wire Transfer" Inherits="Account_Portage_Bill_SalaryTransfer" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.1.7.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.alerts.js" type="text/javascript"></script>        
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
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
        #page-content a:link
        {
            color: blue;
            background-color: transparent;
            text-decoration: none;
            border: 0px;
        }
        #page-content a:visited
        {
            color: black;
            text-decoration: none;
        }
        #page-content a:hover
        {
            color: blue;
            text-decoration: none;
        }
        
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../Images/bg.png) left -1672px repeat-x;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .taskpane
        {
            background-image: url(../images/taskpane.png);
            background-repeat: no-repeat;
            background-position: -2px -2px;
        }
        .tooltip
        {
            display: none;
            background: transparent url(../Images/black_arrow.png);
            font-size: 12px;
            height: 90px;
            width: 180px;
            padding: 15px;
            color: #fff;
        }
        .interview-schedule-table
        {
            padding: 0;
            border-collapse: collapse;
        }
        .interview-schedule-table div
        {
            border: 0px solid gray;
            height: 16px;
            width: 16px;
            margin-top: 2px;
            background: url(../Images/Interview_1.png) no-repeat;
        }
        
        .CrewStatus_Current
        {
            background-color: #aabbdd;
        }
        .CrewStatus_SigningOff
        {
            background-color: #F3F781;
        }
        .CrewStatus_SignedOff
        {
            background-color: #F5A9A9;
        }
        .CrewStatus_Assigned
        {
            background-color: #BBB6FF;
        }
        .CrewStatus_Planned
        {
            background-color: #F781F3;
        }
        .CrewStatus_Pending
        {
            background-color: #81BEF7;
        }
        .CrewStatus_Inactive
        {
            background-color: #848484;
            color: #E6E6E6;
        }
        .CrewStatus_NoVoyage
        {
            background-color: #A9F5D0;
        }
        .CrewStatus_NTBR
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_NTBR_Row
        {
            color: Red;
        }
        .CrewStatus_UNFIT
        {
            background-color: RED;
            color: Yellow;
        }
        .CrewStatus_UNFIT_Row
        {
            color: Red;
        }
        .imgCOC
        {
            vertical-align: middle;
        }
        .CrewStatus_Rejected
        {
            background-color: RED;
            color: Yellow;
        }
        input
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        select
        {
            height: 21px;
            font-family: Tahoma;
            font-size: 11px;
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
            background-color: #E0F8E0;
            border: 1px solid #F1C15F;
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
    <style type="text/css">
        .tdh
        {
            font-size: 11px;
            text-align: right;
            padding: 2px 3px 2px 0px;
            vertical-align: middle;
            font-weight: bold;
            width: 100px;
            border: 1px solid #DADADA;
        }
        .tdd
        {
            font-size: 11px;
            text-align: left;
            padding: 2px 2px 2px 3px;
            vertical-align: middle;
            border: 1px solid #DADADA;
        }
        
        .CreateHtmlTableFromDataTable-Data-Attachment
        {
            border: 0;
        }
        .CreateHtmlTableFromDataTable-Data-Attachment td
        {
            border: 0;
        }
    </style>
    <script type="text/javascript">
        var lastExecutor = null;




        function ASync_SideLetter(Voyage_ID, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncGet_Side_Letter', false, { "Voyage_ID": Voyage_ID }, onSuccessASync_SideLetter, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();

        }


        function onSuccessASync_SideLetter(retVal, eventArgs) {

            js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
        }




        function ASync_Get_Wages(Crewid, Month, Year, evt, objthis) {

            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../WebService.asmx', 'get_CrewWages', false, { "CrewID": Crewid, "Month": Month, "Year": Year }, onSuccessASync_Get_Wages, Onfail, new Array(evt, objthis));
            lastExecutor = service.get_executor();

        }

        function Onfail() { }

        function onSuccessASync_Get_Wages(retVal, eventArgs) {

            js_ShowToolTip(retVal, eventArgs[0], eventArgs[1]);
        }

        function checkNumber(id) {

            var obj = document.getElementById(id);
            if (isNaN(obj.value)) {
                obj.value = 0;
                alert("Only number allowed !");
            }

        }

        function RefreshOnClose() {

            var functionname_id = "<%=btnSearch.ClientID%>";

            var postBackstr = "__doPostBack('" + functionname_id.replace(/_/gi, '$') + "','" + functionname_id.replace(/_/gi, '$') + "_Click')";
            window.setTimeout(postBackstr, 0, 'JavaScript');

            return true;
        }


        function RefreshSessionAttachment() {

            var functionname_id = "<%=btnRefreshSessionAttachment.ClientID%>";

            var postBackstr = "__doPostBack('" + functionname_id.replace(/_/gi, '$') + "','" + functionname_id.replace(/_/gi, '$') + "_Click')";
            window.setTimeout(postBackstr, 0, 'JavaScript');

            return true;
        }




        function AlertForMorethanOneAllotments(msg, id) {
          
            var JSConfirmStatusid = "<%=JSConfirmStatus.ClientID%>";

            if (document.getElementById(JSConfirmStatusid).value == "false") {
                document.getElementById(JSConfirmStatusid).value = "true";
                $.alerts.okButton = " Yes ";
                $.alerts.cancelButton = " No ";



                var strMsg = "More than one allotment exists for this crew." + "\n\n"
                                       + msg + "\n\n"
                                       + "Do you want to approve ?";

                var aa1 = jConfirm(strMsg, ' Alert !', function (r) {

                    if (r) {

                        var strid = '#' + id.toString();
                       
                        $(strid).click();


                        return true;

                    }
                    else {
                        document.getElementById(JSConfirmStatusid).value = "false";

                        return false;

                    }

                });
            }

            return false;



        }


        function ShowFlagRemark(Allotment_ID, Vessel_ID, PBill_Date, CrewID) {

            document.getElementById('ctl00_MainContent_hdfAllotmentIDFlag').value = Allotment_ID;
            document.getElementById('ctl00_MainContent_hdfVesselIDFlag').value = Vessel_ID;
            document.getElementById('ctl00_MainContent_hdfPBillDateFlag').value = PBill_Date;
            document.getElementById('ctl00_MainContent_hdfCrewIDFlag').value = CrewID;
            document.getElementById('ctl00_MainContent_txtFlagRemark').value = "";
            try { document.getElementById('__divMsgTooltip_Fixed').style.display = 'none'; } catch (ex) { }
            showModal('dvFlagRemark', false, RefreshSessionAttachment);
            ShowLogFromAddPopup(Allotment_ID, Vessel_ID, PBill_Date);


        }

        var lastExecutorFlag = null;
        function ShowFlagRemarkasTooltip(Allotment_ID, Vessel_ID, PBill_Date, evt, objthis) {

            if (lastExecutorFlag != null)
                lastExecutorFlag.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncACC_GET_Allotment_Flag', false, { "Allotment_ID": Allotment_ID, "Vessel_ID": Vessel_ID, "PBill_Date": PBill_Date }, onSuccessFlagRemarkas, Onfail, new Array(evt, objthis));
            lastExecutorFlag = service.get_executor();

        }

        function onSuccessFlagRemarkas(retVal, eventArgs) {

            js_ShowToolTip(" <table style='border-radius:5px;max-width:500px' class='CreateHtmlTableFromDataTable-table'><tr><td>" + retVal + "</td> </tr> </table>", eventArgs[0], eventArgs[1]);
        }


        var lastExecutorFlagPopUP = null;
        function ShowLogFromAddPopup(Allotment_ID, Vessel_ID, PBill_Date) {

            if (lastExecutorFlagPopUP != null)
                lastExecutorFlagPopUP.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'asyncACC_GET_Allotment_Flag', false, { "Allotment_ID": Allotment_ID, "Vessel_ID": Vessel_ID, "PBill_Date": PBill_Date }, onShowLogFromAddPopup, Onfail);
            lastExecutorFlagPopUP = service.get_executor();

        }

        function onShowLogFromAddPopup(retVal, eventArgs) {

            document.getElementById('dvFlagremarkPopup').innerHTML = retVal;
        }



        function ShowFlagReOpen(Allotment_ID, Vessel_ID, PBill_Date) {
            document.getElementById('ctl00_MainContent_hdfFlagReOpenAllotmentID').value = Allotment_ID;
            document.getElementById('ctl00_MainContent_hdfFlagReOpenVesselID').value = Vessel_ID
            document.getElementById('ctl00_MainContent_hdfFlagReOpenPBillDate').value = PBill_Date
            document.getElementById('ctl00_MainContent_txtReasonForReopenFlag').value = "";
            try { document.getElementById('__divMsgTooltip_Fixed').style.display = 'none'; } catch (ex) { }
            showModal('dvReOpenFlag');

        }
       

    </script>
    <script type="text/javascript">
        //------------NEW ALLOTMENT ENTRY-------------------
        //-------------------------------------------------------
        function NewAllotment_Click() {

            var SearchText = $('[id$=txtSearch]').val();
            $('#dvPopupFrame').attr("Title", "New Allotment");
            $('#dvPopupFrame').css({ "width": "900px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { var h = (this.contentWindow.document.body.offsetHeight + 20); this.style.height = h + 'px'; $('#dvPopupFrame').css({ "height": (h + 70) + 'px' }); });

            var URL = "NewAllotment.aspx?search=" + SearchText + "&rnd=" + Math.random();
            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);
        }

        function ReloadAllotment() {
            $('[id$=btnSearch]').trigger('click');
        }
        


    </script>
    <style type="text/css">
        .ajax__fileupload_button
        {
            background-color: green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="page-title">
        Salary by Wire Transfer
    </div>
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvFilter" style="border: 1px solid #cccccc; margin: 2px;">
                    <table width="100%" border="0">
                        <tr>
                            <td align="right">
                                Fleet
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFleet_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Year
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" Style="width: 200px"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Approved By Crew Team
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlApproval" runat="server" Style="width: 200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlApproval_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">-SELECT ALL-</asp:ListItem>
                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="0">Pending</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Nationality
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" Width="156px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Bank Name
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDLBank" runat="server" Style="width: 120px">
                                    <asp:ListItem Value="0">-SELECT ALL-</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Vessel
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlVessel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlVessel_SelectedIndexChanged"
                                    Style="width: 200px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Month
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMonth" runat="server" Style="width: 200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-Select-</asp:ListItem>
                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                    <asp:ListItem Value="9">Sept</asp:ListItem>
                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Status
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlStatus" runat="server" Style="width: 200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-SELECT ALL-</asp:ListItem>
                                    <asp:ListItem Value="1">Sent</asp:ListItem>
                                    <asp:ListItem Value="2">Not Sent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" colspan="3">
                                <asp:RadioButtonList ID="rbtnVerificationStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Verified" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Not Verified" Value="2" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left; padding-top: 5px; vertical-align: top;">
                                <asp:CheckBox ID="chkAmountIsGreaterthanZero" runat="server" Text="Amount > 0" Checked="true" /><br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: right; padding: 5px 2px 5px 0px; background: url(../Images/bg.png) left -10px repeat-x;">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 30%">
                                <asp:Button ID="btnReworkAllotmentToVessel" ForeColor="#7F14AC" Font-Size="9" Font-Names="verdana"
                                    runat="server" OnClientClick="javascript:var c=confirm('This will rework the allotment to vessel,do you want to continue ?');if(c)return true;else return false;"
                                    Text="Rework Allotment" OnClick="btnReworkAllotmentToVessel_Click" />
                            </td>
                            <td align="right">
                                <asp:CheckBox ID="chkFlagedItems" Text="Show Flagged Items" runat="server" />&nbsp;
                                <asp:TextBox ID="txtSearch" runat="server" Style="width: 100px" />
                                &nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnAddNewAllotment" runat="server" Text="Add New Allotment" OnClientClick="NewAllotment_Click(); return false;" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="dvMain" style="border: 1px solid #cccccc; margin: 2px;">
                    <asp:GridView ID="gvAllotments" DataKeyNames="id,Vessel_ID,AllotmentID" runat="server"
                        AutoGenerateColumns="False" CellPadding="4" AllowPaging="True" PageSize="30"
                        Width="100%" ShowFooter="true" EmptyDataText="No Record Found" CaptionAlign="Bottom"
                        GridLines="None" OnRowDataBound="gvAllotments_RowDataBound" OnPageIndexChanging="gvAllotments_PageIndexChanging"
                        OnRowEditing="gvAllotments_RowEditing" OnRowUpdating="gvAllotments_RowUpdating"
                        OnRowCancelingEdit="gvAllotments_RowCancelingEdit" CssClass="gridmain-css">
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                        <Columns>
                            <asp:TemplateField HeaderText="S/N" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# ((GridViewRow)Container).RowIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vessel" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnVessel_ID" runat="server" Value='<%# Eval("Vessel_ID")%>' />
                                    <asp:Label ID="lblVessel" runat="server" Text='<%# Eval("vessel_short_name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="S/C" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("ID")%>' CssClass="staffInfo"
                                        Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>                                        
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Staff Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" Visible="false" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Staff_fullName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rank" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRank" runat="server" Text='<%# Eval("Rank_Short_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acc. No." HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccountId" Visible="false" runat="server" Text='<%# Eval("BankAccId") %>'></asp:Label>
                                    <asp:Label ID="lblLeabeWage" runat="server" Text='<%# GetText(Eval("Acc_NO").ToString()) %>'></asp:Label>
                                    <asp:Label ID="lblkAccountInfo" runat="server" Font-Bold="true" Text="i" Font-Italic="true"
                                        Font-Size="11px" ForeColor="Blue"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAccount" AppendDataBoundItems="true" DataSourceID="objDsourceAccount"
                                        Width="120px" DataTextField="Acc_NO_Bnf" Text='<%#Eval("BankAccId")%>' DataValueField="id"
                                        runat="server">
                                        <asp:ListItem Text="New Account" Value="0">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objDsourceAccount" runat="server" SelectMethod="Get_Crew_BankAccList"
                                        TypeName="SMS.Business.PortageBill.BLL_PortageBill">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblId" Name="CrewID" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank Name" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("Bank_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PB Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblPBDate" runat="server" Text='<%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("PBill_Date"))) %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='Total:'></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="padding-right: 3px">
                                                <asp:Image ID="imgSideLetter" runat="server" ImageAlign="Bottom" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:#,##0.00}")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAllotmentAmount" onchange="checkNumber(id)" Style="text-align: right"
                                        runat="server" Text='<%#Eval("Amount")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblAmountTotal" runat="server" Text=''></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency_TypeId" runat="server" Visible="false" Text='<%# Eval("Currency_id")%>'></asp:Label>
                                    <asp:Label ID="LblCurrency_Type" runat="server" Text='<%# Eval("Currency")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verify" HeaderStyle-HorizontalAlign="Center" AccessibleHeaderText="VerifyRecords">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnAllotmentID" runat="server" Value='<%# Eval("AllotmentID")%>' />
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Convert.ToBoolean(Eval("Approval_Status")) %>'
                                        Enabled='<%#(Eval("approval_status").ToString()=="1" || UDFLib.ConvertToDecimal(Eval("Amount"))==0 )?false:true %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnViewToVerify" runat="server" OnClick="btnViewToVerify_Click" CommandArgument='<%#Eval("ID").ToString()+","+Eval("PBill_Date").ToString()+","+Eval("AllotmentID").ToString() %>'
                                        Text="View And Verify" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnUnverify" runat="server" OnClick="btnUnverify_Click" CommandArgument='<%#Eval("Vessel_ID").ToString()+","+Eval("AllotmentID").ToString() %>'
                                        Visible='<%# Eval("Approval_Status").ToString()=="1"?true:false %>' Enabled='<%# Eval("Released_To_Bank").ToString()=="0"?true:false %>'
                                        Text="Unverify" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% of salary" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPercent_of_salary" ToolTip="percentage of full salary" Text='<%#Eval("PERCENT_OF_SALARY")%>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblSalary" Text="Wage" Style="cursor: pointer; color: Blue" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowHeader="true" ButtonType="Button" EditText="Edit" UpdateText="Update"
                                AccessibleHeaderText="EditRecords" ShowEditButton="true" ShowCancelButton="true" />
                            <asp:TemplateField AccessibleHeaderText="DeleteRecords">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%#Eval("Vessel_ID").ToString()+","+Eval("PBill_Date").ToString()+","+Eval("AllotmentID").ToString() %>'
                                        ImageUrl="~/Images/Delete.png" OnClientClick="return confirm('Are you sure, you want to delete the record?')"
                                        Text="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="AllotmentFlag">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgbtnAddFlagRemark" runat="server" ImageUrl="~/Images/Allot-Flag-Inactive.png" />
                                            </td>
                                            <td style="padding-left: 15px">
                                                <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                    Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;ACC_Dtl_Allotments&#39;,&#39; ID="+Eval("AllotmentID").ToString()+" and Vessel_ID="+Eval("Vessel_ID").ToString()+"&#39;,event,this)" %>'
                                                    AlternateText="info" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#E6F8E0" ForeColor="#333333" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <EditRowStyle CssClass="RowStyle-css" BackColor="LightGreen" />
                        <PagerStyle Font-Size="Larger" CssClass="pager" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <div style="margin: 2px; border: 1px solid #cccccc; height: 22px; vertical-align: bottom;
                    background: url(../Images/bg.png) left -10px repeat-x; color: Black; text-align: left;
                    padding-top: 5px; background-color: #F6CEE3;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 150px; text-align: left;">
                                Page Size:
                                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 180px">
                                You are in Page:
                                <asp:Label ID="lblPageStatus" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="width: 40px">
                            </td>
                            <td style="width: 100px">
                                Total Staff:<asp:Label ID="lblRecordCount" runat="server" Text="0"></asp:Label>
                            </td>
                            <td style="width: 20px">
                            </td>
                            <td style="width: 210px">
                            </td>
                            <td style="text-align: left">
                                <div id="dvInterviewSchedule">
                                </div>
                            </td>
                            <td style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Div1" style="border: 1px solid #cccccc; margin: 2px; text-align: right;">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="12px"
                        BackColor="Yellow" Font-Italic="true" Width="400px"></asp:Label>
                    <asp:Button ID="btnVerify" runat="server" Visible="false" Text="Verify Selected"
                        OnClick="btnVerify_Click" /></div>
                <div id="dvVerifyAllotment" style="display: none; color: Black; border: 1px solid gray;
                    padding: 2px; width: 800px" title="Verify Allotment">
                    <asp:UpdatePanel ID="uudverifyaalot" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="right" style="background-color: LightGreen; width: 100%">
                                        <uc1:ctlRecordNavigation ID="ctlRecordNavigationAllotment" OnNavigateRow="BindAllotmentsToVerify"
                                            runat="server" />
                                            <asp:HiddenField ID="JSConfirmStatus" Value="false" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <asp:FormView ID="frmvCrewDetails" Width="100%" runat="server">
                                <ItemTemplate>
                                    <table style="border-collapse: collapse; border: 1px solid #DADADA; width: 100%">
                                        <tr>
                                            <td class="tdh" style="width: 20%">
                                                Staff Code :
                                            </td>
                                            <td class="tdd" style="width: 30%">
                                                <asp:HyperLink ID="lblSC" runat="server" NavigateUrl='<%# "../Crew/CrewDetails.aspx?ID=" + Eval("ID")%>' CssClass="staffInfo"
                                                    Target="_blank" Text='<%# Eval("STAFF_CODE")%>'></asp:HyperLink>
                                            </td>
                                            <td class="tdh">
                                                Vessel :
                                            </td>
                                            <td class="tdd" style="width: 150px">
                                                <%# Eval("vessel_short_name")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Name :
                                            </td>
                                            <td class="tdd">
                                                <%# Eval("Staff_fullName")%>
                                            </td>
                                            <td class="tdh">
                                                Joining Date :
                                            </td>
                                            <td class="tdd">
                                                <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Joining_Date")))%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Rank :
                                            </td>
                                            <td class="tdd">
                                                <%# Eval("Rank_Short_Name")%>
                                            </td>
                                            <td class="tdh">
                                                Signoff Date :
                                            </td>
                                            <td class="tdd">
                                                 <%#UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("Sign_Off_Date")))%>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="border-collapse: collapse; border: 1px solid #DADADA; width: 100%">
                                        <tr>
                                            <td class="tdh" style="width: 20%">
                                                A/C No :
                                            </td>
                                            <td class="tdd" style="width: 30%">
                                                <%# Eval("Acc_NO") %>
                                            </td>
                                            <td rowspan="5" valign="middle" style="font-size: 11px; border: 1px solid #DADADA;
                                                padding: 0px 0px 0px 0px; text-align: center">
                                                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100px">
                                                    <tr>
                                                        <td style="height: 25px; font-size: 11px; font-weight: bold; border-bottom: 1px solid #DADADA">
                                                            Salary-by-wire-transfer
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 75px">
                                                            <span style="color: Blue; font-weight: bold">
                                                                <%# Eval("Amount","{0:#,##0.00}")%></span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td rowspan="5" style="text-align: center; border: 1px solid #DADADA;">
                                                <asp:Button ID="btnVerifyAllot" runat="server" Width="90px" Height="30px" Font-Size="12px"
                                                    Font-Names="verdana" Text="Verify" OnClick="btnVerifyAllot_Click" CommandArgument='<%#Eval("Vessel_ID").ToString()+","+Eval("AllotmentID").ToString()+","+Eval("id").ToString()%>'
                                                    Enabled='<%#(Eval("approval_status").ToString()=="1" || UDFLib.ConvertToDecimal(Eval("Amount"))==0 ) || objUA.Approve==0 ?false:true %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Beneficiary :
                                            </td>
                                            <td class="tdd">
                                                <%# Eval("Beneficiary")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Bank Name :
                                            </td>
                                            <td class="tdd">
                                                <%# Eval("Bank_Name")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Address :
                                            </td>
                                            <td class="tdd" style="width: 220px">
                                                <%# Eval("Bank_Address")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdh">
                                                Swift Code :
                                            </td>
                                            <td class="tdd">
                                                <%# Eval("SwiftCode")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                            <br />
                            <table style="border-collapse: collapse; border: 1px solid #DADADA; width: 100%">
                                <tr>
                                    <td style="font-weight: bold; border: 1px solid #DADADA; width: 250px">
                                        <asp:Label ID="lblPrevMonthPBDate" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td style="font-weight: bold; border: 1px solid #DADADA; width: 250px">
                                        Contract
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td style="font-weight: bold; border: 1px solid #DADADA; width: 250px">
                                        <asp:Label ID="lblPortageBilldate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="padding: 3px; width: 250px; border: 1px solid #DADADA">
                                        <asp:GridView ID="gvPrevMonthPB" runat="server" Width="100%" AutoGenerateColumns="false"
                                            GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Salary_type")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Component">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComponent" runat="server" Text='<%#Eval("name")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td valign="top" style="padding: 3px; width: 250px; border: 1px solid #DADADA">
                                        <asp:GridView ID="gvJoiningWages" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Salary_type")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Component">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComponent" runat="server" Text='<%#Eval("name")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </td>
                                    <td style="width: 20px">
                                        &nbsp;
                                    </td>
                                    <td valign="top" style="padding: 3px; width: 250px; border: 1px solid #DADADA">
                                        <asp:GridView ID="gvBortagebill" runat="server" Width="100%" AutoGenerateColumns="false"
                                            GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Salary_type")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Component">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComponent" runat="server" Text='<%#Eval("name")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount")%>' ForeColor='<%#Eval("Salary_type").ToString()=="Ded"?System.Drawing.Color.Maroon:System.Drawing.Color.Black %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="HeaderStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <RowStyle CssClass="RowStyle-css" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="dvFlagRemark" title="Reason for this red-flag" style="display: none; width: 515px">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <table style="width: 100%;" cellpadding="0">
                                    <tr>
                                        <td style="font-weight: bold; text-align: left; padding-bottom: 5px">
                                            Date :
                                        </td>
                                        <td align="left">
                                            <%=UDFLib.ConvertUserDateFormat(DateTime.Now.ToShortDateString())%>
                                        </td>
                                        <td style="font-weight: bold; text-align: center">
                                            User :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblDlaguser" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: left; font-weight: bold">
                                            Message
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtFlagRemark" runat="server" MaxLength="7000" TextMode="MultiLine"
                                    Height="200px" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2"
                                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                                MaximumNumberOfFiles="10" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSaveFlagRemark" Text="Save and Close" runat="server" Height="30px"
                                    OnClick="btnSaveFlagRemark_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnMarkCompleted" runat="server" Height="30px" OnClick="tbnMarkCompleted_Click"
                                    Text="Mark as completed" />
                                <br />
                                <br />
                                <div id="dvFlagremarkPopup" style="max-width: 500px; max-height: 150px; border: 1px 1px 1px 1px;
                                    overflow: auto">
                                </div>
                                <asp:HiddenField ID="hdfAllotmentIDFlag" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="hdfVesselIDFlag" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="hdfPBillDateFlag" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="hdfCrewIDFlag" EnableViewState="true" runat="server" />
                                <div style="display: none">
                                    <asp:Button ID="btnRefreshSessionAttachment" runat="server" OnClick="btnRefreshSessionAttachment_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvReOpenFlag" title="Reason for Re-open red flag" style="display: none;
                    width: 500px">
                    <table>
                        <tr>
                            <td align="left">
                                <b>Reason</b><br />
                                <asp:TextBox ID="txtReasonForReopenFlag" runat="server" Height="80px" Width="490px"
                                    TextMode="MultiLine" MaxLength="7000" ValidationGroup="reopenFlag"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvReasonReOpenFlag" runat="server" ControlToValidate="txtReasonForReopenFlag"
                                    Display="Static" SetFocusOnError="true" Font-Size="12px" ErrorMessage="Please enter reason"
                                    ValidationGroup="reopenFlag"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnReOpenFlag" runat="server" OnClick="btnReOpenFlag_Click" ValidationGroup="reopenFlag"
                                    Height="28px" Width="150px" Text="Re-open" />
                                <asp:HiddenField ID="hdfFlagReOpenAllotmentID" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="hdfFlagReOpenVesselID" EnableViewState="true" runat="server" />
                                <asp:HiddenField ID="hdfFlagReOpenPBillDate" EnableViewState="true" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" style="min-height: 200px" width="100%">
            </iframe>
            <div style="text-align: right">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClientClick="hideModal('dvPopupFrame'); return false;"
                    BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                    Height="24px" BackColor="#81DAF5" Width="80px" />
            </div>
        </div>
    </div>
</asp:Content>
