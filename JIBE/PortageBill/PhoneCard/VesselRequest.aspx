<%@ Page Title="Phone Card Request List  " Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="VesselRequest.aspx.cs" Inherits="PortageBill_PhoneCard_VesselRequest" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link href="../../Styles/PhoneCard.css" rel="stylesheet" type="text/css" />--%>
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
    <script type="text/javascript">
        function PrintDiv(dvID) {

            var a = window.open('', '', 'left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
            a.document.write(document.getElementById(dvID).innerHTML);
            a.document.close();
            a.focus();
            a.print();
            a.close();
            return false;
        }
    </script>
    <style type="text/css">
        .ob_iLboICBC li
        {
            float: left;
            width: 150px;
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
        .noline
        {
            color: Lime;
            text-decoration: none;
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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
        <center>
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
                    <b>Phone Card Request List </b>
                </div>
                <div style="height: 650px; color: Black;">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ImgExpExcel" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td width="15%" align="right" valign="top">
                                                                Fleet :
                                                            </td>
                                                            <td width="20%" valign="top" align="left">
                                                                <uc1:ucCustomDropDownList ID="DDLFleet" runat="server" UseInHeader="false" OnApplySearch="DDLFleet_SelectedIndexChanged"
                                                                    Height="150" Width="160" />
                                                            </td>
                                                            <td width="10%" align="right" valign="top">
                                                                From Date :
                                                            </td>
                                                            <td width="15%" valign="top" align="left">
                                                                <asp:TextBox ID="txtFromDate" CssClass="input" runat="server" Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calFromDate" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td width="10%" align="right">
                                                                Request Number:
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <asp:TextBox ID="txtCardNumber" runat="server" Width="120px" CssClass="input"></asp:TextBox>
                                                            </td>
                                                            <td width="10%">
                                                                &nbsp;&nbsp;
                                                                <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                                                &nbsp;&nbsp;
                                                                <img src="../../Images/Printer.png" style="cursor: hand;" title="*Print*" alt="Print"
                                                                    onclick="PrintDiv('divGrid')" />
                                                                &nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="15%" align="right" valign="top">
                                                                Vessel :
                                                            </td>
                                                            <td width="20%" valign="top" align="left">
                                                                <uc1:ucCustomDropDownList ID="DDLVessel" runat="server" UseInHeader="false" OnApplySearch="DDLVessel_SelectedIndexChanged"
                                                                    Height="200" Width="160" />
                                                            </td>
                                                            <td width="10%" align="right" valign="top">
                                                                To Date :
                                                            </td>
                                                            <td width="15%" valign="top" align="left">
                                                                <asp:TextBox ID="txtToDate" CssClass="input" runat="server" Width="80px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="calToDate" runat="server" Enabled="True" TargetControlID="txtToDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                            <td align="right">
                                                                Status:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" Width="120px" CssClass="input">
                                                                    <asp:ListItem Text="--Select All--" Value="-1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="OPEN" Value="OPEN"></asp:ListItem>
                                                                    <asp:ListItem Text="SENTTOOFFICE" Value="SENTTOOFFICE"></asp:ListItem>
                                                                    <asp:ListItem Text="APPROVED" Value="APPROVED"></asp:ListItem>
                                                                    <asp:ListItem Text="CLOSED" Value="CLOSED"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnSearch" Text="Search" runat="server" Width="90px" ToolTip="Search"
                                                                    OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnClearFilter" ToolTip="Clear Filter" runat="server" Text="Clear Filter"
                                                                    Width="90px" OnClick="btnClearFilter_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divGrid" style="position: relative; z-index: 0; width: 100%;">
                                <asp:GridView ID="gvPhoneCardRequest" runat="server" EmptyDataText="NO RECORDS FOUND!"
                                    AutoGenerateColumns="False" OnRowDataBound="gvPhoneCardRequest_RowDataBound"
                                    DataKeyNames="id" CellPadding="3" CellSpacing="0" Width="100%" OnSorting="gvPhoneCardRequest_Sorting"
                                    AllowSorting="true" Font-Size="11px" GridLines="None" CssClass="GridView-css">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReqNumberrHeader" runat="server" CommandArgument="REQUEST_NUMBER"
                                                    ForeColor="Black" CssClass="noline">Request Number</asp:LinkButton>
                                                <img id="REQUEST_NUMBER" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnRequestNumber" runat="server" CommandArgument='<%#Eval("ID") + ","+Eval("VESSEL_ID") %>'
                                                    CommandName="ViewRequest" Text='<%#Eval("REQUEST_NUMBER") %>' Style="color: Black"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtVesslNameHeader" runat="server" CommandName="Sort" CommandArgument="VESSEL_ID"
                                                    ForeColor="Black" CssClass="noline">Vessel Name </asp:LinkButton>
                                                <img id="VESSEL_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%#Eval("VESSEL_NAME")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblReqStatusHeader" runat="server" ForeColor="Black" CssClass="noline">Status</asp:LinkButton>
                                                <img id="REQUEST_STATUS" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblReqStatus" runat="server" Text='<%#Eval("REQUEST_STATUS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtRequestDateHeader" runat="server" CommandName="Sort" CommandArgument="DATE_OF_CREATION"
                                                    ForeColor="Black" CssClass="noline">Request Date</asp:LinkButton>
                                                <img id="DATE_OF_CREATION" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("DATE_OF_CREATION"))) %>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblTotalCardRequestHeader" runat="server" CommandName="Sort"
                                                    CommandArgument="TOTAL_REQUEST" ForeColor="Black" CssClass="noline">Total Card Request</asp:LinkButton>
                                                <img id="TOTAL_REQUEST" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalcardRequest" runat="server" Text='<%#Eval("TOTAL_REQUEST")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblPortageBillDateHeader" runat="server" CommandName="Sort" CommandArgument="PBILL_DATE"
                                                    ForeColor="Black" CssClass="noline">Portage Bill Date</asp:LinkButton>
                                                <img id="PBILL_DATE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# UDFLib.ConvertUserDateFormat(Convert.ToString(Eval("PBILL_DATE")))%>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindIndex" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </center>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").on("click", "#<%=btnSearch.ClientID %>", function () {
                var Msg = "";
                if ($.trim($("#<%=txtFromDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtFromDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>')) {
                        Msg = "Enter valid From Date<%=UDFLib.DateFormatMessage() %>\n";
                    }
                }
                if ($.trim($("#<%=txtToDate.ClientID %>").val()) != "") {
                    if (IsInvalidDate($.trim($("#<%=txtToDate.ClientID %>").val()), '<%= UDFLib.GetDateFormat() %>')) {
                        Msg += "Enter valid To Date<%=UDFLib.DateFormatMessage() %>";
                    }
                }
                if (Msg != "") {
                    alert(Msg);
                    return false;
                }
            });
        });
    </script>
</asp:Content>
