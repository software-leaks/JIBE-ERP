<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" EnableEventValidation="false" Title="Work/Rest Hours details"
    CodeFile="RestHourDetails.aspx.cs" Inherits="RestHourDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        $(document).ready(function () {

            var wh = 'ID=<%=Request.QueryString["ID"]%> and VESSEL_ID= <%=Request.QueryString["Vessel_ID"]%>';

            Get_Record_Information_Details('CRW_DTL_RESTHOURS', wh);

        });


       

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
        <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
            height: 100%;">
            <div id="page-header" class="page-title">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <b>Work/Rest Hours details</b>
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
                                    <tr>
                                        <td style="height: 30px" align="right">
                                            <b>Reports To :&nbsp;&nbsp;</b>
                                        </td>
                                        <td colspan="5" style="height: 30px">
                                            <asp:Label ID="lblManage" Width="450px" runat="server"> </asp:Label>
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
                    </table> </div> </td> </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td class="HeaderCellColor" style="height: 24px">
                            <asp:Label ID="lblRestHourDate" runat="server" Width="250px" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="95%" valign="top">
                            <div style="padding-top: 5px; padding-bottom: 5px; padding-left: 5px; padding-right: 5px;">
                                <asp:Repeater ID="rpDeckLogBook01" runat="server">
                                    <HeaderTemplate>
                                        <table cellspacing="1" cellpadding="1" width="100%">
                                            <tr align="center">
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
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center">
                                            <td>
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0000_0030").ToString() %>'>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0030_0100").ToString() %>'>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0100_0130").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0100_0130")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0130_0200").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0130_0200")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0200_0230").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0200_0230")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0230_0300").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0230_0300")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0300_0330").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0300_0330")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0330_0400").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0330_0400")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0400_0430").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0400_0430")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0430_0500").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0430_0500")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0500_0530").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0500_0530")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0530_0600").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0530_0600")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0600_0630").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0600_0630")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0630_0700").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0630_0700")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0700_0730").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0700_0730")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0730_0800").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0730_0800")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0800_0830").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0800_0830")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0830_0900").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0830_0900")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0900_0930").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_0900_0930")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_0930_1000").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_0930_1000")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1000_1030").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1000_1030")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1030_1100").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_1030_1100")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1100_1130").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1100_1130")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1130_1200").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1130_1200")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1200_1230").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1200_1230")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1230_1300").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1230_1300")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1300_1330").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1300_1330")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1330_1400").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1330_1400")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1400_1430").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1400_1430")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1430_1500").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1430_1500")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1500_1530").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1500_1530")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1530_1600").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1530_1600")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1600_1630").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1600_1630")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1630_1700").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_1630_1700")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1700_1730").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1700_1730")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1730_1800").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1730_1800")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1800_1830").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1800_1830")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1830_1900").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_1830_1900")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1900_1930").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_1900_1930")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_1930_2000").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_1930_2000")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2000_2030").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_2030_2100")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2030_2100").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_2030_2100")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2100_2130").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_2100_2130")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2130_2200").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_2130_2200")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2200_2230").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_2200_2230")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2230_2300").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_2230_2300")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2300_2330").ToString() %>'>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "WH_2300_2330")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"WH_2330_2400").ToString() %>'>
                                                <%-- <%# DataBinder.Eval(Container.DataItem, "WH_2330_2400")%>--%>
                                                &nbsp;&nbsp;
                                            </td>
                                            <%--<td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Ex_WH_2400_2430").ToString() %>'>--%>
                                            <%-- <%# DataBinder.Eval(Container.DataItem, "Ex_WH_2400_2430")%>--%>
                                            <%--   &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Ex_WH_2430_2500").ToString() %>'>--%>
                                            <%-- <%# DataBinder.Eval(Container.DataItem, "Ex_WH_2430_2500")%>--%>
                                            <%--  &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Ex_WH_2500_2530").ToString() %>'>--%>
                                            <%--<%# DataBinder.Eval(Container.DataItem, "Ex_WH_2500_2530")%>--%>
                                            <%-- &nbsp;&nbsp;
                                            </td>
                                            <td class='<%# "CellClass" + DataBinder.Eval(Container.DataItem,"Ex_WH_2530_2600").ToString() %>'>--%>
                                            <%-- <%# DataBinder.Eval(Container.DataItem, "Ex_WH_2530_2600")%>--%>
                                            <%--    &nbsp;&nbsp;
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr align="center">
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
                                                <%--  2--%>
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
                            <table cellspacing="0" cstyle="background-color: White;" width="100%">
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
                                    <td align="right" style="width: 11%; border-left: 1px solid white;" valign="top">
                                        Seafarer's Remarks :&nbsp;&nbsp;
                                    </td>
                                    <td style="width: 35%; border-left: 1px solid white;">
                                        <asp:TextBox ID="txtSeafarerRemarks" runat="server" MaxLength="500" Width="400px" 
                                            TextMode="MultiLine" Height="71px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right" style="width: 8%; border-left: 1px solid white;" valign="top" >
                                        Verifier's Remarks :
                                    </td>
                                    <td style="width: 35%; border-left: 1px solid white;">
                                        <asp:TextBox ID="txtVerifierRemarks" runat="server" MaxLength="400" Width="350px"
                                            TextMode="MultiLine" Height="83px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px">
                                    </td>
                                    <td style="height: 20px; vertical-align:top">
                                        <asp:CheckBox ID="chkArrival" runat="server" Text="Arrival" Width="60"></asp:CheckBox>
                                        <asp:CheckBox ID="chkDeparture" runat="server" Text="Departure" Width="80"></asp:CheckBox>
                                        <asp:CheckBox ID="chkEmergency" runat="server" Text="Emergency" Width="85"></asp:CheckBox>
                                        <br />
                                        <asp:CheckBox ID="chkDrill" runat="server" Text="Drill" Width="60"></asp:CheckBox>
                                        <asp:CheckBox ID="chkOther" runat="server" Text="Others- please specify" Width="150"></asp:CheckBox>
                                    </td>
                                    <td style="height: 20px">
                                    </td>
                                    <td style="height: 20px">
                                        <asp:CheckBox ID="chkEmergencyVerify" runat="server" Text="Emergency  other overriding operational condition. Musters, fire - fighting, lifeboat drills, and drills prescribed by national laws and regulation and by international instruments."
                                            Width="450"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                <td align="right" style="width: 10%; border-left: 1px solid white;" valign="top">
                                      <span id="lblRev" runat="server" visible="false"> Re-verification Remarks :&nbsp;&nbsp;</span>
                                    </td>
                                 <td style="width: 35%; border-left: 1px solid white;">
                                        <asp:TextBox ID="txtReverification" runat="server" MaxLength="500" Width="400px"  Visible="false"
                                            TextMode="MultiLine" Height="71px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                
                                </tr>
                                  
                                </table>
                            <table>
                            <tr>
                             <td align="right" style="width: 5%; border-left: 1px solid white;">
                                           Created by :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgCreated" runat="server" Height="30px" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkCreated" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                           &nbsp; &nbsp; &nbsp; &nbsp;
                                        </td>
                                        <td align="right" style="width: 5%; border-left: 1px solid white;">
                                           Modified by :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgModif" runat="server" Height="30px" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkModif" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>
                            </tr>
                            <tr>
                             <td align="right" style="width: 5%; border-left: 1px solid white;">
                                           Verified by :&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                            <asp:Image ID="imgVerified" runat="server" Height="30px" />
                                        </td>
                                        <td align="left" style="width: 10%; border-left: 1px solid white;">
                                            <asp:HyperLink ID="lnkVerified" CssClass="FieldDottedLine link" runat="server"
                                                ForeColor="Blue"></asp:HyperLink>
                                        </td>
                                        <td style="width: 2%; border-left: 1px solid white;">
                                           &nbsp; &nbsp; &nbsp; &nbsp;
                                        </td>
                            </tr>
                            </table>

                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 80%; text-align: left">
                <div id="dvRecordInformation" style="float: left; width: 100%">
                </div>
            </div>
        </div>
    </center>
</asp:Content>
