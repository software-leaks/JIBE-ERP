<%@ Page Title=" JiBE:Yearly Inspection Summary" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="InspectionYearSummary.aspx.cs" Inherits="Technical_Reports_InspectionYearSummary"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min1.8.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function hidedivision() {

            document.getElementById('DivMainContent').style.overflow = 'hidden';
            document.getElementById('DivFooterRow').style.display = 'none';
        }


        function GetDivData() {

            var str = document.getElementById('divMain').innerHTML;
            document.getElementById($('[id$=hdnContent]').attr('id')).value = str;
            //        __dopostback("BtnPrintPDF", "onclick")
            __doPostBack("<%=BtnPrintPDF.UniqueID %>", "onclick");

        }

  
    </script>
    <%--<style type="text/css">
        .page
        {
            width: 1440px;
            background-color: #fff;
            margin: 5px auto 0px auto;
            border: 1px solid #496077;
        }
        .Summary-FooterStyle-css
        {
            /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
            color: #333333;
            background-color: #fff;
            font-weight: bold;
            font-size: 11px;
            border: 1px solid #000;
        }
        .Summary-FooterStyle-css td
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Summary-HeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #333333;
            font-size: 11px;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            background-color: #CCCCCC;
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Summary-HeaderStyle-css th
        {
            border: 1px solid #000;
            border-collapse: collapse;
        }
        .Summary-SubHeaderStyle-css
        {
            /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
            color: #fff;
            font-size: 11px;
            padding: 5px;
            text-align: left;
            vertical-align: middle;
            background-color: #333333;
            border: 1px solid #000; /* border-collapse: collapse;*/
        }
        .Summary-SubHeaderStyle-css th
        {
            border: 1px solid #000; /* border-collapse: collapse;*/
        }
        
        .PrintButton
        {
            background: url(../Images/print.JPG);
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="page-title">
        Yearly Inspection Summary
    </div>
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 98%;" align="right">
                    <asp:HiddenField ID="hdnContent" runat="server" ClientIDMode="Static" />
                    <asp:Button ID="BtnPrevious" runat="server" Text="<" Width="30px" Height="30px" OnClick="BtnPrevious_Click"
                        Font-Bold="True" ToolTip="Previous Year" />
                    <asp:Button ID="BtnNext" runat="server" Text=">" Width="30px" Height="30px" OnClick="BtnNext_Click"
                        Font-Bold="True" ToolTip="Next Year" />
                    &nbsp;&nbsp;
                    <asp:ImageButton ID="BtnPrint" runat="server" OnClientClick="return PrintDiv('divMain');"
                        ImageUrl="~/Images/Printer.png" Width="20px" Height="20px" ToolTip="Print"></asp:ImageButton>
                </td>
                <td style="width:2%; vertical-align:top; " align="right">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="BtnPrintPDF" runat="server" ImageUrl="~/Images/PDF-icon.png"
                                Width="20px" Height="20px" ToolTip="Export PDF" OnClick="BtnPrintPDF_Click"
                                OnClientClick="GetDivData()"></asp:ImageButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMain" style="width: 100%;">
        <style type="text/css">
            .page
            {
                width: 1440px;
                background-color: #fff;
                margin: 5px auto 0px auto;
                border: 1px solid #496077;
            }
            .Summary-FooterStyle-css
            {
                /* background: url(../../Images/gridheaderbg-silver-image.png) left -0px repeat-x;*/
                color: #333333;
                background-color: #fff;
                font-weight: bold;
                font-size: 12px;
                border: 1px solid #000;
                font-family:Tahoma;
            }
            .Summary-FooterStyle-css td
            {
                border: 1px solid #000;
                border-collapse: collapse;
            }
            .Summary-HeaderStyle-css
            {
                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                color: #333333;
                font-size: 12px;
                padding: 5px;
                text-align: left;
                vertical-align: middle;
                background-color: #CCCCCC;
                border: 1px solid #000;
                border-collapse: collapse;
                font-family:Tahoma;
            }
            .Summary-HeaderStyle-css th
            {
                border: 1px solid #000;
                border-collapse: collapse;
            }
            .Summary-SubHeaderStyle-css
            {
                /* background: url(../Images/gridheaderbg-silver-image.png) left -0px repeat-x; /*background: url(../Images/gradient-blue.png) left -500px repeat-x;*/
                color: #fff;
                font-size: 12px;
                padding: 5px;
                text-align: left;
                vertical-align: middle;
                background-color: #333333;
                border: 1px solid #000; /* border-collapse: collapse;*/
                font-family:Tahoma;
            }
            .Summary-SubHeaderStyle-css th
            {
                border: 1px solid #000; /* border-collapse: collapse;*/
            }
            
            .PrintButton
            {
                background: url(../Images/print.JPG);
            }
        </style>
        <table style="width: 100%;" id="tbl">
            <tr>
                <td style="" align="center">
                    <asp:Image ID="imgLogo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="DivHeaderRow" style="width: 100%;">
                    </div>
                    <div id="DivMainContent" style="width: 100%;">
                        <%--onscroll="OnScrollDiv(this)"--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdSummary" GridLines="Vertical" AutoGenerateColumns="true" CellPadding="8"
                                    runat="server" OnDataBound="grdSummary_DataBound" OnRowCreated="grdSummary_RowCreated"
                                    AllowPaging="False" ShowFooter="true" OnSorted="grdSummary_Sorted" OnSorting="grdSummary_Sorting">
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" Font-Size="12px" Font-Names="Tahoma"
                                        BackColor="White" HorizontalAlign="Center" />
                                    <RowStyle CssClass="RowStyle-css" Font-Size="12px" Font-Names="Tahoma" HorizontalAlign="Center"
                                        BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" Width="100%" />
                                    <EmptyDataTemplate>
                                        No Records Found
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle HorizontalAlign="Center" ForeColor="Red" Font-Size="12px" Font-Names="Tahoma" Font-Bold="true"
                                         BorderStyle="Solid" BorderColor="#000" BorderWidth="1px" />
                                    <HeaderStyle CssClass="Summary-HeaderStyle-css" />
                                    <FooterStyle CssClass="Summary-FooterStyle-css" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="DivFooterRow" style="width: 100%;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" id="tblStat" style="width: 50%; border-width: thin; border-style: solid;
                        border-color: Black;" border="1">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblHead" runat="server" Text="Statistics" Font-Bold="True" Width="100%"
                                    ForeColor="Black" BackColor="#CCCCCC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAvgDaysOnBoard" runat="server" Text="" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblAvgDaysOnBoardVal" runat="server" Text="0%" Font-Names="Tahoma" Font-Size="12px" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAvgDaysOnShore" runat="server" Text="" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblAvgDaysOnShoreVal" runat="server" Text="0%" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOnBoardDA" runat="server" Text="" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblOnBoardDAVal" runat="server" Text="$0" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAshoreDA" runat="server" Text="" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblAshoreDAVal" runat="server" Text="$0" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAvgDA" runat="server" Text="Average Daily allowance based on 35/65 ratio"
                                    ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblAvgDAVal" runat="server" Text="$0" ForeColor="Black" Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
