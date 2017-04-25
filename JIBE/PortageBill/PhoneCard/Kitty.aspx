<%@ Page Title="Phone Card Kitty" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Kitty.aspx.cs" Inherits="PortageBill_PhoneCard_Kitty" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/uc_SupplierList.ascx" TagName="uc_SupplierList" TagPrefix="ucSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link href="../../Styles/PhoneCard.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
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

        function validation() {

            if (document.getElementById("ctl00_MainContent_txtCardNumber").value == "") {
                alert("Please enter card number.");
                document.getElementById("ctl00_MainContent_txtCardNumber").focus();
                return fals;
            }

            if (document.getElementById("ctl00_MainContent_txtUnit").value == "") {
                alert("Please enter card Unit.");
                document.getElementById("ctl00_MainContent_txtUnit").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_txtPin").value == "") {
                alert("Please enter card pin.");
                document.getElementById("ctl00_MainContent_txtPin").focus();
                return false;
            }
            if (document.getElementById("ctl00_MainContent_ddlVesselAdd").value == "0")
             {
                alert("Please select vessel.");
                document.getElementById("ctl00_MainContent_ddlVesselAdd").focus();
                return false;
            }

            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2;">
        <center>        
 <asp:UpdateProgress ID="upUpdateProgress"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 <img src="../../Images/loaderbar.gif" alt="Please Wait"/>
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
            <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
                height: 100%;">
                <div id="page-header" class="page-title">
                    <b>Phone Card Kitty </b>
                </div>
                <div style="height: 650px; color: Black;">
                    <asp:UpdatePanel ID="UpdatePanelport" runat="server">
                      <Triggers>
                    <asp:PostBackTrigger ControlID="ImgExpExcel"/>                   
                    </Triggers>
                        <ContentTemplate>
                            <div style="margin: 1px; padding: 1px; border: 1px solid #cccccc;">
                                <table width="100%" cellpadding="1" cellspacing="0">
                                    <tr>
                                        <td width="100%" valign="top" style="border: 1px solid gray; color: Black">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table width="100%" cellpadding="1" cellspacing="0">
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
                                                                Card Number
                                                            </td>
                                                            <td width="15%" align="left">
                                                                <asp:TextBox ID="txtSatatus" runat="server" Width="120px" CssClass="input"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                 <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Export to Excel" OnClick="ImgExpExcel_Click"
                                                                    Width="16px" ImageUrl="~/Images/Excel-icon.png" />
                                                                                                                         
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnNew" Text="New Card" runat="server" Width="90px" ToolTip="Add New Card"
                                                                    OnClick="btnNew_Click" />
                                                            </td>
                                                            <td>
                                                             <asp:Button ID="btnUpload" Text="Upload Cards" runat="server" Width="90px" ToolTip="Upload Card" />
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
                                                                Title
                                                            </td>
                                                            <td width="15%" align="left">
                                                                <asp:TextBox ID="txtTitle" CssClass="input" runat="server" Width="120px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                  <img src="../../Images/Printer.png" style="cursor: hand;" alt="Print" title="*Print*"   onclick="PrintDiv('divGrid')" />
                                                            </td>
                                                            <td>                                                           
                                                              <asp:Button ID="btnSearch" Text="Search" runat="server" Width="90px" ToolTip="Search"
                                                                    OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" Width="90px" OnClick="btnClearFilter_Click" />
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
                                <asp:GridView ID="gvPhoneCardKitty" runat="server" EmptyDataText="NO RECORDS FOUND!"
                                    AutoGenerateColumns="False" OnRowDataBound="gvPhoneCardKitty_RowDataBound" GridLines="None"
                                    DataKeyNames="id" CellPadding="3" CellSpacing="0" Width="100%" OnSorting="gvPhoneCardKitty_Sorting"
                                    AllowSorting="true" Font-Size="11px" CssClass="GridView-css">
                                    <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                     <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px"/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Port">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblVesselNameHeader" runat="server" CommandName="Sort" CommandArgument="VESSEL_ID"
                                                    ForeColor="Black" CssClass="noline">Vessel</asp:LinkButton>
                                                <img id="VESSEL_ID" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.VESSEL_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="150px"></ItemStyle>
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
                                                <asp:LinkButton ID="lblCardNumberHeader" runat="server" CommandName="Sort" CommandArgument="CARD_NUM"
                                                    ForeColor="Black" CssClass="noline">Card Number</asp:LinkButton>
                                                <img id="REQUEST_NUMBER" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCardNumber" runat="server" Text='<%#Eval("CARD_NUM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtcardPinHeader" runat="server" CommandName="Sort" CommandArgument="CARD_PIN"
                                                    ForeColor="Black" CssClass="noline">Card Pin </asp:LinkButton>
                                                <img id="CARD_PIN" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCardPin" runat="server" Text='<%#Eval("CARD_PIN")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="false" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblCardUnitsHeader" runat="server" CommandName="Sort" CommandArgument="CARD_UNITS"
                                                    ForeColor="Black" CssClass="noline">Unit</asp:LinkButton>
                                                <img id="CARD_UNITS" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCardUnits" runat="server" Text='<%#Eval("CARD_UNITS")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtTitleHeader" runat="server" CommandName="Sort" CommandArgument="TITLE"
                                                    ForeColor="Black" CssClass="noline">Title</asp:LinkButton>
                                                <img id="TITLE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TITLE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSubTitleHeader" runat="server" CommandName="Sort" CommandArgument="SUBTITLE"
                                                    ForeColor="Black" CssClass="noline">Sub Title</asp:LinkButton>
                                                <img id="SUBTITLE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubtitle" runat="server" Text='<%#Eval("SUBTITLE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblSupplierHeader" runat="server" CommandName="Sort" CommandArgument="Full_NAME"
                                                    ForeColor="Black" CssClass="noline">Supplier</asp:LinkButton>
                                                <img id="Full_NAME" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplier" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Full_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblStaffNameHeader" runat="server" CommandName="Sort" CommandArgument="VOYAGEID"
                                                    ForeColor="Black" CssClass="noline">Voyage</asp:LinkButton>
                                                <img id="VOYAGEID" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStaffName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.STAFF_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px"></ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindKitty" />
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
    <div id="divUploadCard" title="Assign Card" style="display: none; border: 1px solid Gray;
        font-family: Tahoma; text-align: left; font-size: 11px; background-color: Gray;
        color: Black; width: 500px;">
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <table width="100%" cellpadding="5" style="border: 1px solid Gray" cellspacing="0">
                            <tr>
                                <td colspan="2" style="text-align: center; color: Black; font-weight: bold;">
                                    Add Card
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Card Number :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardNumber" runat="server" Width="150px" MaxLength="10" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Card Unit :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUnit" runat="server" Width="60px" CssClass="input" MaxLength="6"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pin Code :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPin" runat="server" Width="60px" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Title :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetail" runat="server" Width="320px" CssClass="input" TextMode="MultiLine"
                                        Height="40px" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sub Title :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubDetail" runat="server" Width="320px" CssClass="input" Height="40px"
                                        TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vessel:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVesselAdd" runat="server" Width="135px" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Supplier:
                                </td>
                                <td>
                                    <ucSupplier:uc_SupplierList ID="SupplierList" Width="150px" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Button ID="btnSaveEventEdit" runat="server" Text="Save" OnClientClick="return validation();" OnClick="btnSaveEventEdit_Click" />
                                    <asp:Button ID="btnCloseEventEdit" runat="server" Text="Close" OnClick="btnCloseEventEdit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
