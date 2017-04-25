<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" EnableEventValidation="false"
    CodeFile="RestHour_Report.aspx.cs" Inherits="RestHourDetails" Title="Rest Hours Report" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="tlk4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        var lastExecutor = null;


        function asyncGet_RestHourExceptions(RestHourID, Vessel_ID, evt, objthis, isclicked, pageheader) {


            if (lastExecutor != null)
                lastExecutor.abort();

            var service = Sys.Net.WebServiceProxy.invoke('../../JibeWebService.asmx', 'asyncGet_RestHourExceptions', false, { "RestHourID": RestHourID, "Vessel_ID": Vessel_ID }, onSuccess_asyncGet_RestHourExceptions, Onfail, new Array(evt, objthis, isclicked, pageheader));

            lastExecutor = service.get_executor();

        }
        function Onfail(msg) {

           // alert(msg._message);
        }

        function onSuccess_asyncGet_RestHourExceptions(retVal, Args) {

            js_ShowToolTip_Fixed(retVal, Args[0], Args[1], Args[3]);
        }
 
        
        
    </script>
    <style type="text/css">
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage1
        {
            background-color: Yellow;
            color: Black;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
            text-align: Left;
            width: 18px;
        }
        .HeaderCellColor1
        {
            background-color: #3399CC;
            color: White;
            text-align: right;
            width: 18px;
        }
        .FieldDottedLine
        {
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            border-bottom: 1px dashed;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 0px solid #FFB733;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #CCCCCC;
            border: 1px solid gray;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid gray;
        }
        .page
        {
            width: 100%;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <center>
        <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/1.png" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelcurr" runat="server">
            <ContentTemplate>
                <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                    height: 100%;">
                    <div id="page-header" class="page-title">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <b>Rest Hours Report </b>
                                </td>
                            </tr>
                        </table>
                        <div style="visibility: hidden">
                            <asp:HiddenField ID="hdnVesselID" runat="server" />
                            <asp:HiddenField ID="hdnLogBookID" runat="server" />
                            <asp:Button ID="btnHiddenSubmit" runat="server" Text="btnHiddenSubmit" OnClick="btnHiddenSubmit_Click" />
                        </div>
                    </div>
                    <div style="text-align: left; overflow: hidden" id="dvPageContent">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    From:
                                    <asp:Label ID="lbl1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFrom" runat="server" OnTextChanged="txtFrom_TextChanged"></asp:TextBox>
                                    <tlk4:CalendarExtender ID="caleDateFrom" TargetControlID="txtFrom" Format="dd-MM-yyyy"
                                        runat="server">
                                    </tlk4:CalendarExtender>
                                </td>
                                <td>
                                    To:
                                    <asp:Label ID="Label1" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTo" runat="server" AutoPostBack="true" OnTextChanged="txtTo_TextChanged"></asp:TextBox>
                                    <tlk4:CalendarExtender ID="CalendarExtender1" TargetControlID="txtTo" Format="dd-MM-yyyy"
                                        runat="server">
                                    </tlk4:CalendarExtender>
                                </td>
                                <td>
                                    Vessel
                                    <asp:Label ID="Label2" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVesselList" runat="server" OnSelectedIndexChanged="ddlVesselList_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Crew
                                    <asp:Label ID="Label3" runat="server" ForeColor="#FF0000" Text="*"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCrewList" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="txtSearch" runat="server" Text="Search" OnClick="txtSearch_Click" />
                                </td>
                                <td style="text-align: right">
                                    <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to excel" OnClick="ImgExpExcel_Click"
                                        ImageUrl="~/Images/Exptoexcel.png" />
                                </td>
                            </tr>
                        </table>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td valign="top" style="border: 1px solid gray; color: Black">
                                    <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;">
                                        <table cellspacing="1" cellpadding="0" width="100%">
                                            <tr style="height: 30px">
                                                <td width="12%;" align="right">
                                                    <b>Staff Code :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStaffCode" Width="150px" runat="server"> </asp:Label>
                                                </td>
                                                <td width="12%;" align="right">
                                                    <b>Staff Name :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStaffName" Width="350px" runat="server"> </asp:Label>
                                                </td>
                                                <td width="12%;" align="right" style="border-right: 1px solid white">
                                                    <b>Rank :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblStaffRank" Width="150px" runat="server"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td width="12%;" align="right">
                                                    <b>Date of Joining :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDateofjoing" Width="150px" runat="server"> </asp:Label>
                                                </td>
                                                <td width="12%;" align="right">
                                                    <b>Sign On Date-Time :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblSignOn" Width="150px" runat="server"> </asp:Label>
                                                </td>
                                                <td width="12%;" align="right">
                                                    <b>Sign Off Date :&nbsp;&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDateofsignoff" Width="150px" runat="server"> </asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 28px; border-bottom: 1px solid gray; color: Black">
                                                </td>
                                            </tr>
                                            <%-- <tr style="height: 30px">
                                        <td width="6%;" align="right">
                                            <b>Rank :&nbsp;&nbsp;</b>
                                        </td>
                                        <td align="left" style="border-right: 1px solid white">
                                            <asp:Label ID="lblManagerRank" Width="150px" runat="server"> </asp:Label>
                                        </td>
                                        <td width="6%;" align="right">
                                            <b>Name :&nbsp;&nbsp;</b>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblManagerName" Width="350px" runat="server"> </asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>--%>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td width="95%" valign="top">
                                    <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;">
                                        <asp:Repeater ID="rpDeckLogBook01" runat="server">
                                            <HeaderTemplate>
                                                <table cellspacing="1" cellpadding="1" width="100%">
                                                    <tr align="center" style="height: 25px">
                                                        <td style="width: 80px; font-size: 9px; text-align: center" class="HeaderCellColor1">
                                                            Date
                                                        </td>
                                                        <td class="HeaderCellColor1" style="width: 60px">
                                                            Hours 0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            3
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            4
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            5
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            6
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            7
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            8
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            9
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            3
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            4
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            5
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            6
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            7
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            8
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            9
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            0
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            1
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            2
                                                        </td>
                                                        <td class="HeaderCellColor">
                                                            3
                                                        </td>
                                                        <td class="HeaderCellColor1">
                                                            <%--2--%>
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Rest hours
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Rest hours (In Any 24 Hrs)
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap; margin: 0px 5px 5px 0px">
                                                            Rest hours in 7 Days
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Over Time
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Seafarer's Remark
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Verifier's Remark
                                                        </td>
                                                        <td class="HeaderCellColor1" style="white-space: nowrap">
                                                            Exceptions
                                                        </td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr align="center">
                                                    <td style="width: 60px; font-size: 9px">
                                                        <%# DataBinder.Eval(Container.DataItem,"REST_HOURS_DATE")  %>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0000_0030").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0030_0100").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0100_0130").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0130_0200").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0200_0230").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0230_0300").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0300_0330").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0330_0400").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0400_0430").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0430_0500").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0500_0530").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0530_0600").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0600_0630").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0630_0700").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0700_0730").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0730_0800").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0800_0830").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0830_0900").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0900_0930").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0930_1000").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1000_1030").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1030_1100").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1100_1130").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1130_1200").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1200_1230").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1230_1300").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1300_1330").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1330_1400").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1400_1430").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1430_1500").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1500_1530").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1530_1600").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1600_1630").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1630_1700").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1700_1730").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1730_1800").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1800_1830").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1830_1900").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1900_1930").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1930_2000").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2000_2030").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2030_2100").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2100_2130").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2130_2200").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2200_2230").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2230_2300").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2300_2330").ToString() %>'>
                                                    </td>
                                                    <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2330_2400").ToString() %>'>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "REST_HOURS")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "REST_HOURS_ANY24")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "RestHrs7Day")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "OverTime_HOURS")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "Seafarer_Remarks")%>
                                                    </td>
                                                    <td>
                                                        <%# DataBinder.Eval(Container.DataItem, "Verifier_Remarks")%>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="hlnkPB" runat="server" NavigateUrl="#" Style="cursor: pointer"
                                                            ImageUrl="~/Images/bullet-red-icon.png" Text="PT" onclick='<%#"asyncGet_RestHourExceptions(&#39;"+Eval("ID").ToString()+"&#39;,&#39;"+Eval("Vessel_ID").ToString()+"&#39;,event,this,1,&#39;Attachments&#39;);" %>'
                                                            Visible='<%#Eval("ImgVisibility").ToString()=="No"?false:true%>'></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr align="center">
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1" style="width: 60px">
                                                        Hours 0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        3
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        4
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        5
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        6
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        7
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        8
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        9
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        3
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        4
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        5
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        6
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        7
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        8
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        9
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        0
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        1
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                        2
                                                    </td>
                                                    <td class="HeaderCellColor">
                                                        3
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                    <td class="HeaderCellColor1">
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" valign="top">
                                    <table cellspacing="0" style="background-color: White; display: none" width="100%;">
                                        <tr>
                                            <td style="height: 20px" colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 15%; border-left: 1px solid white;">
                                                Seafarer's Remarks :&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 35%; border-left: 1px solid white;">
                                                <asp:TextBox ID="txtSeafarerRemarks" runat="server" MaxLength="500" Width="350px"
                                                    TextMode="MultiLine" Height="71px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 15%; border-left: 1px solid white;">
                                                Verifier's Remarks
                                            </td>
                                            <td style="width: 35%; border-left: 1px solid white;">
                                                <asp:TextBox ID="txtVerifierRemarks" runat="server" MaxLength="400" Width="350px"
                                                    TextMode="MultiLine" Height="83px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px">
                                            </td>
                                            <td style="height: 20px; vertical-align: top">
                                                <asp:CheckBox ID="chkArrival" runat="server" Text="Arrival" Width="100"></asp:CheckBox>
                                                <asp:CheckBox ID="chkDeparture" runat="server" Text="Departure" Width="100"></asp:CheckBox>
                                                <asp:CheckBox ID="chkEmergency" runat="server" Text="Emergency" Width="100"></asp:CheckBox>
                                                <asp:CheckBox ID="chkDrill" runat="server" Text="Drill" Width="100"></asp:CheckBox>
                                            </td>
                                            <td style="height: 20px">
                                            </td>
                                            <td style="height: 20px">
                                                <asp:CheckBox ID="chkEmergencyVerify" runat="server" Text="Emergency  other overriding operational condition. Musters, fire - fighting, lifeboat drills, and drills prescribed by national laws and regulation and by international instruments."
                                                    Width="350"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 80%; text-align: left">
                        <asp:Label ID="lblRestHourDate" runat="server" Width="250px" Visible="false" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblManage" Width="450px" runat="server"> </asp:Label>
                        <div id="dvRecordInformation" style="float: left; width: 100%">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgExpExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
</asp:Content>
